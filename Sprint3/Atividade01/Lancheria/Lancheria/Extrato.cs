using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lancheria
{
    public class Extrato
    {
        public decimal ValorTotal { get; set; }
        public List<Produto> Produtos { get; set; }

        public Extrato(decimal valorTotal, List<Produto> produtos)
        {
            Produtos = produtos;
            ValorTotal = valorTotal;
        }
    }
}
