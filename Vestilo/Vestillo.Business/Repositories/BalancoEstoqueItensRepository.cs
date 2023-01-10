using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Connection;
using Vestillo.Lib;

namespace Vestillo.Business.Repositories
{
    public class BalancoEstoqueItensRepository : GenericRepository<BalancoEstoqueItens>
    {
        public BalancoEstoqueItensRepository()
            : base(new DapperConnection<BalancoEstoqueItens>())
        {
        }

        public IEnumerable<BalancoEstoqueItensView> GetViewByBalanco(int idBalanco)
        {
            var SQL = new StringBuilder();

            SQL.AppendLine("SELECT	it.*, p.Id AS ProdutoId, p.Referencia AS ProdutoReferencia, p.Descricao AS ProdutoDescricao, ");
            SQL.AppendLine(" cat.descricao as CatalogoDescricao, col.descricao as ColecaoDescricao, B.ZerarEmpenho as EmpenhoDivergencia, ");
            SQL.AppendLine(" c.Abreviatura AS CorAbreviatura, t.Abreviatura AS TamanhoAbreviatura, IFNULL(e.Saldo, 0) AS Saldo, IFNULL(e.Empenhado, 0) AS Empenhado ");
            SQL.AppendLine("FROM 	BalancoEstoqueItens it");
            SQL.AppendLine(" INNER JOIN BalancoEstoque B ON B.Id = it.BalancoEstoqueId");
            SQL.AppendLine(" INNER JOIN Produtos P ON P.Id = it.ProdutoId");
            SQL.AppendLine(" LEFT JOIN catalogo cat ON it.CatalogoId = cat.id ");
            SQL.AppendLine(" LEFT JOIN colecoes col ON it.ColecaoId = col.id ");
            SQL.AppendLine(" LEFT JOIN cores c ON it.CorId = c.id ");
            SQL.AppendLine(" LEFT JOIN tamanhos t ON it.TamanhoId = t.id ");
            SQL.AppendLine(" LEFT JOIN estoque e ON e.produtoid = it.ProdutoId AND IFNULL(e.tamanhoid, 0) = IFNULL(it.TamanhoId,0) AND IFNULL(e.corid, 0) = IFNULL(it.CorId, 0) AND e.AlmoxarifadoId = B.AlmoxarifadoId");
            SQL.AppendLine("WHERE it.BalancoEstoqueId = " + idBalanco);

            var cn = new DapperConnection<BalancoEstoqueItensView>();
            return cn.ExecuteStringSqlToList(new BalancoEstoqueItensView(), SQL.ToString());
        }

        public void DeleteByBalanco(int balancoId)
        {
            string SQL = "DELETE FROM BalancoEstoqueItens WHERE BalancoEstoqueId = " + balancoId.ToString();
            _cn.ExecuteNonQuery(SQL);
        }
    }
}
