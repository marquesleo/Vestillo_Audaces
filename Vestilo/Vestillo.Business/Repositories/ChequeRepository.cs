using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Connection;
using Vestillo.Lib;

namespace Vestillo.Business.Repositories
{
    public class ChequeRepository: GenericRepository<Cheque>
    {
        public ChequeRepository()
            : base(new DapperConnection<Cheque>())
        {
        }

        public IEnumerable<Cheque> GetListaPorCampoEValor(string campoBusca, string valor)
        {
            return _cn.ExecuteToList(new Cheque(), campoBusca + " = " + valor.ToString());
        }

        public IEnumerable<Cheque> GetByContasReceberBaixa(int contasReceberBaixaId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT * FROM cheques WHERE contasreceberbaixaid = " );
            SQL.Append(contasReceberBaixaId);
            SQL.AppendLine("        AND " + FiltroEmpresa());

            return _cn.ExecuteStringSqlToList(new Cheque(), SQL.ToString());
        }

        public IEnumerable<Cheque> GetByContasPagarBaixa(int contasPagarBaixaId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT * FROM cheques WHERE contaspagarbaixaid = ");
            SQL.Append(contasPagarBaixaId);
            SQL.AppendLine("        AND " + FiltroEmpresa());

            return _cn.ExecuteStringSqlToList(new Cheque(), SQL.ToString());
        }

        public IEnumerable<Cheque> GetByBordero(int borderoId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT * FROM cheques WHERE borderoId = ");
            SQL.Append(borderoId);
            SQL.AppendLine("        AND " + FiltroEmpresa());

            return _cn.ExecuteStringSqlToList(new Cheque(), SQL.ToString());
        }

        public IEnumerable<ChequeView> GetAllView()
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT C.*, (CASE WHEN CR.NumTitulo IS NULL THEN CP.NumTitulo ELSE CR.NumTitulo END) AS NumTitulo, ");
            SQL.AppendLine("CO.Nome AS NomeCliente,  (CASE WHEN C.TipoEmitenteCheque = 1 THEN 'Cliente' ELSE 'Empresa' END) AS  TipoEmitenteChequeDesc");
            SQL.AppendLine("FROM cheques C");
            SQL.AppendLine("LEFT JOIN ContasReceberBaixa CRB ON CRB.Id = C.ContasReceberBaixaId");
            SQL.AppendLine("LEFT JOIN ContasPagarBaixa CPB ON CPB.Id = C.ContasPagarBaixaId");
            SQL.AppendLine("LEFT JOIN contasreceber CR ON CR.Id = CRB.ContasReceberId");
            SQL.AppendLine("LEFT JOIN contaspagar CP ON CP.Id = CPB.ContasPagarId");
            SQL.AppendLine("LEFT JOIN colaboradores CO  ON CO.Id = C.ColaboradorId");
            SQL.AppendLine("WHERE " + FiltroEmpresa("", "C"));
           
            var cn = new DapperConnection<ChequeView>();
            return cn.ExecuteStringSqlToList(new ChequeView(), SQL.ToString());
        }

