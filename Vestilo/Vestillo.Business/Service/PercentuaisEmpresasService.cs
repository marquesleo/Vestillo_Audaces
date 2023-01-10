
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Controllers;
using Vestillo.Business.Service.APP;
using Vestillo.Business.Service.Web;

namespace Vestillo.Business.Service
{
    public class PercentuaisEmpresasService: GenericService<Cargo, CargoRepository, CargoController>
    {
        public PercentuaisEmpresasService()
        {
            base.RequestUri = "PercentuaisEmpresas";
        }

        public new IPercentuaisEmpresasService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new PercentuaisEmpresasWeb(this.RequestUri);
            }
            else
            {
                return new PercentuaisEmpresasAPP();
            }
        }   

    }
}
