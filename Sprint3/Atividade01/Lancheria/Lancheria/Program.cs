using Nancy.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Lancheria
{
    internal class Program
    {
        static List<Produto> ProdutosExistentes = new List<Produto>()
        {
            new Produto(100, "Cachorro quente", 5.70m),
            new Produto(101, "X Completo", 18.30m),
            new Produto(102, "X Salada", 16.50m),
            new Produto(103, "Hamburguer", 22.40m),
            new Produto(104, "Coca 2L", 10.00m),
            new Produto(105, "Refrigerante", 1.00m),
        };

        static List<Produto> ProdutosPedidos = new List<Produto>();
        static int numeroMesa = 0;

        static void Main(string[] args)
        {
            numeroMesa = PegarMesa();
            bool exibeMenu = true;
            while (exibeMenu)
            {
                exibeMenu = Menu();
            }
        }

        private static bool Menu()
        {
            Console.Clear();
            Console.Write("▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒ PEDIDOS ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒");
            Console.WriteLine(" ");
            Console.WriteLine("╔═════════════════MENU DE OPÇÕES════════════════╗    ");
            Console.WriteLine("║ 1 EFETUAR PEDIDO                              ║    ");
            Console.WriteLine("║ 2 SAIR                                        ║    ");
            Console.WriteLine("╚═══════════════════════════════════════════════╝    ");
            Console.WriteLine(" ");
            Console.Write("DIGITE UMA OPÇÃO : ");

            switch (Console.ReadLine())
            {
                case "1":
                    Pedido();
                    return true;
                case "2":
                    return false;
                default:
                    return true;
            }
        }

        private static void Pedido()
        {
            Console.Clear();
            Console.WriteLine("Pedido");
            MostrarCardapioERealizaPedidos();
            Console.Write("Pressione uma tecla para continuar...");
            Console.ReadKey();
        }

        private static int PegarMesa()
        {
            Console.WriteLine("Digite a mesa, válidos apenas de 1 a 4");
            int numeroMesa = 0;
            while (!(numeroMesa > 0 && numeroMesa < 5))
            {
                string entrada = Console.ReadLine();
                Int32.TryParse(entrada, out numeroMesa);

                if (numeroMesa > 0 && numeroMesa < 5)
                {
                    return numeroMesa;
                }
                else
                {
                    PegarMesa();
                }
            }
            return -1;
        }

        static void MostrarCardapioERealizaPedidos()
        {
            Console.WriteLine(
            @"- Entrada em loop :
            Código  Produto                   Preço Unitário(R$)
            100       Cachorro quente     R$  5, 70
            101       X Completo          R$ 18, 30
            102       X Salada            R$ 16, 50
            103       Hamburguer          R$ 22, 40
            104       Coca 2L             R$ 10, 00
            105       Refrigerante        R$  1, 00
            999       Encerra pedido");

            int codigo = 0;
            while (!(codigo != 999 && codigo != 0))
            {
                codigo = PegarCodigoProduto();
                if (codigo == 999) break;
                Produto produtoPedido = ProdutosExistentes.Find(p => p.Codigo == codigo);
                ProdutosPedidos.Add(produtoPedido);
            }

            if (codigo == 999)
            {
                decimal total = 0;
                Console.WriteLine($"A mesa {numeroMesa} pediu os seguintes itens:");
                int contagem = 1;
                foreach (Produto produto in ProdutosPedidos)
                {
                    Console.WriteLine($"{contagem++} - {produto.Nome}");
                    total += produto.Preco;
                }
                string totalSaida = total.ToString("F2", CultureInfo.InvariantCulture);
                Console.WriteLine("Com valor total de R$: " + totalSaida);
                MostrarListaProdutos(total);
            }
        }

        private static int PegarCodigoProduto()
        {
            Console.WriteLine("Digite o codigo do produto!");
            int codigo = 0;
            while (!(codigo >= 100 && codigo <= 105))
            {
                string entrada = Console.ReadLine();
                Int32.TryParse(entrada, out codigo);

                if (codigo == 999) return 999;
                if (codigo >= 100 && codigo <= 105)
                    return codigo;
                else
                    PegarCodigoProduto();
            }
            return 999;
        }

        private static void MostrarListaProdutos(decimal totalSaida)
        {
            var serializer = new JavaScriptSerializer();
            Extrato extrato = new Extrato(totalSaida, ProdutosPedidos);
            string saidaJson = serializer.Serialize(extrato);
            string jsonFormatado = JValue.Parse(saidaJson).ToString(Formatting.Indented);

            Console.WriteLine(jsonFormatado);
        }
    }
}
