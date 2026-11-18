using OrderFlow.Application.DTOs;
using OrderFlow.Application.Interfaces;
using OrderFlow.Domain.Entities;

namespace OrderFlow.Application.UseCases.Estoques;

public class CriarEstoqueUseCase
{
    private readonly IEstoqueRepository _estoqueRepository;
    private readonly IUnidadeRepository _unidadeRepository;
    private readonly IProdutoRepository _produtoRepository;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;

    public CriarEstoqueUseCase(
        IEstoqueRepository estoqueRepository,
        IUnidadeRepository unidadeRepository,
        IProdutoRepository produtoRepository,
        IUnidadeDeTrabalho unidadeDeTrabalho)
    {
        _estoqueRepository = estoqueRepository;
        _unidadeRepository = unidadeRepository;
        _produtoRepository = produtoRepository;
        _unidadeDeTrabalho = unidadeDeTrabalho;
    }

    public async Task<Estoque> ExecutarAsync(CriarEstoqueDto dto)
    {
        if (dto is null)
            throw new ArgumentNullException(nameof(dto));

        var unidade = await _unidadeRepository.ObterPorIdAsync(dto.UnidadeId);

        if (unidade is null || !unidade.Ativa)
            throw new InvalidOperationException("Unidade inexistente ou inativa.");

        var produto = await _produtoRepository.ObterPorIdAsync(dto.ProdutoId);

        if (produto is null || !produto.Ativo)
            throw new InvalidOperationException("Produto inexistente ou inativo.");

        var estoqueExistente =
            await _estoqueRepository.ObterPorProdutoEUnidadeAsync(
                dto.UnidadeId,
                dto.ProdutoId);

        if (estoqueExistente is not null)
            throw new InvalidOperationException("Já existe estoque para este produto nesta unidade.");

        var estoque = new Estoque(
            dto.UnidadeId,
            dto.ProdutoId,
            dto.QuantidadeTotal);

        await _estoqueRepository.AdicionarAsync(estoque);
        await _unidadeDeTrabalho.SalvarAlteracoesAsync();

        return estoque;
    }
}