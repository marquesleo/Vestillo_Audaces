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
    public class BorderoCobrancaRepository: GenericRepository<BorderoCobranca>
    {
        public BorderoCobrancaRepository()
            : base(new DapperConnection<BorderoCobranca>())
        {

        }

        public IEnumerable<BorderoCobranca>  GetByDocumento(int documentoId, bool isCheque)
        {
            StringBuilder SQL = new StringBuilder();

            SQL.AppendLine("SELECT  DISTINCT B.* ");
            SQL.AppendLine("FROM 	BorderoCobranca B");
            SQL.AppendLine("	INNER JOIN borderocobrancadocumentos BD ON BD.BorderoCobrancaId = B.Id");

            if (isCheque)
            {
                SQL.AppendLine("WHERE BD.ChequeId = " + documentoId.ToString());
            }
            else
            {
                SQL.AppendLine("WHERE BD.ContasReceberId = " + documentoId.ToString());
            }

            return _cn.ExecuteStringSqlToList(new BorderoCobranca(), SQL.ToString());
        }

        public void BaixarEstornarBordero(BorderoCobranca bordero, bool estornar = false)
        {
            string SQL = "UPDATE BorderoCobranca SET DataAcerto = @DataAcerto, Status = " + (estornar ? "1" : "2") + " WHERE Id = @Id";
            _cn.ExecuteUpdate(bordero, SQL);
        }
    }
}
