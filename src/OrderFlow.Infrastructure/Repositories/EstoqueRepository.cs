using Microsoft.EntityFrameworkCore;
using OrderFlow.Application.Interfaces;
using OrderFlow.Domain.Entities;
using OrderFlow.Infrastructure.Data;

namespace OrderFlow.Infrastructure.Repositories;

public class EstoqueRepository : IEstoqueRepository
{
    private readonly OrderFlowDbContext _context;

    public EstoqueRepository(OrderFlowDbContext context)
    {
        _context = context;
    }

    public async Task<Estoque?> ObterPorProdutoEUnidadeAsync(
        long unidadeId,
        long produtoId)
    {
        return await _context.Estoques
            .FirstOrDefaultAsync(e =>
                e.UnidadeId == unidadeId &&
                e.ProdutoId == produtoId);
    }

    public async Task<List<Estoque>> ListarTodosAsync()
    {
        return await _context.Estoques
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AdicionarAsync(Estoque estoque)
    {
        await _context.Estoques.AddAsync(estoque);
    }
}