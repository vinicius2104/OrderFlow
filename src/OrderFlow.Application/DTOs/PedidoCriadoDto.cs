namespace OrderFlow.Application.DTOs;

public class PedidoCriadoDto
{
    public long PedidoId { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal ValorTotal { get; set; }
    public string StatusPagamento { get; set; } = string.Empty;
    public DateTime CriadoEm { get; set; }
}