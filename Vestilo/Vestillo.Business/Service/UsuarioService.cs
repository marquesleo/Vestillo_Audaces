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
    public class UsuarioService: GenericService<Usuario, UsuarioRepository, UsuarioController>
    {
        public UsuarioService()
        {
            base.RequestUri = "Usuario";
        }

        public new IUsuarioService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new UsuarioServiceWeb(this.RequestUri);
            }
            else
            {
                return new UsuarioServiceAPP();
            }
        }   
    }
}
