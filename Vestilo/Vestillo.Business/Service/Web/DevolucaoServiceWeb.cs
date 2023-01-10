
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
    public class DevolucaoServiceWeb : GenericServiceWeb<Devolucao, DevolucaoRepository, DevolucaoController>, IDevolucaoService
    {

        public DevolucaoServiceWeb(string requestUri) : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<DevolucaoView> GetAllView()
        {
            throw new NotImplementedException();
        }
    }
}


