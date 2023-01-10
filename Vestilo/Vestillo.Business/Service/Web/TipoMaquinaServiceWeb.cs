

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
    public class TipoMaquinaServiceWeb : GenericServiceWeb<TipoMaquina, TipoMaquinaRepository, TipoMaquinaController>, ITipoMaquinaService
    {

        public TipoMaquinaServiceWeb(string requestUri)  : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<TipoMaquina> GetByAtivos(int AtivoInativo)
        {
            var c = new ConnectionWebAPI<TipoMaquina>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri + "?Ativo=" + AtivoInativo.ToString());
        }
    }
}


