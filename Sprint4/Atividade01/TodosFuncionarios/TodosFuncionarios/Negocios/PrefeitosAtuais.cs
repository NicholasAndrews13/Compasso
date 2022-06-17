using System;

namespace AllFuncionarios.Negocios
{
    public class PrefeitosAtuais
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public DateTime InicioMandato { get; set; }
        public DateTime FimMandato { get; set; }
        public Guid CidadeId { get; set; }
        public DateTime UltimaAtualizacao { get; set; }
    }
}
