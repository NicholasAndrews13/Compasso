using AllFuncionarios.Dados;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class VW_ALL_FUNCIONARIOSConfiguration : IEntityTypeConfiguration<VW_ALL_FUNCIONARIOS>
{
    public void Configure(EntityTypeBuilder<VW_ALL_FUNCIONARIOS> builder)
    {
        builder
            .ToTable("VW_ALL_FUNCIONARIOS");

        builder
            .Property(vw => vw.Id)
            .HasColumnName("Id");

        builder
            .Property(vw => vw.Nome)
            .HasColumnName("Nome");

        builder
            .Property(vw => vw.DataNascimento)
            .HasColumnName("DataNascimento");

        builder
            .Property(vw => vw.CidadeId)
            .HasColumnName("CidadeId");

        builder
            .Property(vw => vw.UltimaAtualizacao)
            .HasColumnName("UltimaAtualizacao");

    }
}