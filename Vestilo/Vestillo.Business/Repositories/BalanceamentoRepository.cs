using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class BalanceamentoRepository : GenericRepository<Balanceamento>
    {
        public BalanceamentoRepository()
            : base(new DapperConnection<Balanceamento>())
        {
        }
    }
}
