using CidadesCliente;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CidadeCliente
{
    [NotMapped]
    public class Endereco
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Cidade))]
        public int CidadeId { get; set; }
        [ForeignKey(nameof(Cliente))]
        public int ClienteId { get; set; }
    }
}
