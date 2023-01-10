using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class HistoricoChequeRepository : GenericRepository<HistoricoCheque>
    {
        public HistoricoChequeRepository()
            : base(new DapperConnection<HistoricoCheque>())
        {
        }

        public IEnumerable<HistoricoChequeView> GetByCheque(int chequeId)
        {
          var SQL = new StringBuilder();
            SQL.AppendLine("SELECT H.*, U.Nome AS NomeUsuario");
            SQL.AppendLine("FROM HistoricoCheque H");
            SQL.AppendLine("INNER JOIN Usuarios U ON U.Id = H.UsuarioId");
            SQL.AppendLine("WHERE ChequeId = " + chequeId.ToString());
            SQL.AppendLine("ORDER BY Data Desc");
        

            var cn = new DapperConnection<HistoricoChequeView>();
            return cn.ExecuteStringSqlToList(new HistoricoChequeView(), SQL.ToString());
        
        }

    }
}