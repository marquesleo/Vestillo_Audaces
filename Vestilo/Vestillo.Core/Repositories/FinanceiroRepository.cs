using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Core.Models;
using Vestillo.Core.Connection;
using Vestillo.Lib;

namespace Vestillo.Core.Repositories
{
    public class FinanceiroRepository : GenericRepository<Titulo>
    {
        public IEnumerable<Titulo> ListParcelasEmAbertoPorVendedores(int[] vendedores)
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT	CR.*,");
            sql.AppendLine("        IFNULL(C.Nome, CR.NomeCliente) AS NomeCliente,C.DebitoAntigo, C.Referencia AS RefCliente, IFNULL(CR.Status, 0) AS Status, TC.Descricao AS TipoCobrancaDescricao,IF(CR.PossuiAtividade = 0,'Não','Sim') as SimNao ");
            sql.AppendLine("FROM 	contasreceber CR");
            sql.AppendLine("    LEFT JOIN colaboradores AS C ON C.Id = CR.IdCliente");
            sql.AppendLine("    LEFT JOIN bancos AS B ON B.Id = CR.IdBanco");
            sql.AppendLine("    LEFT JOIN TiposCobranca TC ON TC.Id = CR.IdTipoCobranca");
            sql.AppendLine("WHERE 	CR.Status IN (1, 2)");
            sql.AppendLine("		AND CR.Ativo = 1");
            sql.AppendLine("		AND CR.Prefixo <> 'NEG' ");
            sql.AppendLine("        AND " + FiltroEmpresa("IdEmpresa", "CR"));
            sql.AppendLine("        AND CR.IdVendedor IN(" + string.Join(", ", vendedores) + ")");
            sql.AppendLine("ORDER BY CR.DataVencimento, CR.NumTitulo, CR.Prefixo, CR.Parcela");

            return VestilloConnection.ExecSQLToList<Titulo>(sql.ToString());
        }


        public IEnumerable<Titulo> ListParcelasBaixadasHoje(int[] vendedores)
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT	CR.*");
            sql.AppendLine("        ,(SELECT SUM(ValorDinheiro) + SUM(ValorCheque) FROM contasreceberbaixa CB WHERE CB.ContasReceberId = CR.Id AND DATEDIFF(CURDATE(), DATE_FORMAT(CB.DataBaixa, '%Y-%m-%d')) = 1) AS ValorPagoDia");
            sql.AppendLine("        ,IFNULL(C.Nome, CR.NomeCliente) AS NomeCliente,C.DebitoAntigo, C.Referencia AS RefCliente, IFNULL(CR.Status, 0) AS Status, TC.Descricao AS TipoCobrancaDescricao ");
            sql.AppendLine("FROM 	contasreceber CR");
            sql.AppendLine("    LEFT JOIN colaboradores AS C ON C.Id = CR.IdCliente");
            sql.AppendLine("    LEFT JOIN bancos AS B ON B.Id = CR.IdBanco");
            sql.AppendLine("    LEFT JOIN TiposCobranca TC ON TC.Id = CR.IdTipoCobranca");
            sql.AppendLine("WHERE 	CR.Status IN (2,3) ");
            sql.AppendLine("		AND CR.Ativo = 1");
            sql.AppendLine("		AND CR.Prefixo <> 'NEG' ");
            sql.AppendLine("        AND " + FiltroEmpresa("IdEmpresa", "CR"));
            sql.AppendLine("        AND CR.IdVendedor IN(" + string.Join(", ", vendedores) + ")");
            sql.AppendLine("        AND (SELECT COUNT(1) FROM contasreceberbaixa CB WHERE CB.ContasReceberId = CR.Id AND DATEDIFF(CURDATE(), DATE_FORMAT(CB.DataBaixa, '%Y-%m-%d')) = 1) > 0");
            sql.AppendLine("ORDER BY CR.DataVencimento");

