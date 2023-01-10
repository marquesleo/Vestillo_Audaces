
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.Interface;

namespace Vestillo.Business.Service.APP
{
    public class PlanoContasNaturezasFinanceirasServiceAPP : GenericServiceAPP<PlanoContasNaturezasFinanceiras, PlanoContasNaturezasFinanceirasRepository, PlanoContasNaturezasFinanceirasController>, IPlanoContasNaturezasFinanceirasService
    {
        public PlanoContasNaturezasFinanceirasServiceAPP()
            : base(new PlanoContasNaturezasFinanceirasController())
        {

        }

        public void Save(List<PlanoContasNaturezasFinanceiras> planoContasNaturezasFinanceiras, int planoContasId)
        {
            controller.Save(planoContasNaturezasFinanceiras, planoContasId);
        }

        public IEnumerable<PlanoContasNaturezasFinanceirasView> GetRelatorioPlanoContas(DateTime dataInicial, DateTime dataFinal)
        {
            return controller.GetRelatorioPlanoContas(dataInicial, dataFinal);
        }
    }
}
