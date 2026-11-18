using OrderFlow.Domain.Entities;

namespace OrderFlow.Application.Interfaces;

public interface IUnidadeRepository
{
    Task<Unidade?> ObterPorIdAsync(long id);
    Task<List<Unidade>> ListarTodasAsync();
    Task AdicionarAsync(Unidade unidade);
}