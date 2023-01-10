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
    public class FatNfeItensServiceWeb : GenericServiceWeb<FatNfeItens, FatNfeItensRepository, FatNfeItensController>, IFatNfeItensService
    {
        public FatNfeItensServiceWeb(string requestUri) : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<FatNfeItens> GetListByNfe(int IdNfe)
        {
            var c = new ConnectionWebAPI<FatNfeItens>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri + "idnfe = " + IdNfe.ToString());
        }

        public IEnumerable<FatNfeItens> GetListByNfeItensComplementar(int IdNfe)
        {
            var c = new ConnectionWebAPI<FatNfeItens>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri + "idnfe = " + IdNfe.ToString());
        }
        

        public IEnumerable<FatNfeItensView> GetListByNfeItensView(int IdNfe)
        {
            var c = new ConnectionWebAPI<FatNfeItensView>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri + "idnfe = " + IdNfe.ToString());
        }

        public IEnumerable<FatNfeItensView> GetListByNfeItensViewAgrupados(int IdNfe)
        {
            var c = new ConnectionWebAPI<FatNfeItensView>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri + "idnfe = " + IdNfe.ToString());
        }

        
    }
}
