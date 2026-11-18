namespace OrderFlow.Domain.Entities;

public class ItemPedido
{
    public long Id { get; private set; }
    public long PedidoId { get; private set; }
    public long ProdutoId { get; private set; }

    public int Quantidade { get; private set; }
    public decimal PrecoUnitario { get; private set; }
    public decimal Subtotal { get; private set; }

    private ItemPedido() { }

    public ItemPedido(long produtoId, int quantidade, decimal precoUnitario)
    {
        if (produtoId <= 0)
            throw new ArgumentException("O produto deve ser válido.");

        if (quantidade <= 0)
            throw new ArgumentException("A quantidade deve ser maior que zero.");

        if (precoUnitario <= 0)
            throw new ArgumentException("O preço deve ser maior que zero.");

        ProdutoId = produtoId;
        Quantidade = quantidade;
        PrecoUnitario = precoUnitario;

        CalcularSubtotal();
    }

    public void CalcularSubtotal()
    {
        Subtotal = Quantidade * PrecoUnitario;
    }

    public void AtualizarQuantidade(int novaQuantidade)
    {
        if (novaQuantidade <= 0)
            throw new ArgumentException("A quantidade deve ser maior que zero.");

        Quantidade = novaQuantidade;
        CalcularSubtotal();
    }
}