using OrderFlow.Domain.Entities;

namespace OrderFlow.Application.Interfaces;

public interface IProcessadorPagamento
{
    Task<bool> ProcessarAsync(Pagamento pagamento);
}