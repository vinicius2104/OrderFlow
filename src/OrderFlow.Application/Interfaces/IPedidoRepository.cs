using OrderFlow.Domain.Entities;

namespace OrderFlow.Application.Interfaces;

public interface IPedidoRepository
{
    Task<Pedido?> ObterPorChaveIdempotenciaAsync(string chave);

    Task<Pedido?> ObterPorIdAsync(long id);

    Task AdicionarAsync(Pedido pedido);
}