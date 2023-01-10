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
    public class FichaFaccaoServiceWeb : GenericServiceWeb<FichaFaccao, FichaFaccaoRepository, FichaFaccaoController>, IFichaFaccaoService
    {

        public FichaFaccaoServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<FichaFaccao> VerificaFaccao(List<int> idPacote, List<int> idProduto, List<int> idFaccao)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<FichaFaccao> GetByIdFicha(int idFicha)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<FichaFaccao> GetByIdFaccao(int idFaccao)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FichaFaccaoView> GetByIdFichaView(int idFicha)
        {
            throw new NotImplementedException();
        }
    }
}