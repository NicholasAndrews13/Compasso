using System.ComponentModel.DataAnnotations.Schema;

namespace CidadeCliente
{
    [NotMapped]
    public class CidadeDados
    {
        public int Id { get; set; }
        public string cep { get; set;}
        public string logradouro { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string localidade { get; set; }
        public string uf { get; set; }
        public string ibge { get; set; }
        public string gia { get; set; }
        public string ddd { get; set; }
        public string siafi { get; set; }
        public int clienteId { get; set; }
    }
}