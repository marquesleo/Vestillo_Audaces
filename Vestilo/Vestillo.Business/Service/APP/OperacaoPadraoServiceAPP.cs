using System;
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
    public class OperacaoPadraoServiceAPP : GenericServiceAPP<OperacaoPadrao, OperacaoPadraoRepository, OperacaoPadraoController>, IOperacaoPadraoService
    {

        public OperacaoPadraoServiceAPP() : base(new OperacaoPadraoController())
        {
        }

        public IEnumerable<OperacaoPadrao> GetByAllSetorBal()
        {
            return controller.GetByAllSetorBal();
        }

        public IEnumerable<OperacaoPadrao> GetByAtivos(int AtivoInativo)
        {
            return controller.GetByAtivos(AtivoInativo);
        }

        public IEnumerable<OperacaoPadrao> GetListPorDescricao(string desc)
        {
            OperacaoPadraoController controller = new OperacaoPadraoController();
            return controller.GetListPorDescricao(desc);
        }

        public IEnumerable<OperacaoPadrao> GetListPorReferencia(string referencia)
        {
            OperacaoPadraoController controller = new OperacaoPadraoController();
            return controller.GetListPorReferencia(referencia);
        }

        public IEnumerable<OperacaoPadrao> GetListById(int id)
        {
            OperacaoPadraoController controller = new OperacaoPadraoController();
            return controller.GetListById(id);
        }
    }
}



