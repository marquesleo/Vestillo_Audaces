using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;
using System.Data;

namespace Vestillo.Business.Repositories
{
    public class PlanoContasNaturezasFinanceirasRepository: GenericRepository<PlanoContasNaturezasFinanceiras>
    {
        public PlanoContasNaturezasFinanceirasRepository()
            : base(new DapperConnection<PlanoContasNaturezasFinanceiras>())
        {

        }

        public void DeleteByPlanoContas(int planoContasId)
        {
            _cn.ExecuteNonQuery("DELETE FROM PlanoContasNaturezasFinanceiras WHERE PlanoContasId = " + planoContasId.ToString());
        }

        public IEnumerable<PlanoContasNaturezasFinanceirasView> GetRelatorioPlanoContas(DateTime dataInicial, DateTime dataFinal)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT PlanoContasId, Mes, Descricao, SUM(Valor) AS Valor, Tipo  FROM ( ");
            sql.AppendLine("SELECT");
            sql.AppendLine("		P.ID as PlanoContasId, ");
            sql.AppendLine("		DATE_FORMAT(CRB.DataBaixa, '%m-%Y') AS Mes, ");
            sql.AppendLine("		N.Descricao AS Descricao, ");

            if (VestilloSession.RealizaBaixaParcial)
            {
                sql.AppendLine("		(SUM(IFNULL(CRB.ValorDinheiro, 0)) + SUM(IFNULL(CRB.ValorCheque, 0))) - SUM(IFNULL(CRB.ValorCredito, 0)) AS Valor,");
            }
            else
            {
                sql.AppendLine("		SUM(IFNULL(CR.ValorPago, 0)) AS Valor,");
            }            

            sql.AppendLine("		'RECEITA' AS Tipo");
            sql.AppendLine("FROM 	planocontas p ");
            sql.AppendLine("	inner join planocontasnaturezasfinanceiras pn on p.id = pn.PlanoContasId ");
            sql.AppendLine("	inner join naturezasfinanceiras N on pn.naturezafinanceiraid = n.id");
            sql.AppendLine("    INNER JOIN contasreceber 					CR  ON CR.IdNaturezaFinanceira =  N.Id");
            sql.AppendLine("	INNER JOIN contasreceberbaixa 				CRB ON CRB.ContasReceberId = CR.Id");
            sql.AppendLine("WHERE CR.Status <> 4  AND DATE_FORMAT(CRB.DataBaixa, '%Y-%m-%d') BETWEEN '" + dataInicial.ToString("yyyy-MM-dd") + "' AND '" + dataFinal.ToString("yyyy-MM-dd") + "' AND" + FiltroEmpresa("IdEmpresa", "CR"));
            sql.AppendLine("GROUP BY ");
            sql.AppendLine(" p.id,  N.Descricao");

            sql.AppendLine("UNION ALL");

            sql.AppendLine("SELECT 	");
            sql.AppendLine("		P.ID as PlanoContasId, ");
            sql.AppendLine("		DATE_FORMAT(CPB.DataBaixa, '%m-%Y') AS Mes,");
            sql.AppendLine("		N.Descricao AS Descricao,");

            if (VestilloSession.RealizaBaixaParcial)
            {
                sql.AppendLine("		(SUM(IFNULL(CPB.ValorDinheiro, 0)) + SUM(IFNULL(CPB.ValorCheque, 0))) - SUM(IFNULL(CPB.ValorCredito, 0))  AS Valor,");
            }
            else
            {
                sql.AppendLine("		SUM(IFNULL(CP.ValorPago, 0)) AS Valor,");
            }            

            sql.AppendLine("		'DESPESA' AS Tipo");
            sql.AppendLine("FROM 	planocontas p");
            sql.AppendLine("	inner join planocontasnaturezasfinanceiras pn on p.id = pn.PlanoContasId ");
            sql.AppendLine("	inner join naturezasfinanceiras N on pn.naturezafinanceiraid = n.id ");
            sql.AppendLine("	INNER JOIN contaspagar 						CP  ON CP.IdNaturezaFinanceira =  N.Id ");
            sql.AppendLine("	INNER JOIN contaspagarbaixa 				CPB ON CPB.ContasPagarId = CP.Id");
            sql.AppendLine("WHERE 	DATE_FORMAT(CPB.DataBaixa, '%Y-%m-%d') BETWEEN '" + dataInicial.ToString("yyyy-MM-dd") + "' AND '" + dataFinal.ToString("yyyy-MM-dd") + "' AND" + FiltroEmpresa("IdEmpresa", "CP"));
            sql.AppendLine("GROUP BY ");
            sql.AppendLine(" p.id,  N.Descricao");

            //aqui cheque
            sql.AppendLine(" UNION ALL ");

            sql.AppendLine(" SELECT ");
            sql.AppendLine("		P.ID as PlanoContasId, ");
            sql.AppendLine("        DATE_FORMAT(CHQ.Compensacao, '%m-%Y') AS Mes, ");
            sql.AppendLine("        N.Descricao AS Descricao, ");
            sql.AppendLine("        SUM(IFNULL(CHQ.ValorCompensado, 0)) AS Valor, ");
            sql.AppendLine("        'RECEITA' AS Tipo ");
            sql.AppendLine("FROM 	planocontas p");
            sql.AppendLine("	inner join planocontasnaturezasfinanceiras pn on p.id = pn.PlanoContasId ");
            sql.AppendLine("	inner join naturezasfinanceiras N on pn.naturezafinanceiraid = n.id ");
            sql.AppendLine("    INNER JOIN cheques CHQ ON CHQ.NaturezaFinanceiraId = N.Id ");
            sql.AppendLine(" WHERE TipoEmitenteCheque = 1 AND DATE_FORMAT(CHQ.Compensacao, '%Y-%m-%d') BETWEEN '" + dataInicial.ToString("yyyy-MM-dd") + "' AND '" + dataFinal.ToString("yyyy-MM-dd") + "' AND" + FiltroEmpresa("EmpresaId", "CHQ"));
            sql.AppendLine(" GROUP BY p.id,  N.Descricao ");


            sql.AppendLine(" UNION ALL ");

            sql.AppendLine(" SELECT ");
            sql.AppendLine("		P.ID as PlanoContasId, ");
            sql.AppendLine("        DATE_FORMAT(CHQ.Compensacao, '%m-%Y') AS Mes, ");
            sql.AppendLine("        N.Descricao AS Descricao, ");
            sql.AppendLine("        SUM(IFNULL(CHQ.ValorCompensado, 0)) AS Valor, ");
            sql.AppendLine("        'DESPESA' AS Tipo ");
            sql.AppendLine("FROM 	planocontas p");
            sql.AppendLine("	inner join planocontasnaturezasfinanceiras pn on p.id = pn.PlanoContasId ");
            sql.AppendLine("	inner join naturezasfinanceiras N on pn.naturezafinanceiraid = n.id ");
            sql.AppendLine("    INNER JOIN cheques CHQ ON CHQ.NaturezaFinanceiraId = N.Id ");
            sql.AppendLine(" WHERE TipoEmitenteCheque = 2 AND DATE_FORMAT(CHQ.Compensacao, '%Y-%m-%d') BETWEEN '" + dataInicial.ToString("yyyy-MM-dd") + "' AND '" + dataFinal.ToString("yyyy-MM-dd") + "' AND" + FiltroEmpresa("EmpresaId", "CHQ"));
            sql.AppendLine(" GROUP BY p.id,  N.Descricao ");

            sql.AppendLine(" ) AS t GROUP BY PlanoContasId, Descricao ");
            sql.AppendLine(" ORDER BY PlanoContasId, Descricao, Valor ");

            DapperConnection<PlanoContasNaturezasFinanceirasView> cn = new DapperConnection<PlanoContasNaturezasFinanceirasView>();
            return cn.ExecuteStringSqlToList(new PlanoContasNaturezasFinanceirasView(), sql.ToString());
        }
    }
}
