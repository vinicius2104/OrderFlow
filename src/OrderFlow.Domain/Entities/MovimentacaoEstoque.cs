namespace OrderFlow.Domain.Entities;

public class MovimentacaoEstoque
{
    public long Id { get; private set; }
    public long EstoqueId { get; private set; }
    public long? PedidoId { get; private set; }
    public long? UsuarioId { get; private set; }

    public string Tipo { get; private set; } = string.Empty;
    public int Quantidade { get; private set; }
    public string? Descricao { get; private set; }

    public DateTime CriadoEm { get; private set; } = DateTime.UtcNow;

    private MovimentacaoEstoque() { }

    public MovimentacaoEstoque(
        long estoqueId,
        string tipo,
        int quantidade,
        long? pedidoId = null,
        long? usuarioId = null,
        string? descricao = null)
    {
        if (estoqueId <= 0)
            throw new ArgumentException("O estoque deve ser válido.");

        if (string.IsNullOrWhiteSpace(tipo))
            throw new ArgumentException("O tipo da movimentação é obrigatório.");

        if (quantidade <= 0)
            throw new ArgumentException("A quantidade deve ser maior que zero.");

        EstoqueId = estoqueId;
        Tipo = tipo;
        Quantidade = quantidade;
        PedidoId = pedidoId;
        UsuarioId = usuarioId;
        Descricao = descricao;
    }
}