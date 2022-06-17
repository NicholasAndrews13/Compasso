using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiProduto
{
    public class PalavraChave
    {
        [Key]
        public int Id { get; set; }
        public int IdProduto { get; set; }
        public string Nome { get; set; }
    }
}