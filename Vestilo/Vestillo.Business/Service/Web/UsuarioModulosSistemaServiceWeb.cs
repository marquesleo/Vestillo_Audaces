using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.Interface;
using Vestillo.Lib;

namespace Vestillo.Business.Service.Web
{
    public class UsuarioModulosSistemaServiceWeb : GenericServiceWeb<UsuarioModulosSistema, UsuarioModulosSistemaRepository, UsuarioModulosSistemaController>, IUsuarioModulosSistemaService
    {
        public UsuarioModulosSistemaServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public void UpdateModuloPadraoUsuario(int usuarioId, int moduloId)
        {
            throw new NotImplementedException();
        }
    }

}