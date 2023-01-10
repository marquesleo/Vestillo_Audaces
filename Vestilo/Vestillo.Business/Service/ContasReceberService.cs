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
    public class ContasReceberService : GenericService<ContasReceber, ContasReceberRepository, ContasReceberController>
    {
        public ContasReceberService()
        {
            base.RequestUri = "ContasReceber";
        }

        public new IContasReceberService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new ContasReceberServiceWeb(this.RequestUri);
            }
            else
            {
                return new ContasReceberServiceAPP();
            }
        }  
    }
}
