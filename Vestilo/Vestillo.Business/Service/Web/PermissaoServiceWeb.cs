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
    public class PermissaoServiceWeb: GenericServiceWeb<Permissao, PermissaoRepository, PermissaoController>, IPermissaoService
    {

        public PermissaoServiceWeb(string requestUri) : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<Permissao> GetByGrupos(string grupos)
        {
            var c = new ConnectionWebAPI<Permissao>(VestilloSession.UrlWebAPI);
            var lstGrupos = new List<Permissao>();
            return c.Get(this.RequestUri + "?grupos=" + grupos);
        }
    }
}
