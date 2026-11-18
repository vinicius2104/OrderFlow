using OrderFlow.Application.Interfaces;

namespace OrderFlow.Application.UseCases.Unidades;

public class DesativarUnidadeUseCase
{
    private readonly IUnidadeRepository _unidadeRepository;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;

    public DesativarUnidadeUseCase(
        IUnidadeRepository unidadeRepository,
        IUnidadeDeTrabalho unidadeDeTrabalho)
    {
        _unidadeRepository = unidadeRepository;
        _unidadeDeTrabalho = unidadeDeTrabalho;
    }

    public async Task<bool> ExecutarAsync(long id)
    {
        if (id <= 0)
            throw new ArgumentException("O ID da unidade é inválido.");

        var unidade = await _unidadeRepository.ObterPorIdAsync(id);

        if (unidade is null)
            return false;

        unidade.Desativar();

        await _unidadeDeTrabalho.SalvarAlteracoesAsync();

        return true;
    }
}