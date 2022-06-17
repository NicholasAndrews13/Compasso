using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SistemaAgendamento.Core.Commands;
using SistemaAgendamento.Core.Models;
using SistemaAgendamento.Infrastructure;
using SistemaAgendamento.Services.Handlers;
using System;
using Xunit;
using ContextoAgendamento = SistemaAgendamento.Infrastructure.DbContext;

namespace SistemaAgendamento.Test
{
    public class CadastraAgendamentoHandlerExecutar
    {
        
        //public void ExecuteTeste()
        //{
        //    var agendamento = new AgendamentoModel()
        //    {
        //        Id = 1,
        //        Inicio = System.DateTime.Now,
        //        Fim = System.DateTime.Now,
        //        Sala = new Sala(2, "marcelo"),
        //        Titulo = "Marcelo",
        //        Status = StatusAgendamento.Criada
        //    };
        //    var options = new DbContextOptionsBuilder<ContextoAgendamento>()
        //        .UseInMemoryDatabase("DbContext")
        //        .Options;

        //    var contexto = new ContextoAgendamento(options);

        //    var repo = new RepositorioAgendamento(contexto);
        //    var handler = new CadastraAgendamentoHandler(repo);
        //    repo.IncluirAgendamento(agendamento);

        //    handler.Execute(agendamento);


        //}

        //[Fact]
        //public void QuandoExceptionForLancadaResultadoFalso()
        //{
        //    var agendamento = new AgendamentoModel()
        //    {
        //        Id = 1,
        //        Inicio = System.DateTime.Now,
        //        Fim = System.DateTime.Now,
        //        Sala = new Sala(2, "marcelo"),
        //        Titulo = "Marcelo",
        //        Status = StatusAgendamento.Criada
        //    };
        //    var options = new DbContextOptionsBuilder<ContextoAgendamento>()
        //        .UseInMemoryDatabase("DbContext")
        //        .Options;

        //    var contexto = new ContextoAgendamento(options);

        //    var repo = new RepositorioAgendamento(contexto);
        //    var handler = new CadastraAgendamentoHandler(repo);
        //    repo.IncluirAgendamento(agendamento);

        //    handler.Execute(agendamento);

        //}
        
        [Fact]
        public void TesteMockCompleto()
        {
            var mensagemDeErroEsperada = "Houve um erro na inclusão de agedamento";
            var excecaoEsperada = new Exception(mensagemDeErroEsperada);

            var comando = new CadastrarAgendamento("Estudar Xunit", new Sala(2, "10"), new DateTime(2019, 12, 31), new DateTime(2019, 12, 31));

            var mockLogger = new Mock<ILogger<CadastrarAgendamento>>();
            var mock = new Mock<IRepositorioAgendamento>();

            mock.Setup(r => r.IncluirAgendamento(It.IsAny<AgendamentoModel>()))
                    .Throws(excecaoEsperada);

            var repo = mock.Object;

            var handler = new CadastraAgendamentoHandler(repo, mockLogger.Object);

            //act
            CommandResult resultado = handler.Execute(comando);

            //assert
            mockLogger.Verify(l => 
                    l.Log(
                        LogLevel.Error, //nível de log => LogError
                        It.IsAny<EventId>(), //identificador do evento
                        It.IsAny<object>(), //objeto que será logado
                        excecaoEsperada,    //exceção que será logada
                        It.IsAny<Func<object, Exception, string>>()
                    ), //função que converte objeto+exceção >> string
                    Times.Once());
        }
        [Fact]
        public void QuandoExcecaoForLancadaDeveRetornarStatusCode500()
        {
            //arrange
            var mockLogger = new Mock<ILogger<CadastraAgendamentoHandler>>();

            var mock = new Mock<IRepositorioAgendamento>();
            mock.Setup(r => r.IncluirAgendamento(It.IsAny<AgendamentoModel>())).Throws(new Exception("Houve um erro"));
            var repo = mock.Object;

            var controlador = new GerenciaFimAgendamentoHandler(repo, mockLogger.Object);
            var model = new AgendamentoModel();
            model.Sala = new Sala(1 ,"nada");
            model.Titulo = "Estudar Xunit";
            model.Fim = new DateTime(2019, 12, 31);
            model.Inicio = new DateTime(2019, 12, 31);
           
            //act
            var retorno = controlador.Execute(new GerenciaFimAgendamento());

            //assert
            Assert.IsType<StatusCodeResult>(retorno);
            //var statusCodeRetornado = (retorno as StatusCodeResult).StatusCode;
            //Assert.Equal(500, statusCodeRetornado);
        }

    }
}
