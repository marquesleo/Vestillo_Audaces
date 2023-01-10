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
    public class NaturezaFinanceiraServiceAPP: GenericServiceAPP<NaturezaFinanceira, NaturezaFinanceiraRepository, NaturezaFinanceiraController>, INaturezaFinanceiraService
    {
        public NaturezaFinanceiraServiceAPP()
            : base(new NaturezaFinanceiraController())
        {
        }

        public IEnumerable<NaturezaFinanceira> GetPorReferencia(string referencia)
        {
            NaturezaFinanceiraController controller = new NaturezaFinanceiraController();
            return controller.GetPorReferencia(referencia);
        }

        public IEnumerable<NaturezaFinanceira> GetPorDescricao(string desc)
        {
            NaturezaFinanceiraController controller = new NaturezaFinanceiraController();
            return controller.GetPorDescricao(desc);
        }

        public IEnumerable<NaturezaFinanceira> GetByIdList(int id)
        {
            NaturezaFinanceiraController controller = new NaturezaFinanceiraController();
            return controller.GetByIdList(id);
        }
    }
}
