using Microsoft.EntityFrameworkCore;
using OrderFlow.Application.Interfaces;

namespace OrderFlow.Infrastructure.Data;

public class UnidadeDeTrabalho : IUnidadeDeTrabalho
{
    private readonly OrderFlowDbContext _context;

    public UnidadeDeTrabalho(OrderFlowDbContext context)
    {
        _context = context;
    }

    public async Task<int> SalvarAlteracoesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task<T> ExecutarEmTransacaoAsync<T>(
        Func<Task<T>> operacao)
    {
        await using var transacao =
            await _context.Database.BeginTransactionAsync();

        try
        {
            var resultado = await operacao();

            await transacao.CommitAsync();

            return resultado;
        }
        catch
        {
            await transacao.RollbackAsync();
            throw;
        }
    }
}