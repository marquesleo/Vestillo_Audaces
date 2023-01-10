
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class PlanejamentoSemanalController : GenericController<PlanejamentoSemanal, PlanejamentoSemanalRepository>
    {
        public IEnumerable<PlanejamentoSemanal> GetByRefView(string referencia)
        {
            using (var repository = new PlanejamentoSemanalRepository())
            {
                return repository.GetByRefView(referencia);
            }
        }
    }

   
}
