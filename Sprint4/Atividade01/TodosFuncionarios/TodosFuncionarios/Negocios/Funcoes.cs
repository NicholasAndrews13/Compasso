using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AllFuncionarios.Negocios
{
    public class Funcoes
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public int NivelAcesso { get; set; }
        [NotMapped]
        public List<FuncoesFuncionarios> Funcionarios { get; set; }
        public DateTime? UltimaAtualizacao { get; set; }
    }
}
