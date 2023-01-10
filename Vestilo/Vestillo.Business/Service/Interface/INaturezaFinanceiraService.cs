using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service
{
    public interface INaturezaFinanceiraService : IService<NaturezaFinanceira, NaturezaFinanceiraRepository, NaturezaFinanceiraController>
    {
        IEnumerable<NaturezaFinanceira> GetPorReferencia(String referencia);

        IEnumerable<NaturezaFinanceira> GetPorDescricao(String desc);

        IEnumerable<NaturezaFinanceira> GetByIdList(int id);
    }
}
