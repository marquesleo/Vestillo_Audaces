using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Models.Views;

namespace Vestillo.Business.Controllers
{
    public class BalancoEstoqueItensController : GenericController<BalancoEstoqueItens, BalancoEstoqueItensRepository>
    {
        public IEnumerable<BalancoEstoqueItensView> GetViewByBalanco(int idBalanco)
        {
            using (var repository = new BalancoEstoqueItensRepository())
            {
                return repository.GetViewByBalanco(idBalanco);
            }
        }

        public void DeleteByBalanco(int balancoId)
        {
            using (var repository = new BalancoEstoqueItensRepository())
            {
                repository.DeleteByBalanco(balancoId);
            }
        }
    }
}
