using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo;
using Vestillo.Business.Models;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class ObservacaoProdutoRepository : GenericRepository<ObservacaoProduto>
    {
        public ObservacaoProdutoRepository()
            : base(new DapperConnection<ObservacaoProduto>())
        {

        }

        public ObservacaoProdutoView GetByProduto(int produtoId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT o.*, p.Referencia as Referencia ");
            sql.AppendLine(" FROM ObservacaoProduto o ");
            sql.AppendLine(" INNER JOIN produtos p ON p.Id = o.ProdutoId ");
            sql.AppendLine(" WHERE   o.ProdutoId = " + produtoId.ToString());

            var cn = new DapperConnection<ObservacaoProdutoView>();
            var ret = new ObservacaoProdutoView();
            cn.ExecuteToModel(ref ret, sql.ToString());
            return ret;
        }
    }
}
