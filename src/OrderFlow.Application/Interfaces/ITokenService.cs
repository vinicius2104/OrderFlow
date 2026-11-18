namespace OrderFlow.Application.Interfaces;

public interface ITokenService
{
    string GerarToken(long usuarioId, string nome, string perfil);
}