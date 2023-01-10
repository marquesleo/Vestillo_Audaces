
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
    public class ContadorNossoNumeroService : GenericService<ContadorNossoNumero, ContadorNossoNumeroRepository, ContadorNossoNumeroController>
    {
        public ContadorNossoNumeroService()
        {
            base.RequestUri = "ContadorNossoNumero";
        }

        public new IContadorNossoNumeroService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new ContadorNossoNumeroServiceWeb(this.RequestUri);
            }
            else
            {
                return new ContadorNossoNumeroServiceAPP();
            }
        }
    }
}
