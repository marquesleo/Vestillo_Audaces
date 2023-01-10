using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo;
using Vestillo.Business.Models;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    class DespesaFixaVariavelMesRepository: GenericRepository<DespesaFixaVariavelMes>
    {
        public DespesaFixaVariavelMesRepository()
            : base(new DapperConnection<DespesaFixaVariavelMes>())
        {

        }


        public DespesaFixaVariavelMes GetByDespesaFixaVariavelEMes(int despesaFixaVariavelId, int mes)
        {
            DespesaFixaVariavelMes result = new DespesaFixaVariavelMes();
            _cn.ExecuteToModel("DespesaFixaVariavelId = " + despesaFixaVariavelId.ToString() + " AND mes = " + mes.ToString(), ref result);
            return result;
        }

        public IEnumerable<DespesaFixaVariavelMes> GetByDespesaFixaVariavel(int despesaFixaVariavelId)
        {
            return _cn.ExecuteToList(new DespesaFixaVariavelMes(), "DespesaFixaVariavelId = " + despesaFixaVariavelId.ToString());
        }

        public void DeleteByDespesaFixaVariavel(int despesaFixaVariavelId)
        {
            _cn.ExecuteNonQuery("DELETE FROM DespesaFixaVariavelMes WHERE DespesaFixaVariavelId = " + despesaFixaVariavelId.ToString());
        }

        public IEnumerable<DespesaFixaVariavelMes> GetDespesasNaturezas(int ano, string mes, int? naturezaId)
        {
            var SQL = new StringBuilder();

            SQL.AppendLine(" SELECT 0 AS Id, 0 AS DespesaFixaVariavelId, n.descricao AS Despesa, n.Id AS NaturezaFinanceiraId, " + mes +" AS mes, ");
            SQL.AppendLine(" (SELECT SUM(valorparcela) FROM contaspagar ");
            SQL.AppendLine("    WHERE YEAR(datavencimento) = "+ ano +" AND  MONTH(datavencimento) = "+ mes +" AND " + FiltroEmpresa("contaspagar.idempresa") + "AND contaspagar.Ativo = 1");
            SQL.AppendLine("    AND idnaturezafinanceira = n.id ");
            SQL.AppendLine("    GROUP BY idnaturezafinanceira) AS ValorPrevisto, ");
            SQL.AppendLine(" (SELECT SUM(ValorDinheiro + valorcheque + valorcredito)  ");
            SQL.AppendLine("    FROM contaspagarbaixa ");
            SQL.AppendLine("    INNER JOIN contaspagar ON contaspagar.id = contaspagarid  AND " + FiltroEmpresa("contaspagar.idempresa") + "AND contaspagar.Ativo = 1");
            SQL.AppendLine("    WHERE YEAR(databaixa) = " + ano + " AND  MONTH(databaixa) = " + mes );
            SQL.AppendLine("    AND idnaturezafinanceira = n.id ");
            SQL.AppendLine("    GROUP BY idnaturezafinanceira) AS ValorRealizado ");
            SQL.AppendLine(" FROM naturezasfinanceiras n ");
            SQL.AppendLine(" WHERE " + FiltroEmpresa("n.idempresa"));
            SQL.AppendLine("    AND n.automatico = 1 ");

            if(naturezaId != null && naturezaId > 0)
                SQL.AppendLine("    AND n.Id = " + naturezaId);

            SQL.AppendLine(" GROUP BY n.id ");
            

           return _cn.ExecuteStringSqlToList(new DespesaFixaVariavelMes(), SQL.ToString());

        }

        public void UpdateDespesasNaturezas(int ano, DespesaFixaVariavelMes despesaMes )
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("UPDATE despesafixavariavelmes SET ");
            SQL.AppendLine(" ValorPrevisto = " + despesaMes.ValorPrevisto.ToString().Replace(",", "."));
            SQL.AppendLine(",  ValorRealizado = " + despesaMes.ValorRealizado.ToString().Replace(",", "."));
            SQL.AppendLine(" WHERE mes = " + despesaMes.Mes + " AND NaturezaFinanceiraId = " + despesaMes.NaturezaFinanceiraId);
            SQL.AppendLine(" AND DespesaFixaVariavelId = ");
            SQL.Append(" IFNULL((SELECT Id FROM despesafixavariavel WHERE ano = " + ano + " AND AutomatizarContasPagar = 1 AND "+ FiltroEmpresa("despesafixavariavel.EmpresaId") + " ), 0) ");

            _cn.ExecuteNonQuery(SQL.ToString());
        }

        public void DeleteByNaturezaFinanceira(int idNatFinanceira)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine(" DELETE FROM despesafixavariavelmes WHERE  NaturezaFinanceiraId = " + idNatFinanceira);            

            _cn.ExecuteNonQuery(SQL.ToString());
        }
    }
}
