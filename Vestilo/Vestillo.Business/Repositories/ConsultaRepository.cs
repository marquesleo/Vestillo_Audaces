using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;
using Vestillo.Lib;



namespace Vestillo.Business.Repositories
{
    public class ConsultaRepository: GenericRepository<Consulta>
    {
        public ConsultaRepository()
            : base(new DapperConnection<Consulta>())
        {
        }

        public DataTable GetRetornoConsulta(string sql)
        {
            return _cn.ExecuteToDataTable(sql);
        }

        public IEnumerable<Consulta> GetPorIdForm(int idForm)
        {
            var SQL = new Select()
                .Campos("C.*")
                .From("consultas C")
                .Where("C.IdForm = " + idForm);

            var col = new Consulta();
            return _cn.ExecuteStringSqlToList(col, SQL.ToString());
        }
    }
}
