

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
    public class TransferenciaServiceWeb : GenericServiceWeb<Transferencia, TransferenciaRepository, TransferenciaController>, ITransferenciaService
    {

        public TransferenciaServiceWeb(string requestUri) : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<TransferenciaView> GetAllView()
        {
            throw new NotImplementedException();
        }
    }
}


