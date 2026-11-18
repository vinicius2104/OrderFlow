using Microsoft.EntityFrameworkCore;
using OrderFlow.Application.Interfaces;
using OrderFlow.Domain.Entities;
using OrderFlow.Infrastructure.Data;

namespace OrderFlow.Infrastructure.Repositories;

public class PedidoRepository : IPedidoRepository
{
    private readonly OrderFlowDbContext _context;

    public PedidoRepository(OrderFlowDbContext context)
    {
        _context = context;
    }

    public async Task<Pedido?> ObterPorChaveIdempotenciaAsync(string chave)
    {
        return await _context.Pedidos
            .FirstOrDefaultAsync(p => p.ChaveIdempotencia == chave);
    }

    public async Task<Pedido?> ObterPorIdAsync(long id)
    {
        return await _context.Pedidos
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AdicionarAsync(Pedido pedido)
    {
        await _context.Pedidos.AddAsync(pedido);
    }
}