
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.Interface;
using Vestillo.Lib;

namespace Vestillo.Business.Service.Web
{
    public class ControlaEmailCobrancaServiceWeb : GenericServiceWeb<ControlaEmailCobranca, ControlaEmailCobrancaRepository, ControlaEmailCobrancaController>, IControlaEmailCobrancaService
    {
        public ControlaEmailCobrancaServiceWeb(string requestUri) : base(requestUri)
        {
            this.RequestUri = requestUri;
        }
        
        public IEnumerable<ControlaEmailCobrancaView> GetAllView()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ControlaEmailCobrancaView> GetAllViewAtivos()
        {
            throw new NotImplementedException();
        }

    }
}
