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
    public class MaquinaColaboradorServiceWeb : GenericServiceWeb<MaquinaColaborador, MaquinaColaboradorRepository, MaquinaColaboradorController>, IMaquinaColaboradorService
    {
        public MaquinaColaboradorServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public MaquinaColaboradorView GetByColaborador(int IdColaborador)
        {
            var c = new ConnectionWebAPI<MaquinaColaboradorView>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "idcolaborador = " + IdColaborador.ToString());
        }

        public IEnumerable<MaquinaColaboradorView> GetListByColaborador(int IdColaborador)
        {
            var c = new ConnectionWebAPI<MaquinaColaboradorView>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri + "idcolaborador = " + IdColaborador.ToString());
        }

    }
}
