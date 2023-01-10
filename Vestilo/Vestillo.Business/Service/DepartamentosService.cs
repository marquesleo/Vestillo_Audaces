
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.APP;
using Vestillo.Business.Service.Web;

namespace Vestillo.Business.Service
{
    public class DepartamentosService : GenericService<Departamentos, DepartamentosRepository, DepartamentosController>
    {
        public DepartamentosService()
        {
            base.RequestUri = "Departamentos";
        }

        public new IDepartamentosService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new DepartamentosServiceWeb(this.RequestUri);
            }
            else
            {
                return new DepartamentosServicesAPP();
            }
        }   
    }
}
