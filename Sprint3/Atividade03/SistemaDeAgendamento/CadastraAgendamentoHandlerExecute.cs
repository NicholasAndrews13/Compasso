using System;

namespace
{

    public class CadastraAgendamentoHandlerExecute
    {
        public CadastraAgendamentoHandlerExecute()
        {

            public void ExecuteTeste()
            {
                var options = new DbContextOptionsBuilder<DbContext>()
                    .UseInMemoryDatabase("DbContext")
                    .Options;

                var contexto = new DbContext(options);

                var repo = new RepositorioAgendamento(contexto);

                repo


            }
        }
    }
}