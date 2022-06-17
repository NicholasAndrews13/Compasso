using CidadesCliente;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace CidadeCliente.Controllers
{
    [Route("[controller]")]
    public class ClienteControle : ControllerBase
    {
        DbOperacoes<Cliente> dbClientes;
        DbOperacoes<Cidade> dbCidades;
        Login login;
        static int portaA = 5092;
        public ClienteControle()
        {
            dbClientes = new DbOperacoes<Cliente>();
            dbCidades = new DbOperacoes<Cidade>();

            var lista = TodosLogin().Where(u => u.LoginAtual == true).ToList();
            if (lista.Count() == 1)
                login = lista.First();

            if (!ConferirLogin())
                return;
        }

        public bool ConferirLogin()
        {
            if (login == null)
                return false;
            else
                return true;
        }

        public List<Login> TodosLogin()
        {
            RestClient restClient = new RestClient(string.Format($"http://localhost:{portaA}/Login/SelecionarTodos"));
            RestRequest restRequest = new RestRequest($"http://localhost:{portaA}/Login/SelecionarTodos");
            var resposta = restClient.ExecuteGet(restRequest);
            return JsonConvert.DeserializeObject<List<Login>>(resposta.Content);
        }
        public void RegistrarAuditoria(Auditoria auditoria)
        {
            RestClient restClient = new RestClient(string.Format($"http://localhost:{portaA}/Auditoria/Cadastrar"));
            RestRequest restRequest = new RestRequest($"http://localhost:{portaA}/Auditoria/Cadastrar");
            restRequest.AddBody(auditoria);
            var resposta = restClient.ExecutePost(restRequest);
        }
        public void SalvarAuditoria(int idCliente, int idProduto, string mensagemEvento)
        {
            var auditoria = new Auditoria()
            {
                ClienteId = idCliente,
                Evento = mensagemEvento,
                ProdutoId = idProduto,
                UsuarioId = login.Id
            };

            RegistrarAuditoria(auditoria);
        }

        public int ClienteProximoId()
        {
            var entidade = dbClientes.UltimaEntidade();

            return (1 + entidade.Id);
        }
        
        public bool CidadeExiste(Cidade cidade, Cliente cliente)
        {
            var contagem = dbCidades.RetornarTudo().Where(e =>
                e.Nome == cidade.Nome &&
                e.Estado == cidade.Estado &&
                e.ClienteId == cliente.Id
            ).Count();

            if (contagem != 0)
                return true;
            else
                return false;
        }

        public Cliente ClienteExiste(Cliente cliente)
        {
            return dbClientes.SelecionarPorId(cliente.Id);
        }

       

        [HttpPost]
        [Route("Cadastrar")]
        public IActionResult Cadastrar([FromBody] Cliente cliente)
        {
            if (!ConferirLogin())
              return NotFound("Não permitido sem logar");
            
            var retorno = dbCidades.RetornaCepDados(cliente.Cep);
            if (retorno.localidade == null)
            {
                SalvarAuditoria(0, 0, "Cep Inexistente");
                return NotFound("Erro Ao Buscar Cep");
            }
                
            cliente.Bairro = retorno.bairro;
            cliente.Logradouro = retorno.logradouro;
            var mensagem = "";
            var entidade = dbClientes.RetornarTudo().Where(e =>
                e.Nome == cliente.Nome &&
                e.DataNascimento == cliente.DataNascimento 
                ).FirstOrDefault();
            if (entidade == null)
                entidade = dbClientes.Cadastrar(cliente);
            mensagem = "Cliente cadastrado \n";
            var cidade = new Cidade();
            cidade.Estado = retorno.uf;
            cidade.Nome = retorno.localidade;
            cidade.ClienteId = entidade.Id;

            bool existeCidade = CidadeExiste(cidade,entidade);
            if (!existeCidade)
            {
                cidade = dbCidades.Cadastrar(cidade);
                mensagem += "Cidade cadastrada com sucesso";
            }
            
            SalvarAuditoria(entidade.Id, 0, mensagem);
            return Ok(mensagem);
        }

        [HttpDelete]
        [Route("Deletar/{id}")]
        public IActionResult Deletar([FromRoute] int id)
        {

            if (!ConferirLogin())
                return NotFound("Não permitido sem logar");

            dbClientes.Deletar(id);
            SalvarAuditoria(id, 0, "Cliente deletado com sucesso!");
            return Ok("Cliente deletado com sucesso");
        }

        [HttpGet]
        [Route("Selecionar/{id}")]
        public ActionResult<Cliente> Selecionar([FromRoute] int id)
        {

            if (!ConferirLogin())
                return NotFound("Não permitido sem logar");

            var cliente = dbClientes.SelecionarPorId(id);
            cliente = dbClientes.CLienteComCidades(cliente);
            SalvarAuditoria(id, 0, $"Cliente de id {id} pesquisado!");
            return Ok(cliente);
        }
        
        [HttpGet]
        [Route("SelecionarTodos")]
        public ActionResult<List<Cliente>> SelecionarTodos()
        {
            if (!ConferirLogin())
                return NotFound("Não permitido sem logar");

            var lista = dbClientes.ClientesComCidades();
            SalvarAuditoria(0, 0, "Pesquisado todos Clientes!");
            return Ok(lista);
        }

        [HttpPut]
        [Route("Atualizar")]
        public ActionResult<List<Cliente>> Atualizar([FromBody] Cliente cliente)
        {
            if (!ConferirLogin())
                return NotFound("Não permitido sem logar");
            var clienteAntigo = dbClientes.SelecionarPorId(cliente.Id);
            if (clienteAntigo == null)
                return NotFound("Não existe cliente com id selecionado");

            clienteAntigo.DataNascimento = cliente.DataNascimento;
            clienteAntigo.Cep = cliente.Cep;
            clienteAntigo.Nome = cliente.Nome;
            clienteAntigo.Cidades = cliente.Cidades;
            clienteAntigo.Bairro = cliente.Bairro;
            clienteAntigo.Logradouro = cliente.Logradouro;
            
            dbClientes.Editar(clienteAntigo);
            SalvarAuditoria(clienteAntigo.Id, 0, "Cliente Atualizado com sucesso!");
            return Ok("Cliente atualizado com sucesso");
        }

    }
}
