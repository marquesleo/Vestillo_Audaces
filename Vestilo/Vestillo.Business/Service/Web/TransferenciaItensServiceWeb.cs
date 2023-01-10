
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
    public class TransferenciaItensServiceWeb : GenericServiceWeb<TransferenciaItens, TransferenciaItensRepository, TransferenciaItensController>, ITransferenciaItensService
    {
        public TransferenciaItensServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<TransferenciaItens> GetListByItens(int IdNfe)
        {
            var c = new ConnectionWebAPI<TransferenciaItens>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri + "idnfe = " + IdNfe.ToString());
        }

        public IEnumerable<TransferenciaItensView> GetListByItensView(int IdNfe)
        {
            var c = new ConnectionWebAPI<TransferenciaItensView>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri + "idnfe = " + IdNfe.ToString());
        }


    }
}
