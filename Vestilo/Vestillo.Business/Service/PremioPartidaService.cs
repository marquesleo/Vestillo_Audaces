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
    public class PremioPartidaService : GenericService<PremioPartida, PremioPartidaRepository, PremioPartidaController>
    {
        public PremioPartidaService()
        {
            base.RequestUri = "PremioPartida";
        }

        public new IPremioPartidaService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new PremioPartidaServiceWeb(this.RequestUri);
            }
            else
            {
                return new PremioPartidaServiceAPP();
            }
        }
    }
}
