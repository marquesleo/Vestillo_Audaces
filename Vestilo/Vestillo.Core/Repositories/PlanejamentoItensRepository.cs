
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Core.Models;
using Vestillo.Core.Connection;

namespace Vestillo.Core.Repositories
{
    public class PlanejamentoItensRepository : GenericRepository<PlanejamentoItens>
    {
        public IEnumerable<PlanejamentoItens> ListBySemanas(int PlanejamentoId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT  * FROM planejamentoitens WHERE planejamentoitens.PlanejamentoId = " + PlanejamentoId.ToString());
            sql.AppendLine("ORDER BY planejamentoitens.id ");

            return VestilloConnection.ExecSQLToListWithNewConnection<PlanejamentoItens>(sql.ToString());
        }

    }
}