        public IEnumerable<ConsultaChequeRelatorio> GetChequeRelatorio(FiltroChequeRelatorio filtro)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT c.TipoEmitenteCheque as Tipo, c.Status as Status, c.Banco as BancoCheque, c.Agencia as AgenciaCheque, c.Conta as ContaCheque, c.NumeroCheque as Numero,");
            SQL.AppendLine("    c.Valor as Valor, c.DataEmissao as Emissao, c.DataVencimento as Vencimento, ");
            SQL.AppendLine("    CASE WHEN c.Status IN (1) THEN NULL ELSE c.Compensacao END as Compensacao, ");
            SQL.AppendLine("    COL.nome as NomeCliente, c.DeTerceiro as DeTerceiro, b.id as IdBanco, b.agencia as BancoAgencia, b.conta as BancoConta, b.descricao as BancoDescricao, ");
            SQL.AppendLine("    IF(c.ContasReceberBaixaId IS NOT NULL AND c.ContasPagarBaixaId IS NOT NULL, ");
            SQL.AppendLine("        IF(hc.observacao LIKE '%Baixa Contas a Pagar%', 'Contas Pagar', 'Contas Receber'), ");
            SQL.AppendLine("        IF(c.ContasPagarBaixaId IS NOT NULL, 'Contas Pagar', 'Contas Receber') ");
            SQL.AppendLine("    ) AS UtilizadoEm, ");
            SQL.AppendLine("    IF(c.ContasReceberBaixaId IS NOT NULL AND c.ContasPagarBaixaId IS NOT NULL, ");
            SQL.AppendLine("        IF(hc.observacao LIKE '%Baixa Contas a Pagar%', CONCAT(cp.NumTitulo, ' - ', cp.NomeFornecedor), CONCAT(cr.NumTitulo, ' - ', cr.NomeCliente)), ");
            SQL.AppendLine("        IF(c.ContasPagarBaixaId IS NOT NULL, CONCAT(cp.NumTitulo, ' - ', cp.NomeFornecedor), CONCAT(cr.NumTitulo, ' - ', cr.NomeCliente)) ");
            SQL.AppendLine("    ) AS TituloUtilizado ");
            SQL.AppendLine("FROM cheques C");
            SQL.AppendLine("LEFT JOIN bancos b ON C.BancoMovimentacaoId = b.id");
            SQL.AppendLine("LEFT JOIN colaboradores COL ON C.ColaboradorId = COL.id");
            SQL.AppendLine("LEFT JOIN contaspagarbaixa CPB ON CPB.id = c.ContasPagarBaixaId");
            SQL.AppendLine("LEFT JOIN contaspagar CP ON CP.id = CPB.ContasPagarId");
            SQL.AppendLine("LEFT JOIN contasreceberbaixa CRB ON CRB.id = c.ContasReceberBaixaId");
            SQL.AppendLine("LEFT JOIN contasreceber CR ON CR.id = CRB.ContasReceberId");
            SQL.AppendLine("LEFT JOIN historicocheque HC ON HC.chequeid = c.id ");
            SQL.AppendLine("WHERE " + FiltroEmpresa("c.EmpresaId"));
            SQL.AppendLine("    AND HC.id IN (SELECT MAX(id) FROM historicocheque GROUP BY chequeid ORDER BY id DESC) ");

            if (filtro.Banco != null && filtro.Banco.Count() > 0)
                SQL.AppendLine(" AND c.BancoMovimentacaoId in (" + string.Join(",", filtro.Banco.ToArray()) + ")");

            if (filtro.Conta != null && filtro.Conta != "")
                SQL.AppendLine(" AND c.conta like '%" + filtro.Conta + "%' ");

            if (filtro.Agencia != null && filtro.Agencia != "")
                SQL.AppendLine(" AND c.agencia like '%" + filtro.Agencia + "%' ");

            if (filtro.NumeroCheque != null && filtro.NumeroCheque.Count() > 0)
                SQL.AppendLine(" AND c.id in (" + string.Join(",", filtro.NumeroCheque.ToArray()) + ")");

            if (filtro.Cliente != null && filtro.Cliente.Count() > 0)
                SQL.AppendLine(" AND c.Colaboradorid in (" + string.Join(",", filtro.Cliente.ToArray()) + ")");

            if (filtro.DaEmissao != null && filtro.AteEmissao != null && filtro.DaEmissao != "" && filtro.AteEmissao != "")
                SQL.AppendLine(" AND Date(c.DataEmissao) between '" + filtro.DaEmissao + "' AND '" + filtro.AteEmissao + "'");

            if (filtro.DoVencimento != null && filtro.AteVencimento != null && filtro.DoVencimento != "" && filtro.AteVencimento != "")
                SQL.AppendLine(" AND Date(c.DataVencimento) between '" + filtro.DoVencimento + "' AND '" + filtro.AteVencimento + "'");

            if(filtro.Compensado)//se compensasao foi habilitado
            {
                if (filtro.DoCompensado != null && filtro.AteCompensado != null)
                    SQL.AppendLine(" AND Date(c.Compensacao) between '" + filtro.DoCompensado + "' AND '" + filtro.AteCompensado + "'");
            }

