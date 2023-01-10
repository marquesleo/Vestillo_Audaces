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
    public interface ICondPagamentoService : IService<CondPagamento, CondPagamentoRepository, CondPagamentoController>
    {
        IEnumerable<CondPagamento> GetPorReferencia(String referencia);

        IEnumerable<CondPagamento> GetPorDescricao(String desc);

        IEnumerable<CondPagamento> GetByIdList(int id);

    }
}
