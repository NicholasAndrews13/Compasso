using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Auditoria.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuditoriaControle : ControllerBase
    {
        DbOperacoes<Auditoria> dbOperacoesAuditoria;
        DbOperacoes<Login> dbOperacoesLogin;
        Login login;
        public AuditoriaControle()
        {
            dbOperacoesAuditoria = new DbOperacoes<Auditoria>();
            dbOperacoesLogin = new DbOperacoes<Login>();
            var lista = dbOperacoesLogin.RetornarTudo().Where(l => l.LoginAtual == true);
            if (lista.Count() == 1)
                login = lista.First();
            else
                ConferirLogin();

        }

        public ActionResult ConferirLogin()
        {
            if (login == null)
                return NotFound("Você deve logar antes de usar o sistema");
            else
                return Ok();
        }

        [HttpPost]
        [Route("Cadastrar")]
        public ActionResult Cadastrar([FromBody] Auditoria auditoria)
        {
            if(auditoria.Data == System.DateTime.MinValue)
                auditoria.Data = System.DateTime.Now;
            auditoria.UsuarioId = login.Id;
            dbOperacoesAuditoria.Cadastrar(auditoria);

            return Ok();
        }

        [HttpDelete]
        [Route("Deletar/{id}")]
        public IActionResult Deletar([FromRoute] int id)
        {
            dbOperacoesAuditoria.Deletar(id);
            return Ok();
        }

        [HttpGet]
        [Route("Selecionar/{id}")]
        public ActionResult<Auditoria> Selecionar([FromRoute] int id)
        {
            var auditoria = dbOperacoesAuditoria.SelecionarPorId(id);
            return Ok(auditoria);
        }

        [HttpGet]
        [Route("SelecionarTodos")]
        public ActionResult<List<Auditoria>> SelecionarTodos()
        {
            var lista = dbOperacoesAuditoria.RetornarTudo();
            return Ok(lista);
        }
    }
}
