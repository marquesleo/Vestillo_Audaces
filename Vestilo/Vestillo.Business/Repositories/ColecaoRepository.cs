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
    public class ColecaoRepository: GenericRepository<Colecao>
    {
        public ColecaoRepository()
            : base(new DapperConnection<Colecao>())
        {
        }

        public IEnumerable<Colecao> GetListPorDescricao(string Descricao)
        {
            var SQL = new Select()
                .Campos("Id,Abreviatura,Descricao ")
                .From("colecoes ")
               .Where(" descricao like '%" + Descricao + "%'");

            var tm = new Colecao();
            return _cn.ExecuteStringSqlToList(tm, SQL.ToString());
        }
    }
}
