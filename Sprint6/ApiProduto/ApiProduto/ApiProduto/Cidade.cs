using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CidadesClientes
{
    [NotMapped]
    public class Cidade
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set;}
        public string Estado { get; set; }
        public int ClienteId { get; set; }

    }
}
