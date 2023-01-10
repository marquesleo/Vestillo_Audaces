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
    public class NcmRepository : GenericRepository<Ncm>
    {
        public NcmRepository()
            : base(new DapperConnection<Ncm>())
        {
        }

        public Ncm GetByReferencia(String referencia)
        {
            var SQL = new Select()
                .Campos(" * ")
                .From("ncm")
                .Where("referencia = '" + referencia + "'");

            var ncm = new Ncm();
            _cn.ExecuteToModel(ref ncm, SQL.ToString());
            return ncm;
        }
    }
}
