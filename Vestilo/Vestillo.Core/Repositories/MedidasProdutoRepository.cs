
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Core.Models;
using Vestillo.Core.Connection;

namespace Vestillo.Core.Repositories
{
    public class MedidasProdutoRepository : GenericRepository<MedidasProduto>
    {
        public IEnumerable<MedidasProdutoView> ListByProdutoMedidas(int ProdutoId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT 	TM.descricao as DescricaoTamanho");
            sql.AppendLine("		,D.Descricao AS DescricaoMedida");
            sql.AppendLine("		,M.*");
            sql.AppendLine("FROM 	MedidasProduto M");
            sql.AppendLine("	INNER JOIN Tamanhos   TM  ON TM.Id = M.TamanhoId");
            sql.AppendLine("    INNER JOIN descricaomedida D  ON D.Id = M.DescricaoMedidaId	");
            sql.AppendLine("WHERE 	M.ProdutoId = " + ProdutoId.ToString());
            sql.AppendLine("ORDER BY M.Id ");

            return VestilloConnection.ExecSQLToListWithNewConnection<MedidasProdutoView>(sql.ToString());
        }

    }
}
