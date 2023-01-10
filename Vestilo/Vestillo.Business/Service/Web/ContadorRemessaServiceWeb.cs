
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
    public class ContadorRemessaServiceWeb : GenericServiceWeb<ContadorRemessa, ContadorRemessaRepository, ContadorRemessaController>, IContadorRemessaService
    {

        public ContadorRemessaServiceWeb(string requestUri) : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public int GetProximo(int IdBanco)
        {
            throw new NotImplementedException();
        }

        public ContadorRemessa GetByBanco(int IdBanco)
        {

            throw new NotImplementedException();
        }
    }
}
