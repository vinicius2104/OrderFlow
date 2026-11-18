using OrderFlow.Domain.Entities;

namespace OrderFlow.Application.Interfaces;

public interface IProdutoRepository
{
    Task<Produto?> ObterPorIdAsync(long id);
    Task<List<Produto>> ListarTodosAsync();
    Task AdicionarAsync(Produto produto);
  
}