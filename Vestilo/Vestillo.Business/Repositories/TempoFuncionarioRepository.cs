using Vestillo.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class TempoFuncionarioRepository: GenericRepository<TempoFuncionario>
    {
        public TempoFuncionarioRepository()
            : base(new DapperConnection<TempoFuncionario>())
        {
        }

        public IEnumerable<TempoFuncionario> GetByFuncionarioIdEData(int funcId, DateTime data)
        {
            var cn = new DapperConnection<TempoFuncionario>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	tf.* ");
            SQL.AppendLine("FROM 	tempofuncionario tf");
            SQL.AppendLine("INNER JOIN funcionarios f ON f.id = tf.FuncionarioId");
            SQL.AppendLine("WHERE f.id = " + funcId);
            SQL.AppendLine(" AND date(tf.data) = '" + data.ToString("yyyy-MM-dd") + "'");
            SQL.AppendLine("ORDER BY tf.Id DESC");

            return cn.ExecuteStringSqlToList( new TempoFuncionario(), SQL.ToString());
        }

        public IEnumerable<TempoFuncionario> GetByFuncionarioIdEDatas(int funcId, DateTime daData, DateTime ateData)
        {
            var cn = new DapperConnection<TempoFuncionario>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	tf.* ");
            SQL.AppendLine("FROM 	tempofuncionario tf");
            SQL.AppendLine("INNER JOIN funcionarios f ON f.id = tf.FuncionarioId");
            SQL.AppendLine("WHERE f.id = " + funcId);
            SQL.AppendLine(" AND date(tf.data) BETWEEN  '" + daData.ToString("yyyy-MM-dd") + "' AND '" + ateData.ToString("yyyy-MM-dd") + "'");
            SQL.AppendLine("ORDER BY tf.Id DESC");

            return cn.ExecuteStringSqlToList(new TempoFuncionario(), SQL.ToString());
        }

        public TempoFuncionario GetByData(DateTime data)
        {
            var cn = new DapperConnection<TempoFuncionario>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT tf.Data, SUM(tf.Tempo) AS Tempo ");
            SQL.AppendLine("FROM 	tempofuncionario tf");
            SQL.AppendLine("INNER JOIN funcionarios f ON f.id = tf.FuncionarioId");
            SQL.AppendLine("WHERE " );
            SQL.AppendLine(" date(tf.data) =  '" + data.ToString("yyyy-MM-dd") + "'");
            SQL.AppendLine("GROUP BY tf.data");
            SQL.AppendLine("ORDER BY tf.Id DESC");

            var tempo = new TempoFuncionario();
            cn.ExecuteToModel(ref tempo, SQL.ToString());

            return tempo;
        }

        public TempoFuncionario GetByYear()
        {
            var cn = new DapperConnection<TempoFuncionario>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT tf.Data, SUM(tf.Tempo) AS Tempo ");
            SQL.AppendLine("FROM 	tempofuncionario tf");
            SQL.AppendLine("INNER JOIN funcionarios f ON f.id = tf.FuncionarioId");
            SQL.AppendLine("WHERE ");
            SQL.AppendLine(" YEAR(tf.data) =  '" + DateTime.Today.ToString("yyyy") + "'");
            SQL.AppendLine("GROUP BY YEAR(tf.data)");
            SQL.AppendLine("ORDER BY tf.Id DESC");

            var tempo = new TempoFuncionario();
            cn.ExecuteToModel(ref tempo, SQL.ToString());

            return tempo;
        }

        public TempoFuncionario GetByDatas(DateTime daData, DateTime ateData)
        {
            var cn = new DapperConnection<TempoFuncionario>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT tf.Data, SUM(tf.Tempo) AS Tempo ");
            SQL.AppendLine("FROM 	tempofuncionario tf");
            SQL.AppendLine("INNER JOIN funcionarios f ON f.id = tf.FuncionarioId");
            SQL.AppendLine("WHERE ");
            SQL.AppendLine(" date(tf.data) BETWEEN  '" + daData.ToString("yyyy-MM-dd") + "' AND '" + ateData.ToString("yyyy-MM-dd") + "'");
            SQL.AppendLine("GROUP BY YEAR(tf.data)");
            SQL.AppendLine("ORDER BY tf.Id DESC");

            var tempo = new TempoFuncionario();
            cn.ExecuteToModel(ref tempo, SQL.ToString());

            return tempo;
        }

        public IEnumerable<TempoFuncionario> GetByFuncionario(int funcId)
        {
            var cn = new DapperConnection<TempoFuncionario>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	tf.* ");
            SQL.AppendLine("FROM 	tempofuncionario tf");
            SQL.AppendLine("INNER JOIN funcionarios f ON f.id = tf.FuncionarioId");
            SQL.AppendLine("WHERE f.id = " + funcId);
            SQL.AppendLine("ORDER BY tf.Id DESC");

            return cn.ExecuteStringSqlToList(new TempoFuncionario(), SQL.ToString());
        }
    }
}
