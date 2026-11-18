
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderFlow.Domain.Entities;

namespace OrderFlow.Infrastructure.Data.Configuracoes;

public class UnidadeConfiguracao : IEntityTypeConfiguration<Unidade>
{
    public void Configure(EntityTypeBuilder<Unidade> builder)
    {
        builder.ToTable("unidades");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Nome)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(u => u.Cnpj)
            .HasMaxLength(14)
            .IsRequired();

        builder.HasIndex(u => u.Cnpj)
            .IsUnique(); //nao sera permitido mais de uma unidade com o mesmo CNPJ

        builder.Property(u => u.Endereco)
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(u => u.Ativa)
            .IsRequired();

        builder.Property(u => u.CriadoEm)
            .IsRequired();
    }
}