            if (filtro.StatusDevolvido || filtro.StatusCompensado || filtro.StatusIncluido || filtro.StatusProrrogado || filtro.StatusResgatado)
              {
                  SQL.AppendLine(" AND (");

                  if (filtro.StatusIncluido)
                      SQL.Append(" c.Status IN (1)");

                  if (filtro.StatusCompensado && filtro.StatusIncluido)
                      SQL.Append(" OR c.Status IN (2)");
                  else if (filtro.StatusCompensado)
                      SQL.Append("c.Status IN (2)");

                  if (filtro.StatusDevolvido && (filtro.StatusIncluido || filtro.StatusCompensado))
                      SQL.Append(" OR c.Status = 3 ");
                  else if (filtro.StatusDevolvido) 
                      SQL.Append(" c.Status = 3 ");

                  if (filtro.StatusProrrogado && (filtro.StatusIncluido || filtro.StatusCompensado || filtro.StatusDevolvido))
                      SQL.Append(" OR c.Status = 4 ");
                  else if (filtro.StatusProrrogado)
                      SQL.Append(" c.Status = 4 ");

                  if (filtro.StatusResgatado && (filtro.StatusIncluido || filtro.StatusCompensado || filtro.StatusDevolvido || filtro.StatusProrrogado))
                      SQL.Append(" OR c.Status = 5 ");
                  else if (filtro.StatusResgatado)
                      SQL.Append(" c.Status = 5 ");

                  SQL.Append(")");
              }

            if (filtro.TipoCliente || filtro.TipoEmpresa)
            {
                SQL.AppendLine(" AND (");

                if (filtro.TipoCliente)
                    SQL.Append(" c.tipoEmitenteCheque  IN (1)");

                if (filtro.TipoEmpresa && filtro.TipoCliente)
                    SQL.Append(" OR c.tipoEmitenteCheque  IN (2)");
                else if (filtro.TipoEmpresa)
                    SQL.Append("c.tipoEmitenteCheque  IN (2)");

                SQL.Append(")");
            }

            switch (filtro.Ordenar)
            {
                case 0:
                    SQL.AppendLine(" ORDER BY b.id");
                    break;
                case 1:
                    SQL.AppendLine(" ORDER BY c.conta");
                    break;
                case 2:
                    SQL.AppendLine(" ORDER BY c.agencia");
                    break;
                case 3:
                    SQL.AppendLine(" ORDER BY c.NumeroCheque");
                    break;
                case 4:
                    SQL.AppendLine(" ORDER BY col.referencia");
                    break;
                case 5:
                    SQL.AppendLine(" ORDER BY c.DataEmissao");
                    break;
                case 6:
                    SQL.AppendLine(" ORDER BY c.DataVencimento");
                    break;
                default:
                    break;
            }


