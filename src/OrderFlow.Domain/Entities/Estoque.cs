namespace OrderFlow.Domain.Entities;

public class Estoque
{
    public long Id { get; private set; }
    public long UnidadeId { get; private set; }
    public long ProdutoId { get; private set; }

    public int QuantidadeTotal { get; private set; }
    public int QuantidadeReservada { get; private set; }

    public DateTime AtualizadoEm { get; private set; } = DateTime.UtcNow;

    private Estoque() { } //entity framework

    public Estoque(long unidadeId, long produtoId, int quantidadeTotal)
    {
        if (unidadeId <= 0 || produtoId <= 0)
            throw new ArgumentException("Unidade e produto devem ser válidos.");

        if (quantidadeTotal < 0)
            throw new ArgumentException("A quantidade não pode ser negativa.");

        UnidadeId = unidadeId;
        ProdutoId = produtoId;
        QuantidadeTotal = quantidadeTotal;
        QuantidadeReservada = 0;
    }

    public int ConsultarDisponibilidade()
    {
        return QuantidadeTotal - QuantidadeReservada;
    }

    public void ReservarQuantidade(int quantidade)
    {
        if (quantidade <= 0)
            throw new ArgumentException("A quantidade deve ser maior que zero.");

        if (quantidade > ConsultarDisponibilidade())
            throw new InvalidOperationException("Estoque insuficiente.");

        QuantidadeReservada += quantidade;
        AtualizadoEm = DateTime.UtcNow;
    }

    public void LiberarReserva(int quantidade)
    {
        if (quantidade <= 0 || quantidade > QuantidadeReservada)
            throw new InvalidOperationException("Quantidade de reserva inválida.");

        QuantidadeReservada -= quantidade;
        AtualizadoEm = DateTime.UtcNow;
    }

    public void ConfirmarBaixa(int quantidade)
    {
        if (quantidade <= 0 || quantidade > QuantidadeReservada)
            throw new InvalidOperationException("Quantidade reservada insuficiente.");

        QuantidadeReservada -= quantidade;
        QuantidadeTotal -= quantidade;
        AtualizadoEm = DateTime.UtcNow;
    }
}