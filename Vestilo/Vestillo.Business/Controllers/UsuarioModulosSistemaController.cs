using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class UsuarioModulosSistemaController : GenericController<UsuarioModulosSistema, UsuarioModulosSistemaRepository>
    {
        public void UpdateModuloPadraoUsuario(int usuarioId, int moduloId)
        {
            using (UsuarioModulosSistemaRepository repository = new UsuarioModulosSistemaRepository())
            {
                repository.UpdateModuloPadraoUsuario(usuarioId, moduloId);
            }
        }
    }
}
