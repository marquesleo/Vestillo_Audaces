
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
    public class PlanoContasServiceAPP: GenericServiceAPP<PlanoContas, PlanoContasRepository, PlanoContasController>, IPlanoContasService
    {
        public PlanoContasServiceAPP()   : base(new PlanoContasController())
        {

        }

        public void Save(ref PlanoContas planoContas, PlanoContas planoContasPai)
        {
            controller.Save(ref planoContas, planoContasPai);
        }

        public IEnumerable<RelatorioPlanoContasView> GetRelatorioDespesasXReceitas(DateTime dataInicial, DateTime dataFinal)
        {
            return controller.GetRelatorioDespesasXReceitas(dataInicial, dataFinal);
        }
    }
}
