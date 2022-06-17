using System;
using System.Globalization;

namespace uri1009
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var vendedor = Console.ReadLine();
            double salario = Double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            double vendasValor = Double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

            double salarioMensal = salario + (vendasValor * 0.15); // salário mais 15 %
            string salarioMensalFormatado = salarioMensal.ToString("F2", CultureInfo.InvariantCulture);
            Console.WriteLine($"TOTAL = R$ {salarioMensalFormatado}");

        }
    }
}
