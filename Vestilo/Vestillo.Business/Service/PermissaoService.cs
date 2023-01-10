using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Controllers;

namespace Vestillo.Business.Service
{
    public class PermissaoService: GenericService<Permissao, PermissaoRepository, PermissaoController>
    {
        public PermissaoService()
        {
            base.RequestUri = "Permissao";
        }

        public new IPermissaoService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new Web.PermissaoServiceWeb(this.RequestUri);
            }
            else
            {
                return new APP.PermissaoServiceAPP();
            }
        }
    }
}
