using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class TotaisCaixasController : GenericController<TotaisCaixas, TotaisCaixasRepository>
    {

        public IEnumerable<TotaisCaixas> GetPorNumCaixa(string numCaixa)
        {
            using (TotaisCaixasRepository repository = new TotaisCaixasRepository())
            {
                return repository.GetPorNumCaixa(numCaixa);
            }
        }

        public IEnumerable<TotaisCaixas> GetByIdList(int idCaixa)
        {
            using (TotaisCaixasRepository repository = new TotaisCaixasRepository())
            {
                return repository.GetByIdList(idCaixa);
            }
        }

        public IEnumerable<TotaisCaixasView> GetPorData(string numCaixa, DateTime dataInicial, DateTime dataFinal) 
        {
            using (TotaisCaixasRepository repository = new TotaisCaixasRepository())
            {
                return repository.GetPorData( numCaixa, dataInicial,dataFinal);
            }
        }
    }
}

