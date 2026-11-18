using OrderFlow.Domain.Entities;

namespace OrderFlow.Application.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario?> ObterPorIdAsync(long id);

    Task<Usuario?> ObterPorEmailAsync(string email);

    Task AdicionarAsync(Usuario usuario);
}