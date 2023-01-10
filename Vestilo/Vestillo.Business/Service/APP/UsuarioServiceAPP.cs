using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service.APP
{
    public class UsuarioServiceAPP : GenericServiceAPP<Usuario, UsuarioRepository, UsuarioController>, IUsuarioService
    {

        public UsuarioServiceAPP(): base (new UsuarioController())
        {
        }

        public Usuario GetByLogin(string login, ref IEnumerable<Empresa> empresas, ref IEnumerable<ModuloSistema> modulos)
        {
            return controller.GetByLogin(login, ref empresas, ref modulos);
        }

        public IEnumerable<Usuario> GetVendedores()
        {
            return controller.GetVendedores();
        }
    }
}
