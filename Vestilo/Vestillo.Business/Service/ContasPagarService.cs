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
    public class ContasPagarService : GenericService<ContasPagar, ContasPagarRepository, ContasPagarController>
    {
        public ContasPagarService()
        {
            base.RequestUri = "ContasPagar";
        }

        public new IContasPagarService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new ContasPagarServiceWeb(this.RequestUri);
            }
            else
            {
                return new ContasPagarServiceAPP();
            }
        }  
    }
}
