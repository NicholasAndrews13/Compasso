using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AllFuncionarios.Dados
{
    public class FuncoesConfiguration : IEntityTypeConfiguration<Funcoes>
    {
        public void Configure(EntityTypeBuilder<Funcoes> builder)
        {
            builder.ToTable("Funcoes");

            builder
               .Property(c => c.Id)
               .HasColumnName("Id");

            builder
               .Property(c => c.Descricao)
               .HasColumnName("Descricao");

            builder
               .Property(c => c.NivelAcesso)
               .HasColumnName("NivelAcesso");

            builder
               .Property(c => c.UltimaAtualizacao)
               .HasColumnName("UltimaAtualizacao");
        }
    }
}