using System.Collections.Generic;
using System.Linq;

namespace Auditoria.Controllers
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

        public void Editar(TEntity entidade)
        {
            conexao.Set<TEntity>().Update(entidade);
            conexao.SaveChanges();
        }

    }
}
