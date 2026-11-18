
namespace OrderFlow.Domain.Entities;

public class Produto
{
    public long Id { get; private set; }

    public string Nome { get; private set; } = string.Empty;

    public string? Descricao { get; private set; }

    public decimal Preco { get; private set; }

    public bool Ativo { get; private set; } = true;

    public DateTime CriadoEm { get; private set; } = DateTime.UtcNow;

    private Produto() { }

    public Produto(string nome, decimal preco, string? descricao = null)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome do produto é obrigatório.");

        if (preco <= 0)
            throw new ArgumentException("O preço deve ser maior que zero.");

        Nome = nome;
        Preco = preco;
        Descricao = descricao;
    }

    public void AtualizarPreco(decimal novoPreco)
    {
        if (novoPreco <= 0)
            throw new ArgumentException("O preço deve ser maior que zero.");

        Preco = novoPreco;
    }

    public void AtualizarDados(string nome, string? descricao, decimal novoPreco)
    {
         if (string.IsNullOrWhiteSpace(nome))
        throw new ArgumentException("O nome do produto é obrigatório.");

    if (novoPreco <= 0)
        throw new ArgumentException("O preço deve ser maior que zero.");

    Nome = nome;
    Descricao = descricao;
    AtualizarPreco(novoPreco);
    }

    public void Desativar()
    {
        Ativo = false;
    }

    public void Ativar()
    {
        Ativo = true;
    }
}
