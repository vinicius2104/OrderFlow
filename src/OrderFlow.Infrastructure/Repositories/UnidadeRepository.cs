using Microsoft.EntityFrameworkCore;
using OrderFlow.Application.Interfaces;
using OrderFlow.Domain.Entities;
using OrderFlow.Infrastructure.Data;

namespace OrderFlow.Infrastructure.Repositories;

public class UnidadeRepository : IUnidadeRepository
{
    private readonly OrderFlowDbContext _context;

    public UnidadeRepository(OrderFlowDbContext context)
    {
        _context = context;
    }

    public async Task<Unidade?> ObterPorIdAsync(long id)
    {
        return await _context.Unidades
            .FirstOrDefaultAsync(u => u.Id == id);
    }
    public async Task<List<Unidade>> ListarTodasAsync()
    {
        return await _context.Unidades
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task AdicionarAsync(Unidade unidade)
    {
        await _context.Unidades.AddAsync(unidade);
    }
}