using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;
using System.Data;
using Vestillo.Business.Models.Views;

namespace Vestillo.Business.Repositories
{
    public class PlanoContasRepository: GenericRepository<PlanoContas>
    {
        public PlanoContasRepository()
            : base(new DapperConnection<PlanoContas>())
        {
        }

        public override IEnumerable<PlanoContas> GetAll()
        {
            List<PlanoContas> result = new List<PlanoContas>();

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT ");
            sql.AppendLine("    P.*,");
            sql.AppendLine("    N.id AS NaturezaFinanceiraId,");
            sql.AppendLine("    N.descricao AS NaturezaFinanceiraDescricao,");
            sql.AppendLine("    N.referencia AS NaturezaFinanceiraReferencia");
            sql.AppendLine("FROM");
            sql.AppendLine("    PlanoContas P ");
            sql.AppendLine("    LEFT JOIN PlanoContasNaturezasFinanceiras PN ");
            sql.AppendLine("        ON PN.PlanoContasId = P.Id ");
            sql.AppendLine("    LEFT JOIN naturezasfinanceiras N ");
            sql.AppendLine("        ON N.id = PN.NaturezaFinanceiraId ");
            sql.AppendLine("WHERE (P.EmpresaId IS NULL OR P.EmpresaId = " + VestilloSession.EmpresaLogada.Id.ToString() + ")");
            sql.AppendLine("Order BY P.Id");

            DataTable dt = _cn.ExecuteToDataTable(sql.ToString());

            PlanoContas planoContas = null;

            if (dt != null && dt.Rows.Count > 0)
            {
                int idAnterior = 0;

                foreach (DataRow row in dt.Rows)
                {
                    if (idAnterior != int.Parse(row["Id"].ToString()))
                    {
                        planoContas = new PlanoContas();
                        planoContas.Id = int.Parse(row["Id"].ToString());

                        if (! string.IsNullOrEmpty(row["PlanoContasPaiId"].ToString()))
                            planoContas.PlanoContasPaiId = int.Parse(row["PlanoContasPaiId"].ToString());

                        planoContas.Ativo = bool.Parse(row["Ativo"].ToString());
                        planoContas.Codigo = row["Codigo"].ToString();
                        planoContas.Descricao = row["Descricao"].ToString();
                        planoContas.EmpresaId = int.Parse(row["EmpresaId"].ToString());
                        if (row["Padrao"].ToString() != "")
                            planoContas.Padrao = bool.Parse(row["Padrao"].ToString());

                        planoContas.Naturezas = new List<NaturezaFinanceira>();
                        result.Add(planoContas);
                    }

                    if (!string.IsNullOrEmpty(row["NaturezaFinanceiraId"].ToString()))
                    {
                        planoContas.Naturezas.Add(new NaturezaFinanceira()
                        {
                            Id = int.Parse(row["NaturezaFinanceiraId"].ToString()),
                            Descricao = row["NaturezaFinanceiraDescricao"].ToString(),
                            Referencia = row["NaturezaFinanceiraReferencia"].ToString()
                        });
                    }
                    idAnterior = planoContas.Id;
                }
            }

            return result;
        }

