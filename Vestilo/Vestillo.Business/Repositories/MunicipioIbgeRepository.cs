using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class MunicipioIbgeRepository: GenericRepository<MunicipioIbge>
    {
        public MunicipioIbgeRepository()
            : base(new DapperConnection<MunicipioIbge>())
        {
        }

        public IEnumerable<MunicipioIbge> GetAllWhere(String where)
        {
            MunicipioIbge m = new MunicipioIbge();
            return _cn.ExecuteToList(m, "uf = '" + where + "'");
        }
    }
}
