using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.Interface;
using Vestillo.Business.Models.Views;

namespace Vestillo.Business.Service.APP
{
    public class BalancoEstoqueItensServiceAPP : GenericServiceAPP<BalancoEstoqueItens, BalancoEstoqueItensRepository, BalancoEstoqueItensController>, IBalancoEstoqueItensService
    {
        public BalancoEstoqueItensServiceAPP()
            : base(new BalancoEstoqueItensController())
        {
        }

        public IEnumerable<BalancoEstoqueItensView> GetViewByBalanco(int idBalanco)
        {
            BalancoEstoqueItensController controller = new BalancoEstoqueItensController();
            return controller.GetViewByBalanco(idBalanco);
        }
    }
}
