using AllFuncionarios.Dados;
using Microsoft.EntityFrameworkCore;
using System;

namespace AllFuncionarios
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var contexto = new CidadesContexto())
            {
                Console.WriteLine("Resultados da View All Funcionários: ");
                string sqlSelect = "SELECT Id, Nome, DataNascimento, CidadeId, UltimaAtualizacao FROM VW_ALL_FUNCIONARIOS";
                var ResultSet = contexto.VW_ALL_FUNCIONARIOS.FromSqlRaw(sqlSelect);

                foreach (var item in ResultSet)
                {
                    Console.WriteLine("ID: {0} - NOME: {1} - DATA DE NASCIMENTO: {2} - ID CIDADE: {3} - ULTIMA ATUALIZAÇÃO: {4}", item.Id, item.Nome, item.DataNascimento, item.CidadeId, item.UltimaAtualizacao);
                }
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                Console.Clear();

                string sqlInsert = "INSERT [dbo].[Cidades] ([Id], [Nome], [Populacao], [TaxaCriminalidade], [ImpostoSobreProduto], [EstadoCalamidade], [UltimaAtualizacao])  " +
                    "VALUES(N'74c60f6f-ae05-4bca-ba68-83dd2f3d9a74', N'Santa Cruz do sul', 10000000, 77.5, 77.5, 10, CAST(N'2021-09-28T19:16:39.5833333' AS DateTime2))";

                string sqlProcedureInsert = @"CREATE PROCEDURE [dbo].[SP_ADD_CIDADE]
                @P_Id uniqueidentifier,
                @P_Nome varchar(max),
                @P_Populacao INTEGER,
                @P_TaxaCriminalidade DECIMAL(4,2),
                @P_ImpostoSobreProduto DECIMAL(4,2),
                @P_EstadoCalamidade BIT
                AS
	                BEGIN
		                INSERT INTO Cidades (
			                Id
			                , Nome
			                , Populacao
			                , TaxaCriminalidade
			                , ImpostoSobreProduto
			                , EstadoCalamidade
			                , UltimaAtualizacao)
		                VALUES (
			                @P_Id
			                , @P_Nome
			                , @P_Populacao
			                , @P_TaxaCriminalidade
			                , @P_ImpostoSobreProduto
			                , @P_EstadoCalamidade
			                , CURRENT_TIMESTAMP
		                )
	                END";

                //contexto.Database.ExecuteSqlRaw(sqlProcedureInsert);

                Guid id = new Guid();
                var nome = "N'Cidade do caraio'";
                int populacao = 10;
                float TaxaCriminalidade = 10.9f;
                float ImpostoSobreProduto = 10.0f;
                bool EstadoCalamidade = false;
                object[] parametros = new object[] { id, nome, populacao, TaxaCriminalidade, ImpostoSobreProduto, EstadoCalamidade };
                contexto.Database.ExecuteSqlRaw($"SP_ADD_CIDADE @p0 ,@p1 ,@p2 ,@p3 ,@p4 ,@p5", parameters: parametros);
                var resultadoInsert = contexto.Cidades.FromSqlRaw("SELECT Id,Nome,Populacao,TaxaCriminalidade,ImpostoSobreProduto,EstadoCalamidade,UltimaAtualizacao FROM Cidades");
                foreach (var item in resultadoInsert)
                {
                    Console.WriteLine("ID: {0} - NOME: {1} - População: {2} -  Calamidade: {3} ", item.Id, item.Nome, item.Populacao, item.EstadoCalamidade);
                }

                Console.ReadLine();
            }
        }
    }
}
