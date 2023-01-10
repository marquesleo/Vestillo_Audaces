
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
    public interface ITotaisCaixasService : IService<TotaisCaixas, TotaisCaixasRepository, TotaisCaixasController>
    {
        IEnumerable<TotaisCaixas> GetPorNumCaixa(string numCaixa);
        IEnumerable<TotaisCaixas> GetByIdList(int idCaixa);
        IEnumerable<TotaisCaixasView> GetPorData(string numCaixa, DateTime dataInicial, DateTime dataFinal);
    }
}
