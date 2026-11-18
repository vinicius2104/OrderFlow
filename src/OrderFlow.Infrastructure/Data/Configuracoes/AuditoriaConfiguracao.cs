
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderFlow.Domain.Entities;

namespace OrderFlow.Infrastructure.Data.Configuracoes;

public class AuditoriaConfiguracao : IEntityTypeConfiguration<Auditoria>
{
    public void Configure(EntityTypeBuilder<Auditoria> builder)
    {
        builder.ToTable("auditorias");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.UsuarioId)
            .IsRequired(false);

        builder.Property(a => a.Entidade)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(a => a.RegistroId)
            .IsRequired();

        builder.Property(a => a.Acao)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(a => a.Detalhes)
            .HasMaxLength(1000);

        builder.Property(a => a.CriadoEm)
            .IsRequired();

        builder.HasOne<Usuario>()
            .WithMany()
            .HasForeignKey(a => a.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
