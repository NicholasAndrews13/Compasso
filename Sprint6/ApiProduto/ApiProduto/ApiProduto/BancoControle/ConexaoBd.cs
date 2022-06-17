using CidadesClientes;
using Microsoft.EntityFrameworkCore;

namespace ApiProduto
{
    public class ConexaoBd : DbContext
    {
        public DbSet<Produto> Produto { get; set; }
        public DbSet<PalavraChave> PalavraChave { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=SisProdutos;Trusted_Connection=True;";
            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
