
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderFlow.Domain.Entities;

namespace OrderFlow.Infrastructure.Data.Configuracoes;

public class PagamentoConfiguracao : IEntityTypeConfiguration<Pagamento>
{
    public void Configure(EntityTypeBuilder<Pagamento> builder)
    {
        builder.ToTable("pagamentos");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.PedidoId)
            .IsRequired();

        builder.HasIndex(p => p.PedidoId)
            .IsUnique();

        builder.Property(p => p.Valor)
            .HasPrecision(10, 2)
            .IsRequired();

        builder.Property(p => p.Metodo)
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(p => p.Status)
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(p => p.CodigoTransacao)
            .HasMaxLength(100);

        builder.Property(p => p.CriadoEm)
            .IsRequired();

        builder.Property(p => p.AtualizadoEm)
            .IsRequired();

        builder.HasOne<Pedido>()
            .WithMany()
            .HasForeignKey(p => p.PedidoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
