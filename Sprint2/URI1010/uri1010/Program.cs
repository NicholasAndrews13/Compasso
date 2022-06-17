using System;
using System.Globalization;

namespace uri1010
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Peça 1
            // 0 = codigo
            // 1 = quantidade
            // 2 = valor unidade
            string textoPeca1 = Console.ReadLine();
            string textoPeca2 = Console.ReadLine();

            string[] peca1 = textoPeca1.Split(' ');
            string[] peca2 = textoPeca2.Split(' ');

            double totalPeca1 = RetornaValorProduto(peca1[1], peca1[2]);

            double totalPeca2 = RetornaValorProduto(peca2[1], peca2[2]);

            double totalFinal = totalPeca1 + totalPeca2;
            string resultado = totalFinal.ToString("F2", CultureInfo.InvariantCulture);
            Console.WriteLine($"VALOR A PAGAR: R$ {resultado}");

        }

        public static double RetornaValorProduto(string quantidadePeca, string valorUnidadePeca)
        {
            return Double.Parse(quantidadePeca, CultureInfo.InvariantCulture) * Double.Parse(valorUnidadePeca, CultureInfo.InvariantCulture);
        }

    }
}
}
