using Microsoft.EntityFrameworkCore;
using OrderFlow.Application.Interfaces;
using OrderFlow.Domain.Entities;
using OrderFlow.Infrastructure.Data;

namespace OrderFlow.Infrastructure.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly OrderFlowDbContext _context;

    public ProdutoRepository(OrderFlowDbContext context)
    {
        _context = context;
    }

    public async Task<Produto?> ObterPorIdAsync(long id)
    {
        return await _context.Produtos
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Produto>> ListarTodosAsync()
    {
        return await _context.Produtos
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task AdicionarAsync(Produto produto)
{
    await _context.Produtos.AddAsync(produto);
}
}