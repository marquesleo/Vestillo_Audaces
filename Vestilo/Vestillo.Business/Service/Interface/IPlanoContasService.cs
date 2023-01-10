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
    public interface IPlanoContasService : IService<PlanoContas, PlanoContasRepository, PlanoContasController>
    {
        void Save(ref PlanoContas planoContas, PlanoContas planoContasPai);
        IEnumerable<RelatorioPlanoContasView> GetRelatorioDespesasXReceitas(DateTime dataInicial, DateTime dataFinal);
    }
}
