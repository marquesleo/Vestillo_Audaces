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
    public class MunicipioIbgeService : GenericService<MunicipioIbge, MunicipioIbgeRepository, MunicipioIbgeController>
    {
        public MunicipioIbgeService()
        {
            base.RequestUri = "MunicipioIbge";
        }

        public new IMunicipioService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new MunicipioIbgeServiceWeb(this.RequestUri);
            }
            else
            {
                return new MunicipioIbgeServiceAPP();
            }
        }  
    }
}
