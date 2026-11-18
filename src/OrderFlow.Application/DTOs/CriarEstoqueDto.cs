namespace OrderFlow.Application.DTOs;

public class CriarEstoqueDto
{
    public long UnidadeId { get; set; }
    public long ProdutoId { get; set; }
    public int QuantidadeTotal { get; set; }
}