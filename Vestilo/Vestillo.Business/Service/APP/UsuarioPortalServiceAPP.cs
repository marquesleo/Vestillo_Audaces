
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
    public class UsuarioPortalServiceAPP : GenericServiceAPP<UsuarioPortal, UsuarioPortalRepository, UsuarioPortalController>, IUsuarioPortalService
    {

        public UsuarioPortalServiceAPP() : base(new UsuarioPortalController())
        {
        }               

        public IEnumerable<UsuarioPortal> GetVendedores()
        {
            return controller.GetVendedores();
        }
    }
}
