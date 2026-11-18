using OrderFlow.Application.DTOs;
using OrderFlow.Application.Interfaces;
using OrderFlow.Domain.Entities;

namespace OrderFlow.Application.UseCases.Unidades;

public class CriarUnidadeUseCase
{
    private readonly IUnidadeRepository _unidadeRepository;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;

    public CriarUnidadeUseCase(
        IUnidadeRepository unidadeRepository,
        IUnidadeDeTrabalho unidadeDeTrabalho)
    {
        _unidadeRepository = unidadeRepository;
        _unidadeDeTrabalho = unidadeDeTrabalho;
    }

    public async Task<Unidade> ExecutarAsync(CriarUnidadeDto dto)
    {
        if (dto is null)
            throw new ArgumentNullException(nameof(dto));

        var unidade = new Unidade(
            dto.Nome,
            dto.Cnpj,
            dto.Endereco
        );

        await _unidadeRepository.AdicionarAsync(unidade);

        await _unidadeDeTrabalho.SalvarAlteracoesAsync();

        return unidade;
    }
}