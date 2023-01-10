using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class AlineaChequeRepository : GenericRepository<AlineaCheque>
    {
        public AlineaChequeRepository()
            : base(new DapperConnection<AlineaCheque>())
        {
        }

        public AlineaCheque GetByAbreviatura(string abreviatura)
        {
            AlineaCheque ret = null;
            _cn.ExecuteToModel("Abreviatura = '" + abreviatura + "'", ref ret);
            return ret;
        }

        public IEnumerable<AlineaCheque> GetListByAbreviatura(string abreviatura)
        {
            return  _cn.ExecuteToList(new AlineaCheque(), "Abreviatura LIKE '%" + abreviatura + "%'");
          }

        public IEnumerable<AlineaCheque> GetListByDescricao(string descricao)
        {
            return _cn.ExecuteToList(new AlineaCheque(), "Descricao LIKE '%" + descricao + "%'");
         
        }
    }
}