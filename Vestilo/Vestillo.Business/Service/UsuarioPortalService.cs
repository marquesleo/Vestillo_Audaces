
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
    public class UsuarioPortalService : GenericService<UsuarioPortal, UsuarioPortalRepository, UsuarioPortalController>
    {
        public UsuarioPortalService()
        {
            base.RequestUri = "UsuarioPortal";
        }

        public new IUsuarioPortalService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new UsuarioPortalServiceWeb(this.RequestUri);
            }
            else
            {
                return new UsuarioPortalServiceAPP();
            }
        }
    }
}
