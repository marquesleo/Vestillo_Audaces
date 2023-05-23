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
    public class GrupProdutoRepository : GenericRepository<GrupProduto>
    {
        public GrupProdutoRepository()
            : base(new DapperConnection<GrupProduto>())
        {
        }


        public IEnumerable<GrupProduto> GetListPorDescricao(string Descricao)
        {
            var SQL = new Select()
                .Campos("Id,Abreviatura,Descricao,Tipo ")
                .From("grupoprodutos ")
               .Where(" descricao like '%" + Descricao + "%'");

            var tm = new GrupProduto();
            return _cn.ExecuteStringSqlToList(tm, SQL.ToString());
        }
    }
}