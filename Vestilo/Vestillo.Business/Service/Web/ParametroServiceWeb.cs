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
    public class ParametroServiceWeb : GenericServiceWeb<Parametro, ParametroRepository, ParametroController>, IParametroService
    {

        public ParametroServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public Parametro GetByChave(string chave)
        {
            var c = new ConnectionWebAPI<Parametro>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "chave=" + chave);
        }


        public IEnumerable<Parametro> GetAllVisaoCliente()
        {
            throw new NotImplementedException();            
        }
    }
}
