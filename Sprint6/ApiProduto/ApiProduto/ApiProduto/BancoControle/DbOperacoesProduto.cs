using ApiProduto;
using System.Collections.Generic;
using System.Linq;

namespace Desafio6
{
    public class DbOperacoesProduto 
    {
        static ConexaoBd conexao = new ConexaoBd();
        DbOperacoesPalavraChave dbPalavra;
        public DbOperacoesProduto()
        {
            dbPalavra = new DbOperacoesPalavraChave();
        }

        public Produto Cadastrar(Produto obj)
        {
            conexao.Set<Produto>().Add(obj);
            conexao.SaveChanges();

            return conexao.Set<Produto>().ToList().Last<Produto>();
        }
         public void Deletar( int id)
         {
            var cliente = conexao.Set<Produto>().Find(id);
            conexao.Set<Produto>().Remove(cliente);
            
            conexao.SaveChanges();
         }

        public Produto SelecionarPorId(int id)
        {
            var palavras = dbPalavra.RetornarTudo().Where(e => e.IdProduto == id).Select(e => e.Nome).ToList();
            var produto = conexao.Set<Produto>().Where(e => e.Id == id);
            Produto saida = null;
            if(produto.Count()  == 1)
            {
                saida = produto.First();
                saida.PalavrasChave = palavras;
                return saida;
            }
            return saida;
        }

        public List<Produto> RetornarTudo()
        {
            var listaCompleta =  conexao.Set<Produto>().Where(e => true).ToList();
            var listaSaida = new List<Produto>();
            foreach (var item in listaCompleta)
            {
                item.PalavrasChave = SelecionarPorId(item.Id).PalavrasChave;
                listaSaida.Add(item);
            }

            return listaSaida;
        }

        public void Editar(Produto cidadeAtualizada)
        {
            conexao.Set<Produto>().Update(cidadeAtualizada);
            conexao.SaveChanges();
        }

    }
}
