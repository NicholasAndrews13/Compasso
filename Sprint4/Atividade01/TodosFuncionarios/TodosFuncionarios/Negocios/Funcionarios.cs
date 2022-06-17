using System;

namespace AllFuncionarios.Negocios
{
    public class Funcionarios
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public Guid CidadeId { get; set; }
        public DateTime UltimaAtualizacao { get; set; }
    }
}
