namespace OrderFlow.Domain.Entities;

public class Pedido
{
    private readonly List<ItemPedido> _itens = new();

    public long Id { get; private set; }
    public long UnidadeId { get; private set; }
    public long UsuarioId { get; private set; }
    public long? ClienteId { get; private set; }

    public string Status { get; private set; } = "PENDENTE_PAGAMENTO";
    public string CanalPedido { get; private set; } = string.Empty;
    public string ChaveIdempotencia { get; private set; } = string.Empty; //controlar possiveis pedidos duplicados

    public decimal ValorTotal { get; private set; }

    public DateTime CriadoEm { get; private set; } = DateTime.UtcNow;
    public DateTime AtualizadoEm { get; private set; } = DateTime.UtcNow;

    public IReadOnlyCollection<ItemPedido> Itens => _itens.AsReadOnly();

    private Pedido() { }

    public Pedido(
        long unidadeId,
        long usuarioId,
        string canalPedido,
        string chaveIdempotencia,
        long? clienteId = null)
    {
        if (unidadeId <= 0 || usuarioId <= 0)
            throw new ArgumentException("Unidade e usuário devem ser válidos.");

        if (string.IsNullOrWhiteSpace(canalPedido))
            throw new ArgumentException("O canal do pedido é obrigatório.");

        if (string.IsNullOrWhiteSpace(chaveIdempotencia))
            throw new ArgumentException("A chave de idempotência é obrigatória.");

        UnidadeId = unidadeId;
        UsuarioId = usuarioId;
        ClienteId = clienteId;
        CanalPedido = canalPedido;
        ChaveIdempotencia = chaveIdempotencia;
    }

    public void AdicionarItem(ItemPedido item)
    {
        if (item is null)
            throw new ArgumentNullException(nameof(item));

        _itens.Add(item);
        CalcularTotal();
        AtualizadoEm = DateTime.UtcNow;
    }

    public void CalcularTotal()
    {
        ValorTotal = _itens.Sum(item => item.Subtotal);
    }

    public void AtualizarStatus(string novoStatus)
    {
        if (string.IsNullOrWhiteSpace(novoStatus))
            throw new ArgumentException("O status é obrigatório.");

        Status = novoStatus;
        AtualizadoEm = DateTime.UtcNow;
    }

    public void Cancelar()
    {
        Status = "CANCELADO";
        AtualizadoEm = DateTime.UtcNow;
    }
}