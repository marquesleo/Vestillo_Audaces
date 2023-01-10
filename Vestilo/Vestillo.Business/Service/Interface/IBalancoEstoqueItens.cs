using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Models.Views;

namespace Vestillo.Business.Service
{
    public interface IBalancoEstoqueItensService : IService<BalancoEstoqueItens, BalancoEstoqueItensRepository, BalancoEstoqueItensController>
    {
        IEnumerable<BalancoEstoqueItensView> GetViewByBalanco(int idBalanco);
    }
}
