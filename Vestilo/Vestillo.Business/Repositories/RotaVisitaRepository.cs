using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class RotaVisitaRepository:GenericRepository<RotaVisita>
    {
        public RotaVisitaRepository()
            : base(new DapperConnection<RotaVisita>())
        {
        }

        public IEnumerable<RotaVisita> GetPorReferencia(string referencia)
        {
            RotaVisita m = new RotaVisita();

            return _cn.ExecuteToList(m, "referencia like '%" + referencia + "%' And ativo = 1" );
        }

        public IEnumerable<RotaVisita> GetPorDescricao(string desc)
        {
            RotaVisita m = new RotaVisita();
            return _cn.ExecuteToList(m, "descricao like '%" + desc + "%' And ativo = 1" );
        }

        public IEnumerable<RotaVisita> GetByIdList(int id)
        {
            RotaVisita m = new RotaVisita();
            return _cn.ExecuteToList(m, "id = " + id + " And ativo = 1");
        }
    }
}
