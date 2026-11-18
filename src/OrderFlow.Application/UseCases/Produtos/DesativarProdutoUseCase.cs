using OrderFlow.Application.Interfaces;

namespace OrderFlow.Application.UseCases.Produtos;

public class DesativarProdutoUseCase
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;

    public DesativarProdutoUseCase(
        IProdutoRepository produtoRepository,
        IUnidadeDeTrabalho unidadeDeTrabalho)
    {
        _produtoRepository = produtoRepository;
        _unidadeDeTrabalho = unidadeDeTrabalho;
    }

    public async Task<bool> ExecutarAsync(long id)
    {
        if (id <= 0)
            throw new ArgumentException("O ID do produto é inválido.");

        var produto = await _produtoRepository.ObterPorIdAsync(id);

        if (produto is null)
            return false;

        produto.Desativar();

        await _unidadeDeTrabalho.SalvarAlteracoesAsync();

        return true;
    }
}