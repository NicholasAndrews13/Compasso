using CidadesCliente;
using Microsoft.EntityFrameworkCore;

namespace CidadeCliente
{
    public class ConexaoBd : DbContext
    {
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Cidade> Cidade { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=CidadeCliente;Trusted_Connection=True;";
            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
