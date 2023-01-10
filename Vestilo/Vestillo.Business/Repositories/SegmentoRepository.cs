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
    public class SegmentoRepository: GenericRepository<Segmento>
    {
        public SegmentoRepository()
            : base(new DapperConnection<Segmento>())
        {
        }

        public IEnumerable<Segmento> GetListPorDescricao(string Descricao)
        {
            var SQL = new Select()
                .Campos("Id,Abreviatura,Descricao ")
                .From("segmentos ")
               .Where(" descricao like '%" + Descricao + "%'");

            var tm = new Segmento();
            return _cn.ExecuteStringSqlToList(tm, SQL.ToString());
        }
    }
}
