using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Lib;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class ServicoRepository : GenericRepository<Servico>
    {
        public ServicoRepository()
            : base(new DapperConnection<Servico>())
        {
        }

        public IEnumerable<Servico> GetPorReferencia(string referencia)
        {
            Servico m = new Servico();

            return _cn.ExecuteToList(m, "referencia like '%" + referencia + "%' And ativo = 1");
        }

        public IEnumerable<Servico> GetPorDescricao(string desc)
        {
            Servico m = new Servico();
            return _cn.ExecuteToList(m, "descricao like '%" + desc + "%' And ativo = 1");
        }

        public IEnumerable<Servico> GetByIdList(int id)
        {
            Servico m = new Servico();
            return _cn.ExecuteToList(m, "id =" + id);
        }

    }
}
