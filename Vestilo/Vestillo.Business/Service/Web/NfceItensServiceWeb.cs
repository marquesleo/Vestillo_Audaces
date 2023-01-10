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
    public class NfceItensServiceWeb : GenericServiceWeb<NfceItens, NfceItensRepository, NfceItensController>, INfceItensService
    {
        public NfceItensServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }
        
        public IEnumerable<NfceItens> GetListByNfce(int IdNfce)
        {
            var c = new ConnectionWebAPI<NfceItens>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri + "idnfce = " + IdNfce.ToString());
        }


        public IEnumerable<NfceItensView> GetListViewItensNfce(int IdNfce, bool emissao = false)
        {
            var c = new ConnectionWebAPI<NfceItensView>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri + "idnfce = " + IdNfce.ToString());
        }


        public IEnumerable<NfceItensView> GetListViewItensNfceAgrupado(int IdNfce, bool emissao = false)
        {
            var c = new ConnectionWebAPI<NfceItensView>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri + "idnfce = " + IdNfce.ToString());
        }
        
    }
}
