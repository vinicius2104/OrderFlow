using OrderFlow.Application.Interfaces;
using OrderFlow.Domain.Entities;

namespace OrderFlow.Application.UseCases.Produtos;

public class ListarProdutosUseCase
{
    private readonly IProdutoRepository _produtoRepository;

    public ListarProdutosUseCase(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task<List<Produto>> ExecutarAsync()
    {
        return await _produtoRepository.ListarTodosAsync();
    }
}