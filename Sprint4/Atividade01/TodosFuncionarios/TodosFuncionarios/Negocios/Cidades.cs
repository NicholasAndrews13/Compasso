using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AllFuncionarios.Negocios
{
    public class Cidades
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int Populacao { get; set; }
        public float TaxaCriminalidade { get; set; }
        public float ImpostoSobreProduto { get; set; }
        public bool EstadoCalamidade { get; set; }
        public DateTime UltimaAtualizacao { get; set; }
        [NotMapped]
        public List<Funcionarios> Funcionarios { get; set; }
        [NotMapped]
        public List<PrefeitosAtuais> PrefeitosAtuais { get; set; }
    }
}

