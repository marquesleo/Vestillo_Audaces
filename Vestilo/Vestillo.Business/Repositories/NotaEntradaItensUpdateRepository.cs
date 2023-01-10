
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
    public class NotaEntradaItensUpdateRepository : GenericRepository<NotaEntradaItensUpdate>
    {
        public NotaEntradaItensUpdateRepository() : base(new DapperConnection<NotaEntradaItensUpdate>())
        {
        }
    }
}
