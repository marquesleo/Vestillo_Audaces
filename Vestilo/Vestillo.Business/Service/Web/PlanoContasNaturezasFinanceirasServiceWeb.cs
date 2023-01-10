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
    public class PlanoContasNaturezasFinanceirasServiceWeb : GenericServiceWeb<PlanoContasNaturezasFinanceiras, PlanoContasNaturezasFinanceirasRepository, PlanoContasNaturezasFinanceirasController>, IPlanoContasNaturezasFinanceirasService
    {

        public PlanoContasNaturezasFinanceirasServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public void Save(List<PlanoContasNaturezasFinanceiras> planoContasNaturezasFinanceiras, int planoContasId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PlanoContasNaturezasFinanceirasView> GetRelatorioPlanoContas(DateTime dataInicial, DateTime dataFinal)
        {
            throw new NotImplementedException();
        }
    }
}


