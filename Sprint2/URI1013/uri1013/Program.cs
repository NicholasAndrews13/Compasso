using System;

namespace uri1013
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.ReadKey();
            string[] numero = Console.ReadLine().Split(' ');

            int maior = RetornaMaior(RetornaMaior(numero[0], numero[1]).ToString(), numero[2]);

            Console.WriteLine(maior + " eh o maior");
        }
        public static int RetornaMaior(string numero1, string numero2)
        {
            int numero1Int = Int32.Parse(numero1);
            int numero2Int = Int32.Parse(numero2);
            return (numero1Int + numero2Int + Math.Abs(numero1Int - numero2Int)) / 2;
        }
    }
}
