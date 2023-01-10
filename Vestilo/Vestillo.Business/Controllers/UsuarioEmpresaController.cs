using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class UsuarioEmpresaController: GenericController<UsuarioEmpresa, UsuarioEmpresaRepository>
    {
        public IEnumerable<UsuarioEmpresa> GetByUsuarioId(int usuarioId)
        {
            using (UsuarioEmpresaRepository repository = new UsuarioEmpresaRepository())
            {
                return repository.GetByUsuarioId(usuarioId);
            }
        }
    }
}
