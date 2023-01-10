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
    public class CondPagamentoServiceAPP: GenericServiceAPP<CondPagamento, CondPagamentoRepository, CondPagamentoController>, ICondPagamentoService
    {
        public CondPagamentoServiceAPP()
            : base(new CondPagamentoController())
        {
        }

        public IEnumerable<CondPagamento> GetPorReferencia(string referencia)
        {
            CondPagamentoController controller = new CondPagamentoController();
            return controller.GetPorReferencia(referencia);
        }

        public IEnumerable<CondPagamento> GetPorDescricao(string desc)
        {
            CondPagamentoController controller = new CondPagamentoController();
            return controller.GetPorDescricao(desc);
        }

        public IEnumerable<CondPagamento> GetByIdList(int id)
        {
            CondPagamentoController controller = new CondPagamentoController();
            return controller.GetByIdList(id);
        }

    }
}
