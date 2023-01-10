
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
    public class SerieNfceServiceWeb : GenericServiceWeb<SerieNfce , SerieNfceRepository, SerieNfceController>, ISerieNfceService
    {

        public SerieNfceServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public SerieNfce GetByNumeracao(int SerieNota)
        {
   

            var c = new ConnectionWebAPI<SerieNfce>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "Serie=" + SerieNota );
        }
    }
}
