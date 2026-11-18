using OrderFlow.Application.DTOs;
using OrderFlow.Application.Interfaces;

namespace OrderFlow.Application.UseCases.Estoques;

public class ListarEstoquesUseCase
{
    private readonly IEstoqueRepository _estoqueRepository;

    public ListarEstoquesUseCase(IEstoqueRepository estoqueRepository)
    {
        _estoqueRepository = estoqueRepository;
    }

    public async Task<List<EstoqueDto>> ExecutarAsync()
    {
        var estoques = await _estoqueRepository.ListarTodosAsync();

        return estoques.Select(e => new EstoqueDto
        {
            Id = e.Id,
            UnidadeId = e.UnidadeId,
            ProdutoId = e.ProdutoId,
            QuantidadeTotal = e.QuantidadeTotal,
            QuantidadeReservada = e.QuantidadeReservada,
            QuantidadeDisponivel = e.ConsultarDisponibilidade()
        }).ToList();
    }
}