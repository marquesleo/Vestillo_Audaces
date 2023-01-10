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
    public class CalendarioFaixasRepository: GenericRepository<CalendarioFaixas>
    {
        public CalendarioFaixasRepository() : base(new DapperConnection<CalendarioFaixas>())
        {
        }

        public void DeleteByCalendario(int calendarioId)
        {
            string sql = "DELETE FROM  CalendarioFaixas WHERE CalendarioId = " + calendarioId.ToString();
            _cn.ExecuteNonQuery(sql);
        }

        public List<CalendarioFaixas> GetByCalendario(int calendarioId)
        {
            string sql = "SELECT * FROM  CalendarioFaixas WHERE CalendarioId = " + calendarioId.ToString();
            return _cn.ExecuteStringSqlToList(new CalendarioFaixas(), sql).ToList();
        }
    }
}
