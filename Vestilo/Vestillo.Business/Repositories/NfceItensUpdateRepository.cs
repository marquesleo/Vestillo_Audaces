

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Lib;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class NfceItensUpdateRepository : GenericRepository<NfceItensUpdate>
    {
        public NfceItensUpdateRepository() : base(new DapperConnection<NfceItensUpdate>())
        {
        }
    }
}
