using OrderFlow.Domain.Entities;

namespace OrderFlow.Application.Interfaces;

public interface IMovimentacaoEstoqueRepository
{
    Task AdicionarAsync(MovimentacaoEstoque movimentacao);
}