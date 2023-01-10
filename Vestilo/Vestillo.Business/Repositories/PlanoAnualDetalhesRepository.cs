using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class PlanoAnualDetalhesRepository : GenericRepository<PlanoAnualDetalhes>
    {
        public PlanoAnualDetalhesRepository()
            : base(new DapperConnection<PlanoAnualDetalhes>())
        {
        }
    }
}
