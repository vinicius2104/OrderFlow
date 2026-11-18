using Microsoft.AspNetCore.Identity;
using OrderFlow.Application.Interfaces;

namespace OrderFlow.Infrastructure.Servicos;

public class ServicoDeSenha : IServicoDeSenha
{
    private readonly PasswordHasher<object> _hasher = new();

    public string GerarHash(string senha)
    {
        if (string.IsNullOrWhiteSpace(senha))
            throw new ArgumentException("A senha é obrigatória.");

        return _hasher.HashPassword(null!, senha);
    }

    public bool VerificarSenha(string senha, string hash)
    {
        if (string.IsNullOrWhiteSpace(senha) ||
            string.IsNullOrWhiteSpace(hash))
            return false;

        var resultado = _hasher.VerifyHashedPassword(
            null!,
            hash,
            senha
        );

        return resultado != PasswordVerificationResult.Failed;
    }
}