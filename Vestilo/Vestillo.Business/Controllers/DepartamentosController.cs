
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class DepartamentosController : GenericController<Departamentos, DepartamentosRepository>
    {
        public IEnumerable<Departamentos> GetByAtivos(int AtivoInativo)
        {
            using (DepartamentosRepository repository = new DepartamentosRepository())
            {
                return repository.GetByAtivos(AtivoInativo);
            }
        }

    }
}
