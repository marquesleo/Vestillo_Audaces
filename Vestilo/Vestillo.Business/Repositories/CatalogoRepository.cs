
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
    public class CatalogoRepository : GenericRepository<Catalogo>
    {
        public CatalogoRepository() : base(new DapperConnection<Catalogo>())
        {
        }

        public IEnumerable<Catalogo> GetListPorDescricao(string Descricao)
        {
            var SQL = new Select()
                .Campos("Id,Abreviatura,Descricao ")
                .From("catalogo ")
               .Where(" descricao like '%" + Descricao + "%'");

            var tm = new Catalogo();
            return _cn.ExecuteStringSqlToList(tm, SQL.ToString());
        }
    }
}