using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Controllers;
using Vestillo.Business.Repositories;
using Vestillo.Lib;

namespace Vestillo.Business.Service.Web
{
    public class UsuarioServiceWeb: GenericServiceWeb<Usuario, UsuarioRepository, UsuarioController>, IUsuarioService
    {

        public UsuarioServiceWeb(string requestUri) : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public Usuario GetByLogin(string login, ref IEnumerable<Empresa> empresas, ref IEnumerable<ModuloSistema> modulos)
        {
            var c = new ConnectionWebAPI<Usuario>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "login=" + login);
        }

        public IEnumerable<Usuario> GetVendedores()
        {
            throw new NotImplementedException();
        }
    }
}
