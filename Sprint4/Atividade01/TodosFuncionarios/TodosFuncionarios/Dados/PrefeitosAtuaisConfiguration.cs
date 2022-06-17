using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AllFuncionarios.Dados
{
    public class PrefeitosAtuaisConfiguration : IEntityTypeConfiguration<PrefeitosAtuais>
    {
        public void Configure(EntityTypeBuilder<PrefeitosAtuais> builder)
        {
            builder.ToTable("PrefeitosAtuais");

            builder
                .Property(p => p.Id)
                .HasColumnName("Id")
                .IsRequired();

            builder
                .Property(p => p.Nome)
                .HasColumnName("Nome");

            builder
                .Property(p => p.InicioMandato)
                .HasColumnName("InicioMandato")
                .HasColumnType("datetime")
                .IsRequired();

            builder
                .Property(p => p.FimMandato)
                .HasColumnName("FimMandato")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(c => c.CidadeId)
                .HasColumnName("CidadeId")
                .IsRequired();

            builder
                .Property(c => c.UltimaAtualizacao)
                .HasColumnName("UltimaAtualizacao");


        }
    }
}