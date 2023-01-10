
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
    public class DevolucaoItensServiceWeb : GenericServiceWeb<DevolucaoItens, DevolucaoItensRepository, DevolucaoItensController>, IDevolucaoItensService
    {
        public DevolucaoItensServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<DevolucaoItens> GetListByNfe(int IdNfe)
        {
            var c = new ConnectionWebAPI<DevolucaoItens>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri + "idnfe = " + IdNfe.ToString());
        }

        public IEnumerable<DevolucaoItens> GetListByNfeItensComplementar(int IdNfe)
        {
            var c = new ConnectionWebAPI<DevolucaoItens>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri + "idnfe = " + IdNfe.ToString());
        }


        public IEnumerable<DevolucaoItensView> GetListByNfeItensView(int IdNfe)
        {
            var c = new ConnectionWebAPI<DevolucaoItensView>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri + "idnfe = " + IdNfe.ToString());
        }

        public IEnumerable<DevolucaoItensView> GetListByNfeItensViewAgrupados(int IdNfe)
        {
            var c = new ConnectionWebAPI<DevolucaoItensView>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri + "idnfe = " + IdNfe.ToString());
        }

        
    }
}
