using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Controllers;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service
{
    public interface IDespesaFixaVariavelService : IService<DespesaFixaVariavel, DespesaFixaVariavelRepository, DespesaFixaVariavelController>
    {
        int GetUltimoAno();
        DespesaFixaVariavel GetByAno(int ano);
        IEnumerable<DespesaFixaVariavelMes> GetDespesasNaturezasByAno(int ano);
        void UpdateDespesaByNatureza(int idNatFinanceira, bool automatico);
        void UpdateTotaisDespesa(int despesaId);
    }
}
