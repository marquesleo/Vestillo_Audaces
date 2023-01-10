using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;
using Vestillo.Lib;

namespace Vestillo.Business.Repositories
{
    public class UniMedidaRepository : GenericRepository<UniMedida>
    {
        public UniMedidaRepository()
            : base(new DapperConnection<UniMedida>())
        {
        }

        public IEnumerable<UniMedida> GetListPorDescricao(string Descricao)
        {
            var SQL = new Select()
                .Campos("Id,Abreviatura,Descricao ")
                .From("unidademedidas ")
               .Where(" descricao like '%" + Descricao + "%'");

            var tm = new UniMedida();
            return _cn.ExecuteStringSqlToList(tm, SQL.ToString());
        }

    }
}