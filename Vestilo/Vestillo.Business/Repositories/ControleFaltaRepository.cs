using Vestillo.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class ControleFaltaRepository : GenericRepository<ControleFalta>
    {
        public ControleFaltaRepository()
            : base(new DapperConnection<ControleFalta>())
        {
        }

        public IEnumerable<ControleFalta> GetByPremio(int premioId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT *");
            SQL.AppendLine("FROM controlefalta");
            SQL.AppendLine("WHERE premioid = ");
            SQL.Append(premioId);

            var cn = new DapperConnection<ControleFalta>();
            return cn.ExecuteStringSqlToList(new ControleFalta(), SQL.ToString());
        }
    }
}
