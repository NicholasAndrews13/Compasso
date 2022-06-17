using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Auditoria.Controllers
{
    [Route("[controller]")]
    public class LoginControle : ControllerBase
    {
        DbOperacoes<Login> bdLogin;
        DbOperacoes<Auditoria> bdAuditoria;
        Login login;
        public LoginControle()
        {
            bdLogin = new DbOperacoes<Login>();
            bdAuditoria = new DbOperacoes<Auditoria>();
        }

        public bool ConferirLogin()
        {
            SalvarAuditoria("Usuario acessou o sistema pela auditoria");

            var loginresposnse = bdLogin.RetornarTudo().Where(l => l.LoginAtual == true);
            if (loginresposnse.Count() == 1 && login == null)
            {
                login = loginresposnse.First();
                SalvarAuditoria("Usuario Logado com sucesso");
            }

            if (login == null)
            {
                SalvarAuditoria("Usuario acessou sistema sem logar");
                return false;
            }

            return true;
            
        }
        public void SalvarAuditoria(string mensagemEvento)
        {
            if (login == null)
               bdAuditoria.Cadastrar(
               new Auditoria()
               {
                   Evento = mensagemEvento,
                   UsuarioId = login == null ?  0 : login.Id,
                   Data = DateTime.Now
               });
        }
        public void DeslogarTodos()
        {
            var lista = bdLogin.RetornarTudo().FindAll(l => l.LoginAtual == true);
            foreach (var item in lista)
            {
                item.LoginAtual = false;
                bdLogin.Editar(item);
            }
        }

        [HttpPost]
        [Route("Logar")]
        public ActionResult Logar([FromBody] Login login)
        {
            DeslogarTodos();
            var resposta = bdLogin.RetornarTudo().Where(l => l.Usuario == login.Usuario && login.Password == l.Password);

            if (resposta.Count() > 0)
            { 
                login = resposta.First();
                login.LoginAtual = true;
            }
            else
                return NotFound("Login Não encontrado!");
            

            bdLogin.Editar(login);
            bdAuditoria.Cadastrar(
            new Auditoria()
                {
                    ClienteId = 0,
                    ProdutoId = 0,
                    Evento = "Usuario Logou",
                    UsuarioId = login.Id,
                    Data = DateTime.Now
                }
            );
            return Ok("Usuario logado com sucesso!");
        }

        [HttpPost]
        [Route("Cadastrar")]
        public ActionResult CadastrarLogin([FromBody] Login login)
        {
            login.LoginAtual = false;
            bdLogin.Cadastrar(login);
            SalvarAuditoria("usuario cadastrado");
            return Ok("Usuario cadastrado com sucesso");
        }
        
        [HttpPost]
        [Route("Deslogar")]
        public ActionResult Deslogar()
        {
            if (!ConferirLogin())
            {
                SalvarAuditoria("Alguém falhou ao tentar deslogar");
                return NotFound("Usuario não logado");
            }
            else
                login = bdLogin.RetornarTudo().Where(l => l.LoginAtual == true).First();
            login.LoginAtual = false;
            bdLogin.Editar(login);
            SalvarAuditoria("Usuario Deslogou");
            return Ok("Usuario deslogado");
        }
        
        [HttpDelete]
        [Route("Deletar/{id}")]
        public IActionResult Deletar([FromRoute] int id)
        {
            if (!ConferirLogin())
            {
                SalvarAuditoria("Alguém falhou ao deletar Login");
                return NotFound("Usuario não logado");
            }

            bdLogin.Deletar(id);
            SalvarAuditoria("usuario deletado");
            return Ok("Usuario deletado");
        }

        [HttpGet]
        [Route("Selecionar/{id}")]
        public ActionResult<Login> Selecionar([FromRoute] int id)
        {
            if (!ConferirLogin())
            {
                SalvarAuditoria("Alguém falhou ao selecionar login");
                return NotFound("Usuario não logado");
            }
            var login = bdLogin.SelecionarPorId(id);
            SalvarAuditoria("usuario cadastrado");
            return Ok(login);
        }

        [HttpGet]
        [Route("SelecionarTodos")]
        public ActionResult<List<Login>> SelecionarTodos()
        {
            var lista = new List<Login>();
            if (!ConferirLogin())
            {
                SalvarAuditoria("Alguém falhou ao selecionar todos logins");
                return NotFound(lista);
            }
            
            lista = bdLogin.RetornarTudo();
            SalvarAuditoria("Usuario selecionou todos logins");
            return Ok(lista);
        }

        [HttpPut]
        [Route("Atualizar")]
        public ActionResult AtualizarEntidade([FromBody] Login loginBody)
        {
            if (!ConferirLogin())
            {
                SalvarAuditoria("Alguém tentou atualizar login");
                return NotFound("Usuario não logado");
            }
            var retorno = bdLogin.SelecionarPorId(loginBody.Id);
            if (retorno == null)
                return NotFound("Não foi encontrado login com id selecionado!");
            if (retorno.Id == login.Id)
            {
                login.Usuario = loginBody.Usuario;
                login.Password = loginBody.Password;
                bdLogin.Editar(login);
                SalvarAuditoria("Usuario atualizou login");
                return Ok("Usuario atualizado com sucesso!");
            }

            if (loginBody.Id == 0)
                return NotFound("Usuario não possuia id");
            bdLogin.Editar(loginBody);
            SalvarAuditoria("Usuario atualizou login");
            return Ok("Usuario atualizado com sucesso!");
        }
    }
}
