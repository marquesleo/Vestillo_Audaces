using Vestillo.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class OcorrenciaRepository : GenericRepository<Ocorrencia>
    {
        public OcorrenciaRepository()
            : base(new DapperConnection<Ocorrencia>())
        {
        }
    }
}
