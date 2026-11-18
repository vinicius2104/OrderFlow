namespace OrderFlow.Domain.Entities;

public class Pagamento
{
    public long Id { get; private set; }
    public long PedidoId { get; private set; }

    public decimal Valor { get; private set; }
    public string Metodo { get; private set; } = string.Empty;
    public string Status { get; private set; } = "PENDENTE";
    public string? CodigoTransacao { get; private set; }

    public DateTime CriadoEm { get; private set; } = DateTime.UtcNow;
    public DateTime AtualizadoEm { get; private set; } = DateTime.UtcNow;

    private Pagamento() { }

    public Pagamento(long pedidoId, decimal valor, string metodo)
    {
        if (pedidoId <= 0)
            throw new ArgumentException("O pedido deve ser válido.");

        if (valor <= 0)
            throw new ArgumentException("O valor deve ser maior que zero.");

        if (string.IsNullOrWhiteSpace(metodo))
            throw new ArgumentException("O método de pagamento é obrigatório.");

        PedidoId = pedidoId;
        Valor = valor;
        Metodo = metodo;
    }

    public void Aprovar(string codigoTransacao)
    {
        if (Status != "PENDENTE")
            throw new InvalidOperationException("O pagamento já foi processado.");

        if (string.IsNullOrWhiteSpace(codigoTransacao))
            throw new ArgumentException("O código da transação é obrigatório.");

        Status = "APROVADO";
        CodigoTransacao = codigoTransacao;
        AtualizadoEm = DateTime.UtcNow;
    }

    public void Recusar(string codigoTransacao)
    {
        if (Status != "PENDENTE")
            throw new InvalidOperationException("O pagamento já foi processado.");

        Status = "RECUSADO";
        CodigoTransacao = codigoTransacao;
        AtualizadoEm = DateTime.UtcNow;
    }
}