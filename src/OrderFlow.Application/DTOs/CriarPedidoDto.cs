namespace OrderFlow.Application.DTOs;

public class CriarPedidoDto
{
    public long UnidadeId { get; set; }
    public long? ClienteId { get; set; }
    public string CanalPedido { get; set; } = string.Empty;
    public string ChaveIdempotencia { get; set; } = string.Empty;

    public List<CriarItemPedidoDto> Itens { get; set; } = new();
}

public class CriarItemPedidoDto
{
    public long ProdutoId { get; set; }
    public int Quantidade { get; set; }
}