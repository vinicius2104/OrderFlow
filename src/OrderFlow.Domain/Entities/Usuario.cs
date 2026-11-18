namespace OrderFlow.Domain.Entities;

public class Usuario
{
    public long Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string SenhaHash { get; private set; } = string.Empty;
    public string Perfil { get; private set; } = string.Empty;
    public bool Ativo { get; private set; } = true;
    public DateTime CriadoEm { get; private set; } = DateTime.UtcNow;

    private Usuario() { } //entity framework

    public Usuario(string nome, string email, string senhaHash, string perfil)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome é obrigatório.");

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("O e-mail é obrigatório.");

        if (string.IsNullOrWhiteSpace(senhaHash))
            throw new ArgumentException("O hash da senha é obrigatório.");

        if (string.IsNullOrWhiteSpace(perfil))
            throw new ArgumentException("O perfil é obrigatório.");

        Nome = nome;
        Email = email;
        SenhaHash = senhaHash;
        Perfil = perfil;
    }

    public void AlterarPerfil(string novoPerfil)
    {
        if (string.IsNullOrWhiteSpace(novoPerfil))
            throw new ArgumentException("O perfil é obrigatório.");

        Perfil = novoPerfil;
    }

    public void Ativar()
    {
        Ativo = true;
    }

    public void Desativar()
    {
        Ativo = false;
    }
}