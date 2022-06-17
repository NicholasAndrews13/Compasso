using System;

namespace uri1251
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string entrada1 = Console.ReadLine();
            string entrada2 = Console.ReadLine();
            ContaOcorrenciaTabelaAscii(entrada1);
            ContaOcorrenciaTabelaAscii(entrada2);

        }

        private static void ContaOcorrenciaTabelaAscii(string entrada)
        {
            string[] saida = new string[1000];
            int menor = entrada.Length;
            string[] saidaMenor = new string[1000];
            int codigoAscii = 0;
            bool encontrouNovo = false;
            while (entrada.Length != 0)
            {
                encontrouNovo = false;
                foreach (char caractere in entrada)
                {
                    if ((int)caractere > 31 && (int)caractere <= 127)
                    {
                        saida = ContaOcorrenciasERemove(entrada, caractere);

                        if ((Int32.Parse(saida[1]) <= menor))
                        {
                            saidaMenor = saida;
                            codigoAscii = (int)caractere;
                            menor = Int32.Parse(saida[1]);
                            encontrouNovo = true;
                        }
                    }
                }
                menor = saidaMenor[0].Length;
                Console.WriteLine(codigoAscii + " " + saidaMenor[1]);
                entrada = saidaMenor[0];

            }
        }

        static string[] ContaOcorrenciasERemove(string entrada, char ocorrencia)
        {

            int Ocorrencia = (int)ocorrencia;
            string saida = "";
            int codigoAscii = 0;
            int contador = 0;
            foreach (char caractere in entrada)
            {
                codigoAscii = (int)caractere;
                if (Ocorrencia == codigoAscii)
                {
                    contador++;
                }
                else
                {
                    saida += caractere;
                }

            }
            var arraySaida = new string[]{
                saida, contador.ToString()
            };
            return arraySaida;
        }
    }
}
