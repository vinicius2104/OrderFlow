using OrderFlow.Application.Interfaces;

namespace OrderFlow.Application.UseCases.Pedidos;

using OrderFlow.Application.DTOs;
using OrderFlow.Domain.Entities;

public class CriarPedidoUseCase
{

    private readonly IProdutoRepository _produtoRepository;
    private readonly IUnidadeRepository _unidadeRepository;
    private readonly IEstoqueRepository _estoqueRepository;
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IPagamentoRepository _pagamentoRepository;
    private readonly IProcessadorPagamento _processadorPagamento;
    private readonly IMovimentacaoEstoqueRepository _movimentacaoRepository;
    private readonly IAuditoriaRepository _auditoriaRepository;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;

    public CriarPedidoUseCase(
        IProdutoRepository produtoRepository,
        IUnidadeRepository unidadeRepository,
        IEstoqueRepository estoqueRepository,
        IPedidoRepository pedidoRepository,
        IPagamentoRepository pagamentoRepository,
        IProcessadorPagamento processadorPagamento,
        IMovimentacaoEstoqueRepository movimentacaoRepository,
        IAuditoriaRepository auditoriaRepository,
        IUnidadeDeTrabalho unidadeDeTrabalho)
    {
        _produtoRepository = produtoRepository;
        _unidadeRepository = unidadeRepository;
        _estoqueRepository = estoqueRepository;
        _pedidoRepository = pedidoRepository;
        _pagamentoRepository = pagamentoRepository;
        _processadorPagamento = processadorPagamento;
        _movimentacaoRepository = movimentacaoRepository;
        _auditoriaRepository = auditoriaRepository;
        _unidadeDeTrabalho = unidadeDeTrabalho;
    }

