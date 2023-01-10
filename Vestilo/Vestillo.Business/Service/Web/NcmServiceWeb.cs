using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Lib;

namespace Vestillo.Business.Service.Web
{
    public class NcmServiceWeb: GenericServiceWeb<Ncm, NcmRepository, NcmController>, INcmService
    {
        public NcmServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public Ncm GetByReferencia(String referencia)
        {
            var c = new ConnectionWebAPI<Ncm>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "referencia = '" + referencia + "'");
        }
    }
}
