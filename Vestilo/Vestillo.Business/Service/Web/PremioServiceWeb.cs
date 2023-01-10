using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.APP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Service.Web
{
    public class PremioServiceWeb : GenericServiceWeb<Premio, PremioRepository, PremioController>, IPremioService
    {
        public PremioServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public Premio GetByIdView(int id)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Premio> GetByDescricao(string descricao)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Premio> GetByReferencia(string referencia)
        {
            throw new NotImplementedException();
        }
    }
}
