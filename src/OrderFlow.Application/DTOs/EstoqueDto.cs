namespace OrderFlow.Application.DTOs;

public class EstoqueDto
{
    public long Id { get; set; }
    public long UnidadeId { get; set; }
    public long ProdutoId { get; set; }
    public int QuantidadeTotal { get; set; }
    public int QuantidadeReservada { get; set; }
    public int QuantidadeDisponivel { get; set; }
}