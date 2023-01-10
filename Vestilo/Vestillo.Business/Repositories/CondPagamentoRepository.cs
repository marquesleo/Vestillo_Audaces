using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class CondPagamentoRepository:GenericRepository<CondPagamento>
    {
        public CondPagamentoRepository(): base (new DapperConnection<CondPagamento>())
        {
        }

        public IEnumerable<CondPagamento> GetPorReferencia(string referencia)
        {
            CondPagamento m = new CondPagamento();

            return _cn.ExecuteToList(m, "referencia like '%" + referencia + "%' And ativo = 1");
        }

        public IEnumerable<CondPagamento> GetPorDescricao(string desc)
        {
            CondPagamento m = new CondPagamento();
            return _cn.ExecuteToList(m, "descricao like '%" + desc + "%' And ativo = 1");
        }

        public IEnumerable<CondPagamento> GetByIdList(int id)
        {
            CondPagamento m = new CondPagamento();
            return _cn.ExecuteToList(m, "id =" + id);
        }
    }
}
