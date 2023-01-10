using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service
{
    public interface IUsuarioModulosSistemaService : IService<UsuarioModulosSistema, UsuarioModulosSistemaRepository, UsuarioModulosSistemaController>
    {
        void UpdateModuloPadraoUsuario(int usuarioId, int moduloId);
    }
}
