using System;
using System.Text;

namespace uri1234
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string entrada;
            StringBuilder saida = new StringBuilder();
            bool mudar;
            while ((entrada = Console.ReadLine()) != null)
            {
                saida.Clear();
                mudar = true;
                foreach (var c in entrada)
                {
                    if (!c.Equals(' '))
                    {
                        saida.Append(mudar ? char.ToUpper(c) : char.ToLower(c));
                        mudar = !mudar;
                    }
                    else
                    {
                        saida.Append(c);
                    }
                }
                Console.WriteLine(saida);
            }
        }
    }
}
