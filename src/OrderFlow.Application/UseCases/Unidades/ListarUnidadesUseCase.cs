using OrderFlow.Application.Interfaces;
using OrderFlow.Domain.Entities;

namespace OrderFlow.Application.UseCases.Unidades;

public class ListarUnidadesUseCase
{
    private readonly IUnidadeRepository _unidadeRepository;

    public ListarUnidadesUseCase(IUnidadeRepository unidadeRepository)
    {
        _unidadeRepository = unidadeRepository;
    }

    public async Task<List<Unidade>> ExecutarAsync()
    {
        return await _unidadeRepository.ListarTodasAsync();
    }
}