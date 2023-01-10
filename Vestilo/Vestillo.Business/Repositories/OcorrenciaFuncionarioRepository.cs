using Vestillo.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class OcorrenciaFuncionarioRepository: GenericRepository<OcorrenciaFuncionario>
    {
        public OcorrenciaFuncionarioRepository()
            : base(new DapperConnection<OcorrenciaFuncionario>())
        {
        }

        public IEnumerable<OcorrenciaFuncionarioView> GetByFuncionarioIdEData(int funcId, DateTime data)
        {
            var cn = new DapperConnection<OcorrenciaFuncionarioView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	of.*, o.Descricao as OcorrenciaDescricao, o.Tipo as OcorrenciaTipo ");
            SQL.AppendLine("FROM 	ocorrenciafuncionario of");
            SQL.AppendLine("INNER JOIN funcionarios f ON f.id = of.FuncionarioId");
            SQL.AppendLine("INNER JOIN ocorrencias o ON o.id = of.OcorrenciaId");
            SQL.AppendLine("WHERE f.id = " + funcId);
            SQL.AppendLine(" AND of.data = '" + data.ToString("yyyy-MM-dd") + "'");
            SQL.AppendLine("ORDER BY of.Id DESC");

            return cn.ExecuteStringSqlToList(new OcorrenciaFuncionarioView(), SQL.ToString());
        }

        public IEnumerable<OcorrenciaFuncionarioView> GetByFuncionarioIdEDatas(int funcId, DateTime daData, DateTime ateData)
        {
            var cn = new DapperConnection<OcorrenciaFuncionarioView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	of.*, o.Descricao as OcorrenciaDescricao, o.Tipo as OcorrenciaTipo ");
            SQL.AppendLine("FROM 	ocorrenciafuncionario of");
            SQL.AppendLine("INNER JOIN funcionarios f ON f.id = of.FuncionarioId");
            SQL.AppendLine("INNER JOIN ocorrencias o ON o.id = of.OcorrenciaId");
            SQL.AppendLine("WHERE f.id = " + funcId);
            SQL.AppendLine(" AND of.data BETWEEN '" + daData.ToString("yyyy-MM-dd") + "' AND '" + ateData.ToString("yyyy-MM-dd") + "'");
            SQL.AppendLine("ORDER BY of.Id DESC");

            return cn.ExecuteStringSqlToList(new OcorrenciaFuncionarioView(), SQL.ToString());
        }

        public IEnumerable<OcorrenciaFuncionarioView> GetByData(DateTime data)
        {
            var cn = new DapperConnection<OcorrenciaFuncionarioView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	SUM(of.Tempo) AS Tempo, o.Descricao as OcorrenciaDescricao, o.Tipo as OcorrenciaTipo ");
            SQL.AppendLine("FROM 	ocorrenciafuncionario of");
            SQL.AppendLine("INNER JOIN funcionarios f ON f.id = of.FuncionarioId");
            SQL.AppendLine("INNER JOIN ocorrencias o ON o.id = of.OcorrenciaId");
            SQL.AppendLine("WHERE ");
            SQL.AppendLine(" of.data = '" + data.ToString("yyyy-MM-dd") + "' ");
            SQL.AppendLine("GROUP BY of.data, o.Tipo");
            SQL.AppendLine("ORDER BY of.Id DESC");

            return cn.ExecuteStringSqlToList(new OcorrenciaFuncionarioView(), SQL.ToString());
        }

        public IEnumerable<OcorrenciaFuncionarioView> GetByYear()
        {
            var cn = new DapperConnection<OcorrenciaFuncionarioView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	SUM(of.Tempo) AS Tempo, o.Descricao as OcorrenciaDescricao, o.Tipo as OcorrenciaTipo ");
            SQL.AppendLine("FROM 	ocorrenciafuncionario of");
            SQL.AppendLine("INNER JOIN funcionarios f ON f.id = of.FuncionarioId");
            SQL.AppendLine("INNER JOIN ocorrencias o ON o.id = of.OcorrenciaId");
            SQL.AppendLine("WHERE ");
            SQL.AppendLine(" YEAR(of.data) = '" + DateTime.Today.ToString("yyyy") + "' ");
            SQL.AppendLine("GROUP BY YEAR(of.data), o.Tipo");
            SQL.AppendLine("ORDER BY of.Id DESC");

            return cn.ExecuteStringSqlToList(new OcorrenciaFuncionarioView(), SQL.ToString());
        }
       
            public IEnumerable<OcorrenciaFuncionarioView> GetByDatas(DateTime daData, DateTime ateData)
        {
            var cn = new DapperConnection<OcorrenciaFuncionarioView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	SUM(of.Tempo) AS Tempo, o.Descricao as OcorrenciaDescricao, o.Tipo as OcorrenciaTipo ");
            SQL.AppendLine("FROM 	ocorrenciafuncionario of");
            SQL.AppendLine("INNER JOIN funcionarios f ON f.id = of.FuncionarioId");
            SQL.AppendLine("INNER JOIN ocorrencias o ON o.id = of.OcorrenciaId");
            SQL.AppendLine("WHERE ");
            SQL.AppendLine(" of.data BETWEEN '" + daData.ToString("yyyy-MM-dd") + "' AND '" + ateData.ToString("yyyy-MM-dd") + "'");
            SQL.AppendLine("GROUP BY YEAR(of.data), o.Tipo");
            SQL.AppendLine("ORDER BY of.Id DESC");

            return cn.ExecuteStringSqlToList(new OcorrenciaFuncionarioView(), SQL.ToString());
        }

        public IEnumerable<OcorrenciaFuncionarioView> GetByFuncionario(int funcId)
        {
            var cn = new DapperConnection<OcorrenciaFuncionarioView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	of.*, o.Descricao as OcorrenciaDescricao, o.Tipo as OcorrenciaTipo ");
            SQL.AppendLine("FROM 	ocorrenciafuncionario of");
            SQL.AppendLine("INNER JOIN funcionarios f ON f.id = of.FuncionarioId");
            SQL.AppendLine("INNER JOIN ocorrencias o ON o.id = of.OcorrenciaId");
            SQL.AppendLine("WHERE f.id = " + funcId);
            SQL.AppendLine("ORDER BY of.Id DESC");

            return cn.ExecuteStringSqlToList(new OcorrenciaFuncionarioView(), SQL.ToString());
        }

        public IEnumerable<OcorrenciaRelatorioView> GetOcorrenciasRelatorio (int ano, decimal diasUteis)
        {
            var cn = new DapperConnection<OcorrenciaRelatorioView>();

            var SQL = new StringBuilder();            
            SQL.AppendLine("SELECT Mes, SUM(Jornada)  AS Jornada, SUM(Producao) AS Producao, SUM(Ocorrencia) AS Ocorrencia,");
            SQL.AppendLine(" SUM(Extra)    AS Extra, SUM(Falta)    AS Falta, SUM(BHCredito)  AS BHCredito,");
            SQL.AppendLine(" SUM(BhDebito) AS BhDebito, SUM(Punicao)  AS Punicao, SUM(qtdTotal)/" + diasUteis + " as MediaCostureira ");
            SQL.AppendLine("FROM( ");
            SQL.AppendLine("    SELECT month(of.data) as Mes, 0 AS Jornada, 0 AS Producao,");
            SQL.AppendLine("        SUM(ROUND(IF(o.id <> 1 AND o.id <> 2 AND o.id <> 3 AND o.id <> 4 AND o.id <> 5, IFNULL(of.tempo, 0), 0), 4)) AS Ocorrencia,");
            SQL.AppendLine("        SUM(ROUND(IF(o.id = 4, IFNULL(of.tempo, 0), 0), 4)) AS Extra,");
            SQL.AppendLine("        SUM(ROUND(IF(o.id = 3, IFNULL(of.tempo, 0), 0), 4)) AS Falta, ");
            SQL.AppendLine("        SUM(ROUND(IF(o.id = 2, IFNULL(of.tempo, 0), 0), 4)) AS BHCredito, ");
            SQL.AppendLine("        SUM(ROUND(IF(o.id = 1, IFNULL(of.tempo, 0), 0), 4)) AS BHDebito, ");
            SQL.AppendLine("        SUM(ROUND(IF(o.id = 5, IFNULL(of.tempo, 0), 0), 4)) AS Punicao, ");
            SQL.AppendLine("        0 as qtdTotal ");
            SQL.AppendLine("    FROM ocorrenciafuncionario of ");
            SQL.AppendLine("    INNER JOIN ocorrencias o ON o.id = of.ocorrenciaid ");
            SQL.AppendLine("    WHERE Year(of.data) = ' " + ano + " '  ");
            SQL.AppendLine("    GROUP BY month(of.data) ");
            SQL.AppendLine("    UNION ALL ");
            SQL.AppendLine("    SELECT month(p.data) as Mes, SUM(ROUND(IFNULL(p.jornada, 0), 4)) AS Jornada, ");
            SQL.AppendLine("        0 AS Producao, 0 AS Ocorrencia, 0 AS Extra, 0 AS Falta, 0 AS BHCredito, 0 AS BHDebito, 0 AS Punicao, count(*) as qtdTotal ");
            SQL.AppendLine("    FROM produtividade p ");
            SQL.AppendLine("    WHERE  YEAR(p.data) = ' "+ ano + " ' ");
            SQL.AppendLine("    GROUP BY month(p.data) ");
            SQL.AppendLine("    UNION ALL ");
            SQL.AppendLine("    SELECT month(tf.data) AS MesAno, 0 AS Jornada, ");
            SQL.AppendLine("        SUM(ROUND(IFNULL(tf.tempo, 0), 4)) AS Producao, ");
            SQL.AppendLine("        0 AS Ocorrencia, 0 AS Extra, 0 AS Falta, 0 AS BHCredito, 0 AS BHDebito, 0 AS Punicao, 0 as qtdTotal ");
            SQL.AppendLine("    FROM tempofuncionario tf ");
            SQL.AppendLine("    WHERE YEAR(TF.data) = ' " + ano +" ' ");
            SQL.AppendLine("    GROUP BY month(TF.data) ");
            SQL.AppendLine("    UNION ALL ");
            SQL.AppendLine("    SELECT month(oo.data) AS MesAno, 0 AS Jornada, ");
            SQL.AppendLine("        SUM(ROUND(IFNULL(go.tempo, 0) * IFNULL(pct.quantidade, 0), 4)) AS Producao, ");
            SQL.AppendLine("        0 AS Ocorrencia, 0 AS Extra, 0 AS Falta, 0 AS BHCredito, 0 AS BHDebito, 0 AS Punicao, 0 AS qtdTotal ");
            SQL.AppendLine("    FROM  operacaooperadora oo ");
            SQL.AppendLine("    INNER JOIN pacotes pct ON pct.id = oo.pacoteid AND NOT ISNULL(pct.referencia) ");
            SQL.AppendLine("    INNER JOIN grupopacote gp ON gp.id = pct.grupopacoteid ");
            SQL.AppendLine("    INNER JOIN grupooperacoes go ON go.grupopacoteid = gp.id AND(oo.operacaoid = go.id OR ISNULL(oo.operacaoid)) ");
            SQL.AppendLine("    WHERE Year(oo.data) = ' " + ano + " ' ");
            SQL.AppendLine("    GROUP BY month(oo.data) ");
            SQL.AppendLine(") AS Uniao ");
            SQL.AppendLine("GROUP BY Mes ");

            return cn.ExecuteStringSqlToList(new OcorrenciaRelatorioView(), SQL.ToString());
        }

        public IEnumerable<OcorrenciaFuncionario> GetFaltaByFuncionario(int funcId, int doMes, int ateMes)
        {
            var cn = new DapperConnection<OcorrenciaFuncionarioView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	of.* ");
            SQL.AppendLine("FROM 	ocorrenciafuncionario of");
            SQL.AppendLine("INNER JOIN ocorrencias o ON o.id = of.OcorrenciaId");
            SQL.AppendLine("WHERE of.funcionarioid = " + funcId);
            SQL.AppendLine("    AND o.Descricao like '%*Falta%' ");
            SQL.AppendLine("    AND month(of.data) between " +  doMes  + " and " + ateMes );
            SQL.AppendLine("ORDER BY of.data DESC");

            return cn.ExecuteStringSqlToList(new OcorrenciaFuncionarioView(), SQL.ToString());
        }
    }
}