            return VestilloConnection.ExecSQLToList<Titulo>(sql.ToString());
        }

        public IEnumerable<PedidoVendaDashFinanceiro> ListPedidoVendedoresParaDash(int[] vendedores)
        {
          
            var sql = new StringBuilder();
            sql.AppendLine("SELECT p.*,");
            sql.AppendLine("cliente.referencia as RefCliente,");
            sql.AppendLine("cliente.nome as NomeCliente,");
            sql.AppendLine("cliente.razaosocial as RazaoColaborador,");
            sql.AppendLine("vendedor.referencia as RefVendedor,");
            sql.AppendLine("vendedor.nome as NomeVendedor,");
            sql.AppendLine("vendedor2.referencia as RefVendedor2,");
            sql.AppendLine("vendedor2.nome as NomeVendedor2,");
            sql.AppendLine("transportadora.referencia as RefTransportadora,");
            sql.AppendLine("transportadora.nome as NomeTransportadora,");
            sql.AppendLine("r.referencia as RefRotaVisita,");
            sql.AppendLine("r.descricao as DescricaoRotaVisita,");
            sql.AppendLine("tp.referencia as RefTabelaPreco,");
            sql.AppendLine("tp.descricao as DescricaoTabelaPreco");
            sql.AppendLine("FROM 	pedidovenda p");
            sql.AppendLine("LEFT JOIN tabelaspreco tp on tp.id	= p.TabelaPrecoId");
            sql.AppendLine("INNER JOIN colaboradores cliente ON cliente.id = p.ClienteId");
            sql.AppendLine("LEFT JOIN colaboradores vendedor ON vendedor.id = p.VendedorId");
            sql.AppendLine("LEFT JOIN colaboradores vendedor2 ON vendedor2.id = p.VendedorId2");
            sql.AppendLine("LEFT JOIN colaboradores transportadora ON transportadora.id = p.TransportadoraId");
            sql.AppendLine("LEFT JOIN rotavisitas r ON r.id = p.RotaId");
            sql.AppendLine("WHERE 	p.Status IN (6, 7, 8, 9, 10,11)");
            sql.AppendLine("        AND " + FiltroEmpresa("EmpresaId", "p"));
            sql.AppendLine("        AND cliente.IdVendedor IN(" + string.Join(", ", vendedores) + ")");
            sql.AppendLine("ORDER BY p.Referencia");

            return VestilloConnection.ExecSQLToList<PedidoVendaDashFinanceiro>(sql.ToString());
        }

        public DespesaXReceitaDashFinanceiro GetDespesaEReceita(int[] vendedores, DateTime data, bool considerarPenasMes = true)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT ");

            sql.AppendLine("(SELECT");
            sql.AppendLine("		IFNULL(SUM(IFNULL(CRB.ValorDinheiro, 0)) + SUM(IFNULL(CRB.ValorCheque, 0)), 0)");
            sql.AppendLine("FROM 	naturezasfinanceiras N");
            sql.AppendLine("	INNER JOIN contasreceber 					CR  ON CR.IdNaturezaFinanceira =  N.Id ");
            sql.AppendLine("	INNER JOIN contasreceberbaixa 				CRB ON CRB.ContasReceberId = CR.Id");
            sql.AppendLine("    INNER JOIN tipodocumentos 					TD  ON TD.Id = CR.IdTipoDocumento");
            sql.AppendLine("WHERE   " + FiltroEmpresa("IdEmpresa", "CR"));
            sql.AppendLine("        AND IdVendedor IN(" + string.Join(", ", vendedores) + ")");
            sql.AppendLine("        AND IFNULL(CR.Prefixo,'') <> 'NEG'");

            if (considerarPenasMes)
                sql.AppendLine("       AND DATE_FORMAT(CRB.DataBaixa, '%Y-%m') = '" + data.ToString("yyyy-MM") + "'");  
            else
                sql.AppendLine("       AND DATE_FORMAT(CRB.DataBaixa, '%Y-%m-%d') = '" + data.ToString("yyyy-MM-dd") + "'");  

            sql.AppendLine(") AS ValorReceita ");

            sql.AppendLine(",(SELECT 	");
            sql.AppendLine("		IFNULL(SUM(IFNULL(CPB.ValorDinheiro, 0)) + SUM(IFNULL(CPB.ValorCheque, 0)), 0)");
            sql.AppendLine("FROM 	naturezasfinanceiras N");
            sql.AppendLine("	INNER JOIN contaspagar 						CP  ON CP.IdNaturezaFinanceira =  N.Id ");
            sql.AppendLine("	INNER JOIN contaspagarbaixa 				CPB ON CPB.ContasPagarId = CP.Id");
            sql.AppendLine("WHERE 	" + FiltroEmpresa("IdEmpresa", "CP"));
            if (considerarPenasMes)
                sql.AppendLine("       AND DATE_FORMAT(CPB.DataBaixa, '%Y-%m') = '" + data.ToString("yyyy-MM") + "'");
            else
                sql.AppendLine("       AND DATE_FORMAT(CPB.DataBaixa, '%Y-%m-%d') = '" + data.ToString("yyyy-MM-dd") + "'");  
            sql.AppendLine(") AS ValorDespesa ");
            sql.AppendLine("FROM DUAL");

            return VestilloConnection.ExecSQLToModel<DespesaXReceitaDashFinanceiro>(sql.ToString());
        }

        public DadosFaturamentoDashFinanceiro GetDadosFaturamentoDashFinanceiro(int[] vendedores)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT ");
            sql.AppendLine("(SELECT   ");
            sql.AppendLine("    IFNULL(ROUND(((SUM(IFNULL(n.TotalProdutos,0)) - SUM(IFNULL(n.ValDesconto,0)))),2),0)  ");
            sql.AppendLine("FROM nfe n");
            sql.AppendLine("    INNER JOIN colaboradores cli ON cli.id = n.IdColaborador ");
            sql.AppendLine("    INNER JOIN colaboradores ven ON ven.id = n.idvendedor ");
            sql.AppendLine("WHERE  Tipo = 0  AND StatusNota <> 2 AND DATE_FORMAT(n.DataInclusao, '%m-%Y') ");
            sql.AppendLine("	        AND n.idvendedor IN (" + string.Join(", ", vendedores) + ")");
            sql.AppendLine("            AND " + FiltroEmpresa("IdEmpresa", "n"));
            sql.AppendLine("            AND DATE_FORMAT(n.DataInclusao, '%Y-%m-%d') = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'");  
            sql.AppendLine(") AS ValorFaturadoHoje ");
            sql.AppendLine(",(SELECT   ");
            sql.AppendLine("    COUNT(1)  ");
            sql.AppendLine("FROM nfe n");
            sql.AppendLine("    INNER JOIN colaboradores cli ON cli.id = n.IdColaborador ");
            sql.AppendLine("    INNER JOIN colaboradores ven ON ven.id = n.idvendedor ");
            sql.AppendLine("WHERE  Tipo = 0 AND StatusNota <> 2 AND DATE_FORMAT(n.DataInclusao, '%m-%Y') ");
            sql.AppendLine("	        AND n.idvendedor IN (" + string.Join(", ", vendedores) + ")");
            sql.AppendLine("            AND " + FiltroEmpresa("IdEmpresa", "n"));
            sql.AppendLine("            AND DATE_FORMAT(n.DataInclusao, '%Y-%m-%d') = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'");  
            sql.AppendLine(") AS QtdFaturadoHoje ");
            sql.AppendLine(",(SELECT   ");
            sql.AppendLine("    IFNULL(ROUND(((SUM(IFNULL(n.TotalProdutos,0)) - SUM(IFNULL(n.ValDesconto,0)))),2),0)  ");
            sql.AppendLine("FROM nfe n");
            sql.AppendLine("    INNER JOIN colaboradores cli ON cli.id = n.IdColaborador ");
            sql.AppendLine("    INNER JOIN colaboradores ven ON ven.id = n.idvendedor ");
            sql.AppendLine("WHERE  Tipo = 0 AND StatusNota <> 2 AND DATE_FORMAT(n.DataInclusao, '%m-%Y') ");
            sql.AppendLine("	        AND n.idvendedor IN (" + string.Join(", ", vendedores) + ")");
            sql.AppendLine("            AND " + FiltroEmpresa("IdEmpresa", "n"));
            sql.AppendLine("            AND DATE_FORMAT(n.DataInclusao, '%Y-%m') = '" + DateTime.Now.ToString("yyyy-MM") + "'");
            sql.AppendLine(") AS ValorAcumuladoMes ");
            sql.AppendLine(",(SELECT   ");
            sql.AppendLine("    COUNT(1)  ");
            sql.AppendLine("FROM nfe n");
            sql.AppendLine("    INNER JOIN colaboradores cli ON cli.id = n.IdColaborador ");
            sql.AppendLine("    INNER JOIN colaboradores ven ON ven.id = n.idvendedor ");
            sql.AppendLine("WHERE  Tipo = 0 AND StatusNota <> 2 AND DATE_FORMAT(n.DataInclusao, '%m-%Y') ");
            sql.AppendLine("	        AND n.idvendedor IN (" + string.Join(", ", vendedores) + ")");
            sql.AppendLine("            AND " + FiltroEmpresa("IdEmpresa", "n"));
            sql.AppendLine("            AND DATE_FORMAT(n.DataInclusao, '%Y-%m') = '" + DateTime.Now.ToString("yyyy-MM") + "'");
            sql.AppendLine(") AS QtdAcumuladoMes ");
            sql.AppendLine("FROM DUAL");

            return VestilloConnection.ExecSQLToModel<DadosFaturamentoDashFinanceiro>(sql.ToString());
        }

        public IEnumerable<Titulo> ListParcelasEmProtestoPorVendedores(int[] vendedores)
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT	CR.*,");
            sql.AppendLine("        IFNULL(C.Nome, CR.NomeCliente) AS NomeCliente,C.DebitoAntigo, C.Referencia AS RefCliente, IFNULL(CR.Status, 0) AS Status, TC.Descricao AS TipoCobrancaDescricao,IF(CR.PossuiAtividade = 0,'Não','Sim') as SimNao ");
            sql.AppendLine("FROM 	contasreceber CR");
            sql.AppendLine("    LEFT JOIN colaboradores AS C ON C.Id = CR.IdCliente");
            sql.AppendLine("    LEFT JOIN bancos AS B ON B.Id = CR.IdBanco");
            sql.AppendLine("    LEFT JOIN TiposCobranca TC ON TC.Id = CR.IdTipoCobranca");
            sql.AppendLine("WHERE 	CR.Status IN (1, 2)");
            sql.AppendLine("		AND CR.IdTipoCobranca = 9");
            sql.AppendLine("		AND CR.Ativo = 1");
            sql.AppendLine("		AND CR.Prefixo <> 'NEG' ");
            sql.AppendLine("        AND " + FiltroEmpresa("IdEmpresa", "CR"));
            sql.AppendLine("        AND CR.IdVendedor IN(" + string.Join(", ", vendedores) + ")");
            sql.AppendLine("ORDER BY CR.DataVencimento, CR.NumTitulo, CR.Prefixo, CR.Parcela");

            return VestilloConnection.ExecSQLToList<Titulo>(sql.ToString());
        }

        public IEnumerable<Titulo> ListParcelasEmProtestoPorVendedoresRecuperados(int[] vendedores)
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT	CR.*,");
            sql.AppendLine("        IFNULL(C.Nome, CR.NomeCliente) AS NomeCliente,C.DebitoAntigo, C.Referencia AS RefCliente, IFNULL(CR.Status, 0) AS Status, TC.Descricao AS TipoCobrancaDescricao,IF(CR.PossuiAtividade = 0,'Não','Sim') as SimNao ");
            sql.AppendLine("FROM 	contasreceber CR");
            sql.AppendLine("    LEFT JOIN colaboradores AS C ON C.Id = CR.IdCliente");
            sql.AppendLine("    LEFT JOIN bancos AS B ON B.Id = CR.IdBanco");
            sql.AppendLine("    LEFT JOIN TiposCobranca TC ON TC.Id = CR.IdTipoCobranca");
            sql.AppendLine("WHERE 	");
            sql.AppendLine("		CR.IdTipoCobranca = 9");
            sql.AppendLine("		AND NOT ISNULL(CR.DataPagamento)");
            sql.AppendLine("		AND DATEDIFF (now(), CR.DataPagamento) <= 30");
            sql.AppendLine("		AND CR.Ativo = 1");
            sql.AppendLine("		AND CR.Prefixo <> 'NEG' ");
            sql.AppendLine("        AND " + FiltroEmpresa("IdEmpresa", "CR"));
            sql.AppendLine("        AND CR.IdVendedor IN(" + string.Join(", ", vendedores) + ")");
            sql.AppendLine("ORDER BY CR.DataVencimento, CR.NumTitulo, CR.Prefixo, CR.Parcela");

            return VestilloConnection.ExecSQLToList<Titulo>(sql.ToString());
        }

        public IEnumerable<Titulo> ListParcelasRenegociados1A30(int[] vendedores)
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT CR.* ,");
            sql.AppendLine("        IFNULL(C.Nome, CR.NomeCliente) AS NomeCliente,C.DebitoAntigo, C.Referencia AS RefCliente, IFNULL(CR.Status, 0) AS Status, IF(CR.PossuiAtividade = 0,'Não','Sim') as SimNao ");
            sql.AppendLine(" from contasreceber CR ");
            sql.AppendLine("    LEFT JOIN colaboradores AS C ON C.Id = CR.IdCliente");
            sql.AppendLine("    LEFT JOIN bancos AS B ON B.Id = CR.IdBanco");
            sql.AppendLine("   WHERE CR.Id IN (");
            sql.AppendLine("     select  contasreceber.idTitulopai from contasreceber where NOT ISNULL(idTitulopai) ");
            sql.AppendLine(" AND DATEDIFF (now(), contasreceber.DataEmissao) <= 30 GROUP BY idTitulopai)");
            sql.AppendLine("	AND	CR.IdTipoCobranca <> 9");
            sql.AppendLine("        AND " + FiltroEmpresa("IdEmpresa", "CR"));
            sql.AppendLine("        AND CR.IdVendedor IN(" + string.Join(", ", vendedores) + ")");
            sql.AppendLine("ORDER BY CR.DataVencimento, CR.NumTitulo, CR.Prefixo, CR.Parcela");

            return VestilloConnection.ExecSQLToList<Titulo>(sql.ToString());
        }

        public IEnumerable<Titulo> ListParcelasComAtividadeEQuitado(int[] vendedores)
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT CR.* ,");
            sql.AppendLine("        IFNULL(C.Nome, CR.NomeCliente) AS NomeCliente,C.DebitoAntigo, C.Referencia AS RefCliente, IFNULL(CR.Status, 0) AS Status, IF(CR.PossuiAtividade = 0,'Não','Sim') as SimNao  ");
            sql.AppendLine(" from contasreceber CR ");
            sql.AppendLine("    LEFT JOIN colaboradores AS C ON C.Id = CR.IdCliente");
            sql.AppendLine("    LEFT JOIN TiposCobranca TC ON TC.Id = CR.IdTipoCobranca");
            sql.AppendLine("    LEFT JOIN bancos AS B ON B.Id = CR.IdBanco");
            sql.AppendLine("   WHERE IFNULL(CR.PossuiAtividade,0) = 1  ");
            sql.AppendLine("   AND CR.IdTipoCobranca <> 9  ");
            sql.AppendLine("     AND NOT ISNULL(CR.DataPagamento) ");
            sql.AppendLine("  AND  DATEDIFF (now(), CR.DataPagamento) <= 30");
            sql.AppendLine("        AND " + FiltroEmpresa("IdEmpresa", "CR"));
            sql.AppendLine("        AND CR.IdVendedor IN(" + string.Join(", ", vendedores) + ")");
            sql.AppendLine("ORDER BY CR.DataVencimento, CR.NumTitulo, CR.Prefixo, CR.Parcela");

            return VestilloConnection.ExecSQLToList<Titulo>(sql.ToString());
        }

    }
}
