
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
    public class DepartamentosServiceWeb : GenericServiceWeb<Departamentos, DepartamentosRepository, DepartamentosController>, IDepartamentosService
    {

        public DepartamentosServiceWeb(string requestUri)  : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<Departamentos> GetByAtivos(int AtivoInativo)
        {
            var c = new ConnectionWebAPI<Departamentos>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri + "?Ativo=" + AtivoInativo.ToString());
        }
    }
}


