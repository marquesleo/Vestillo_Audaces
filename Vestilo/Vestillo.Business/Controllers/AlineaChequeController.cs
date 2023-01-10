using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class AlineaChequeController : GenericController<AlineaCheque, AlineaChequeRepository>
    {
        public AlineaCheque GetByAbreviatura(string abreviatura)
        {
            using (var repository = new AlineaChequeRepository())
            {
                return repository.GetByAbreviatura(abreviatura);
            }
        }

        public IEnumerable<AlineaCheque> GetListByAbreviatura(string abreviatura)
        {
            using (var repository = new AlineaChequeRepository())
            {
                return repository.GetListByAbreviatura(abreviatura);
            }
        }

        public IEnumerable<AlineaCheque> GetListByDescricao(string descricao)
        {
            using (var repository = new AlineaChequeRepository())
            {
                return repository.GetListByDescricao(descricao);
            }
        }
    }
}
