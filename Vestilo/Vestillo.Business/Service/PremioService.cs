using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.APP;
using Vestillo.Business.Service.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Service
{
    public class PremioService : GenericService<Premio, PremioRepository, PremioController>
    {
        public PremioService()
        {
            base.RequestUri = "Premio";
        }

        public new IPremioService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new PremioServiceWeb(this.RequestUri);
            }
            else
            {
                return new PremioServiceAPP();
            }
        }   
    }
}
