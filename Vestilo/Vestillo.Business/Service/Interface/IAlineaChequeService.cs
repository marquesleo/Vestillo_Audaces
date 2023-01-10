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
    public interface IAlineaChequeService : IService<AlineaCheque, AlineaChequeRepository, AlineaChequeController>
    {
        AlineaCheque GetByAbreviatura(string abreviatura);
        IEnumerable<AlineaCheque> GetListByAbreviatura(string abreviatura);
        IEnumerable<AlineaCheque> GetListByDescricao(string descricao);
    }
}
