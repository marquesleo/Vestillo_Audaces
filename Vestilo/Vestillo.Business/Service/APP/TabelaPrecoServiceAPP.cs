using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service.APP
{
    public class TabelaPrecoServiceAPP : GenericServiceAPP<TabelaPreco, TabelaPrecoRepository, TabelaPrecoController>, ITabelaPrecoService
    {

        public TabelaPrecoServiceAPP(): base (new TabelaPrecoController())
        {
        }

        public TabelaPreco GetByReferencia(string referencia)
        {
            return controller.GetByReferencia(referencia);
        }

        public IEnumerable<TabelaPreco> GetListPorReferencia(string referencia)
        {
            
            TabelaPrecoController controller = new TabelaPrecoController();
            return controller.GetListPorDescricao(referencia);

        }

        public IEnumerable<TabelaPreco> GetListPorDescricao(string desc)
        {
            TabelaPrecoController controller = new TabelaPrecoController();
            return controller.GetListPorDescricao(desc);
        }

        public void CalcularCustos(ref ItemTabelaPrecoView item, decimal percentualImpostosEEncargos, decimal margemLucro)
        {
            controller.CalcularCustos(ref item, percentualImpostosEEncargos, margemLucro);
        }


        public IEnumerable<TabelaPreco> GetAllView()
        {
            TabelaPrecoController controller = new TabelaPrecoController();
            return controller.GetAllView();
        }
    }
}
