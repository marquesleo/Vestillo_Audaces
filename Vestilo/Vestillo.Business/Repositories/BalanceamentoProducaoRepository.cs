using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Connection;
using Vestillo.Lib;

namespace Vestillo.Business.Repositories
{
    public class BalanceamentoProducaoRepository : GenericRepository<BalanceamentoProducao>
    {
        public BalanceamentoProducaoRepository()
            : base(new DapperConnection<BalanceamentoProducao>())
        {
        }

        public IEnumerable<BalanceamentoProducaoView> GetByBalanceamento(int balancemaneto)
        {
            var cn = new DapperConnection<BalanceamentoProducaoView>();
            var SQL = new Select()
                .Campos("bp.*, s.Abreviatura as Setor")
                .From("BalanceamentoProducao bp")
                .InnerJoin("Setores s", " s.id = bp.setorid")
                .Where("BalanceamentoId = " + balancemaneto);

            var bp = new BalanceamentoProducaoView();
            return cn.ExecuteStringSqlToList(bp, SQL.ToString());
        }
    }
}
