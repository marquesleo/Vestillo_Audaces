
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
    public class NotaEntradaFaccaoItensServiceWeb : GenericServiceWeb<NotaEntradaFaccaoItens, NotaEntradaFaccaoItensRepository, NotaEntradaItensFaccaoController>, INotaEntradaFaccaoItensService
    {
        public NotaEntradaFaccaoItensServiceWeb(string requestUri) : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<NotaEntradaFaccaoItens> GetListByNotaEntradaItens(int IdNota)
        {
            var c = new ConnectionWebAPI<NotaEntradaFaccaoItens>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri + "idnfe = " + IdNota.ToString());
        }



        public IEnumerable<NotaEntradaFaccaoItensView> GetListByNotaEntradaItensView(int IdNota)
        {
            var c = new ConnectionWebAPI<NotaEntradaFaccaoItensView>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri + "idnfe = " + IdNota.ToString());
        }

        public IEnumerable<NotaEntradaFaccaoItensView> GetListByNotaEntradaItensViewAgrupados(int IdNota)
        {
            var c = new ConnectionWebAPI<NotaEntradaFaccaoItensView>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri + "idnfe = " + IdNota.ToString());
        }


    }
}
