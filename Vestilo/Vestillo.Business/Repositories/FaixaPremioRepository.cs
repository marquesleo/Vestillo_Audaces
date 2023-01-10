using Vestillo.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class FaixaPremioRepository: GenericRepository<FaixaPremio>
    {
        public FaixaPremioRepository()
            : base(new DapperConnection<FaixaPremio>())
        {
        }

        public IEnumerable<FaixaPremio> GetByPremio(int premioId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT *");
            SQL.AppendLine("FROM faixapremio");
            SQL.AppendLine("WHERE premioid = ");
            SQL.Append(premioId);

            var cn = new DapperConnection<FaixaPremio>();
            return cn.ExecuteStringSqlToList(new FaixaPremio(), SQL.ToString());
        }
    }
}
