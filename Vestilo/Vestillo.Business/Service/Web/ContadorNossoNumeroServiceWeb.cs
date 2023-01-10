
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
    public class ContadorNossoNumeroServiceWeb : GenericServiceWeb<ContadorNossoNumero, ContadorNossoNumeroRepository, ContadorNossoNumeroController>, IContadorNossoNumeroService
    {

        public ContadorNossoNumeroServiceWeb(string requestUri) : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public string GetProximo(int IdBanco)
        {
            throw new NotImplementedException();
        }

        public ContadorNossoNumero GetByBanco(int IdBanco)
        {

            throw new NotImplementedException();
        }
    }
}
