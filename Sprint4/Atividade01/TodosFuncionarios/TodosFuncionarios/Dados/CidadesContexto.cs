using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllFuncionarios.Dados
{
    public class CidadesContexto : DbContext
    {
        public DbSet<Cidades> Cidades { get; set; }
        public DbSet<Funcionarios> Funcionarios { get; set; }
        public DbSet<PrefeitosAtuais> PrefeitosAtuais { get; set; }
        public DbSet<Funcoes> Funcoes { get; set; }
        public DbSet<FuncoesFuncionarios> FuncoesFuncionarios { get; set; }
        [NotMapped]
        public DbSet<VW_ALL_FUNCIONARIOS> VW_ALL_FUNCIONARIOS { get; set; }
        public DbSet<SP_ADD_CIDADE> SP_ADD_CIDADE { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-I7I9CFI\\SQLEXPRESS; Database=Cidades;Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CidadesConfiguration());
            modelBuilder.ApplyConfiguration(new FuncionariosConfiguration());
            modelBuilder.ApplyConfiguration(new PrefeitosAtuaisConfiguration());
            modelBuilder.ApplyConfiguration(new FuncoesConfiguration());
            modelBuilder.ApplyConfiguration(new FuncoesFuncionariosConfiguration());
            modelBuilder.ApplyConfiguration(new VW_ALL_FUNCIONARIOSConfiguration());
        }
    }
}