using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiProduto
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdCidade { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Categoria { get; set; }
        [NotMapped]
        public List<string> PalavrasChave { get; set; }
    }
}