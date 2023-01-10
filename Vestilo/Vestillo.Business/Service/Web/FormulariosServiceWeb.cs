
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
    public class FormulariosServiceWeb : GenericServiceWeb<Formularios, FormulariosRepository, FormulariosController>, IFormulariosService
    {

        public FormulariosServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<Formularios> GetBuscarPeloTipo(int Tipo)
        {
            var c = new ConnectionWebAPI<Formularios>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri + "?Ativo=" + Tipo.ToString());
        }

    }
       
}


