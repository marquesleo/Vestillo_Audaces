using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Controllers;
using Vestillo.Business.Repositories;
using Vestillo.Lib;
using Vestillo.Business.Service.Interface;

namespace Vestillo.Business.Service.Web
{
    public class PlanoContasServiceWeb : GenericServiceWeb<PlanoContas, PlanoContasRepository, PlanoContasController>, IPlanoContasService
    {

        public PlanoContasServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public void Save(ref PlanoContas planoContas, PlanoContas planoContasPai)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RelatorioPlanoContasView> GetRelatorioDespesasXReceitas(DateTime dataInicial, DateTime dataFinal)
        {
            throw new NotImplementedException();
        }
    }
}


