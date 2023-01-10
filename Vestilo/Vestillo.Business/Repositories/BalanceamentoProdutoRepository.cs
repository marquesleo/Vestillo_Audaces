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
    public class BalanceamentoProdutoRepository : GenericRepository<BalanceamentoProduto>
    {
        public BalanceamentoProdutoRepository()
            : base(new DapperConnection<BalanceamentoProduto>())
        {
        }

        public IEnumerable<BalanceamentoProdutoView> GetByBalanceamento(int balancemaneto)
        {
            var cn = new DapperConnection<BalanceamentoProdutoView>();
            var SQL = new Select()
                .Campos("bp.*, s.Abreviatura as Setor")
                .From("BalanceamentoProduto bp")
                .InnerJoin("Setores s", " s.id = bp.setorid")
                .Where("BalanceamentoId = " + balancemaneto);

            var bp = new BalanceamentoProdutoView();
            return cn.ExecuteStringSqlToList(bp, SQL.ToString());
        }
    }
}
