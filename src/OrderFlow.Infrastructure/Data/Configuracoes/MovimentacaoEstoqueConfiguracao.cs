
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderFlow.Domain.Entities;

namespace OrderFlow.Infrastructure.Data.Configuracoes;

public class MovimentacaoEstoqueConfiguracao
    : IEntityTypeConfiguration<MovimentacaoEstoque>
{
    public void Configure(EntityTypeBuilder<MovimentacaoEstoque> builder)
    {
        builder.ToTable("movimentacoes_estoque");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.EstoqueId)
            .IsRequired();

        builder.Property(m => m.PedidoId)
            .IsRequired(false);

        builder.Property(m => m.UsuarioId)
            .IsRequired(false);

        builder.Property(m => m.Tipo)
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(m => m.Quantidade)
            .IsRequired();

        builder.Property(m => m.Descricao)
            .HasMaxLength(300);

        builder.Property(m => m.CriadoEm)
            .IsRequired();

        builder.HasOne<Estoque>()
            .WithMany()
            .HasForeignKey(m => m.EstoqueId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Pedido>()
            .WithMany()
            .HasForeignKey(m => m.PedidoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Usuario>()
            .WithMany()
            .HasForeignKey(m => m.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
