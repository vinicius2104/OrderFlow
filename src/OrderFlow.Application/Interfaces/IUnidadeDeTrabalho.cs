namespace OrderFlow.Application.Interfaces;

public interface IUnidadeDeTrabalho
{
    Task<int> SalvarAlteracoesAsync();

    Task<T> ExecutarEmTransacaoAsync<T>(
        Func<Task<T>> operacao);
}