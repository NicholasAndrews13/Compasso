using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AllFuncionarios.Dados
{
    public class FuncionariosConfiguration : IEntityTypeConfiguration<Funcionarios>
    {
        public void Configure(EntityTypeBuilder<Funcionarios> builder)
        {
            builder
           .ToTable("Funcionarios");

            builder
                .Property(c => c.Id)
                .HasColumnName("Id");

            builder
                .Property(c => c.Nome)
                .HasColumnName("Nome");

            builder
                .Property(c => c.DataNascimento)
                .HasColumnName("DataNascimento")
                .HasColumnType("datetime")
                .IsRequired();

            builder
                .Property(c => c.CidadeId)
                .HasColumnName("CidadeId")
                .IsRequired();

            builder
                 .Property(c => c.UltimaAtualizacao)
                 .HasColumnName("UltimaAtualizacao")
                 .HasColumnType("datetime")
                 .IsRequired();

        }
    }
}