namespace OrderFlow.Application.Interfaces;

public interface IServicoDeSenha
{
    string GerarHash(string senha);

    bool VerificarSenha(string senha, string hash);
}