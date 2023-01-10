
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
    public class InstrucoesPorBancosServiceWeb : GenericServiceWeb<InstrucoesPorBancos, InstrucoesPorBancosRepository, InstrucoesPorBancosController>, IInstrucoesPorBancosService
    {

        public InstrucoesPorBancosServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }
    }
}


