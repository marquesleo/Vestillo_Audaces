
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.Interface;

namespace Vestillo.Business.Service.APP
{
    public class TotaisCaixasServiceAPP : GenericServiceAPP<TotaisCaixas, TotaisCaixasRepository, TotaisCaixasController>, ITotaisCaixasService
    {
        public TotaisCaixasServiceAPP() : base(new TotaisCaixasController())
        {
        }

        public IEnumerable<TotaisCaixas> GetPorNumCaixa(string numCaixa)
        {
            TotaisCaixasController controller = new TotaisCaixasController();
            return controller.GetPorNumCaixa( numCaixa);
        }

        public IEnumerable<TotaisCaixas> GetByIdList(int idCaixa)
        {
            TotaisCaixasController controller = new TotaisCaixasController();
            return controller.GetByIdList( idCaixa);
        }

        public IEnumerable<TotaisCaixasView> GetPorData(string numCaixa, DateTime dataInicial, DateTime dataFinal)
        {
            TotaisCaixasController controller = new TotaisCaixasController();
            return controller.GetPorData( numCaixa,    dataInicial,  dataFinal);
        }
    }
}
