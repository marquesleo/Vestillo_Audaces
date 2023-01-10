using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Controllers;

namespace Vestillo.Business.Service
{
    public interface IPlanoContasNaturezasFinanceirasService : IService<PlanoContasNaturezasFinanceiras, PlanoContasNaturezasFinanceirasRepository, PlanoContasNaturezasFinanceirasController>
    {
        void Save(List<PlanoContasNaturezasFinanceiras> planoContasNaturezasFinanceiras, int planoContasId);
        IEnumerable<PlanoContasNaturezasFinanceirasView> GetRelatorioPlanoContas(DateTime dataInicial, DateTime dataFinal);
    }
}
