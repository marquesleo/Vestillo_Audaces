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
    public class FichaTecnicaOperacaoMovimentoServiceWeb : GenericServiceWeb<FichaTecnicaOperacaoMovimento, FichaTecnicaOperacaoMovimentoRepository, FichaTecnicaOperacaoMovimentoController>, IFichaTecnicaOperacaoMovimentoService
    {
        public FichaTecnicaOperacaoMovimentoServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<FichaTecnicaOperacaoMovimento> GetByFichaTecnica(int fichaTecnicaId)
        {
            throw new NotImplementedException();
        }
    }
}
