
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderFlow.Domain.Entities;

namespace OrderFlow.Infrastructure.Data.Configuracoes;

public class EstoqueConfiguracao : IEntityTypeConfiguration<Estoque>
{
    public void Configure(EntityTypeBuilder<Estoque> builder)
    {
        builder.ToTable("estoques");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.UnidadeId)
            .IsRequired();

        builder.Property(e => e.ProdutoId)
            .IsRequired();

        builder.HasIndex(e => new { e.UnidadeId, e.ProdutoId })
            .IsUnique();

        builder.Property(e => e.QuantidadeTotal)
            .IsRequired();

        builder.Property(e => e.QuantidadeReservada)
            .IsRequired();

        builder.Property(e => e.AtualizadoEm)
            .IsRequired();

        builder.HasOne<Unidade>()
            .WithMany()
            .HasForeignKey(e => e.UnidadeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Produto>()
            .WithMany()
            .HasForeignKey(e => e.ProdutoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
