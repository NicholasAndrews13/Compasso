using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AllFuncionarios.Dados
{
    public class FuncoesFuncionariosConfiguration : IEntityTypeConfiguration<FuncoesFuncionarios>
    {
        public void Configure(EntityTypeBuilder<FuncoesFuncionarios> builder)
        {

            builder.ToTable("FuncoesFuncionarios");
            builder.HasNoKey();

            builder.Property<Guid>("FuncionarioId")
                .IsRequired();
            // caso nao funcionar tentear id inteiro ao invez de guid;
            builder.Property<Guid>("FuncaoId")
                .IsRequired();
        }
    }
}