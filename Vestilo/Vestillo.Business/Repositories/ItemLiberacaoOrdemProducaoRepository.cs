using Vestillo.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class ItemLiberacaoOrdemProducaoRepository: GenericRepository<ItemLiberacaoOrdemProducao>
    {
        public ItemLiberacaoOrdemProducaoRepository()
            : base(new DapperConnection<ItemLiberacaoOrdemProducao>())
        {
        }

        public IEnumerable<ItemLiberacaoOrdemProducaoView> GetByOrdemIdView(int ordemId)
        {
            var cn = new DapperConnection<ItemLiberacaoOrdemProducaoView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT I.*,");
            SQL.AppendLine("P.Referencia AS ProdutoReferencia,");
            SQL.AppendLine("P.Descricao AS ProdutoDescricao,");
            SQL.AppendLine("T.Abreviatura AS TamanhoDescricao,");
            SQL.AppendLine("C.Abreviatura AS CorDescricao");
            SQL.AppendLine("FROM 	itemliberacaoordemproducao I");
            SQL.AppendLine("INNER JOIN produtos P ON P.Id = I.ProdutoId	");
            SQL.AppendLine("LEFT JOIN tamanhos T ON T.Id = I.TamanhoId ");
            SQL.AppendLine("LEFT JOIN cores C ON C.Id = I.CorId ");
            SQL.AppendLine("WHERE	I.OrdemProducaoId = ");
            SQL.Append(ordemId);

            return cn.ExecuteStringSqlToList(new ItemLiberacaoOrdemProducaoView(), SQL.ToString());
        }

        public void DeletaLiberados(int idOrdem)
        {
            var cn = new DapperConnection<ItemLiberacaoOrdemProducaoView>();
            string SQL = String.Empty;
            SQL = "DELETE from itemliberacaoordemproducao WHERE itemliberacaoordemproducao.OrdemProducaoId = " + idOrdem;
            cn.ExecuteNonQuery(SQL);

            SQL = String.Empty;
            SQL = "DELETE from ordemproducaomateriais WHERE ordemproducaomateriais.ordemproducaoid = " + idOrdem;
            cn.ExecuteNonQuery(SQL);

        }

        public void DeletaMaterialLiberado(int idOrdem)
        {
            var cn = new DapperConnection<ItemLiberacaoOrdemProducaoView>();
            string SQL = String.Empty;
           
            SQL = String.Empty;
            SQL = "DELETE from ordemproducaomateriais WHERE ordemproducaomateriais.ordemproducaoid = " + idOrdem;
            cn.ExecuteNonQuery(SQL);

        }
    }
}
