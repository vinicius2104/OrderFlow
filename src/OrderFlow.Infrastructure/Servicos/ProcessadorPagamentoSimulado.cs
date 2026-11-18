using OrderFlow.Application.Interfaces;
using OrderFlow.Domain.Entities;

namespace OrderFlow.Infrastructure.Servicos;

public class ProcessadorPagamentoSimulado : IProcessadorPagamento
{
    public Task<bool> ProcessarAsync(Pagamento pagamento)
    {
        if (pagamento is null)
            throw new ArgumentNullException(nameof(pagamento));

        // Simulação:
        // Valores acima de R$ 1.000,00 são recusados.
        bool aprovado = pagamento.Valor <= 1000m;

        return Task.FromResult(aprovado);
    }
}