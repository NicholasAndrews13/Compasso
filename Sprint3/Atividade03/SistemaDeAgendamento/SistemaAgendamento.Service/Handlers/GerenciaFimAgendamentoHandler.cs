using System;
using System.Linq;
using SistemaAgendamento.Core.Commands;
using SistemaAgendamento.Core.Models;
using SistemaAgendamento.Infrastructure;
using SistemaAgendamento.Test;

namespace SistemaAgendamento.Services.Handlers
{
    public class GerenciaFimAgendamentoHandler
    {
        IRepositorioAgendamento _repo;

        public GerenciaFimAgendamentoHandler(IRepositorioAgendamento repo, Microsoft.Extensions.Logging.ILogger<CadastraAgendamentoHandler> @object)
        {
            _repo = new RepositorioAgendamento();
        }

        public CommandResult Execute(GerenciaFimAgendamento comando)
        {
            try
            {
                var agora = comando.DataHoraAtual;

                //pegar todas as tarefas não concluídas que passaram do prazo
                var tarefas = _repo
                    .ObtemAgendamentos(t => t.Fim <= agora && t.Status != StatusAgendamento.Concluida)
                    .ToList();

                //atualizá-las com status Atrasada
                tarefas.ForEach(t => t.Status = StatusAgendamento.Cancelada);

                //salvar tarefas
                _repo.AtualizarAgendamentos(tarefas.ToArray());
                return new CommandResult(true);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
                return new CommandResult(false);
            }
        }
    }
}
