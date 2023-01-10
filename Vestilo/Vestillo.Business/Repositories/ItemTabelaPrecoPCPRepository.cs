using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;
using Vestillo.Lib;

namespace Vestillo.Business.Repositories
{
    public class ItemTabelaPrecoPCPRepository: GenericRepository<ItemTabelaPrecoPCP>
    {
        public ItemTabelaPrecoPCPRepository()
            : base(new DapperConnection<ItemTabelaPrecoPCP>())
        {
        
        }

        public IEnumerable<ItemTabelaPrecoPCP> GetItensTabelaPreco(int tabelaPrecoId)
        {
            var SQL = new StringBuilder();

            SQL.AppendLine("SELECT	ITP.*, ");
            SQL.AppendLine("P.Descricao AS ProdutoDescricao, P.Referencia AS ProdutoReferencia ");
            SQL.AppendLine("FROM ItensTabelaPrecoPCP ITP");
            SQL.AppendLine("    INNER JOIN Produtos P ON P.Id  = ITP.ProdutoId");
            SQL.AppendLine("WHERE ITP.TabelaPrecoPCPId = ");
            SQL.Append(tabelaPrecoId);

            return _cn.ExecuteStringSqlToList(new ItemTabelaPrecoPCP(), SQL.ToString());
        }

        public ItemTabelaPrecoPCP GetItemTabelaPreco(int tabelaPrecoId, int produtoId)
        {
            var SQL = new StringBuilder();
            
            SQL.AppendLine("SELECT	ITP.*, ");
            SQL.AppendLine("P.Descricao AS ProdutoDescricao, P.Referencia AS ProdutoReferencia ");
            SQL.AppendLine("FROM ItensTabelaPrecoPCP ITP");
            SQL.AppendLine("    INNER JOIN Produtos P ON P.Id  = ITP.ProdutoId");
            SQL.AppendLine("WHERE ITP.TabelaPrecoPCPId = ");
            SQL.Append(tabelaPrecoId);
            SQL.AppendLine(" AND  ITP.ProdutoId = ");
            SQL.Append(produtoId);

            var ret = new ItemTabelaPrecoPCP();
            _cn.ExecuteToModel(ref ret, SQL.ToString());
            return ret;
        }

        public IEnumerable<ItemTabelaPrecoPCP> GetListByProduto(int produtoId)
        {
            var SQL = new StringBuilder();

            SQL.AppendLine("SELECT	ITP.* ");
            SQL.AppendLine("FROM ItensTabelaPrecoPCP ITP");
            SQL.AppendLine(" INNER JOIN TabelaPrecoPCP T ON T.Id = ITP.TabelaPrecoPCPId ");
            SQL.AppendLine("WHERE  " + FiltroEmpresa("T.EmpresaId") + " AND ITP.ProdutoId = ");
            SQL.Append(produtoId);
           
            return _cn.ExecuteStringSqlToList(new ItemTabelaPrecoPCP(), SQL.ToString());
        }

        public void DeleteByTabelaPreco(int tabelaPrecoId)
        {
            string SQL = "DELETE FROM ItensTabelaPrecoPCP WHERE TabelaPrecoPCPId = " + tabelaPrecoId.ToString();
            _cn.ExecuteNonQuery(SQL);
        }

        public IEnumerable<ItemTabelaPrecoPCP> GetAllByProduto(int produtoId)
        {
            var cn = new DapperConnection<ItemTabelaPrecoPCP>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	* from itenstabelaprecopcp ");            
            SQL.AppendLine(" WHERE ");            
            SQL.AppendLine(" ProdutoId = " + produtoId);            
            SQL.AppendLine(" limit 1 ");            

            return cn.ExecuteStringSqlToList(new ItemTabelaPrecoPCP(), SQL.ToString());
        }
    }
}
