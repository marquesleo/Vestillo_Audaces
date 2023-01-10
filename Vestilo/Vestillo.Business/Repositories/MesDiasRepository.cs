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
    public class MesDiasRepository: GenericRepository<MesDias>
    {
        public MesDiasRepository()
            : base(new DapperConnection<MesDias>())
        {
        }

        public List<MesDias> GetByPremio(int premio)
        {
            var SQL = new Select()
                 .Campos("* ")
                 .From("mesdias ")
                 .Where(" premioId = " + premio);

            var cr = new MesDias();
            return _cn.ExecuteStringSqlToList(cr, SQL.ToString()).ToList();
        }
    }
}
