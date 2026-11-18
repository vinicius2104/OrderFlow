namespace OrderFlow.Application.DTOs;

public class CriarProdutoDto
{
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public decimal Preco { get; set; }
}