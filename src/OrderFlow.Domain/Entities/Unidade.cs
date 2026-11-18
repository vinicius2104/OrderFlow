namespace OrderFlow.Domain.Entities;

public class Unidade
{
    public long Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public string Cnpj { get; private set; } = string.Empty;
    public string Endereco { get; private set; } = string.Empty;
    public bool Ativa { get; private set; } = true;
    public DateTime CriadoEm { get; private set; } = DateTime.UtcNow;

    private Unidade() { } //entity framework

    public Unidade(string nome, string cnpj, string endereco)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome da unidade é obrigatório.");

        if (string.IsNullOrWhiteSpace(cnpj))
            throw new ArgumentException("O CNPJ é obrigatório.");

        if (string.IsNullOrWhiteSpace(endereco))
            throw new ArgumentException("O endereço é obrigatório.");

        Nome = nome;
        Cnpj = cnpj;
        Endereco = endereco;
    }

    public void AtualizarDados(string nome, string endereco)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome da unidade é obrigatório.");

        if (string.IsNullOrWhiteSpace(endereco))
            throw new ArgumentException("O endereço é obrigatório.");

        Nome = nome;
        Endereco = endereco;
    }

    public void Ativar()
    {
        Ativa = true;
    }

    public void Desativar()
    {
        Ativa = false;
    }
}