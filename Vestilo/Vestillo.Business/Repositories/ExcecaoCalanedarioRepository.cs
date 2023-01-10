
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class ExcecaoCalendarioRepository:GenericRepository<ExcecaoCalendario>
    {
        public ExcecaoCalendarioRepository() : base(new DapperConnection<ExcecaoCalendario>())
        {
        }

        public IEnumerable<ExcecaoCalendario> GetPorReferencia(string referencia)
        {
            ExcecaoCalendario m = new ExcecaoCalendario();

            return _cn.ExecuteToList(m, "referencia like '%" + referencia + "%' And ativo = 1");
        }

        public IEnumerable<ExcecaoCalendario> GetPorDescricao(string desc)
        {
            ExcecaoCalendario m = new ExcecaoCalendario();
            return _cn.ExecuteToList(m, "descricao like '%" + desc + "%' And ativo = 1");
        }

        public ExcecaoCalendario GetByDataExcecao(int CalendarioId, DateTime data)
        {
            var cn = new DapperConnection<ExcecaoCalendario>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	* FROM ExcecaoCalendario");
            SQL.AppendLine("WHERE CalendarioId = " + CalendarioId);
            SQL.AppendLine(" AND DATE(DataDaExcecao) = '" + data.ToString("yyyy-MM-dd") + "'");
            ExcecaoCalendario excessao = new ExcecaoCalendario();
            cn.ExecuteToModel(ref excessao, SQL.ToString());
            return excessao;
        }

    }
}
