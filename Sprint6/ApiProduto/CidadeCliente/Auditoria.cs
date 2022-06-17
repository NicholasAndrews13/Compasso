using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CidadeCliente
{
    [NotMapped]
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
