using CidadesClientes;
using Desafio6;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiProduto.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoControle : ControllerBase
    {
        DbOperacoesProduto dbOperacoesProduto;
        DbOperacoesPalavraChave dbpalavra;
        Login login;
        static int portaCC = 47562;
        static int portaA = 5092;
        public ProdutoControle()
        {
            dbOperacoesProduto = new DbOperacoesProduto();
            dbpalavra = new DbOperacoesPalavraChave();
            var lista = TodosLogin().Where(u => u.LoginAtual == true);
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

        public void SalvarAuditoria(int idCliente, int idProduto, string mensagemEvento)
        {
            var auditoria = new Auditoria()
            {
                ClienteId = idCliente,
                Evento = mensagemEvento,
                ProdutoId = idProduto,
                UsuarioId = login.Id,
                Data = DateTime.Now
            };

            RegistrarAuditoria(auditoria);
        }


        public List<Cidade> RetornaCidades()
        {
            RestClient restClient = new RestClient(string.Format($"http://localhost:{portaCC}/Cidade/SelecionarTodos"));
            RestRequest restRequest = new RestRequest($"http://localhost:{portaCC}/Cidade/SelecionarTodos");

            var resposta = restClient.ExecuteGet(restRequest);
            return JsonConvert.DeserializeObject<List<Cidade>>(resposta.Content);
        }

        public List<Cliente> RetornaClientes()
        {
            RestClient restClient = new RestClient(string.Format($"http://localhost:{portaCC}/Cliente/SelecionarTodos"));
            RestRequest restRequest = new RestRequest($"http://localhost:{portaCC}/Cliente/SelecionarTodos");

            var resposta = restClient.ExecuteGet(restRequest);
            return JsonConvert.DeserializeObject<List<Cliente>>(resposta.Content);
        }

        public void RegistrarAuditoria(Auditoria auditoria)
        {
            RestClient restClient = new RestClient(string.Format($"http://localhost:{portaA}/Auditoria/Cadastrar"));
            RestRequest restRequest = new RestRequest($"http://localhost:{portaA}/Auditoria/Cadastrar");
            restRequest.AddBody(auditoria);
            var resposta = restClient.ExecutePost(restRequest);
        }

        public List<Login> TodosLogin()
        {
            RestClient restClient = new RestClient(string.Format($"http://localhost:{portaA}/Login/SelecionarTodos"));
            RestRequest restRequest = new RestRequest($"http://localhost:{portaA}/Login/SelecionarTodos");
            var resposta = restClient.ExecuteGet(restRequest);
            return JsonConvert.DeserializeObject<List<Login>>(resposta.Content);
        }

        [HttpPost]
        [Route("Cadastrar")]
        public ActionResult Cadastrar([FromBody] Produto produto)
        {
            if (!ConferirLogin())
                return NotFound("Não permitido sem logar");

            if (!ClienteExiste(produto.IdCliente))
                return NotFound("Cliente Inexistente Cadastre Primeiro");

            decimal precoAntigo = produto.Preco;
            produto = CalcularFrete(produto);
           
            var mensagem = "Produto Salvo com sucesso";
            if (precoAntigo != produto.Preco)
            {
                mensagem = "Não havia cidades cadastradas que combinam com a selecionada\nAdicionado 29,90 de frete!";
                produto = dbOperacoesProduto.Cadastrar(produto);
            }
            else
                produto = dbOperacoesProduto.Cadastrar(produto);
            
            SalvarAuditoria(produto.IdCliente, produto.Id, mensagem);

            if(!(produto.PalavrasChave == null))
            foreach (var palavra in produto.PalavrasChave)
            {
                dbpalavra.Cadastrar(new PalavraChave()
                {
                    IdProduto = produto.Id,
                    Nome = palavra
                });
            }

            return Ok(mensagem);
        }

        public Produto CalcularFrete(Produto produto)
        {
            var cidades = RetornaCidades();

            var retorno = cidades.Where(c => c.Id == produto.IdCidade).ToList();
            if(retorno.Count == 0)
                produto.Preco += 29.90m;
            SalvarAuditoria(produto.IdCliente, produto.Id, "Produto Frete Calculado");
            return produto;
        }
        
        public bool ClienteExiste(int id)
        {
            var clientes = RetornaClientes();
            foreach (var cliente in clientes)
            {
                if (cliente.Id == id)
                    return true;
            }
            return false;
        }

        [HttpGet]
        [Route("Filtrar/Preco")]
        public ActionResult<List<Produto>> PrecoFiltro([FromBody] decimal preco)
        {
            if (!ConferirLogin())
                return NotFound("Não permitido sem logar");

            var lista = dbOperacoesProduto.RetornarTudo().FindAll(p => p.Preco == preco);
            SalvarAuditoria(0 , 0, "Produto Filtrado por Preco");
            return Ok(lista);
        }
        [HttpGet]
        [Route("Filtrar/Nome")]
        public ActionResult<List<Produto>> NomeFiltro([FromBody] string nome)
        {
            if (!ConferirLogin())
                return NotFound("Não permitido sem logar");

            var lista = dbOperacoesProduto.RetornarTudo().FindAll(p => p.PalavrasChave.Contains(nome) || p.Nome.ToLower() == nome.ToLower());
            SalvarAuditoria( 0 , 0, "Produto Filtrado Por Nome");
            return Ok(lista);
        }

        [HttpGet]
        [Route("Filtrar/Categoria")]
        public ActionResult<List<Produto>> CategoriaFiltro([FromBody] int categoria)
        {
            if (!ConferirLogin())
                return NotFound("Não permitido sem logar");

            var lista = dbOperacoesProduto.RetornarTudo().FindAll(p => p.Categoria == categoria);

            SalvarAuditoria( 0, 0, "Produto Filtrado por Categoria");
            return Ok(lista);
        }

        [HttpDelete]
        [Route("Deletar/{id}")]
        public IActionResult Deletar([FromRoute] int id)
        {
            if (!ConferirLogin())
                return NotFound("Não permitido sem logar");

            dbOperacoesProduto.Deletar(id);
            SalvarAuditoria(0, id, $"Produto Deletado id {id}");
            return Ok("Usuario deletado com sucesso");
        }

        [HttpGet]
        [Route("Selecionar/{id}")]
        public ActionResult<Produto> Selecionar([FromRoute] int id)
        {
            if (!ConferirLogin())
                return NotFound("Não permitido sem logar");

            var Produto = dbOperacoesProduto.SelecionarPorId(id);
            SalvarAuditoria(0, id, $"Produto Selecionado id {id}");
            return Ok(Produto);
        }

        [HttpGet]
        [Route("SelecionarTodos")]
        public ActionResult<List<Produto>> SelecionarTodos()
        {
            if (!ConferirLogin())
                return NotFound("Não permitido sem logar");

            var lista = dbOperacoesProduto.RetornarTudo();
            SalvarAuditoria(0, 0, $"Selecionado todos produtos");
            return Ok(lista);
        }
        
        [HttpGet]
        [Route("Ordenar")]
        public ActionResult<List<Produto>> OrdenarPreco()
        {
            if (!ConferirLogin())
                return NotFound("Não permitido sem logar");

            var lista = dbOperacoesProduto.RetornarTudo().OrderBy(p => p.Preco);
            SalvarAuditoria(0, 0, $"Produto Ordenado por preco ");
            return Ok(lista);
        }

        [HttpPut]
        [Route("Atualizar")]
        public ActionResult<List<Produto>> Atualizar([FromBody] Produto produto)
        {
            if (!ConferirLogin())
                return NotFound("Não permitido sem logar");
            var produtoAntigo = dbOperacoesProduto.SelecionarPorId(produto.Id);
            if (produtoAntigo == null)
                return NotFound("Não foi encontrado produto com respectivo id");
            produtoAntigo.Preco = produto.Preco;
            produtoAntigo.PalavrasChave = produto.PalavrasChave;
            produtoAntigo.IdCidade = produto.IdCidade;
            produtoAntigo.IdCliente = produto.IdCliente;
            produtoAntigo.Categoria= produto.Categoria;
            produtoAntigo.Nome = produto.Nome;
            
            dbOperacoesProduto.Editar(produtoAntigo);
            SalvarAuditoria(0, produtoAntigo.Id, $"Produto Atualizado id {produtoAntigo.Id}");
            return Ok("usuario atualizado com sucesso!");
        }
    }
}
