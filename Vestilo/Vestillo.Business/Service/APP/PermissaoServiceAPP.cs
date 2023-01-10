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
    public class PermissaoServiceAPP : GenericServiceAPP<Permissao, PermissaoRepository, PermissaoController>, IPermissaoService
    {

        public PermissaoServiceAPP(): base (new PermissaoController())
        {
        }

        public  IEnumerable<Permissao> GetByGrupos(string grupos)
        {
            return controller.GetByGrupos(grupos);
        }
    }
}
