using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Connection;
using Vestillo.Lib;
using Vestillo.Business.Models;

namespace Vestillo.Business.Repositories
{
    public class TabelaPrecoPCPRepository: GenericRepository<TabelaPrecoPCP>
    {
        public TabelaPrecoPCPRepository()
            : base(new DapperConnection<TabelaPrecoPCP>())
        {
        }

        public TabelaPrecoPCP GetByReferencia(string referencia)
        {
            var ret = new TabelaPrecoPCP();
            _cn.ExecuteToModel("referencia like '%" + referencia + "%'" , ref ret);            
            return ret;
        }

        public IEnumerable<TabelaPrecoPCP> GetListPorReferencia(string referencia)
        {
            TabelaPrecoPCP m = new TabelaPrecoPCP();

            return _cn.ExecuteToList(m, "referencia like '%" + referencia + "%' ");
        }

        public IEnumerable<TabelaPrecoPCP> GetListPorDescricao(string desc)
        {
            TabelaPrecoPCP m = new TabelaPrecoPCP();
            return _cn.ExecuteToList(m, "descricao like '%" + desc + "%' ");//And ativo = 1
        }


    }
}
