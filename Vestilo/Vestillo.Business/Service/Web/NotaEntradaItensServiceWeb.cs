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
    public class NotaEntradaItensServiceWeb : GenericServiceWeb<NotaEntradaItens, NotaEntradaItensRepository, NotaEntradaItensController>, INotaEntradaItensService
    {
        public NotaEntradaItensServiceWeb(string requestUri) : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<NotaEntradaItens> GetListByNotaEntradaItens(int IdNota)
        {
            var c = new ConnectionWebAPI<NotaEntradaItens>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri + "idnfe = " + IdNota.ToString());
        }



        public IEnumerable<NotaEntradaItensView> GetListByNotaEntradaItensView(int IdNota)
        {
            var c = new ConnectionWebAPI<NotaEntradaItensView>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri + "idnfe = " + IdNota.ToString());
        }

        public IEnumerable<NotaEntradaItensView> GetListByNotaEntradaItensViewAgrupados(int IdNota)
        {
            var c = new ConnectionWebAPI<NotaEntradaItensView>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri + "idnfe = " + IdNota.ToString());
        }

        
    }
}
