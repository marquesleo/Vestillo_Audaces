
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class BalanceamentoSemanalRepository : GenericRepository<BalanceamentoSemanal>
    {
        public BalanceamentoSemanalRepository() : base(new DapperConnection<BalanceamentoSemanal>())
        {
        }
    }
}
