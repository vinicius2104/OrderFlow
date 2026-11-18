using OrderFlow.Domain.Entities;

namespace OrderFlow.Application.Interfaces;

public interface IPagamentoRepository
{
    Task<Pagamento?> ObterPorIdAsync(long id);

    Task<Pagamento?> ObterPorPedidoIdAsync(long pedidoId);

    Task AdicionarAsync(Pagamento pagamento);
}