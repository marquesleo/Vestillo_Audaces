
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
    public class TipoOperacaoServiceWeb : GenericServiceWeb<TipoOperacao, TipoOperacaoRepository, TipoOperacaoController>, ITipoOperacaoService
    {

        public TipoOperacaoServiceWeb(string requestUri)  : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<TipoOperacao> GetByAtivos(int AtivoInativo)
        {
            var c = new ConnectionWebAPI<TipoOperacao>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri + "?Ativo=" + AtivoInativo.ToString());
        }
    }
}


