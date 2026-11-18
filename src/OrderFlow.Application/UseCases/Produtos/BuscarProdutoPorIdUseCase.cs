using OrderFlow.Application.Interfaces;
using OrderFlow.Domain.Entities;

namespace OrderFlow.Application.UseCases.Produtos;

public class BuscarProdutoPorIdUseCase
{
    private readonly IProdutoRepository _produtoRepository;

    public BuscarProdutoPorIdUseCase(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task<Produto?> ExecutarAsync(long id)
    {
        if (id <= 0)
            throw new ArgumentException("O ID do produto é inválido.");

        return await _produtoRepository.ObterPorIdAsync(id);
    }
}