            var cn = new DapperConnection<ConsultaChequeRelatorio>();
            return cn.ExecuteStringSqlToList(new ConsultaChequeRelatorio(), SQL.ToString());
        }

        public IEnumerable<ChequeView>GetByNumeroCheque(string numeroCheque, bool naoCompensado = false)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT C.*, (CASE WHEN CR.NumTitulo IS NULL THEN CP.NumTitulo ELSE CR.NumTitulo END) AS NumTitulo, ");
            SQL.AppendLine("CO.Nome AS NomeCliente");
            SQL.AppendLine("FROM cheques C");
            SQL.AppendLine("LEFT JOIN ContasReceberBaixa CRB ON CRB.Id = C.ContasReceberBaixaId");
            SQL.AppendLine("LEFT JOIN ContasPagarBaixa CPB ON CPB.Id = C.ContasPagarBaixaId");
            SQL.AppendLine("LEFT JOIN contasreceber CR ON CR.Id = CRB.ContasReceberId");
            SQL.AppendLine("LEFT JOIN contaspagar CP ON CP.Id = CPB.ContasPagarId");
            SQL.AppendLine("LEFT JOIN colaboradores CO  ON CO.Id = C.ColaboradorId");
            SQL.AppendLine("WHERE C.NumeroCheque LIKE '%" + numeroCheque.Trim() + "%'");
            SQL.AppendLine("        AND " + FiltroEmpresa("", "C"));
            if(naoCompensado)
                SQL.AppendLine("        AND C.Status <> 2");

            var cn = new DapperConnection<ChequeView>();
            return cn.ExecuteStringSqlToList(new ChequeView(), SQL.ToString());
        }

        public IEnumerable<ChequeView> GetByReferencia(string referencia, bool naoCompensado = false)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT C.*, (CASE WHEN CR.NumTitulo IS NULL THEN CP.NumTitulo ELSE CR.NumTitulo END) AS NumTitulo, ");
            SQL.AppendLine("CO.Nome AS NomeCliente");
            SQL.AppendLine("FROM cheques C");
            SQL.AppendLine("LEFT JOIN ContasReceberBaixa CRB ON CRB.Id = C.ContasReceberBaixaId");
            SQL.AppendLine("LEFT JOIN ContasPagarBaixa CPB ON CPB.Id = C.ContasPagarBaixaId");
            SQL.AppendLine("LEFT JOIN contasreceber CR ON CR.Id = CRB.ContasReceberId");
            SQL.AppendLine("LEFT JOIN contaspagar CP ON CP.Id = CPB.ContasPagarId");
            SQL.AppendLine("LEFT JOIN colaboradores CO  ON CO.Id = C.ColaboradorId");
            SQL.AppendLine("WHERE C.Referencia LIKE '%" + referencia.Trim() + "%'");
            SQL.AppendLine("        AND " + FiltroEmpresa("", "C"));
            if (naoCompensado)
                SQL.AppendLine("        AND C.Status <> 2");

            var cn = new DapperConnection<ChequeView>();
            return cn.ExecuteStringSqlToList(new ChequeView(), SQL.ToString());
        }

        public ChequeView GetViewById(int id)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT C.*, (CASE WHEN CR.NumTitulo IS NULL THEN CP.NumTitulo ELSE CR.NumTitulo END) AS NumTitulo, ");
            SQL.AppendLine("CO.Nome AS NomeCliente");
            SQL.AppendLine("FROM cheques C");
            SQL.AppendLine("LEFT JOIN ContasReceberBaixa CRB ON CRB.Id = C.ContasReceberBaixaId");
            SQL.AppendLine("LEFT JOIN ContasPagarBaixa CPB ON CPB.Id = C.ContasPagarBaixaId");
            SQL.AppendLine("LEFT JOIN contasreceber CR ON CR.Id = CRB.ContasReceberId");
            SQL.AppendLine("LEFT JOIN contaspagar CP ON CP.Id = CPB.ContasPagarId");
            SQL.AppendLine("LEFT JOIN colaboradores CO  ON CO.Id = C.ColaboradorId");
            SQL.AppendLine("WHERE C.Id = " + id);

            var cn = new DapperConnection<ChequeView>();
            var ret = new ChequeView();
            cn.ExecuteToModel(ref ret, SQL.ToString());
            return ret;
        }

        public List<Cheque> GetByData(DateTime data)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT SUM(C.Valor) as Valor, C.TipoEmitenteCheque");
            SQL.AppendLine("FROM cheques C");
            SQL.AppendLine("WHERE DATE(C.DataVencimento) = '" + data.ToString("yyyy-MM-dd") + "'  AND C.Status = 1");
            SQL.AppendLine("        AND " + FiltroEmpresa("", "C"));
            SQL.AppendLine("GROUP BY C.TipoEmitenteCheque");

            var cn = new DapperConnection<Cheque>();
            return cn.ExecuteStringSqlToList(new Cheque(), SQL.ToString()).ToList();
        }

        public List<Cheque> GetByAteData(DateTime data)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT SUM(C.Valor) as Valor, C.TipoEmitenteCheque");
            SQL.AppendLine("FROM cheques C");
            SQL.AppendLine("WHERE DATE(C.DataVencimento) <= '" + data.ToString("yyyy-MM-dd") + "' AND C.Status = 1");
            SQL.AppendLine("        AND " + FiltroEmpresa("", "C"));
            SQL.AppendLine("GROUP BY C.TipoEmitenteCheque");

            var cn = new DapperConnection<Cheque>();
            return cn.ExecuteStringSqlToList(new Cheque(), SQL.ToString()).ToList();
        }

        public List<Cheque> GetByDataEBanco(DateTime data, int bancoId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT SUM(C.Valor) as Valor, C.TipoEmitenteCheque");
            SQL.AppendLine("FROM cheques C");
            SQL.AppendLine("WHERE DATE(C.DataVencimento) = '" + data.ToString("yyyy-MM-dd") + "'  AND C.Status = 1  AND C.BancoMovimentacaoId = " + bancoId);
            SQL.AppendLine("        AND " + FiltroEmpresa("", "C"));
            SQL.AppendLine("GROUP BY C.TipoEmitenteCheque");

            var cn = new DapperConnection<Cheque>();
            return cn.ExecuteStringSqlToList(new Cheque(), SQL.ToString()).ToList();
        }

        public List<Cheque> GetByAteDataEBanco(DateTime data, int bancoId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT SUM(C.Valor) as Valor, C.TipoEmitenteCheque");
            SQL.AppendLine("FROM cheques C");
            SQL.AppendLine("WHERE DATE(C.DataVencimento) <= '" + data.ToString("yyyy-MM-dd") + "' AND C.Status = 1 AND C.BancoMovimentacaoId = " + bancoId);
            SQL.AppendLine("        AND " + FiltroEmpresa("", "C"));
            SQL.AppendLine("GROUP BY C.TipoEmitenteCheque");

            var cn = new DapperConnection<Cheque>();
            return cn.ExecuteStringSqlToList(new Cheque(), SQL.ToString()).ToList();
        }

        public List<ChequeView> GetByDataEBancoConsulta(DateTime data, int bancoId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT C.*, (CASE WHEN CR.NumTitulo IS NULL THEN CP.NumTitulo ELSE CR.NumTitulo END) AS NumTitulo, ");
            SQL.AppendLine("CO.Nome AS NomeCliente");
            SQL.AppendLine("FROM cheques C");
            SQL.AppendLine("LEFT JOIN ContasReceberBaixa CRB ON CRB.Id = C.ContasReceberBaixaId");
            SQL.AppendLine("LEFT JOIN ContasPagarBaixa CPB ON CPB.Id = C.ContasPagarBaixaId");
            SQL.AppendLine("LEFT JOIN contasreceber CR ON CR.Id = CRB.ContasReceberId");
            SQL.AppendLine("LEFT JOIN contaspagar CP ON CP.Id = CPB.ContasPagarId");
            SQL.AppendLine("LEFT JOIN colaboradores CO  ON CO.Id = C.ColaboradorId");
            SQL.AppendLine("WHERE DATE(C.DataVencimento) = '" + data.ToString("yyyy-MM-dd") + "' AND C.Status = 1 ");
            SQL.AppendLine("        AND " + FiltroEmpresa("", "C"));

            if (bancoId > 0)
            {
                SQL.AppendLine("AND C.BancoMovimentacaoId = " + bancoId);
            }
            SQL.AppendLine("ORDER BY C.DataVencimento");

            var cn = new DapperConnection<ChequeView>();
            return cn.ExecuteStringSqlToList(new ChequeView(), SQL.ToString()).ToList();
        }

        public List<ChequeView> GetByAteDataEBancoConsulta(DateTime data, int bancoId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT C.*, (CASE WHEN CR.NumTitulo IS NULL THEN CP.NumTitulo ELSE CR.NumTitulo END) AS NumTitulo, ");
            SQL.AppendLine("CO.Nome AS NomeCliente");
            SQL.AppendLine("FROM cheques C");
            SQL.AppendLine("LEFT JOIN ContasReceberBaixa CRB ON CRB.Id = C.ContasReceberBaixaId");
            SQL.AppendLine("LEFT JOIN ContasPagarBaixa CPB ON CPB.Id = C.ContasPagarBaixaId");
            SQL.AppendLine("LEFT JOIN contasreceber CR ON CR.Id = CRB.ContasReceberId");
            SQL.AppendLine("LEFT JOIN contaspagar CP ON CP.Id = CPB.ContasPagarId");
            SQL.AppendLine("LEFT JOIN colaboradores CO  ON CO.Id = C.ColaboradorId");
            SQL.AppendLine("WHERE DATE(C.DataVencimento) <= '" + data.ToString("yyyy-MM-dd") + "' AND C.Status = 1 ");
            SQL.AppendLine("        AND " + FiltroEmpresa("", "C"));

            if (bancoId > 0)
            {
                SQL.AppendLine("AND C.BancoMovimentacaoId = " + bancoId);
            }
            SQL.AppendLine("ORDER BY C.DataVencimento");

            var cn = new DapperConnection<ChequeView>();
            return cn.ExecuteStringSqlToList(new ChequeView(), SQL.ToString()).ToList();
        }


        public void UpdateCamposCheque(Cheque cheque)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("UPDATE Cheques SET");
            SQL.AppendLine(" DataVencimento = @DataVencimento,");
            SQL.AppendLine(" Banco = @Banco,");
            SQL.AppendLine(" Agencia = @Agencia,");
            SQL.AppendLine(" Conta = @Conta,");
            SQL.AppendLine(" NumeroCheque = @NumeroCheque,");
            SQL.AppendLine(" Valor = @Valor,");
            SQL.AppendLine(" DeTerceiro = @DeTerceiro,");
            SQL.AppendLine(" ProrrogarPara = @ProrrogarPara,");
            SQL.AppendLine(" Visado = @Visado,");
            SQL.AppendLine(" Cruzado = @Cruzado,");
            SQL.AppendLine(" Compensacao = @Compensacao,");
            SQL.AppendLine(" Resgate = @Resgate,");
            SQL.AppendLine(" ColaboradorId = @ColaboradorId,");
            SQL.AppendLine(" TipoEmitenteCheque = @TipoEmitenteCheque,");
            SQL.AppendLine(" NaturezaFinanceiraId = @NaturezaFinanceiraId,");
            SQL.AppendLine(" BancoMovimentacaoId = @BancoMovimentacaoId, ");
            SQL.AppendLine(" Emitente = @Emitente ");
            SQL.AppendLine("WHERE Id = @Id");

            _cn.ExecuteUpdate(cheque, SQL.ToString());
        }

        public void CompensarCheque(Cheque cheque, bool estornar = false)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("UPDATE Cheques SET");
            SQL.AppendLine(" Compensacao = @Compensacao,");
            SQL.AppendLine(" Status = @Status,");
            SQL.AppendLine(" BancoMovimentacaoId = @BancoMovimentacaoId, ");
            SQL.AppendLine(" ValorJuros = @ValorJuros, ");
            SQL.AppendLine(" ValorCompensado = @ValorCompensado ");
            SQL.AppendLine(" WHERE Id = @Id");

            _cn.ExecuteUpdate(cheque, SQL.ToString());
        }

        public void DevolverCheque(Cheque cheque)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("UPDATE Cheques SET");
            SQL.AppendLine(" Status = @Status,");
            SQL.AppendLine(" DataAlinea1 = @DataAlinea1,");
            SQL.AppendLine(" DataAlinea2 = @DataAlinea2, ");
            SQL.AppendLine(" Alinea1Id = @Alinea1Id, ");
            SQL.AppendLine(" Alinea2Id = @Alinea2Id, ");
            SQL.AppendLine(" DataApresentacaoAlinea1 = @DataApresentacaoAlinea1, ");
            SQL.AppendLine(" DataApresentacaoAlinea2 = @DataApresentacaoAlinea2, ");
            SQL.AppendLine(" ContasPagarGeradoId = @ContasPagarGeradoId ");
            SQL.AppendLine("WHERE Id = @Id");

            _cn.ExecuteUpdate(cheque, SQL.ToString());
        }

        public void ProrrogarCheque(Cheque cheque)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("UPDATE Cheques SET");
            SQL.AppendLine(" Status = @Status,");
            SQL.AppendLine(" ProrrogarPara = @ProrrogarPara ");
            SQL.AppendLine("WHERE Id = @Id");

            _cn.ExecuteUpdate(cheque, SQL.ToString());
        }

        public void ResgatarCheque(Cheque cheque)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("UPDATE Cheques SET");
            SQL.AppendLine(" Status = @Status,");
            SQL.AppendLine(" Resgate = @Resgate ");
            SQL.AppendLine("WHERE Id = @Id");

            _cn.ExecuteUpdate(cheque, SQL.ToString());
        }
    }
}
