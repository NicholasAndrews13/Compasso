using System;
using System.ComponentModel.DataAnnotations;

namespace ApiProduto
{
    public class Auditoria
    {
        [Key]
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int ProdutoId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime Data { get; set; }
        public string Evento { get; set; }
    }
}
