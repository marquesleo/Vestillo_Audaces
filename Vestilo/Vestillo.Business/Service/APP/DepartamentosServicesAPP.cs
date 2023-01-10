
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service.APP
{
    public class DepartamentosServicesAPP : GenericServiceAPP<Departamentos, DepartamentosRepository, DepartamentosController>, IDepartamentosService
    {

        public DepartamentosServicesAPP() : base(new DepartamentosController())
        {
        }

        public IEnumerable<Departamentos> GetByAtivos(int AtivoInativo)
        {
            return controller.GetByAtivos(AtivoInativo);
        }
    }
}



