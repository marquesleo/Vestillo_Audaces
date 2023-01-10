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
    public class ContadorCodigoService: GenericService<ContadorCodigo, ContadorCodigoRepository, ContadorCodigoController>
    {
        public ContadorCodigoService()
        {
            base.RequestUri = "ContadorCodigo";
        }

        public new IContadorCodigoService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new ContadorCodigoServiceWeb(this.RequestUri);
            }
            else
            {
                return new ContadorCodigoServiceAPP();
            }
        }   
    }
}