        public override PlanoContas GetById(int id)
        { 
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT ");
            sql.AppendLine("    P.*,");
            sql.AppendLine("    N.id AS NaturezaFinanceiraId,");
            sql.AppendLine("    N.descricao AS NaturezaFinanceiraDescricao,");
            sql.AppendLine("    N.referencia AS NaturezaFinanceiraReferencia");
            sql.AppendLine("FROM");
            sql.AppendLine("    PlanoContas P ");
            sql.AppendLine("    LEFT JOIN PlanoContasNaturezasFinanceiras PN ");
            sql.AppendLine("        ON PN.PlanoContasId = P.Id ");
            sql.AppendLine("    LEFT JOIN naturezasfinanceiras N ");
            sql.AppendLine("        ON N.id = PN.NaturezaFinanceiraId ");
            sql.AppendLine("WHERE P.Id = " + id.ToString());

            DataTable dt = _cn.ExecuteToDataTable(sql.ToString());

            PlanoContas planoContas = null;

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (planoContas == null)
                    {
                        planoContas = new PlanoContas();
                        planoContas.Id = int.Parse(row["Id"].ToString());

                        if (!string.IsNullOrEmpty(row["PlanoContasPaiId"].ToString()))
                            planoContas.PlanoContasPaiId = int.Parse(row["PlanoContasPaiId"].ToString());

                        planoContas.Ativo = bool.Parse(row["Ativo"].ToString());
                        planoContas.Codigo = row["Codigo"].ToString();
                        planoContas.Descricao = row["Descricao"].ToString();
                        planoContas.EmpresaId = int.Parse(row["EmpresaId"].ToString());
                        
                        if (row["Padrao"].ToString() != "")
                            planoContas.Padrao = bool.Parse(row["Padrao"].ToString());

                        planoContas.Naturezas = new List<NaturezaFinanceira>();
                    }

                    if (!string.IsNullOrEmpty(row["NaturezaFinanceiraId"].ToString()))
                    {
                        planoContas.Naturezas.Add(new NaturezaFinanceira()
                        {
                            Id = int.Parse(row["NaturezaFinanceiraId"].ToString()),
                            Descricao = row["NaturezaFinanceiraDescricao"].ToString(),
                            Referencia = row["NaturezaFinanceiraReferencia"].ToString()
                        });
                    }
                }
            }
            return planoContas;
        }

        public IEnumerable<RelatorioPlanoContasView> GetRelatorioDespesasXReceitas(DateTime dataInicial, DateTime dataFinal)
        {
            StringBuilder sql = new StringBuilder();
           
            
            sql.AppendLine("SELECT");
            sql.AppendLine("		DATE_FORMAT(CRB.DataBaixa, '%m-%Y') AS Mes,");
            sql.AppendLine("		CASE WHEN ADM.Id IS NOT NULL THEN ADM.Nome ELSE TD.Descricao END AS Descricao,");

            if(VestilloSession.RealizaBaixaParcial)
            {
                sql.AppendLine("		(SUM(IFNULL(CRB.ValorDinheiro, 0)) ) - SUM(IFNULL(CRB.ValorCredito, 0)) AS Valor,");
            }
            else
            {
                sql.AppendLine("		(SUM(IFNULL(CR.ValorPago, 0)) - SUM(IFNULL(CRB.ValorCheque, 0)) ) AS Valor,");
            }
            
            

            sql.AppendLine("		'RECEITA' AS Tipo");
            sql.AppendLine("FROM 	naturezasfinanceiras N");
            sql.AppendLine("	INNER JOIN contasreceber 					CR  ON CR.IdNaturezaFinanceira =  N.Id ");
            sql.AppendLine("	INNER JOIN contasreceberbaixa 				CRB ON CRB.ContasReceberId = CR.Id");
            sql.AppendLine("    INNER JOIN tipodocumentos 					TD  ON TD.Id = CR.IdTipoDocumento");
            sql.AppendLine("	LEFT  JOIN AdmCartao						ADM ON ADM.Id = CR.IdAdmCartao AND CR.idtipodocumento IN(10, 11)");
            sql.AppendLine("WHERE CR.Status <> 4  AND DATE_FORMAT(CRB.DataBaixa, '%Y-%m-%d') BETWEEN '" + dataInicial.ToString("yyyy-MM-dd") + "' AND '" + dataFinal.ToString("yyyy-MM-dd") + "' AND" + FiltroEmpresa("IdEmpresa", "CR"));

            if (VestilloSession.DesconsideraBancoPadrao)
                sql.AppendLine("    AND CRB.BancoId <> 999 ");

            sql.AppendLine("GROUP BY DATE_FORMAT(CRB.DataBaixa, '%m-%Y')");
            sql.AppendLine("		,CASE WHEN ADM.Id IS NOT NULL THEN ADM.Nome ELSE TD.Descricao END");

            sql.AppendLine("UNION ALL");

            sql.AppendLine("SELECT 	");
            sql.AppendLine("		DATE_FORMAT(CPB.DataBaixa, '%m-%Y') AS Mes,");
            sql.AppendLine("		N.Descricao AS Descricao,");

            if (VestilloSession.RealizaBaixaParcial)
            {   // alterado em 12/07/22 para não duplicar valor de cheque que agora tem query separada
                //sql.AppendLine("		(SUM(IFNULL(CPB.ValorDinheiro, 0)) + SUM(IFNULL(CPB.ValorCheque, 0))) - SUM(IFNULL(CPB.ValorCredito, 0))  AS Valor,");
                sql.AppendLine("		(SUM(IFNULL(CPB.ValorDinheiro, 0)))AS Valor ,");
            }
            else
            {
                sql.AppendLine("		(SUM(IFNULL(CPB.ValorDinheiro, 0)))AS Valor ,");
            }
                
                

            sql.AppendLine("		'DESPESA' AS Tipo");
            sql.AppendLine("FROM 	naturezasfinanceiras N");
            sql.AppendLine("	INNER JOIN contaspagar 						CP  ON CP.IdNaturezaFinanceira =  N.Id ");
            sql.AppendLine("	INNER JOIN contaspagarbaixa 				CPB ON CPB.ContasPagarId = CP.Id");
            sql.AppendLine("WHERE 	DATE_FORMAT(CPB.DataBaixa, '%Y-%m-%d') BETWEEN '" + dataInicial.ToString("yyyy-MM-dd") + "' AND '" + dataFinal.ToString("yyyy-MM-dd") + "' AND" + FiltroEmpresa("IdEmpresa", "CP"));

            if(VestilloSession.DesconsideraBancoPadrao)
                sql.AppendLine("    AND CPB.BancoId <> 999 ");

            sql.AppendLine("GROUP BY DATE_FORMAT(CPB.DataBaixa, '%m-%Y'),");
            sql.AppendLine("N.Descricao");

            //aqui cheque
            sql.AppendLine(" UNION ALL ");

            sql.AppendLine(" SELECT ");
            sql.AppendLine("   DATE_FORMAT(CHQ.Compensacao, '%m-%Y') AS Mes, ");
            sql.AppendLine("   '*Cheque' AS Descricao, ");
            sql.AppendLine(" SUM(IFNULL(CHQ.ValorCompensado, 0)) AS Valor, ");
            sql.AppendLine(" 'RECEITA' AS Tipo ");
            sql.AppendLine(" FROM cheques CHQ ");
            sql.AppendLine(" WHERE TipoEmitenteCheque = 1 AND DATE_FORMAT(CHQ.Compensacao, '%Y-%m-%d') BETWEEN '" + dataInicial.ToString("yyyy-MM-dd") + "' AND '" + dataFinal.ToString("yyyy-MM-dd") + "' AND" + FiltroEmpresa("EmpresaId", "CHQ"));

            if (VestilloSession.DesconsideraBancoPadrao)
                sql.AppendLine("    AND CHQ.BancoMovimentacaoId <> 999 ");

            sql.AppendLine(" GROUP BY DATE_FORMAT(CHQ.Compensacao, '%m-%Y'),Descricao ");


            sql.AppendLine(" UNION ALL ");

            sql.AppendLine(" SELECT ");
            sql.AppendLine("   DATE_FORMAT(CHQ.Compensacao, '%m-%Y') AS Mes, ");
            sql.AppendLine("   '*Cheque' AS Descricao, ");
            sql.AppendLine(" SUM(IFNULL(CHQ.ValorCompensado, 0)) AS Valor, ");
            sql.AppendLine(" 'DESPESA' AS Tipo ");
            sql.AppendLine(" FROM cheques CHQ ");
            sql.AppendLine(" WHERE TipoEmitenteCheque = 2 AND DATE_FORMAT(CHQ.Compensacao, '%Y-%m-%d') BETWEEN '" + dataInicial.ToString("yyyy-MM-dd") + "' AND '" + dataFinal.ToString("yyyy-MM-dd") + "' AND" + FiltroEmpresa("EmpresaId", "CHQ"));
            sql.AppendLine("  AND ISNULL(CHQ.ContasPagarBaixaId) ");

            if (VestilloSession.DesconsideraBancoPadrao)
                sql.AppendLine("    AND CHQ.BancoMovimentacaoId <> 999 ");

            sql.AppendLine(" GROUP BY DATE_FORMAT(CHQ.Compensacao, '%m-%Y'),Descricao ");

            sql.AppendLine("ORDER BY 1, 2, 3");        




            DapperConnection<RelatorioPlanoContasView> cn = new DapperConnection<RelatorioPlanoContasView>();
            return cn.ExecuteStringSqlToList(new RelatorioPlanoContasView(), sql.ToString());
        }
    }
}
