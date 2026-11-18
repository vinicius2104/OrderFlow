using OrderFlow.Application.DTOs;
using OrderFlow.Application.Interfaces;
using OrderFlow.Domain.Entities;

namespace OrderFlow.Application.UseCases.Produtos;

public class AtualizarProdutoUseCase
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;

    public AtualizarProdutoUseCase(
        IProdutoRepository produtoRepository,
        IUnidadeDeTrabalho unidadeDeTrabalho)
    {
        _produtoRepository = produtoRepository;
        _unidadeDeTrabalho = unidadeDeTrabalho;
    }

    public async Task<Produto?> ExecutarAsync(long id, AtualizarProdutoDto dto)
    {
        if (id <= 0)
            throw new ArgumentException("O ID do produto é inválido.");

        if (dto is null)
            throw new ArgumentNullException(nameof(dto));

        var produto = await _produtoRepository.ObterPorIdAsync(id);

        if (produto is null)
            return null;

        produto.AtualizarDados(dto.Nome, dto.Descricao, dto.Preco);

        await _unidadeDeTrabalho.SalvarAlteracoesAsync();

        return produto;
    }
}