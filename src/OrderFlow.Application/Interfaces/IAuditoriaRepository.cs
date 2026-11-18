using OrderFlow.Domain.Entities;

namespace OrderFlow.Application.Interfaces;

public interface IAuditoriaRepository
{
    Task AdicionarAsync(Auditoria auditoria);
}