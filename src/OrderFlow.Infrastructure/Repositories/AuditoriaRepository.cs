using OrderFlow.Application.Interfaces;
using OrderFlow.Domain.Entities;
using OrderFlow.Infrastructure.Data;

namespace OrderFlow.Infrastructure.Repositories;

public class AuditoriaRepository : IAuditoriaRepository
{
    private readonly OrderFlowDbContext _context;

    public AuditoriaRepository(OrderFlowDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(Auditoria auditoria)
    {
        await _context.Auditorias.AddAsync(auditoria);
    }
}