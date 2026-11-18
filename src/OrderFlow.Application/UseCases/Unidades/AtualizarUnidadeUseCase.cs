using OrderFlow.Application.DTOs;
using OrderFlow.Application.Interfaces;
using OrderFlow.Domain.Entities;

namespace OrderFlow.Application.UseCases.Unidades;

public class AtualizarUnidadeUseCase
{
    private readonly IUnidadeRepository _unidadeRepository;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;

    public AtualizarUnidadeUseCase(
        IUnidadeRepository unidadeRepository,
        IUnidadeDeTrabalho unidadeDeTrabalho)
    {
        _unidadeRepository = unidadeRepository;
        _unidadeDeTrabalho = unidadeDeTrabalho;
    }

    public async Task<Unidade?> ExecutarAsync(long id, AtualizarUnidadeDto dto)
    {
        if (id <= 0)
            throw new ArgumentException("O ID da unidade é inválido.");

        if (dto is null)
            throw new ArgumentNullException(nameof(dto));

        var unidade = await _unidadeRepository.ObterPorIdAsync(id);

        if (unidade is null)
            return null;

        unidade.AtualizarDados(dto.Nome, dto.Endereco);

        await _unidadeDeTrabalho.SalvarAlteracoesAsync();

        return unidade;
    }
}