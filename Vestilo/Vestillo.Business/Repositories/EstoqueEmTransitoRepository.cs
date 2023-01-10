
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class EstoqueEmTransitoRepository: GenericRepository<EstoqueEmTransito>
    {
        public EstoqueEmTransitoRepository()
            : base(new DapperConnection<EstoqueEmTransito>())
        {
        }
    }
}
