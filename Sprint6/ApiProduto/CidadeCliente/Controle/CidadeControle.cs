using CidadesCliente;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace CidadeCliente.Controllers
{
    [Route("[controller]")]
    public class CidadeController : ControllerBase
    {

        DbOperacoes<Cidade> dbCidades;
        Login login;
        static int portaA = 5092;
        public CidadeController()
        {
            dbCidades = new DbOperacoes<Cidade>();

            var lista = TodosLogin().Where(u => u.LoginAtual == true).ToList();
            if (lista.Count() == 1)
                login = lista.First();

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


        [HttpPost]
        [Route("Cadastrar")]
        public IActionResult Cadastrar([FromBody] Cidade cidade)
        {
            if (!ConferirLogin())
                return NotFound("Não permitido sem logar");
            dbCidades.Cadastrar(cidade);
            SalvarAuditoria(0, 0, "Cidade Salva com sucesso");
            return Ok("Cidade Cadastrada com sucesso");
        }

        [HttpDelete]
        [Route("Deletar/{id}")]
        public IActionResult Deletar([FromRoute] int id)
        {

            if (!ConferirLogin())
                return NotFound("Não permitido sem logar");
            dbCidades.Deletar(id);
            SalvarAuditoria(0, 0, $"Cidade de id {id} Deletada com sucesso");
            return Ok("Cidade deletada com sucesso");
        }

        [HttpGet]
        [Route("Selecionar/{id}")]
        public ActionResult<Cidade> Selecionar([FromRoute] int id)
        {

            if (!ConferirLogin())
                return NotFound("Não permitido sem logar");
            var cidade = dbCidades.SelecionarPorId(id);

            SalvarAuditoria(0, 0, $"Cidade Selecionada de id {id}");
            return Ok(cidade);
        }

        [HttpGet]
        [Route("SelecionarTodos")]
        public ActionResult<List<Cidade>> SelecionarTodos()
        {

            if (!ConferirLogin())
                return NotFound("Não permitido sem logar");
            var lista = dbCidades.RetornarTudo();
            SalvarAuditoria(0, 0, "Todas Cidades selecionadas!");
            return Ok(lista);
        }

        [HttpPut]
        [Route("Atualizar")]
        public ActionResult<List<Cidade>> Atualizar([FromBody] Cidade cidade)
        {

            if (!ConferirLogin())
                return NotFound("Não permitido sem logar");
            var entidade = dbCidades.SelecionarPorId(cidade.Id);
            if (entidade == null)
                return NotFound("Não foi encontrado entidade com respectivo id");
            entidade.Estado = cidade.Estado;
            entidade.ClienteId = cidade.ClienteId;
            entidade.Nome = cidade.Nome;

            dbCidades.Editar(entidade);
            SalvarAuditoria(0, 0, $"Cidade de id {entidade.Id} atualizada com sucesso!");
            return Ok("cidade atualizada com sucesso");
        }

        [HttpGet]
        [Route("{cep}")]
        public ActionResult CepDados([FromRoute] int cep)
        {
            var conteudo = dbCidades.RetornaCepDados(cep);

            return Ok(conteudo);
        }
    }
}
