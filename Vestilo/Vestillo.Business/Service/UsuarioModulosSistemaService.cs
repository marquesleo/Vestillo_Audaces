using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.APP;
using Vestillo.Business.Service.Interface;
using Vestillo.Business.Service.Web;

namespace Vestillo.Business.Service
{
    public class UsuarioModulosSistemaService : GenericService<UsuarioModulosSistema, UsuarioModulosSistemaRepository, UsuarioModulosSistemaController>
    {
        public UsuarioModulosSistemaService()
        {
            base.RequestUri = "UsuarioModulosSistema";
        }

        public new IUsuarioModulosSistemaService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new UsuarioModulosSistemaServiceWeb(this.RequestUri);
            }
            else
            {
                return new UsuarioModulosSistemaServiceAPP();
            }
        }  
    }
}
