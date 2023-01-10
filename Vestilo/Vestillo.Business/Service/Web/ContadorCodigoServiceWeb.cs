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
    public class ContadorCodigoServiceWeb: GenericServiceWeb<ContadorCodigo, ContadorCodigoRepository, ContadorCodigoController>, IContadorCodigoService
    {

        public ContadorCodigoServiceWeb(string requestUri) : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public ContadorCodigo GetByNome(string nome)
        {
            var c = new ConnectionWebAPI<ContadorCodigo>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "nome=" + nome);
        }
    }
}