    public async Task<PedidoCriadoDto> ExecutarAsync(
    CriarPedidoDto dto,
    long usuarioId)
{
    return await _unidadeDeTrabalho.ExecutarEmTransacaoAsync(
        () => ExecutarDentroDaTransacaoAsync(dto, usuarioId)
    );
}
    public async Task<PedidoCriadoDto> ExecutarDentroDaTransacaoAsync(
    CriarPedidoDto dto,
    long usuarioId)
    {
        if (dto is null)
            throw new ArgumentNullException(nameof(dto));

        if (string.IsNullOrWhiteSpace(dto.ChaveIdempotencia))
            throw new ArgumentException("A chave de idempotência é obrigatória.");

        var pedidoExistente = await _pedidoRepository
            .ObterPorChaveIdempotenciaAsync(dto.ChaveIdempotencia);

        if (pedidoExistente is not null)
            throw new InvalidOperationException(
                "Já existe um pedido com esta chave de idempotência.");

        if (dto.UnidadeId <= 0)
            throw new ArgumentException("A unidade informada é inválida.");

        if (usuarioId <= 0)
            throw new ArgumentException("O usuário responsável é inválido.");

        var unidade = await _unidadeRepository
            .ObterPorIdAsync(dto.UnidadeId);

        if (unidade is null)
            throw new InvalidOperationException("Unidade não encontrada.");

        if (!unidade.Ativa)
            throw new InvalidOperationException("A unidade está inativa.");

        if (dto.Itens is null || dto.Itens.Count == 0)
            throw new ArgumentException("O pedido deve possuir pelo menos um item.");

        var produtosEncontrados = new Dictionary<long, Produto>();

        foreach (var itemDto in dto.Itens)
        {
            if (itemDto.ProdutoId <= 0)
                throw new ArgumentException("O produto informado é inválido.");

            if (itemDto.Quantidade <= 0)
                throw new ArgumentException("A quantidade deve ser maior que zero.");

            var produto = await _produtoRepository
                .ObterPorIdAsync(itemDto.ProdutoId);

            if (produto is null)
                throw new InvalidOperationException(
                    $"Produto {itemDto.ProdutoId} não encontrado.");

            if (!produto.Ativo)
                throw new InvalidOperationException(
                    $"O produto {produto.Nome} está inativo.");

            produtosEncontrados[produto.Id] = produto;


        }
        var reservas = new List<(Estoque Estoque, int Quantidade)>();

        foreach (var grupo in dto.Itens.GroupBy(item => item.ProdutoId))
        {
            var estoque = await _estoqueRepository
                .ObterPorProdutoEUnidadeAsync(dto.UnidadeId, grupo.Key);

            if (estoque is null)
                throw new InvalidOperationException(
                    $"Estoque do produto {grupo.Key} não encontrado nesta unidade.");

            var quantidadeSolicitada = grupo.Sum(item => item.Quantidade);

            if (quantidadeSolicitada > estoque.ConsultarDisponibilidade())
                throw new InvalidOperationException(
                    $"Estoque insuficiente para o produto {grupo.Key}.");

            reservas.Add((estoque, quantidadeSolicitada));
        }

        foreach (var reserva in reservas)
        {
            reserva.Estoque.ReservarQuantidade(reserva.Quantidade);
        }
        var pedido = new Pedido(
            dto.UnidadeId,
            usuarioId,
            dto.CanalPedido,
            dto.ChaveIdempotencia,
            dto.ClienteId
        );

        foreach (var itemDto in dto.Itens)
        {
            var produto = produtosEncontrados[itemDto.ProdutoId];

            var itemPedido = new ItemPedido(
                produto.Id,
                itemDto.Quantidade,
                produto.Preco
            );

            pedido.AdicionarItem(itemPedido);
        }
        await _pedidoRepository.AdicionarAsync(pedido);

        await _unidadeDeTrabalho.SalvarAlteracoesAsync();
        foreach (var reserva in reservas)
        {
            await _movimentacaoRepository.AdicionarAsync(
                new MovimentacaoEstoque(
                    reserva.Estoque.Id,
                    "RESERVA",
                    reserva.Quantidade,
                    pedido.Id,
                    usuarioId,
                    "Estoque reservado para o pedido."
                )
            );
        }

        var pagamento = new Pagamento(
            pedido.Id,
            pedido.ValorTotal,
            "CARTAO"
);

        await _pagamentoRepository.AdicionarAsync(pagamento);

        var aprovado = await _processadorPagamento.ProcessarAsync(pagamento);

        if (aprovado)
        {
            pagamento.Aprovar($"SIMULADO-{Guid.NewGuid()}");
            pedido.AtualizarStatus("CONFIRMADO");

            foreach (var reserva in reservas)
            {
                reserva.Estoque.ConfirmarBaixa(reserva.Quantidade);

                await _movimentacaoRepository.AdicionarAsync(
                    new MovimentacaoEstoque(
                        reserva.Estoque.Id,
                        "BAIXA",
                        reserva.Quantidade,
                        pedido.Id,
                        usuarioId,
                        "Baixa de estoque após aprovação do pagamento."
                    )
                );
            }
        }
        else
        {
            pagamento.Recusar($"SIMULADO-{Guid.NewGuid()}");
            pedido.Cancelar();

            foreach (var reserva in reservas)
            {
                reserva.Estoque.LiberarReserva(reserva.Quantidade);

                await _movimentacaoRepository.AdicionarAsync(
                    new MovimentacaoEstoque(
                        reserva.Estoque.Id,
                        "LIBERACAO",
                        reserva.Quantidade,
                        pedido.Id,
                        usuarioId,
                        "Liberação de estoque após recusa do pagamento."
                    )
                );
            }
        }

        await _auditoriaRepository.AdicionarAsync(
    new Auditoria(
        "Pedido",
        pedido.Id,
        "CRIACAO",
        usuarioId,
        $"Pedido criado com status {pedido.Status}."
    )
);

        await _unidadeDeTrabalho.SalvarAlteracoesAsync();

        return new PedidoCriadoDto
        {
            PedidoId = pedido.Id,
            Status = pedido.Status,
            ValorTotal = pedido.ValorTotal,
            StatusPagamento = pagamento.Status,
            CriadoEm = pedido.CriadoEm
        };


    }
}