using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class GruposProducaoRepository : GenericRepository<GruposProducao>
    {
        public GruposProducaoRepository() : base(new DapperConnection<GruposProducao>())
        {
        }
    }
}
