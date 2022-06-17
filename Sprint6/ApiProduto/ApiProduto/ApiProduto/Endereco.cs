using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiProduto
{

    [NotMapped]
    public class Endereco
    {
        [Key]
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int CidadeId { get; set; }
        public int ProdutoId { get; set; }
    }
}