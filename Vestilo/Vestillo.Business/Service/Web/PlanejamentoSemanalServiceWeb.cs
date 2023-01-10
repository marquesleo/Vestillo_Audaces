
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Controllers;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.Interface;
using Vestillo.Lib;

namespace Vestillo.Business.Service.Web
{
    public class PlanejamentoSemanalServiceWeb : GenericServiceWeb<PlanejamentoSemanal, PlanejamentoSemanalRepository, PlanejamentoSemanalController>, IPlanejamentoSemanalService
    {

        public PlanejamentoSemanalServiceWeb(string requestUri) : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<PlanejamentoSemanal> GetByRefView(string referencia)
        {
            throw new NotImplementedException();
        }
    }
}


