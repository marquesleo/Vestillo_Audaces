using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service.APP
{
    public class DespesaFixaVariavelServiceAPP: GenericServiceAPP<DespesaFixaVariavel, DespesaFixaVariavelRepository, DespesaFixaVariavelController>, IDespesaFixaVariavelService
    {

        public DespesaFixaVariavelServiceAPP()
            : base(new DespesaFixaVariavelController())
        {
        }

        public int GetUltimoAno()
        {
            return controller.GetUltimoAno();
        }

        public DespesaFixaVariavel GetByAno(int ano)
        {
            return controller.GetByAno(ano);
        }

        public IEnumerable<DespesaFixaVariavelMes> GetDespesasNaturezasByAno(int ano)
        {
            return controller.GetDespesasNaturezasByAno(ano);
        }

        public void UpdateDespesaByNatureza(int idNatFinanceira, bool automatico)
        {
            controller.UpdateDespesaByNatureza(idNatFinanceira, automatico);
        }

        public void UpdateTotaisDespesa(int despesaId)
        {
            controller.UpdateTotaisDespesa(despesaId);
        }


    }
}
