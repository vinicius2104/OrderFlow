using OrderFlow.Application.DTOs;
using OrderFlow.Application.Interfaces;
using OrderFlow.Domain.Entities;

namespace OrderFlow.Application.UseCases.Produtos;

public class CriarProdutoUseCase
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;

    public CriarProdutoUseCase(
        IProdutoRepository produtoRepository,
        IUnidadeDeTrabalho unidadeDeTrabalho)
    {
        _produtoRepository = produtoRepository;
        _unidadeDeTrabalho = unidadeDeTrabalho;
    }

    public async Task<Produto> ExecutarAsync(CriarProdutoDto dto)
    {
        if (dto is null)
            throw new ArgumentNullException(nameof(dto));

        var produto = new Produto(
            dto.Nome,
            
            dto.Preco,
            dto.Descricao
        );

        await _produtoRepository.AdicionarAsync(produto);
        await _unidadeDeTrabalho.SalvarAlteracoesAsync();

        return produto;
    }
}