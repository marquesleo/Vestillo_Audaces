
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Core.Models;
using Vestillo.Core.Connection;

namespace Vestillo.Core.Repositories
{
    public class AtualizaLeitorDePrecoRepository : GenericRepository<AtualizaLeitorDePrecoView>
    {
        public IEnumerable<ItensDaTabelaDoLeitorView> ItensDaTabelaParaLeitor(int IdTabela)
        {
                       

            StringBuilder SQl = new StringBuilder();
            SQl.AppendLine("SELECT IFNULL(produtodetalhes.codbarras,'') as codbarras, CONCAT(produtos.Descricao,'-',cores.Abreviatura,'-',tamanhos.Abreviatura) as Descricao,itenstabelapreco.PrecoVenda  ");
            SQl.AppendLine(" FROM itenstabelapreco ");
            SQl.AppendLine(" INNER JOIN produtos on produtos.Id = itenstabelapreco.ProdutoId");
            SQl.AppendLine(" INNER JOIN produtodetalhes on produtodetalhes.IdProduto = produtos.Id");
            SQl.AppendLine(" INNER JOIN cores on cores.Id = produtodetalhes.Idcor ");
            SQl.AppendLine(" INNER JOIN tamanhos on tamanhos.Id = produtodetalhes.IdTamanho ");
            SQl.AppendLine(" WHERE itenstabelapreco.PrecoVenda > 0 AND itenstabelapreco.TabelaPrecoId = " + IdTabela + "  AND IFNULL(produtodetalhes.codbarras,'') <> ''  order by produtos.Descricao ");

            return VestilloConnection.ExecSQLToListWithNewConnection<ItensDaTabelaDoLeitorView>(SQl.ToString());
        }


        public AtualizaLeitorDePrecoView RegistroDoLeitor()
        {


            StringBuilder SQl = new StringBuilder();
            SQl.AppendLine(" SELECT atualizaleitordepreco.id,atualizaleitordepreco.idtabelaPreco,atualizaleitordepreco.IdUsuario,atualizaleitordepreco.DiretorioArquivo,");
            SQl.AppendLine(" tabelaspreco.Referencia as TabReferencia,tabelaspreco.Descricao as TabDescricao,usuarios.Nome as UsuarioNome,atualizaleitordepreco.DataCriacao ");
            SQl.AppendLine(" FROM atualizaleitordepreco");
            SQl.AppendLine(" INNER JOIN usuarios ON usuarios.Id = atualizaleitordepreco.IdUsuario ");
            SQl.AppendLine(" INNER JOIN tabelaspreco ON tabelaspreco.Id = atualizaleitordepreco.idtabelaPreco ");

            return VestilloConnection.ExecSQLToModel<AtualizaLeitorDePrecoView>(SQl.ToString());
        }


        public void UpdateRegistroLeitor(int IdUsuario, int IdTabela, string Diretorio)
        {
            string SQl = String.Empty;

            DateTime aux = DateTime.Now;
            string dia = aux.Day.ToString("d2");//duas casas, preenche com zero esquerda
            string mes = aux.Month.ToString("d2");
            string ano = aux.Year.ToString();
            string Hora = aux.Hour.ToString() +  ":" + aux.Minute.ToString() + ":" + aux.Second.ToString();

            Diretorio = Diretorio.ToString().Replace("\\", "|");

            SQl = " UPDATE AtualizaLeitorDePreco SET IdUsuario = " + IdUsuario + " ,DiretorioArquivo = " + "'" + Diretorio + "'" +  " ,idtabelaPreco = " + IdTabela +
                " ,DataCriacao = " + "'" + ano + "-"  + mes  + "-" + dia + " " + Hora + "'";

            VestilloConnection.ExecNonQuery(SQl);
        }

        public void DeletarDadosTabelaLeitor()
        {
            string SQL = String.Empty;

            SQL = " DELETE FROM BuscaPreco ";
            VestilloConnection.ExecNonQuery(SQL);

            SQL = "ALTER TABLE BuscaPreco AUTO_INCREMENT = 0";
            VestilloConnection.ExecNonQuery(SQL);
        }

        public void AtualizaDadosLeitor(List<BuscaPreco> ListaItens)
        {
            string SQL = String.Empty;

            foreach (var item in ListaItens)
            {
                SQL = $"INSERT INTO buscapreco (EAN, Item, Preco)  values (" +
                    $"'{item.EAN}','{item.Item}','{item.Preco.ToString().Replace(",",".")}')";
                VestilloConnection.ExecNonQuery(SQL);
                SQL = String.Empty;
            }

        }
    }
}
