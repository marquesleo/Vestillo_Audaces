
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Core.Models;
using Vestillo.Core.Connection;

namespace Vestillo.Core.Repositories
{
    public class ProdutoFichaRepository : GenericRepository<ProdutoFicha>
    {
        public IEnumerable<ProdutoFichaView> ListByProdutoFichas(int ProdutoId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT 	MT.descricao as DescricaoMaterial");
            sql.AppendLine("		, MT.Referencia as RefMaterial");
            sql.AppendLine("		,GP.descricao AS DescricaoGrupo");
            sql.AppendLine("		,UM.abreviatura as DescricaoUnidade");
            sql.AppendLine("		,FC.*");
            sql.AppendLine("FROM 	ProdutoFicha FC");
            sql.AppendLine("	INNER JOIN produtos  MT  ON MT.Id = FC.MaterialId");
            sql.AppendLine("   INNER JOIN grupoprodutos GP  ON GP.Id = MT.IdGrupo");
            sql.AppendLine("   INNER JOIN unidademedidas UM  ON UM.Id = MT.IdUniMedida");
            sql.AppendLine("WHERE 	FC.ProdutoId = " + ProdutoId.ToString());
            sql.AppendLine("ORDER BY FC.numero ");

            return VestilloConnection.ExecSQLToListWithNewConnection<ProdutoFichaView>(sql.ToString());
        }

        public void AtualizaCustoItens(int IdMaterial,decimal preco)
        {
            string SQL = String.Empty;

            SQL = " SELECT * from produtoficha where produtoficha.MaterialId = " + IdMaterial;
            var dados = VestilloConnection.ExecSQLToListWithNewConnection<ProdutoFichaView>(SQL);

           
            foreach (var item in dados)
            {
                string SqlAtualiza = String.Empty;

                decimal valor = (item.quantidade * preco);
                decimal custo = (valor * item.custo) / 100;               
                decimal pFichaValor = custo;

                SqlAtualiza = "UPDATE produtoficha set preco = " + preco.ToString().Replace(",", ".") + " ,valor = " + pFichaValor.ToString().Replace(",",".") +
                " WHERE produtoficha.Id = " + item.Id ;
                VestilloConnection.ExecNonQuery(SqlAtualiza);

            }

        }

        public void AtualizaProduto(int IdProduto)
        {
            string SQL = String.Empty;

            SQL = " SELECT SUM(produtoficha.valor) as Valor from produtoficha where produtoficha.ProdutoId = " + IdProduto;
            var dados = VestilloConnection.ExecSQLToListWithNewConnection<ProdutoFichaView>(SQL);


            foreach (var item in dados)
            {
                string SqlAtualiza = String.Empty;

                decimal valor = (item.valor);
               

                SqlAtualiza = "UPDATE produtos set ValorMaterial = " + valor.ToString().Replace(",", ".")  +
                " WHERE produtos.Id = " + IdProduto;
                VestilloConnection.ExecNonQuery(SqlAtualiza);

            }
        }

    }
}
