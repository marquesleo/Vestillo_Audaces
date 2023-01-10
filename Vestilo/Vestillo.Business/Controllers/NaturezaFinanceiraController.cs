using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class NaturezaFinanceiraController : GenericController<NaturezaFinanceira, NaturezaFinanceiraRepository>
    {
        public IEnumerable<NaturezaFinanceira> GetPorReferencia(string referencia)
        {
            using (NaturezaFinanceiraRepository repository = new NaturezaFinanceiraRepository())
            {
                return repository.GetPorReferencia(referencia);
            }
        }

        public IEnumerable<NaturezaFinanceira> GetPorDescricao(string desc)
        {
            using (NaturezaFinanceiraRepository repository = new NaturezaFinanceiraRepository())
            {
                return repository.GetPorDescricao(desc);
            }
        }

        public IEnumerable<NaturezaFinanceira> GetByIdList(int id)
        {
            using (NaturezaFinanceiraRepository repository = new NaturezaFinanceiraRepository())
            {
                return repository.GetByIdList(id);
            }
        }
    }
}
