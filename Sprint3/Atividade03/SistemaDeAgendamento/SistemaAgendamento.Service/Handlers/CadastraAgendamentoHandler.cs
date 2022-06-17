using SistemaAgendamento.Core.Models;
using SistemaAgendamento.Core.Commands;
using SistemaAgendamento.Infrastructure;
using System;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using SistemaAgendamento.Test;

namespace SistemaAgendamento.Services.Handlers
{
    public class CadastraAgendamentoHandler
    {
        IRepositorioAgendamento _repo;
        AgendamentoModel _model;
        ILogger<CadastrarAgendamento> _logger;
        
        public CadastraAgendamentoHandler()
        {
            _repo = new RepositorioAgendamento();
        }
        public CadastraAgendamentoHandler(AgendamentoModel agendamento)
        {
            _model = agendamento;
        }

        public CadastraAgendamentoHandler(IRepositorioAgendamento repoAgendamento)
        {
            _repo = repoAgendamento;
        }
        public CadastraAgendamentoHandler(IRepositorioAgendamento repo, ILogger<CadastrarAgendamento> logger)
        {
            _repo = repo;
            _logger = logger;
        }
        public void Execute(AgendamentoModel agendamento)
        {
            _repo.IncluirAgendamento(agendamento);
        }
        public CommandResult Execute(CadastrarAgendamento comando)
        {
            try
            {
                var agendamento = new AgendamentoModel
                (
                    id: 0,
                    titulo: comando.Titulo,
                    sala: comando.Sala,
                    inicio: comando.Inicio,
                    fim: comando.Fim,
                    status: StatusAgendamento.Criada

                );
                _logger.LogDebug($"Persistindo o objeto {comando.Titulo}");
                _repo.IncluirAgendamento(agendamento);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new CommandResult(false);
            }
            return new CommandResult(true);

        }


    }
}
