using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Service.Web
{
    public class ProdutividadeServiceWeb : GenericServiceWeb<Produtividade, ProdutividadeRepository, ProdutividadeController>, IProdutividadeService
    {
        public ProdutividadeServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public Produtividade GetByFuncionarioIdEData(int funcId, DateTime data)
        {
            throw new NotImplementedException();
        }
    }
}
