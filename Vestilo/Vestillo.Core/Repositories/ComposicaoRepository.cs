
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Core.Models;
using Vestillo.Core.Connection;

namespace Vestillo.Core.Repositories
{
    public class ComposicaoRepository : GenericRepository<Composicao>
    {
        public IEnumerable<ComposicaoView> ListByProdutoETipoComposicao(int ProdutoId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT 	UI.Nome AS NomeUsuarioCriacao");
            sql.AppendLine("		,T.Descricao AS DescricaoTipoArtigo");          
            sql.AppendLine("		,A.*");
            sql.AppendLine("FROM 	EtqComposicao A");
            sql.AppendLine("	INNER JOIN Usuarios     UI  ON UI.Id = A.UsuarioCriacaoAlteracaoId");
            sql.AppendLine("    INNER JOIN TipoArtigo T  ON T.Id = A.TipoArtigoId	");                      
            sql.AppendLine("WHERE 	A.ProdutoId = " + ProdutoId.ToString());           
            sql.AppendLine("ORDER BY A.Numero ");

            return VestilloConnection.ExecSQLToListWithNewConnection<ComposicaoView>(sql.ToString());
        }

    }
}
