using OrderFlow.Domain.Entities;

namespace OrderFlow.Application.Interfaces;

public interface IEstoqueRepository
{
    Task<Estoque?> ObterPorProdutoEUnidadeAsync(
        long unidadeId,
        long produtoId);
    Task<List<Estoque>> ListarTodosAsync();
    Task AdicionarAsync(Estoque estoque);
}