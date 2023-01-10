using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class CondPagamentoController:GenericController<CondPagamento,CondPagamentoRepository>
    {
        public IEnumerable<CondPagamento> GetPorReferencia(string referencia)
        {
            using (CondPagamentoRepository repository = new CondPagamentoRepository())
            {
                return repository.GetPorReferencia(referencia);
            }
        }

        public IEnumerable<CondPagamento> GetPorDescricao(string desc)
        {
            using (CondPagamentoRepository repository = new CondPagamentoRepository())
            {
                return repository.GetPorDescricao(desc);
            }
        }

        public IEnumerable<CondPagamento> GetByIdList(int id)
        {
            using (CondPagamentoRepository repository = new CondPagamentoRepository())
            {
                return repository.GetByIdList(id);
            }
        }
    }
}
