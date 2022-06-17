using ApiProduto;
using System.Collections.Generic;
using System.Linq;

namespace Desafio6
{
    public class DbOperacoesPalavraChave
    {
        static ConexaoBd conexao = new ConexaoBd();
        public DbOperacoesPalavraChave()
        {
        }

        public PalavraChave Cadastrar(PalavraChave obj)
        {
            conexao.Set<PalavraChave>().Add(obj);
            conexao.SaveChanges();

            return conexao.Set<PalavraChave>().ToList().Last<PalavraChave>();
        }
         public void Deletar( int id)
         {
            var cliente = conexao.Set<PalavraChave>().Find(id);
            conexao.Set<PalavraChave>().Remove(cliente);
            
            conexao.SaveChanges();
         }

        public PalavraChave SelecionarPorId(int id)
        {
            var cidade = conexao.Set<PalavraChave>().Where(e => e.Id == id);
            var saida = new PalavraChave();
            if (cidade.Count() > 0)
                saida = cidade.First();
            else
                saida = null;
            return saida;
        }

        public List<PalavraChave> RetornarTudo()
        {
            return conexao.Set<PalavraChave>().Where(e => true).ToList();
        }

        public void Editar(PalavraChave cidadeAtualizada)
        {
            conexao.Set<PalavraChave>().Update(cidadeAtualizada);
            conexao.SaveChanges();
        }

    }
}
