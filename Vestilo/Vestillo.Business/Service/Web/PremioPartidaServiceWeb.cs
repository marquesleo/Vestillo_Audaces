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
    public class PremioPartidaServiceWeb : GenericServiceWeb<PremioPartida, PremioPartidaRepository, PremioPartidaController>, IPremioPartidaService
    {
        public PremioPartidaServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public PremioPartida GetByIdView(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PremioPartida> GetByDescricao(string descricao)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PremioPartida> GetByReferencia(string referencia)
        {
            throw new NotImplementedException();
        }
    }
}
