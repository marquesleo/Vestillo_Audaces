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
    public class FichaTecnicaOperacaoServiceWeb : GenericServiceWeb<FichaTecnicaOperacao, FichaTecnicaOperacaoRepository, FichaTecnicaOperacaoController>, IFichaTecnicaOperacaoService
    {

        public FichaTecnicaOperacaoServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<FichaTecnicaOperacao> GetByProduto(int produtoId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FichaTecnicaOperacao> GetByFichaTecnica(int fichaTecnicaId)
        {
            throw new NotImplementedException();
        }
    }
}


