using CidadesCliente;
using Nancy.Json;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace CidadeCliente
{
    public class DbOperacoes<TEntity>  where TEntity : class
    {
        static ConexaoBd conexao = new ConexaoBd();
        public DbOperacoes()
        {

        }

        public TEntity Cadastrar(TEntity obj)
        {
            conexao.Set<TEntity>().Add(obj);
            conexao.SaveChanges();

            return conexao.Set<TEntity>().ToList().Last<TEntity>();
        }
         public void Deletar( int id)
         {
            var cliente = conexao.Set<TEntity>().Find(id);
            conexao.Set<TEntity>().Remove(cliente);
            
            conexao.SaveChanges();
         }

        public TEntity SelecionarPorId(int id)
        {
            var cidade = conexao.Set<TEntity>().Find(id);
            return cidade;
        }

        public List<TEntity> RetornarTudo()
        {
            return conexao.Set<TEntity>().Where(e => true).ToList();
        }

        public void Editar(TEntity cidadeAtualizada)
        {
            // erro de tracking
            conexao.Set<TEntity>().Update(cidadeAtualizada);
            conexao.SaveChanges();
        }

        public TEntity UltimaEntidade()
        {
            return conexao.Set<TEntity>().Last(); 
        }

        public Cliente CLienteComCidades(Cliente entidade)
        {
            var cidades = conexao.Cidade.Where(e => e.ClienteId == entidade.Id).Select(e => e.Nome).ToList();
            var cliente = conexao.Cliente.Where(e => e.Id == entidade.Id).First();
            Cliente saida = null;
            if (cidades.Count > 0)
            {
                saida = cliente;
                saida.Cidades = cidades;
                return saida;
            }
            return cliente;
        }

        public List<Cliente> ClientesComCidades()
        {
            var listaCompleta = conexao.Cliente.Where(e => true).ToList();
            var listaSaida = new List<Cliente>();
            foreach (var item in listaCompleta)
            {
                var cidadesDoCliente = CLienteComCidades(item).Cidades;
                if (cidadesDoCliente != null)
                    item.Cidades = cidadesDoCliente;
                listaSaida.Add(item);
            }

            return listaSaida;
        }


        public CidadeDados RetornaCepDados(int cep)
        {
            string conteudo= "";
            RestClient restClient = new RestClient(string.Format($"https://viacep.com.br/ws/{cep}/json/", conteudo));
            RestRequest restRequest = new RestRequest($"https://viacep.com.br/ws/{cep}/json/");

            var resposta = restClient.Execute(restRequest);
            var serializer = new JavaScriptSerializer();
            CidadeDados saida = serializer.Deserialize<CidadeDados>(resposta.Content);

            return saida;
        }
    }
}
