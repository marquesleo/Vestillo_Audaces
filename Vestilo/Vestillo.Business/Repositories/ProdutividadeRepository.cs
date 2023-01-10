using Vestillo.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class ProdutividadeRepository: GenericRepository<Produtividade>
    {
        public ProdutividadeRepository()
            : base(new DapperConnection<Produtividade>())
        {
        }

        public Produtividade GetByFuncionarioIdEData(int funcId, DateTime data)
        {
            var cn = new DapperConnection<Produtividade>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	p.* ");
            SQL.AppendLine("FROM 	produtividade p");
            SQL.AppendLine("INNER JOIN funcionarios f ON f.id = p.FuncionarioId");
            SQL.AppendLine("WHERE f.id = " + funcId);
            SQL.AppendLine(" AND p.data = '" + data.ToString("yyyy-MM-dd") + "'");
            SQL.AppendLine("ORDER BY p.Id DESC");

            Produtividade ret = new Produtividade();

            cn.ExecuteToModel(ref ret, SQL.ToString());

            return ret;
        }

        public IEnumerable<Produtividade> GetByFuncionarioIdEDatas(int funcId, DateTime daData, DateTime ateData)
        {
            var cn = new DapperConnection<Produtividade>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	p.* ");
            SQL.AppendLine("FROM 	produtividade p");
            SQL.AppendLine("INNER JOIN funcionarios f ON f.id = p.FuncionarioId");
            SQL.AppendLine("WHERE f.id = " + funcId);
            SQL.AppendLine(" AND p.data BETWEEN '" + daData.ToString("yyyy-MM-dd") + "' AND '" + ateData.ToString("yyyy-MM-dd") + "'");
            SQL.AppendLine("ORDER BY p.Id DESC");

            return cn.ExecuteStringSqlToList(new Produtividade(), SQL.ToString());
        }

        public Produtividade GetByData(DateTime data)
        {
            var cn = new DapperConnection<Produtividade>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	SUM(p.tempo) as Tempo, SUM(p.jornada) AS Jornada ");
            SQL.AppendLine("FROM 	produtividade p");
            SQL.AppendLine("INNER JOIN funcionarios f ON f.id = p.FuncionarioId");
            SQL.AppendLine("WHERE " );
            SQL.AppendLine(" p.data = '" + data.ToString("yyyy-MM-dd") + "'");
            SQL.AppendLine("GROUP BY p.data");
            SQL.AppendLine("ORDER BY p.Id DESC");

            var produtividade = new Produtividade();
            cn.ExecuteToModel(ref produtividade, SQL.ToString());
            return produtividade;
        }

        public Produtividade GetByYear()
        {
            var cn = new DapperConnection<Produtividade>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	SUM(p.tempo) as Tempo, SUM(p.jornada) AS Jornada ");
            SQL.AppendLine("FROM 	produtividade p");
            SQL.AppendLine("INNER JOIN funcionarios f ON f.id = p.FuncionarioId");
            SQL.AppendLine("WHERE ");
            SQL.AppendLine(" YEAR(p.data) = '" + DateTime.Now.ToString("yyyy") + "'");
            SQL.AppendLine("GROUP BY YEAR(p.data)");
            SQL.AppendLine("ORDER BY p.Id DESC");

            var produtividade = new Produtividade();
            cn.ExecuteToModel(ref produtividade, SQL.ToString());
            return produtividade;
        }

        public Produtividade GetByDatas(DateTime daData, DateTime ateData)
        {
            var cn = new DapperConnection<Produtividade>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	SUM(p.tempo) as Tempo, SUM(p.jornada) AS Jornada ");
            SQL.AppendLine("FROM 	produtividade p");
            SQL.AppendLine("INNER JOIN funcionarios f ON f.id = p.FuncionarioId");
            SQL.AppendLine("WHERE ");
            SQL.AppendLine(" p.data BETWEEN '" + daData.ToString("yyyy-MM-dd") + "' AND '" + ateData.ToString("yyyy-MM-dd") + "'");
            SQL.AppendLine("GROUP BY YEAR(p.data)");
            SQL.AppendLine("ORDER BY p.Id DESC");

            var produtividade = new Produtividade();
            cn.ExecuteToModel(ref produtividade, SQL.ToString());
            return produtividade;
        }
    }
}
