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
    public class CfopService: GenericService<Cfop, CfopRepository, CfopController>
    {
        public CfopService()
        {
            base.RequestUri = "Cfop";
        }

        public new ICfopService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new CfopServiceWeb(this.RequestUri);
            }
            else
            {
                return new CfopServiceAPP();
            }
        }  
    }
}
