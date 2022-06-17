using System;
using System.Collections.Generic;

namespace uri1507
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int nLinhas = Int32.Parse(Console.ReadLine());
            List<string> linhas = new List<string>();
            string conteudo = "";
            for (int i = 0; i < nLinhas; i++)
            {
                conteudo = Console.ReadLine();
                linhas.Add(conteudo);
            }
            if (linhas.Count == 1)
            {
                Console.WriteLine("Yes");
                return;
            }
            if (linhas.Count > 0)
                for (int i = 0; i < nLinhas; i++)
                {
                    if (i + 1 == linhas.Count)
                    {
                        break;
                    }

                    if (linhas[i + 1].Contains(linhas[i]))
                        Console.WriteLine("Yes");
                    else
                        Console.WriteLine("No");
                }
            else
                Console.WriteLine("No");

        }
    }
}
