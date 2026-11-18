
using Microsoft.EntityFrameworkCore;
using OrderFlow.Domain.Entities;
using OrderFlow.Infrastructure.Data.Configuracoes;

namespace OrderFlow.Infrastructure.Data;

public class OrderFlowDbContext : DbContext
{
    public OrderFlowDbContext(
        DbContextOptions<OrderFlowDbContext> options)
        : base(options)
    {
    }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Unidade> Unidades => Set<Unidade>();
    public DbSet<Produto> Produtos => Set<Produto>();
    public DbSet<Estoque> Estoques => Set<Estoque>();
    public DbSet<Pedido> Pedidos => Set<Pedido>();
    public DbSet<ItemPedido> ItensPedido => Set<ItemPedido>();
    public DbSet<Pagamento> Pagamentos => Set<Pagamento>();
    public DbSet<MovimentacaoEstoque> MovimentacoesEstoque => Set<MovimentacaoEstoque>();
    public DbSet<Auditoria> Auditorias => Set<Auditoria>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ProdutoConfiguracao());
        modelBuilder.ApplyConfiguration(new UnidadeConfiguracao());
        modelBuilder.ApplyConfiguration(new UsuarioConfiguracao());
        modelBuilder.ApplyConfiguration(new EstoqueConfiguracao());
        modelBuilder.ApplyConfiguration(new PedidoConfiguracao());
        modelBuilder.ApplyConfiguration(new ItemPedidoConfiguracao());
        modelBuilder.ApplyConfiguration(new PagamentoConfiguracao());
        modelBuilder.ApplyConfiguration(new MovimentacaoEstoqueConfiguracao());
        modelBuilder.ApplyConfiguration(new AuditoriaConfiguracao());
    }

}
