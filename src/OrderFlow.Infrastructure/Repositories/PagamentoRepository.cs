using Microsoft.EntityFrameworkCore;
using OrderFlow.Application.Interfaces;
using OrderFlow.Domain.Entities;
using OrderFlow.Infrastructure.Data;

namespace OrderFlow.Infrastructure.Repositories;

public class PagamentoRepository : IPagamentoRepository
{
    private readonly OrderFlowDbContext _context;

    public PagamentoRepository(OrderFlowDbContext context)
    {
        _context = context;
    }

    public async Task<Pagamento?> ObterPorIdAsync(long id)
    {
        return await _context.Pagamentos
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Pagamento?> ObterPorPedidoIdAsync(long pedidoId)
    {
        return await _context.Pagamentos
            .FirstOrDefaultAsync(p => p.PedidoId == pedidoId);
    }

    public async Task AdicionarAsync(Pagamento pagamento)
    {
        await _context.Pagamentos.AddAsync(pagamento);
    }
}