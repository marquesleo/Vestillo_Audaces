using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Lib;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service.Web
{
    public class LayoutGridServiceWeb : GenericServiceWeb<LayoutGrid, LayoutGridRepository, LayoutGridController>, ILayoutGridService
    {
        public LayoutGridServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public LayoutGrid GetByLayout(int formId, int usuarioId)
        {
            var c = new ConnectionWebAPI<LayoutGrid>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "formId=" + formId.ToString() + "&usuarioId=" + usuarioId.ToString());
        }

        public IEnumerable<LayoutGrid> GetListByUsuarioId(int usuarioId)
        {
            var c = new ConnectionWebAPI<LayoutGrid>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri + @"?usuarioId=" + usuarioId.ToString());
        }
    }
}
