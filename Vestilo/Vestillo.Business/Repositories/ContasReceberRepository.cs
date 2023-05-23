using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class ContasReceberRepository: GenericRepository<ContasReceber>
    {
        public class DiasVencidos
        {
            public int NumeroDeDiasVencidos { get; set; }
            public int TipoCobranca { get; set; }
            public DateTime? DataPagamento { get; set; }
        }
        


        public ContasReceberRepository()
            : base(new DapperConnection<ContasReceber>())
        {
        }

        public override IEnumerable<ContasReceber> GetAll()
        {
            return _cn.ExecuteToList(new ContasReceber(), "IdPedidoVenda IS NULL AND " + FiltroEmpresa());
        }

        public IEnumerable<ContasReceberView> GetAllView()
        {
            var cn = new DapperConnection<ContasReceberView>();
            
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT CR.Id, NumTitulo, Prefixo, Parcela, tipodocumentos.descricao as TipoDocumento,CR.DataEmissao, DataVencimento, dataPagamento, ValorParcela, ValorPago, CR.Saldo, IFNULL(CR.Ativo,0) as Ativo,NotaFiscal,nfce.numeronfce as NFCE, ");
            SQL.AppendLine("    IFNULL(C.Nome, CR.NomeCliente) AS NomeCliente, IFNULL(CR.Status, 0) AS Status, TC.Descricao AS TipoCobrancaDescricao, ");
            SQL.AppendLine("    B.Descricao AS NomeBanco , CR.Obs as Obs, CR.SisCob as SisCob,  NF.referencia as Natureza, NF.descricao as NaturezaDesc, CR.PossuiBoleto,CR.RemessaGerada,CR.BancoPortador,IF(CR.PossuiBoleto > 0,'Sim','Não') as SimNao, ");
            SQL.AppendLine("    C.razaosocial AS RazaoSocialColaborador, C.cnpjcpf AS CNPJCPF,M.municipio as Cidade,E.abreviatura as Estado,R.descricao as Rota ");
            SQL.AppendLine("FROM	contasreceber AS CR");
            SQL.AppendLine("    LEFT JOIN colaboradores AS C ON C.Id = CR.IdCliente");
            SQL.AppendLine("    LEFT JOIN municipiosibge AS M ON M.Id = C.idmunicipio");
            SQL.AppendLine("    LEFT JOIN estados AS E ON E.Id = C.idestado");
            SQL.AppendLine("    LEFT JOIN rotavisitas AS R ON R.Id = C.idrota");
            SQL.AppendLine("    LEFT JOIN bancos AS B ON B.Id = CR.IdBanco");
            SQL.AppendLine("    LEFT JOIN TiposCobranca TC ON TC.Id = CR.IdTipoCobranca");
            SQL.AppendLine("    LEFT JOIN nfce ON nfce.id = CR.IdNotaconsumidor");
            SQL.AppendLine("    LEFT JOIN naturezasfinanceiras NF ON NF.Id = CR.IdNaturezaFinanceira");
            SQL.AppendLine("    LEFT JOIN tipodocumentos ON tipodocumentos.Id = CR.IdTipoDocumento");
            SQL.AppendLine("WHERE CR.IdPedidoVenda IS NULL  AND " + FiltroEmpresa("","CR"));
            SQL.AppendLine(" ORDER BY CR.DataEmissao DESC, CR.NumTitulo, CR.Prefixo, CR.Parcela");

            return cn.ExecuteStringSqlToList(new ContasReceberView(), SQL.ToString());
        }

        public IEnumerable<TitulosBaixaLoteView> GetParcelasParaBaixaEmLote(int clienteId, DateTime? dataVencimentoInicial = null, DateTime? dataVencimentoFinal = null)
        {
            var cn = new DapperConnection<TitulosBaixaLoteView>();
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	CR.Id, CR.NumTitulo, Prefixo, Parcela, DataEmissao, DataVencimento, ValorParcela, CR.Saldo, ");
            SQL.AppendLine("		C.Nome As NomeColaborador, TC.Descricao As TipoCobrancaDescricao, B.descricao As NomeBanco,");
            SQL.AppendLine("        IFNULL(CR.Status, 0) AS Status, CR.Juros, CR.Desconto, CR.ValorPago ");
            SQL.AppendLine("FROM 	contasreceber AS CR");
            SQL.AppendLine("	LEFT JOIN colaboradores AS C ON C.Id = CR.IdCliente");
            SQL.AppendLine("    LEFT JOIN bancos AS B ON B.Id = CR.IdBanco");
            SQL.AppendLine("	LEFT JOIN TiposCobranca TC ON TC.Id = CR.IdTipoCobranca");
            SQL.AppendLine("WHERE	CR.IdPedidoVenda IS NULL AND CR.Saldo > 0");
            SQL.AppendLine("		AND " + FiltroEmpresa("", "CR"));
            SQL.AppendLine("		AND IdCliente = " + clienteId.ToString());
            SQL.AppendLine("        AND CR.Ativo = 1");

            if (dataVencimentoInicial != null)
            {
                SQL.AppendLine("        AND CR.DataVencimento >= '" + dataVencimentoInicial.GetValueOrDefault().Date.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            }

            if (dataVencimentoFinal != null)
            {
                SQL.AppendLine("        AND CR.DataVencimento <= '" + dataVencimentoFinal.GetValueOrDefault().Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            }


            SQL.AppendLine("ORDER BY CR.DataVencimento;");

            return cn.ExecuteStringSqlToList(new TitulosBaixaLoteView(), SQL.ToString());
        }

        public IEnumerable<TitulosBaixaLoteView> GetParcelasParaEstornarBaixaEmLote(int contasReceberBaixaId)
        {
            var cn = new DapperConnection<TitulosBaixaLoteView>();
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	CR.Id, CR.NumTitulo, Prefixo, Parcela, DataEmissao, DataVencimento, ValorParcela, CR.Saldo, ");
            SQL.AppendLine("		C.Nome As NomeColaborador, TC.Descricao As TipoCobrancaDescricao, B.descricao As NomeBanco,");
            SQL.AppendLine("        IFNULL(CR.Status, 0) AS Status, CR.Juros, CR.Desconto, C.Id AS ColaboradorId, C.Referencia AS RefColaborador, CPB.Id AS BaixaId");
            SQL.AppendLine("FROM 	contasreceber CR");
            SQL.AppendLine("	LEFT JOIN colaboradores AS C ON C.Id = CR.IdCliente");
            SQL.AppendLine("    LEFT JOIN bancos AS B ON B.Id = CR.IdBanco");
            SQL.AppendLine("	LEFT JOIN TiposCobranca TC ON TC.Id = CR.IdTipoCobranca");
            SQL.AppendLine("	INNER JOIN contasreceberbaixa CPB ON CPB.ContasReceberId = CR.Id");
            SQL.AppendLine("WHERE CPB.ContasReceberBaixaLoteId = " + contasReceberBaixaId);
            SQL.AppendLine("ORDER BY CR.DataVencimento;");

            return cn.ExecuteStringSqlToList(new TitulosBaixaLoteView(), SQL.ToString());
        }

        public IEnumerable<ContasReceberView> GetViewByReferencia(string referencia, int[] status = null, bool SomenteParaBoleto = false,int BancoPortador = 0)
        {
            var cn = new DapperConnection<ContasReceberView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT CR.Id,NotaFiscal, NumTitulo, Prefixo, Parcela, DataEmissao, DataVencimento, dataPagamento, ValorParcela, ValorPago, CR.Saldo as Saldo, IFNULL(CR.Ativo,0) as Ativo, ");
            
            SQL.AppendLine("CR.PossuiBoleto,CR.RemessaGerada,CR.BancoPortador,IF(CR.PossuiBoleto > 0,'Sim','Não') as SimNao, ");
            
            SQL.AppendLine("    C.Nome AS NomeCliente, C.razaosocial AS razaosocial, IFNULL(CR.Status, 0) AS Status,");
            SQL.AppendLine("    B.Descricao AS NomeBanco, CR.Obs as Obs, CR.SisCob as SisCob ");
            SQL.AppendLine("FROM	contasreceber AS CR");
            SQL.AppendLine("    LEFT JOIN colaboradores AS C ON C.Id = CR.IdCliente");
            SQL.AppendLine("    LEFT JOIN bancos AS B ON B.Id = CR.IdBanco");
            SQL.AppendLine("WHERE " + FiltroEmpresa("CR.IdEmpresa"));
            SQL.AppendLine(" AND CR.NumTitulo LIKE  '%" + referencia + "%' ");

            if(SomenteParaBoleto)
            {
                SQL.AppendLine(" AND CR.Status = 1 AND CR.RemessaGerada = 0 AND (CR.BancoPortador IS NULL OR CR.BancoPortador =" + BancoPortador +") ");

            }
            else
            {
                if (status != null && status.Length > 0)
                    SQL.AppendLine(" AND CR.Status IN (" + string.Join(",", status) + ") ");
            }

            

            SQL.AppendLine(" ORDER BY NumTitulo, Prefixo, Parcela");

            return cn.ExecuteStringSqlToList(new ContasReceberView(), SQL.ToString());
        }

        public ContasReceberView GetViewById(int id)
        {
            var cn = new DapperConnection<ContasReceberView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT CR.*, ");
            SQL.AppendLine("    C.Referencia AS RefCliente,");
            SQL.AppendLine("    IFNULL(C.Nome, CR.NomeCliente) AS NomeCliente,");
            SQL.AppendLine("    B.Descricao AS NomeBanco");
            SQL.AppendLine("FROM	contasreceber AS CR");
            SQL.AppendLine("    LEFT JOIN colaboradores AS C ON C.Id = CR.IdCliente");
            SQL.AppendLine("    LEFT JOIN bancos AS B ON B.Id = CR.IdBanco");
            SQL.AppendLine("WHERE CR.IdPedidoVenda IS NULL  AND CR.Id = " + id.ToString());

            var cr = new ContasReceberView();

            cn.ExecuteToModel(ref cr, SQL.ToString());

            return cr;
        }

        public decimal GetByAteData(DateTime data)
        {
            var cn = new DapperConnection<ContasReceber>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT SUM(CR.saldo) AS saldo");
            SQL.AppendLine("FROM	contasreceber AS CR");
            SQL.AppendLine("WHERE isnull(CR.idpedidovenda) AND date(CR.DataEmissao) <= '" + data.ToString("yyyy-MM-dd") + "' AND CR.Saldo > 0  AND CR.Ativo = 1 ");
            SQL.AppendLine("        AND " + FiltroEmpresa("", "CR"));

            var cr = new ContasReceber();

            cn.ExecuteToModel(ref cr, SQL.ToString());

            return cr.Saldo;
        }

        public decimal GetByData(DateTime data)
        {
            var cn = new DapperConnection<ContasReceber>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT SUM(CR.saldo) AS saldo");
            SQL.AppendLine("FROM	contasreceber AS CR");
            SQL.AppendLine("WHERE isnull(CR.idpedidovenda) AND date(CR.DataVencimento) = '" + data.ToString("yyyy-MM-dd") + "' AND CR.Saldo > 0  AND CR.Ativo = 1  ");
            SQL.AppendLine("        AND " + FiltroEmpresa("", "CR"));

            var cr = new ContasReceber();

            cn.ExecuteToModel(ref cr, SQL.ToString());

            return cr.Saldo;
        }

        public decimal GetByAteDataEBanco(DateTime data, int bancoId)
        {
            var cn = new DapperConnection<ContasReceber>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT SUM(CR.saldo) AS saldo");
            SQL.AppendLine("FROM	contasreceber AS CR");
            SQL.AppendLine("WHERE isnull(CR.idpedidovenda) AND date(CR.DataVencimento) <= '" + data.ToString("yyyy-MM-dd") + "' AND CR.Saldo > 0 AND CR.Ativo = 1 AND CR.idBanco = " + bancoId);
            SQL.AppendLine("        AND " + FiltroEmpresa("", "CR"));

            var cr = new ContasReceber();

            cn.ExecuteToModel(ref cr, SQL.ToString());

            return cr.Saldo;
        }

        public decimal GetByDataEBanco(DateTime data, int bancoId)
        {
            var cn = new DapperConnection<ContasReceber>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT SUM(CR.saldo) AS saldo");
            SQL.AppendLine("FROM	contasreceber AS CR");
            SQL.AppendLine("WHERE isnull(CR.idpedidovenda) AND date(CR.DataVencimento) = '" + data.ToString("yyyy-MM-dd") + "' AND CR.Saldo > 0 AND CR.Ativo = 1 AND CR.idBanco = " + bancoId);
            SQL.AppendLine("        AND " + FiltroEmpresa("", "CR"));

            var cr = new ContasReceber();

            cn.ExecuteToModel(ref cr, SQL.ToString());

            return cr.Saldo;
        }

        public List<ContasReceberView> GetByDataEBancoConsulta(DateTime data, int bancoId)
        {
            var cn = new DapperConnection<ContasReceberView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT CR.Id, NumTitulo, Prefixo, Parcela, DataEmissao, DataVencimento, dataPagamento, ValorParcela, ValorPago, CR.Saldo, IFNULL(CR.Ativo,0) as Ativo, ");
            SQL.AppendLine("    C.Nome AS NomeCliente, IFNULL(CR.Status, 0) AS Status,");
            SQL.AppendLine("    B.Descricao AS NomeBanco, CR.Obs as Obs, CR.SisCob as SisCob ");
            SQL.AppendLine("FROM	contasreceber AS CR");
            SQL.AppendLine("    LEFT JOIN colaboradores AS C ON C.Id = CR.IdCliente");
            SQL.AppendLine("    LEFT JOIN bancos AS B ON B.Id = CR.IdBanco");
            SQL.AppendLine("WHERE isnull(CR.idpedidovenda) AND date(CR.DataVencimento) = '" + data.ToString("yyyy-MM-dd") + "' AND CR.Saldo > 0 AND CR.Ativo = 1 ");
            SQL.AppendLine("        AND " + FiltroEmpresa("", "CR"));

            if (bancoId > 0)
                SQL.AppendLine(" AND CR.idBanco = " + bancoId);

            SQL.AppendLine(" ORDER BY CR.DataVencimento");

            return cn.ExecuteStringSqlToList(new ContasReceberView(), SQL.ToString()).ToList();
        }

        public List<ContasReceberView> GetByAteDataEBancoConsulta(DateTime data, int bancoId)
        {
            var cn = new DapperConnection<ContasReceberView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT CR.Id, NumTitulo, Prefixo, Parcela, DataEmissao, DataVencimento, dataPagamento, ValorParcela, ValorPago, CR.Saldo, IFNULL(CR.Ativo,0) as Ativo, ");
            SQL.AppendLine("    C.Nome AS NomeCliente, IFNULL(CR.Status, 0) AS Status,");
            SQL.AppendLine("    B.Descricao AS NomeBanco, CR.Obs as Obs, CR.SisCob as SisCob ");
            SQL.AppendLine("FROM	contasreceber AS CR");
            SQL.AppendLine("    LEFT JOIN colaboradores AS C ON C.Id = CR.IdCliente");
            SQL.AppendLine("    LEFT JOIN bancos AS B ON B.Id = CR.IdBanco");
            SQL.AppendLine("WHERE isnull(CR.idpedidovenda) AND date(CR.DataVencimento) <= '" + data.ToString("yyyy-MM-dd") + "' AND CR.Saldo > 0 AND CR.Ativo = 1 ");
            SQL.AppendLine("        AND " + FiltroEmpresa("", "CR"));

            if (bancoId > 0)
                SQL.AppendLine(" AND CR.idBanco = " + bancoId);

            SQL.AppendLine(" ORDER BY CR.DataVencimento");

            return cn.ExecuteStringSqlToList(new ContasReceberView(), SQL.ToString()).ToList();
        }

        public ContasReceber GetByCheque(int chequeId)
        {
            ContasReceber cr = new ContasReceber();
            _cn.ExecuteToModel("IdCheque = " + chequeId.ToString(), ref cr);

            return cr;
        }

        public IEnumerable<ContasReceber> GetByNotaConsumidor(int idNotaConsumidor)
        {
            return _cn.ExecuteToList(new ContasReceber(), "IdNotaConsumidor = " + idNotaConsumidor.ToString());
        }

        public IEnumerable<ContasReceberView> GetListaPorCampoEValor(string campoBusca, string valor, bool CarregarSomenteAval = false)
        {
            var cn = new DapperConnection<ContasReceberView>();

            var SQL = new StringBuilder();            
            
            SQL.AppendLine("SELECT CR.*, ");
            SQL.AppendLine("    C.Nome AS NomeCliente,");
            SQL.AppendLine("    B.Descricao AS NomeBanco ");            
            SQL.AppendLine("FROM	contasreceber AS CR ");
            SQL.AppendLine("    LEFT JOIN colaboradores AS C ON C.Id = CR.IdCliente ");
            SQL.AppendLine("    LEFT JOIN bancos AS B ON B.Id = CR.IdBanco ");           
            SQL.AppendLine(" WHERE  CR.IdEmpresa = ");
            SQL.Append(Business.VestilloSession.EmpresaLogada.Id);
            SQL.AppendLine(" AND CR." + campoBusca + " = '" + valor + "'");
            if (CarregarSomenteAval)
            {
                SQL.AppendLine(" AND CR.IdTipoDocumento = 999 ");
            }
            SQL.AppendLine(" ORDER BY NumTitulo, Prefixo, dataVencimento,Parcela");

            return cn.ExecuteStringSqlToList(new ContasReceberView(), SQL.ToString());
        }

        public void UpdatePossuiAtividade(int IdTitulo, int SimNao)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("UPDATE contasreceber SET ");
            SQL.AppendLine("PossuiAtividade = ");
            SQL.Append(SimNao);
            SQL.AppendLine(" WHERE Id = ");
            SQL.Append(IdTitulo);

            _cn.ExecuteNonQuery(SQL.ToString());
        }

        public void UpdateNotaFiscal(int IdFatNfe, string NumNota)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("UPDATE contasreceber SET ");
            SQL.AppendLine("NotaFiscal = ");
            SQL.Append(NumNota);
            SQL.AppendLine(" WHERE IdFatNfe = ");
            SQL.Append(IdFatNfe);

            _cn.ExecuteNonQuery(SQL.ToString());
        }

        public void UpdateDadosBoleto(int IdTitulo, int IdBancoPortador)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("UPDATE contasreceber SET ");
            SQL.AppendLine("  PossuiBoleto = 1, ");
            SQL.AppendLine("  BancoPortador = ");
            SQL.Append(IdBancoPortador);
            SQL.AppendLine(" WHERE Id = ");
            SQL.Append(IdTitulo);

            _cn.ExecuteNonQuery(SQL.ToString());
        }

        public void UpdateTituloBaixadoPeloBoleto(int IdTitulo, decimal Juros)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("UPDATE  contasreceber SET saldo = 0, ");            
            SQL.AppendLine(" Juros = ");
            SQL.Append(Juros.ToString().Replace(",", "."));
            SQL.AppendLine(" WHERE Id = ");
            SQL.Append(IdTitulo);

            _cn.ExecuteNonQuery(SQL.ToString());
        }


        public void UpdateConcluirAtividade(int IdTitulo, decimal Saldo)
        {

            var SQL = new StringBuilder();
            SQL.AppendLine("UPDATE atividades SET ");
            if (Saldo <=0)
            {
                SQL.AppendLine("StatusAtividade = 2");
            }
            else
            {
                SQL.AppendLine("StatusAtividade = 1");
            }
            
            SQL.AppendLine(" WHERE IdTitulo = ");
            SQL.Append(IdTitulo);

            _cn.ExecuteNonQuery(SQL.ToString());
        }

        public void UpdateEmailEnviado(int IdTitulo)
        {
            DateTime data = DateTime.Now;            

            var SQL = new StringBuilder();
            SQL.AppendLine("UPDATE  contasreceber SET UltimoEmail =  ");            
            SQL.Append("'" + data.ToString("yyyy-MM-dd") + "'" );
            SQL.AppendLine(" WHERE Id = ");
            SQL.Append(IdTitulo);

            _cn.ExecuteNonQuery(SQL.ToString());
        }


        public IEnumerable<TitulosView> GetParcelasRelatorio(FiltroRelatorioTitulos filtro)
        {
            var cn = new DapperConnection<TitulosView>();
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	CR.Id, CR.NumTitulo, CR.Prefixo, CR.Parcela, CR.DataEmissao, CR.DataVencimento, CR.ValorParcela, CR.Saldo, CR.DataPagamento,NotaFiscal,CR.SisCob as SisCob,TD.descricao as TipoDocumentos,");
            SQL.AppendLine("		C.referencia AS RefColaborador, IFNULL(C.Nome, CR.NomeCliente) As NomeColaborador, TC.Descricao As TipoCobrancaDescricao, B.descricao As NomeBanco,");
            SQL.AppendLine("        IFNULL(CR.Status, 0) AS Status, CR.Juros, CR.Desconto,  N.descricao AS NaturezaDescricao, V.Referencia AS RefVendedor,");
            SQL.AppendLine("        V.Nome As NomeVendedor, CASE WHEN IFNULL(CR.Ativo,0) = 1 THEN 'Sim' ELSE 'Não' END AS Ativo ");

            // Pagamento
            if (filtro.DataPagamentoInicial != null)
            {
                //SQL.AppendLine("    ,((IFNULL(CRB.ValorDinheiro, 0) + IFNULL(CRB.ValorCheque, 0) ) - IFNULL(CRB.ValorCredito, 0)) as ValorPago ");
                SQL.AppendLine("    ,CR.ValorPago ");
            }
            else
            {
                SQL.AppendLine("    ,CR.ValorPago ");
                //
            }

            if (filtro.DataPagamentoInicial != null && filtro.ExibirBaixa)
            {
                SQL.AppendLine("        ,CRB.DataBaixa, CRB.ValorDinheiro AS ValorPagoDinheiro, SUM(CH.Valor) AS ValorPagoCheque, CRB.ValorCredito AS ValorPagoCredito,CRB.Obs AS ObsBaixa ");
            }
            else
            {
                if (filtro.ExibirBaixa)
                {
                    SQL.AppendLine("        ,BX.DataBaixa, BX.ValorDinheiro AS ValorPagoDinheiro, SUM(CH.Valor) AS ValorPagoCheque, BX.ValorCredito AS ValorPagoCredito,BX.Obs AS ObsBaixa ");
                }
            }



            SQL.AppendLine("FROM 	contasreceber AS CR");
            SQL.AppendLine("	LEFT JOIN colaboradores AS C ON C.Id = CR.IdCliente");
            SQL.AppendLine("    LEFT JOIN bancos AS B ON B.Id = CR.IdBanco");
            SQL.AppendLine("	LEFT JOIN TiposCobranca TC ON TC.Id = CR.IdTipoCobranca");
            SQL.AppendLine("    LEFT JOIN naturezasfinanceiras N ON N.id = CR.IdNaturezaFinanceira");
            SQL.AppendLine("    LEFT JOIN colaboradores AS V ON V.Id = CR.IdVendedor");
            SQL.AppendLine("    LEFT JOIN tipodocumentos TD ON TD.id = CR.IdTipoDocumento");

            // Pagamento

            if (filtro.DataPagamentoInicial != null && filtro.ExibirBaixa)
            {
                SQL.AppendLine("   INNER JOIN contasreceberbaixa 				CRB ON CRB.ContasReceberId = CR.Id");
                SQL.AppendLine("    LEFT JOIN Cheques CH ON CH.ContasReceberBaixaId = CRB.Id");

            }
            else
            {

                if (filtro.DataPagamentoInicial != null)
                {
                    SQL.AppendLine("   INNER JOIN contasreceberbaixa 				CRB ON CRB.ContasReceberId = CR.Id");

                }


                if (filtro.ExibirBaixa)
                {
                    SQL.AppendLine("    LEFT JOIN contasreceberbaixa BX ON BX.ContasReceberId = CR.Id");
                    SQL.AppendLine("    LEFT JOIN Cheques CH ON CH.ContasReceberBaixaId = BX.Id");
                }
            }


            SQL.AppendLine("WHERE " + FiltroEmpresa("","CR"));

            if (filtro.Colaboradores != null && filtro.Colaboradores.Length > 0)
                SQL.AppendLine("        AND CR.IdCliente IN (" + string.Join(", ", filtro.Colaboradores) + ")");

            if (filtro.Vendedores != null && filtro.Vendedores.Length > 0)
                SQL.AppendLine("        AND CR.IdVendedor IN (" + string.Join(", ", filtro.Vendedores) + ")");

            if (filtro.Naturezas != null && filtro.Naturezas.Length > 0)
                SQL.AppendLine("        AND CR.IdNaturezaFinanceira IN (" + string.Join(", ", filtro.Naturezas) + ")");

            if (filtro.Titulos != null && filtro.Titulos.Length > 0)
                SQL.AppendLine("        AND CR.Id IN (" + string.Join(", ", filtro.Titulos) + ")");

            if (filtro.Bancos != null && filtro.Bancos.Length > 0)
                SQL.AppendLine("        AND CR.IdBanco IN (" + string.Join(", ", filtro.Bancos) + ")");

            if (filtro.TipoDocumentos != null && filtro.TipoDocumentos.Length > 0)
                SQL.AppendLine("        AND CR.IdTipoDocumento IN (" + string.Join(", ", filtro.TipoDocumentos) + ")");

            // Prefixo
            if (!string.IsNullOrWhiteSpace(filtro.PrefixoInicial))
                SQL.AppendLine("        AND CR.Prefixo BETWEEN '" + filtro.PrefixoInicial.Trim() + "'");

            if (!string.IsNullOrWhiteSpace(filtro.PrefixoFinal))
                SQL.AppendLine("        AND  '" + filtro.PrefixoFinal.Trim() + "'");

            // Emissão
            if (filtro.DataEmissaoInicial != null)
                SQL.AppendLine("        AND CR.DataEmissao BETWEEN '" + filtro.DataEmissaoInicial.GetValueOrDefault().Date.ToString("yyyy-MM-dd HH:mm:ss") + "'");

            if (filtro.DataEmissaoFinal != null)
                SQL.AppendLine("        AND  '" + filtro.DataEmissaoFinal.GetValueOrDefault().Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "'");

            // Vencimento
            if (filtro.DataVencimentoInicial != null)
                SQL.AppendLine("        AND CR.DataVencimento BETWEEN '" + filtro.DataVencimentoInicial.GetValueOrDefault().Date.ToString("yyyy-MM-dd HH:mm:ss") + "'");

            if (filtro.DataVencimentoFinal != null)
                SQL.AppendLine("        AND  '" + filtro.DataVencimentoFinal.GetValueOrDefault().Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "'");

            // Pagamento
            if (filtro.DataPagamentoInicial != null)
                SQL.AppendLine("        AND CRB.DataBaixa BETWEEN '" + filtro.DataPagamentoInicial.GetValueOrDefault().Date.ToString("yyyy-MM-dd HH:mm:ss") + "'");

            if (filtro.DataPagamentoFinal != null)
                SQL.AppendLine("        AND  '" + filtro.DataPagamentoFinal.GetValueOrDefault().Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "'");

            //Tipo Cobrança
            if (filtro.TipoCobranca != null && filtro.TipoCobranca.Length > 0)
                SQL.AppendLine("        AND CR.IdTipoCobranca IN (" + string.Join(", ", filtro.TipoCobranca) + ")");


            if (filtro.Administradoras != null && filtro.Administradoras.Length > 0)
                SQL.AppendLine("        AND CR.IdAdmCartao IN (" + string.Join(", ", filtro.Administradoras) + ")");

            //Status do titulo
            if (filtro.Status != 0 )
            {
                if(filtro.Status == 1)
                {
                    SQL.AppendLine("        AND ( CR.Status = " + filtro.Status + " OR CR.Status = 0 ) "); //só aberto
                }
                else if ( filtro.Status == 2 || filtro.Status == 3) //Só baixado, só parcial
                {
                    SQL.AppendLine("        AND CR.Status = " + filtro.Status);
                }
                else if (filtro.Status == 4)
                {
                    SQL.AppendLine("        AND CR.Status IN(1,2)"); // Aberto e parcial
                }               
            }


            if (filtro.DataPagamentoInicial != null && filtro.ExibirBaixa)
            {
                if (filtro.ExibirBaixa)
                {
                    SQL.AppendLine("    GROUP BY CRB.id");
                    filtro.Agrupar = true;
                }
            }
            else
            {
                if (filtro.ExibirBaixa)
                {
                    SQL.AppendLine("    GROUP BY BX.id");
                    filtro.Agrupar = true;
                }
                else if(filtro.DataPagamentoInicial != null)
                {
                    SQL.AppendLine("    GROUP BY CR.id");
                    filtro.Agrupar = true;
                }
            }



            if (filtro.Agrupar == true)
            {
                SQL.AppendLine("        ORDER BY " + filtro.ColunaParaAgrupar);
            }

            return cn.ExecuteStringSqlToList(new TitulosView(), SQL.ToString());
        }

        public IEnumerable<ContasReceber> GetAllTitulosFilhos(int TituloPai)
        {
            var cn = new DapperConnection<ContasReceber>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT CR.Id as Id, NumTitulo, Prefixo, Parcela, DataEmissao, DataVencimento, dataPagamento, ValorParcela, ValorPago, CR.Saldo, IFNULL(CR.Ativo,0) as Ativo,NotaFiscal, ");
            SQL.AppendLine("    IFNULL(C.Nome, CR.NomeCliente) AS NomeCliente, IFNULL(CR.Status, 0) AS Status, TC.Descricao AS TipoCobrancaDescricao, ");
            SQL.AppendLine("    B.Descricao AS NomeBanco , CR.Obs as Obs,CR.SisCob as SisCob ");
            SQL.AppendLine("FROM	contasreceber AS CR");
            SQL.AppendLine("    LEFT JOIN colaboradores AS C ON C.Id = CR.IdCliente");
            SQL.AppendLine("    LEFT JOIN bancos AS B ON B.Id = CR.IdBanco");
            SQL.AppendLine("    LEFT JOIN TiposCobranca TC ON TC.Id = CR.IdTipoCobranca");
            SQL.AppendLine("WHERE CR.IdTituloPai = " + TituloPai);
            SQL.AppendLine(" ORDER BY CR.DataEmissao DESC, CR.NumTitulo, CR.Prefixo, CR.Parcela");

            return cn.ExecuteStringSqlToList(new ContasReceber(), SQL.ToString());
        }


        public IEnumerable<ContasReceber> GetAllTitulosBaixados(int TituloPai)
        {
            var cn = new DapperConnection<ContasReceber>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT * FROM contasreceberbaixa ");
            SQL.AppendLine("WHERE contasreceberbaixa.ContasReceberId = " + TituloPai);
           

            return cn.ExecuteStringSqlToList(new ContasReceber(), SQL.ToString());
        }

        public void UpdateObsTitulo(int IdTitulo,string obs)
        {

            int i = obs.IndexOf('@');           
            bool MudaPrefixo = false;

            if (i != -1)
            {
                MudaPrefixo = true;
            }

            string resultado = string.Empty;
            if (obs.Length > 1)
            {
                if (MudaPrefixo)
                {
                    resultado = obs.Substring(0, obs.IndexOf('@'));
                }
                if (resultado.Length <= 0)
                {
                    resultado = "";
                }
            }
            else
            {
                resultado = "";
            }

            int ft = obs.IndexOf("Gerado pelo Faturamento");
            if (ft == -1)
            {
                StringBuilder SQL = new StringBuilder();
                SQL.AppendLine("UPDATE contasreceber SET ");
                SQL.AppendLine("Obs = ");
                SQL.Append("'" + resultado + "'");
                if (MudaPrefixo == true)
                {
                    SQL.AppendLine(",Prefixo = 'CTR'");
                }
                SQL.AppendLine(" WHERE Id = ");
                SQL.Append(IdTitulo);
                _cn.ExecuteNonQuery(SQL.ToString());
            }
           
        }

        public IEnumerable<ReceitasFuturasView> GetReceitaFutura(int[] Ano, bool UnirEmpresas)
        {
            
            var cn = new DapperConnection<ReceitasFuturasView>();
            string MesAno = String.Empty;

            List<ReceitasFuturasView> MontaReceitaFutura = null;
            MontaReceitaFutura = new List<ReceitasFuturasView>();
            MontaReceitaFutura.Clear();

            var DataInicioTotal = Ano + "-01-01";
            // var DataFimTotal = int.Parse(Ano) + 1 + "-01-01";

            ReceitasFuturasView itReceita = new ReceitasFuturasView();

            var cnReceitasFuturas = new DapperConnection<ReceitasFuturasView>();

           

            foreach (var itemAno in Ano)
            {
                               
                for (int i = 1; i <= 12; i++)
                {
                    var SQL = new StringBuilder();
                    var SQL2 = String.Empty;


                    if (!UnirEmpresas)
                    {
                        SQL2 = " AND cheques.EmpresaId = " + Business.VestilloSession.EmpresaLogada.Id.ToString();
                    }
                    else
                    {
                        SQL2 = "";
                    }

                        SQL.AppendLine("SELECT ");
                    SQL.Append(itemAno + " as IdVendedor," + itemAno + " as RefVendedor, ");

                    MesAno = i.ToString("d2") + itemAno;

                    SQL.AppendLine("ROUND(SUM(IF(DATE_FORMAT(contasreceber.dataVencimento, '%m%Y') =" + "'" + MesAno + "'" + " AND contasreceber.IdCliente = 60112 AND IFNULL(contasreceber.Saldo,0) > 0,contasreceber.Saldo,0)),4) as TotalAmoMuitoM" + i );
                    SQL.AppendLine(",ROUND(SUM(IF(DATE_FORMAT(contasreceber.dataVencimento, '%m%Y') =" + "'" + MesAno + "'" + " AND contasreceber.IdCliente = 52114 AND IFNULL(contasreceber.Saldo,0) > 0,contasreceber.Saldo,0)),4) as DCIM" + i);
                    SQL.AppendLine(",ROUND(SUM(IF(DATE_FORMAT(contasreceber.dataVencimento, '%m%Y') =" + "'" + MesAno + "'" + " AND contasreceber.IdCliente = 60673 AND IFNULL(contasreceber.Saldo,0) > 0,contasreceber.Saldo,0)),4) as CaboFrioM" + i);
                    SQL.AppendLine(",ROUND(SUM(IF(DATE_FORMAT(contasreceber.dataVencimento, '%m%Y') =" + "'" + MesAno + "'" + " AND contasreceber.IdTipoCobranca = 1 AND contasreceber.IdCliente <> 60112 AND contasreceber.IdCliente <> 52114 AND contasreceber.IdCliente <> 60673 AND ISNULL(contasreceber.IdAdmCartao) AND IFNULL(contasreceber.Saldo,0) > 0 ,contasreceber.Saldo,0)),4) as TotalCarteiraM" + i);
                    SQL.AppendLine(",ROUND(SUM(IF(DATE_FORMAT(contasreceber.dataVencimento, '%m%Y') =" + "'" + MesAno + "'" + " AND contasreceber.IdTipoCobranca = 9 AND contasreceber.IdCliente <> 60112 AND contasreceber.IdCliente <> 52114 AND contasreceber.IdCliente <> 60673 AND ISNULL(contasreceber.IdAdmCartao) AND IFNULL(contasreceber.Saldo,0) > 0 ,contasreceber.Saldo,0)),4) as TotalProtestoM" + i);
                    SQL.AppendLine(",ROUND(SUM(IF(DATE_FORMAT(contasreceber.dataVencimento, '%m%Y') =" + "'" + MesAno + "'" + " AND ISNULL(contasreceber.IdCliente) AND NOT ISNULL(contasreceber.IdAdmCartao) AND IFNULL(contasreceber.Saldo,0) > 0 ,contasreceber.Saldo,0)),4) as TotalCartoCreditoM" + i );
                    SQL.AppendLine(",ROUND(SUM(IF(DATE_FORMAT(contasreceber.dataVencimento, '%m%Y') =" + "'" + MesAno + "'" + " AND ISNULL(contasreceber.IdCliente) AND ISNULL(contasreceber.IdAdmCartao) AND IFNULL(contasreceber.Saldo,0) > 0 ,contasreceber.Saldo,0)),4) as TotalRenegociadoCartaoM" + i);

                    SQL.AppendLine(",(SELECT ROUND(IFNULL(SUM(cheques.Valor),0),4) from cheques where DATE_FORMAT(cheques.DataVencimento, '%m%Y') =" + "'" + MesAno + "'" + " AND ISNULL(cheques.Compensacao) " + SQL2 + ") as TotalChequeM" + i );
                    SQL.AppendLine("FROM contasreceber ");

                    SQL.AppendLine(" WHERE  ");

                    if (!UnirEmpresas)
                    {
                        SQL.AppendLine(" contasreceber.Idempresa = " + Business.VestilloSession.EmpresaLogada.Id.ToString() + " AND " );
                    }
                    SQL.AppendLine(" contasreceber.Ativo = 1 ");

                    var rcf = new ReceitasFuturasView();
                    var dados = cnReceitasFuturas.ExecuteStringSqlToList(rcf, SQL.ToString());

                    if (dados != null && dados.Count() > 0)
                    {
                        List<ReceitasFuturasView> crReceitasFuturas = new List<ReceitasFuturasView>();
                        crReceitasFuturas = dados.ToList();

                        itReceita = new ReceitasFuturasView();
                        itReceita.IdVendedor = itemAno;
                        itReceita.RefVendedor = itemAno.ToString();
                        switch (i)
                        {
                            case 1:
                                itReceita.TotalAmoMuitoM1 = crReceitasFuturas[0].TotalAmoMuitoM1;
                                itReceita.DCIM1 = crReceitasFuturas[0].DCIM1;
                                itReceita.CaboFrioM1 = crReceitasFuturas[0].CaboFrioM1;
                                itReceita.TotalCarteiraM1 = crReceitasFuturas[0].TotalCarteiraM1;
                                itReceita.TotalCartoCreditoM1 = crReceitasFuturas[0].TotalCartoCreditoM1 + crReceitasFuturas[0].TotalRenegociadoCartaoM1;
                                itReceita.TotalProtestoM1 = crReceitasFuturas[0].TotalProtestoM1;
                                itReceita.TotalChequeM1 = crReceitasFuturas[0].TotalChequeM1;
                                MontaReceitaFutura.Add(itReceita);
                                break;
                            case 2:
                                itReceita.TotalAmoMuitoM2 = crReceitasFuturas[0].TotalAmoMuitoM2;
                                itReceita.DCIM2 = crReceitasFuturas[0].DCIM2;
                                itReceita.CaboFrioM2 = crReceitasFuturas[0].CaboFrioM2;
                                itReceita.TotalCarteiraM2 = crReceitasFuturas[0].TotalCarteiraM2;
                                itReceita.TotalCartoCreditoM2 = crReceitasFuturas[0].TotalCartoCreditoM2 + crReceitasFuturas[0].TotalRenegociadoCartaoM2;
                                itReceita.TotalProtestoM2 = crReceitasFuturas[0].TotalProtestoM2;
                                itReceita.TotalChequeM2 = crReceitasFuturas[0].TotalChequeM2;
                                MontaReceitaFutura.Add(itReceita);
                                break;
                            case 3:
                                itReceita.TotalAmoMuitoM3 = crReceitasFuturas[0].TotalAmoMuitoM3;
                                itReceita.DCIM3 = crReceitasFuturas[0].DCIM3;
                                itReceita.CaboFrioM3 = crReceitasFuturas[0].CaboFrioM3;
                                itReceita.TotalCarteiraM3 = crReceitasFuturas[0].TotalCarteiraM3;
                                itReceita.TotalCartoCreditoM3 = crReceitasFuturas[0].TotalCartoCreditoM3 + crReceitasFuturas[0].TotalRenegociadoCartaoM3;
                                itReceita.TotalProtestoM3 = crReceitasFuturas[0].TotalProtestoM3;
                                itReceita.TotalChequeM3 = crReceitasFuturas[0].TotalChequeM3;
                                MontaReceitaFutura.Add(itReceita);
                                break;
                            case 4:
                                itReceita.TotalAmoMuitoM4 = crReceitasFuturas[0].TotalAmoMuitoM4;
                                itReceita.DCIM4 = crReceitasFuturas[0].DCIM4;
                                itReceita.CaboFrioM4 = crReceitasFuturas[0].CaboFrioM4;
                                itReceita.TotalCarteiraM4 = crReceitasFuturas[0].TotalCarteiraM4;
                                itReceita.TotalCartoCreditoM4 = crReceitasFuturas[0].TotalCartoCreditoM4 + crReceitasFuturas[0].TotalRenegociadoCartaoM4;
                                itReceita.TotalProtestoM4 = crReceitasFuturas[0].TotalProtestoM4;
                                itReceita.TotalChequeM4 = crReceitasFuturas[0].TotalChequeM4;
                                MontaReceitaFutura.Add(itReceita);
                                break;
                            case 5:
                                itReceita.TotalAmoMuitoM5 = crReceitasFuturas[0].TotalAmoMuitoM5;
                                itReceita.DCIM5 = crReceitasFuturas[0].DCIM5;
                                itReceita.CaboFrioM5 = crReceitasFuturas[0].CaboFrioM5;
                                itReceita.TotalCarteiraM5 = crReceitasFuturas[0].TotalCarteiraM5;
                                itReceita.TotalCartoCreditoM5 = crReceitasFuturas[0].TotalCartoCreditoM5 + crReceitasFuturas[0].TotalRenegociadoCartaoM5;
                                itReceita.TotalProtestoM5 = crReceitasFuturas[0].TotalProtestoM5;
                                itReceita.TotalChequeM5 = crReceitasFuturas[0].TotalChequeM5;
                                MontaReceitaFutura.Add(itReceita);
                                break;
                            case 6:
                                itReceita.TotalAmoMuitoM6 = crReceitasFuturas[0].TotalAmoMuitoM6;
                                itReceita.DCIM6 = crReceitasFuturas[0].DCIM6;
                                itReceita.CaboFrioM6 = crReceitasFuturas[0].CaboFrioM6;
                                itReceita.TotalCarteiraM6 = crReceitasFuturas[0].TotalCarteiraM6;
                                itReceita.TotalCartoCreditoM6 = crReceitasFuturas[0].TotalCartoCreditoM6 + crReceitasFuturas[0].TotalRenegociadoCartaoM6;
                                itReceita.TotalProtestoM6 = crReceitasFuturas[0].TotalProtestoM6;
                                itReceita.TotalChequeM6 = crReceitasFuturas[0].TotalChequeM6;
                                MontaReceitaFutura.Add(itReceita);
                                break;
                            case 7:
                                itReceita.TotalAmoMuitoM7 = crReceitasFuturas[0].TotalAmoMuitoM7;
                                itReceita.DCIM7 = crReceitasFuturas[0].DCIM7;
                                itReceita.CaboFrioM7 = crReceitasFuturas[0].CaboFrioM7;
                                itReceita.TotalCarteiraM7 = crReceitasFuturas[0].TotalCarteiraM7;
                                itReceita.TotalCartoCreditoM7 = crReceitasFuturas[0].TotalCartoCreditoM7 + crReceitasFuturas[0].TotalRenegociadoCartaoM7;
                                itReceita.TotalProtestoM7 = crReceitasFuturas[0].TotalProtestoM7;
                                itReceita.TotalChequeM7 = crReceitasFuturas[0].TotalChequeM7;
                                MontaReceitaFutura.Add(itReceita);
                                break;
                            case 8:
                                itReceita.TotalAmoMuitoM8 = crReceitasFuturas[0].TotalAmoMuitoM8;
                                itReceita.DCIM8 = crReceitasFuturas[0].DCIM8;
                                itReceita.CaboFrioM8 = crReceitasFuturas[0].CaboFrioM8;
                                itReceita.TotalCarteiraM8 = crReceitasFuturas[0].TotalCarteiraM8;
                                itReceita.TotalCartoCreditoM8 = crReceitasFuturas[0].TotalCartoCreditoM8 + crReceitasFuturas[0].TotalRenegociadoCartaoM8;
                                itReceita.TotalProtestoM8 = crReceitasFuturas[0].TotalProtestoM8;
                                itReceita.TotalChequeM8 = crReceitasFuturas[0].TotalChequeM8;
                                MontaReceitaFutura.Add(itReceita);
                                break;
                            case 9:
                                itReceita.TotalAmoMuitoM9 = crReceitasFuturas[0].TotalAmoMuitoM9;
                                itReceita.DCIM9 = crReceitasFuturas[0].DCIM9;
                                itReceita.CaboFrioM9 = crReceitasFuturas[0].CaboFrioM9;
                                itReceita.TotalCarteiraM9 = crReceitasFuturas[0].TotalCarteiraM9;
                                itReceita.TotalCartoCreditoM9 = crReceitasFuturas[0].TotalCartoCreditoM9 + crReceitasFuturas[0].TotalRenegociadoCartaoM9;
                                itReceita.TotalProtestoM9 = crReceitasFuturas[0].TotalProtestoM9;
                                itReceita.TotalChequeM9 = crReceitasFuturas[0].TotalChequeM9;
                                MontaReceitaFutura.Add(itReceita);
                                break;
                            case 10:
                                itReceita.TotalAmoMuitoM10 = crReceitasFuturas[0].TotalAmoMuitoM10;
                                itReceita.DCIM10 = crReceitasFuturas[0].DCIM10;
                                itReceita.CaboFrioM10 = crReceitasFuturas[0].CaboFrioM10;
                                itReceita.TotalCarteiraM10 = crReceitasFuturas[0].TotalCarteiraM10;
                                itReceita.TotalCartoCreditoM10 = crReceitasFuturas[0].TotalCartoCreditoM10 + crReceitasFuturas[0].TotalRenegociadoCartaoM10;
                                itReceita.TotalProtestoM10 = crReceitasFuturas[0].TotalProtestoM10;
                                itReceita.TotalChequeM10 = crReceitasFuturas[0].TotalChequeM10;
                                MontaReceitaFutura.Add(itReceita);
                                break;
                            case 11:
                                itReceita.TotalAmoMuitoM11 = crReceitasFuturas[0].TotalAmoMuitoM11;
                                itReceita.DCIM11 = crReceitasFuturas[0].DCIM11;
                                itReceita.CaboFrioM11 = crReceitasFuturas[0].CaboFrioM11;
                                itReceita.TotalCarteiraM11 = crReceitasFuturas[0].TotalCarteiraM11;
                                itReceita.TotalCartoCreditoM11 = crReceitasFuturas[0].TotalCartoCreditoM11 + crReceitasFuturas[0].TotalRenegociadoCartaoM11;
                                itReceita.TotalProtestoM11 = crReceitasFuturas[0].TotalProtestoM11;
                                itReceita.TotalChequeM11 = crReceitasFuturas[0].TotalChequeM11;
                                MontaReceitaFutura.Add(itReceita);
                                break;
                            case 12:
                                itReceita.TotalAmoMuitoM12 = crReceitasFuturas[0].TotalAmoMuitoM12;
                                itReceita.DCIM12 = crReceitasFuturas[0].DCIM12;
                                itReceita.CaboFrioM12 = crReceitasFuturas[0].CaboFrioM12;
                                itReceita.TotalCarteiraM12 = crReceitasFuturas[0].TotalCarteiraM12;
                                itReceita.TotalCartoCreditoM12 = crReceitasFuturas[0].TotalCartoCreditoM12 + crReceitasFuturas[0].TotalRenegociadoCartaoM12;
                                itReceita.TotalProtestoM12 = crReceitasFuturas[0].TotalProtestoM12;
                                itReceita.TotalChequeM12 = crReceitasFuturas[0].TotalChequeM12;
                                MontaReceitaFutura.Add(itReceita);
                                break;


                        }

                       
                    }

                }



            }

            return MontaReceitaFutura;
        }

        public decimal GetViewByIdVenda(int id, int Tipo,int TipoPagamento)
        {
            decimal valor = 0;
            var cn = new DapperConnection<ContasReceberView>();

            string SQL = String.Empty;

            if(Tipo == 1)
            {
                if(TipoPagamento > 0)
                {
                    SQL = " select contasreceber.IdTipoDocumento,contasreceber.ValorParcela from contasreceber where contasreceber.IdFatNfe = " + id + " AND contasreceber.IdTipoDocumento = " + TipoPagamento + " AND NOT ISNULL(Idcliente)";

                }
                else
                {
                    SQL = " select contasreceber.IdTipoDocumento,contasreceber.ValorParcela from contasreceber where contasreceber.IdFatNfe = " + id + " AND contasreceber.IdTipoDocumento <> 1 AND  " +
                          "contasreceber.IdTipoDocumento <> 2 AND contasreceber.IdTipoDocumento <> 10 AND contasreceber.IdTipoDocumento <> 11 AND contasreceber.IdTipoDocumento <> 99 AND contasreceber.IdTipoDocumento <> 100 " +
                          "AND contasreceber.IdTipoDocumento <> 101 AND contasreceber.IdTipoDocumento <> 102  AND NOT ISNULL(Idcliente) ";
                }
                
            }
            else
            {
                if (TipoPagamento > 0)
                {
                    SQL = "select contasreceber.IdTipoDocumento,contasreceber.ValorParcela from contasreceber where contasreceber.IdNotaconsumidor = " + id + " AND contasreceber.IdTipoDocumento = " + TipoPagamento + " AND NOT ISNULL(Idcliente)";
                }
                else
                {
                    SQL = " select contasreceber.IdTipoDocumento,contasreceber.ValorParcela from contasreceber where contasreceber.IdNotaconsumidor = " + id + " AND contasreceber.IdTipoDocumento <> 1 AND  " +
                          "contasreceber.IdTipoDocumento <> 2 AND contasreceber.IdTipoDocumento <> 10 AND contasreceber.IdTipoDocumento <> 11 AND contasreceber.IdTipoDocumento <> 99 AND contasreceber.IdTipoDocumento <> 100 " +
                          "AND contasreceber.IdTipoDocumento <> 101 AND contasreceber.IdTipoDocumento <> 102  AND NOT ISNULL(Idcliente) ";
                }
                
            }

            var cr = new ContasReceberView();

            var dados = cn.ExecuteStringSqlToList(cr, SQL.ToString());

            if(dados != null && dados.Count() > 0)
            {
                foreach (var item in dados)
                {
                    valor += item.ValorParcela;
                }
            }

            return valor;
        }

        public string ScoreDC (int idCliente)
        {
            string Letras = String.Empty;
            string DiasVencidos = String.Empty;
            var cn = new DapperConnection<DiasVencidos>();
            DateTime DataFim = DateTime.Now;
            string FimDoPeriodo = DataFim.ToString("yyyy-MM-dd");
            var cr = new DiasVencidos();

            // Não pode ser dos ultimos 6 meses, tem que ser uma analise geral, ver os titulos dos clientes e analisar
            //Primeira analise, clientes protestados que estão com titulo em aberto, bolinha preta
            //Segunda analise, listar todos os titulos e fazer a analise por ponto
            // Seria assim, de 0 a 5 dias, ganha 0 ponto
            // de 6 a 10 dias ganha 2 pontos
            //11 a 20 dias ganha 3 pontos
            // > 21 ganha 5 pontos
            // aí somamos os pontos e definimos en qual bolinha ele está 
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" SELECT contasreceber.*,contasreceber.DataPagamento as DataPagamento,contasreceber.IdTipoCobranca as TipoCobranca,IF(NOT ISNULL(contasreceber.DataPagamento), IF(DATEDIFF (contasreceber.DataPagamento, contasreceber.dataVencimento) <0,0,DATEDIFF (contasreceber.DataPagamento, contasreceber.dataVencimento)),IF(DATEDIFF (now(), contasreceber.dataVencimento) <0,0,DATEDIFF (now(), contasreceber.dataVencimento))) as NumeroDeDiasVencidos ");
            sql.AppendLine(" from contasreceber where contasreceber.DataEmissao BETWEEN DATE_SUB(curdate(),INTERVAL 6 MONTH) AND " + "'" + FimDoPeriodo + "'" + " AND contasreceber.IdTipoDocumento = 3 AND contasreceber.Ativo = 1 " + " AND contasreceber.IdCliente = " + idCliente);


            var dados = cn.ExecuteStringSqlToList(cr, sql.ToString());
            int MaiorDiaVencido = 0;

            if (dados != null && dados.Count() >0)
            {
                foreach (var item in dados)
                {
                    if(item.TipoCobranca == 9 && item.DataPagamento == null)
                    {
                        Letras = "P";
                        return Letras;

                    }
                }

                MaiorDiaVencido = dados.Max(x => x.NumeroDeDiasVencidos);
                if(MaiorDiaVencido <=5)
                {
                    Letras = "A";
                }
                else if(MaiorDiaVencido >= 6 && MaiorDiaVencido <= 10)
                {
                    Letras = "B";
                }
                else if (MaiorDiaVencido >= 11 && MaiorDiaVencido <= 15)
                {
                    Letras = "C";
                }
                else if (MaiorDiaVencido > 15)
                {
                    Letras = "D";
                }
            }
            else
            {
                Letras = "A";
            }

            return Letras;

        }
    }
}
