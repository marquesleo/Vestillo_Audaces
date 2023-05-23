using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Business.Service;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class ContasPagarRepository: GenericRepository<ContasPagar>
    {
        public ContasPagarRepository()
            : base(new DapperConnection<ContasPagar>())
        {
        }

        public IEnumerable<ContasPagarView> GetAllView()
        {
            var cn = new DapperConnection<ContasPagarView>();
            
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT CR.Id, NumTitulo,tipodocumentos.descricao as TipoDocumento, Prefixo, Parcela, DataEmissao, DataVencimento, dataPagamento, ValorParcela, ValorPago, CR.Saldo, IFNULL(CR.Ativo,0) as Ativo, ");
            SQL.AppendLine("    C.Nome AS NomeFornecedor, IFNULL(CR.Status, 0) AS Status,  TC.Descricao AS TipoCobrancaDescricao, CR.PossuiBoleto,");
            SQL.AppendLine("    B.Descricao AS NomeBanco, CR.Obs as Obs, CR.Obs as Obs, NF.referencia as Natureza,NF.descricao as NaturezaDesc, ");
            SQL.AppendLine("    C.razaosocial AS RazaoSocialColaborador, C.cnpjcpf AS CNPJCPF ");
            SQL.AppendLine("FROM	ContasPagar AS CR");
            SQL.AppendLine("    LEFT JOIN colaboradores AS C ON C.Id = CR.IdFornecedor");
            SQL.AppendLine("    LEFT JOIN bancos AS B ON B.Id = CR.IdBanco");
            SQL.AppendLine("    LEFT JOIN TiposCobranca TC ON TC.Id = CR.IdTipoCobranca");
            SQL.AppendLine("    LEFT JOIN naturezasfinanceiras NF ON NF.Id = CR.IdNaturezaFinanceira");
            SQL.AppendLine("    LEFT JOIN tipodocumentos ON tipodocumentos.Id = CR.IdTipoDocumento");
            SQL.AppendLine("WHERE (IdPedidoCompra IS NULL OR IdPedidoCompra = 0) AND " + FiltroEmpresa("", "CR"));
            SQL.AppendLine(" ORDER BY NumTitulo, Prefixo, Parcela");

           
            return cn.ExecuteStringSqlToList(new ContasPagarView(), SQL.ToString());
        }

        public IEnumerable<TitulosBaixaLoteView> GetParcelasParaBaixaEmLote(int fornecedorId, DateTime? dataVencimentoInicial = null, DateTime? dataVencimentoFinal = null)
        {
            var cn = new DapperConnection<TitulosBaixaLoteView>();
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	CP.Id, CP.NumTitulo, Prefixo, Parcela, DataEmissao, DataVencimento, ValorParcela, CP.Saldo, ");
            SQL.AppendLine("		C.Nome As NomeColaborador, TC.Descricao As TipoCobrancaDescricao, B.descricao As NomeBanco,");
            SQL.AppendLine("        IFNULL(CP.Status, 0) AS Status, CP.Juros, CP.Desconto, CP.ValorPago");
            SQL.AppendLine("FROM 	ContasPagar CP");
            SQL.AppendLine("	LEFT JOIN colaboradores AS C ON C.Id = CP.IdFornecedor");
            SQL.AppendLine("    LEFT JOIN bancos AS B ON B.Id = CP.IdBanco");
            SQL.AppendLine("	LEFT JOIN TiposCobranca TC ON TC.Id = CP.IdTipoCobranca");
            SQL.AppendLine("WHERE	CP.Saldo > 0");
            SQL.AppendLine("        AND " + FiltroEmpresa("", "CP"));
            SQL.AppendLine("		AND IdFornecedor = " + fornecedorId.ToString());
            SQL.AppendLine("        AND CP.Ativo = 1");

            if (dataVencimentoInicial != null)
            {
                SQL.AppendLine("        AND CP.DataVencimento >= '" + dataVencimentoInicial.GetValueOrDefault().Date.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            }

            if (dataVencimentoFinal != null)
            {
                SQL.AppendLine("        AND CP.DataVencimento <= '" + dataVencimentoFinal.GetValueOrDefault().Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            }


            SQL.AppendLine("ORDER BY CP.DataVencimento;");

            return cn.ExecuteStringSqlToList(new TitulosBaixaLoteView(), SQL.ToString());
        }

        public IEnumerable<ContasPagarView> GetViewByReferencia(string referencia, int[] status = null)
        {
            var cn = new DapperConnection<ContasPagarView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT CR.Id, NumTitulo, Prefixo, Parcela, DataEmissao, DataVencimento, dataPagamento, ValorParcela, ValorPago, CR.Saldo, IFNULL(CR.Ativo,0) as Ativo, ");
            SQL.AppendLine("    C.Nome AS NomeFornecedor, IFNULL(CR.Status, 0) AS Status,  TC.Descricao AS TipoCobrancaDescricao, ");
            SQL.AppendLine("    B.Descricao AS NomeBanco, CR.Obs as Obs");
            SQL.AppendLine("FROM	ContasPagar AS CR");
            SQL.AppendLine("    LEFT JOIN colaboradores AS C ON C.Id = CR.IdFornecedor");
            SQL.AppendLine("    LEFT JOIN bancos AS B ON B.Id = CR.IdBanco");
            SQL.AppendLine("    LEFT JOIN TiposCobranca TC ON TC.Id = CR.IdTipoCobranca");
            SQL.AppendLine("WHERE  " + FiltroEmpresa("CR.IdEmpresa"));
            SQL.AppendLine("    AND CR.NumTitulo LIKE '%" + referencia + "%'");

            if (status != null && status.Length > 0)
                SQL.AppendLine(" AND CR.Status IN (" + string.Join(",", status) + ") ");

            SQL.AppendLine(" ORDER BY NumTitulo, Prefixo, Parcela");

            return cn.ExecuteStringSqlToList(new ContasPagarView(), SQL.ToString());
        }


        public IEnumerable<TitulosBaixaLoteView> GetParcelasParaEstornarBaixaEmLote(int contasPagarBaixaId)
        {
            var cn = new DapperConnection<TitulosBaixaLoteView>();
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	CP.Id, CP.NumTitulo, Prefixo, Parcela, DataEmissao, DataVencimento, ValorParcela, CP.Saldo, ");
            SQL.AppendLine("		C.Nome As NomeColaborador, TC.Descricao As TipoCobrancaDescricao, B.descricao As NomeBanco,");
            SQL.AppendLine("        IFNULL(CP.Status, 0) AS Status, CP.Juros, CP.Desconto, C.Id AS ColaboradorId, C.Referencia AS RefColaborador, CPB.Id AS BaixaId");
            SQL.AppendLine("FROM 	ContasPagar CP");
            SQL.AppendLine("	LEFT JOIN colaboradores AS C ON C.Id = CP.IdFornecedor");
            SQL.AppendLine("    LEFT JOIN bancos AS B ON B.Id = CP.IdBanco");
            SQL.AppendLine("	LEFT JOIN TiposCobranca TC ON TC.Id = CP.IdTipoCobranca");
            SQL.AppendLine("	INNER JOIN contaspagarbaixa CPB ON CPB.ContasPagarId = CP.Id");
            SQL.AppendLine("WHERE CPB.ContasPagarBaixaLoteId = " + contasPagarBaixaId);
            SQL.AppendLine("ORDER BY CP.DataVencimento;");

            return cn.ExecuteStringSqlToList(new TitulosBaixaLoteView(), SQL.ToString());
        }

        public ContasPagarView GetViewById(int id)
        {
            var cn = new DapperConnection<ContasPagarView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT CR.*, ");
            SQL.AppendLine("    C.Referencia AS RefFornecedor,");
            SQL.AppendLine("    C.Nome AS NomeFornecedor,");
            SQL.AppendLine("    B.Descricao AS NomeBanco");
            SQL.AppendLine("FROM	ContasPagar AS CR");
            SQL.AppendLine("    LEFT JOIN colaboradores AS C ON C.Id = CR.IdFornecedor");
            SQL.AppendLine("    LEFT JOIN bancos AS B ON B.Id = CR.IdBanco");
            SQL.AppendLine("WHERE CR.Id = " + id.ToString());
            SQL.AppendLine(" ORDER BY NumTitulo, Prefixo, Parcela");

            var cr = new ContasPagarView();

            cn.ExecuteToModel(ref cr, SQL.ToString());

            return cr;
        }

        public decimal GetByAteData(DateTime data)
        {
            var cn = new DapperConnection<ContasPagar>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT SUM(CR.saldo) AS SALDO ");
            SQL.AppendLine("FROM	ContasPagar AS CR");
            SQL.AppendLine("WHERE DATE(CR.DataVencimento) <= '" + data.ToString("yyyy-MM-dd") + "' AND CR.Saldo > 0 AND CR.Ativo = 1 ");
            SQL.AppendLine("        AND " + FiltroEmpresa("", "CR"));
            SQL.AppendLine(" ORDER BY NumTitulo, Prefixo, Parcela");

            var cr = new ContasPagar();

            cn.ExecuteToModel(ref cr, SQL.ToString());

            return cr.Saldo;
        }

        public decimal GetByData(DateTime data)
        {
            var cn = new DapperConnection<ContasPagar>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT SUM(CR.saldo) AS SALDO ");
            SQL.AppendLine("FROM	ContasPagar AS CR");
            SQL.AppendLine("WHERE DATE(CR.DataVencimento) = '" + data.ToString("yyyy-MM-dd") + "' AND CR.Saldo > 0  AND CR.Ativo = 1  ");
            SQL.AppendLine("        AND " + FiltroEmpresa("", "CR"));
            SQL.AppendLine(" ORDER BY NumTitulo, Prefixo, Parcela");

            var cr = new ContasPagar();

            cn.ExecuteToModel(ref cr, SQL.ToString());
            
            return cr.Saldo;
        }

        public decimal GetByAteDataEBanco(DateTime data, int bancoId)
        {
            var cn = new DapperConnection<ContasPagar>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT SUM(CR.saldo) AS SALDO ");
            SQL.AppendLine("FROM	ContasPagar AS CR");
            SQL.AppendLine("WHERE DATE(CR.DataVencimento) <= '" + data.ToString("yyyy-MM-dd") + "' AND CR.Saldo > 0 AND CR.Ativo = 1 AND CR.idBanco = " + bancoId);
            SQL.AppendLine("        AND " + FiltroEmpresa("", "CR"));
            SQL.AppendLine(" ORDER BY NumTitulo, Prefixo, Parcela");

            var cr = new ContasPagar();

            cn.ExecuteToModel(ref cr, SQL.ToString());

            return cr.Saldo;
        }

        public decimal GetByDataEBanco(DateTime data, int bancoId)
        {
            var cn = new DapperConnection<ContasPagar>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT SUM(CR.saldo) AS SALDO ");
            SQL.AppendLine("FROM	ContasPagar AS CR");
            SQL.AppendLine("WHERE DATE(CR.DataVencimento) = '" + data.ToString("yyyy-MM-dd") + "' AND CR.Saldo > 0 AND CR.Ativo = 1 AND CR.idBanco = " + bancoId);
            SQL.AppendLine("        AND " + FiltroEmpresa("", "CR"));
            SQL.AppendLine(" ORDER BY NumTitulo, Prefixo, Parcela");

            var cr = new ContasPagar();

            cn.ExecuteToModel(ref cr, SQL.ToString());

            return cr.Saldo;
        }

        public List<ContasPagarView> GetByDataEBancoConsulta(DateTime data, int bancoId)
        {
            var cn = new DapperConnection<ContasPagarView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT CR.Id, NumTitulo, Prefixo, Parcela, DataEmissao, DataVencimento, dataPagamento, ValorParcela, ValorPago, CR.Saldo, IFNULL(CR.Ativo,0) as Ativo, ");
            SQL.AppendLine("    C.Nome AS NomeFornecedor, IFNULL(CR.Status, 0) AS Status,  TC.Descricao AS TipoCobrancaDescricao, ");
            SQL.AppendLine("    B.Descricao AS NomeBanco, CR.Obs as Obs");
            SQL.AppendLine("FROM	ContasPagar AS CR");
            SQL.AppendLine("    LEFT JOIN colaboradores AS C ON C.Id = CR.IdFornecedor");
            SQL.AppendLine("    LEFT JOIN bancos AS B ON B.Id = CR.IdBanco");
            SQL.AppendLine("    LEFT JOIN TiposCobranca TC ON TC.Id = CR.IdTipoCobranca");
            SQL.AppendLine("WHERE DATE(CR.DataVencimento) = '" + data.ToString("yyyy-MM-dd") + "' AND CR.Saldo > 0 AND CR.Ativo = 1 ");
            SQL.AppendLine("        AND " + FiltroEmpresa("", "CR"));

            if (bancoId > 0)
                SQL.AppendLine(" AND CR.idBanco = " + bancoId);

            SQL.AppendLine(" ORDER BY CR.DataVencimento");

            return cn.ExecuteStringSqlToList(new ContasPagarView(), SQL.ToString()).ToList();
        }

        public List<ContasPagarView> GetByAteDataEBancoConsulta(DateTime data, int bancoId)
        {
            var cn = new DapperConnection<ContasPagarView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT CR.Id, NumTitulo, Prefixo, Parcela, DataEmissao, DataVencimento, dataPagamento, ValorParcela, ValorPago, CR.Saldo, IFNULL(CR.Ativo,0) as Ativo, ");
            SQL.AppendLine("    C.Nome AS NomeFornecedor, IFNULL(CR.Status, 0) AS Status,  TC.Descricao AS TipoCobrancaDescricao, ");
            SQL.AppendLine("    B.Descricao AS NomeBanco, CR.Obs as Obs");
            SQL.AppendLine("FROM	ContasPagar AS CR");
            SQL.AppendLine("    LEFT JOIN colaboradores AS C ON C.Id = CR.IdFornecedor");
            SQL.AppendLine("    LEFT JOIN bancos AS B ON B.Id = CR.IdBanco");
            SQL.AppendLine("    LEFT JOIN TiposCobranca TC ON TC.Id = CR.IdTipoCobranca");
            SQL.AppendLine("WHERE DATE(CR.DataVencimento) <= '" + data.ToString("yyyy-MM-dd") + "' AND CR.Saldo > 0 AND CR.Ativo = 1 ");
            SQL.AppendLine("        AND " + FiltroEmpresa("", "CR"));

            if (bancoId > 0)
                SQL.AppendLine(" AND CR.idBanco = " + bancoId);

            SQL.AppendLine(" ORDER BY CR.DataVencimento");

            return cn.ExecuteStringSqlToList(new ContasPagarView(), SQL.ToString()).ToList();
        }

        public IEnumerable<ContasPagarView> GetListaPorCampoEValor(string campoBusca, string valor)
        {
            var cn = new DapperConnection<ContasPagarView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT CR.*, ");
            SQL.AppendLine("    C.Nome AS NomeFornecedor,");
            SQL.AppendLine("    B.Descricao AS NomeBanco ");
            SQL.AppendLine("FROM	ContasPagar AS CR ");
            SQL.AppendLine("    LEFT JOIN colaboradores AS C ON C.Id = CR.IdFornecedor ");
            SQL.AppendLine("    LEFT JOIN bancos AS B ON B.Id = CR.IdBanco ");
            SQL.AppendLine(" WHERE " + FiltroEmpresa("", "CR"));
            SQL.AppendLine(" AND CR." + campoBusca + " = '" + valor + "'");
            SQL.AppendLine(" ORDER BY NumTitulo, Prefixo, Parcela");

            return cn.ExecuteStringSqlToList(new ContasPagarView(), SQL.ToString());
        }

        public IEnumerable<ContasPagarView> GetByPedidoCompra(int pedidoCompraId)
        {
            var cn = new DapperConnection<ContasPagarView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT CR.*, ");
            SQL.AppendLine("    C.Nome AS NomeFornecedor,");
            SQL.AppendLine("    B.Descricao AS NomeBanco ");
            SQL.AppendLine("FROM	ContasPagar AS CR ");
            SQL.AppendLine("    LEFT JOIN colaboradores AS C ON C.Id = CR.IdFornecedor ");
            SQL.AppendLine("    LEFT JOIN bancos AS B ON B.Id = CR.IdBanco ");
            SQL.AppendLine(" WHERE CR.IdPedidoCompra = " + pedidoCompraId.ToString() + "");
            SQL.AppendLine(" ORDER BY NumTitulo, Prefixo, Parcela");

            return cn.ExecuteStringSqlToList(new ContasPagarView(), SQL.ToString());
        }

        public IEnumerable<TitulosView> GetParcelasRelatorio(FiltroRelatorioTitulos filtro)
        {
            var cn = new DapperConnection<TitulosView>();
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	CR.Id, CR.NumTitulo,'' as NotaFiscal, CR.Prefixo, CR.Parcela, CR.DataEmissao, CR.DataVencimento, CR.ValorParcela, CR.Saldo, CR.DataPagamento,TD.descricao as TipoDocumentos, ");
            SQL.AppendLine("		C.referencia AS RefColaborador, C.Nome As NomeColaborador, TC.Descricao As TipoCobrancaDescricao, B.descricao As NomeBanco,");
            SQL.AppendLine("        IFNULL(CR.Status, 0) AS Status, CR.Juros, CR.Desconto, CR.ValorPago, N.descricao AS NaturezaDescricao, ");
            SQL.AppendLine("        CASE WHEN IFNULL(CR.Ativo,0) = 1 THEN 'Sim' ELSE 'Não' END AS Ativo ");
            if (filtro.ExibirBaixa)
            {
                SQL.AppendLine("        ,BX.DataBaixa, BX.ValorDinheiro AS ValorPagoDinheiro, SUM(CH.Valor) AS ValorPagoCheque, BX.ValorCredito AS ValorPagoCredito, BX.Obs AS ObsBaixa ");
            }            

            SQL.AppendLine("FROM 	contaspagar AS CR");
            SQL.AppendLine("	LEFT JOIN colaboradores AS C ON C.Id = CR.IdFornecedor");
            SQL.AppendLine("    LEFT JOIN bancos AS B ON B.Id = CR.IdBanco");
            SQL.AppendLine("	LEFT JOIN TiposCobranca TC ON TC.Id = CR.IdTipoCobranca");
            SQL.AppendLine("    LEFT JOIN naturezasfinanceiras N ON N.id = CR.IdNaturezaFinanceira");
            SQL.AppendLine("    LEFT JOIN tipodocumentos TD ON TD.id = CR.IdTipoDocumento");

            if (filtro.ExibirBaixa)
            {
                SQL.AppendLine("    LEFT JOIN contaspagarbaixa BX ON BX.ContasPagarId = CR.Id");
                SQL.AppendLine("    LEFT JOIN Cheques CH ON CH.ContasPagarBaixaId = BX.Id");
            }

            SQL.AppendLine("WHERE " + FiltroEmpresa("", "CR"));

            if (filtro.Colaboradores != null && filtro.Colaboradores.Length > 0)
                SQL.AppendLine("        AND CR.IdFornecedor IN (" + string.Join(", ", filtro.Colaboradores) + ")");

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
                SQL.AppendLine("        AND CR.DataPagamento BETWEEN '" + filtro.DataPagamentoInicial.GetValueOrDefault().Date.ToString("yyyy-MM-dd HH:mm:ss") + "'");

            if (filtro.DataPagamentoFinal != null)
                SQL.AppendLine("        AND  '" + filtro.DataPagamentoFinal.GetValueOrDefault().Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "'");

            //Tipo Cobrança
            if (filtro.TipoCobranca != null && filtro.TipoCobranca.Length > 0)
                SQL.AppendLine("        AND CR.IdTipoCobranca IN (" + string.Join(", ", filtro.TipoCobranca) + ")");

            //Status do titulo
            if (filtro.Status != 0)
            {
                if (filtro.Status == 1 || filtro.Status == 2 || filtro.Status == 3) //Só Aberto, só baixado, só parcial
                {
                    SQL.AppendLine("        AND CR.Status = " + filtro.Status);
                }
                else if (filtro.Status == 4)
                {
                    SQL.AppendLine("        AND CR.Status IN(1,2)"); // Aberto e parcial
                }
            }

            if (filtro.ExibirBaixa)
            {
                SQL.AppendLine("    GROUP BY BX.id");
                filtro.Agrupar = true;
            }

            if (filtro.Agrupar == true)
            {
                SQL.AppendLine("        ORDER BY " + filtro.ColunaParaAgrupar);
            }

            return cn.ExecuteStringSqlToList(new TitulosView(), SQL.ToString());
        }

        public ContasPagar GetByCheque(int chequeId)
        {
            ContasPagar cr = new ContasPagar();
            _cn.ExecuteToModel("IdCheque = " + chequeId.ToString(), ref cr);

            return cr;
        }

        public void InserirNotinha(int IdContasPagar,int NumLinha,int IdAtividade,int Quantidade,decimal Preco,decimal Total,DateTime DataAtividade)
        {
            string SQl = String.Empty;

            

            SQl = "insert into contaspagarfaccao " +
                  "  (IdContasPagar, NumLinha, IdAtividade, Quantidade, " +
                  "  Preco,  Total,DataAtividade) " +
                  " Values (" +
                   IdContasPagar + "," + NumLinha + "," + IdAtividade + "," + Quantidade + "," +
                   Preco.ToString().Replace(",",".") + "," + Total.ToString().Replace(",",".") + ","+ "'" + DataAtividade.ToString("yyyy-MM-dd") + "'"  + ")";
            _cn.ExecuteNonQuery(SQl);

        }


        public IEnumerable<ContasPagarFaccaoView> GetAtividadesFaccao(int IdTitulo)
        {
            var cn = new DapperConnection<ContasPagarFaccaoView>();
            string SQL = String.Empty;

            SQL = " SELECT *,atividadefaccao.Referencia as RefAtividade,atividadefaccao.Descricao as DescAtividade,DataAtividade from contaspagarfaccao " +
                  " INNER JOIN atividadefaccao ON atividadefaccao.Id = contaspagarfaccao.IdAtividade " +
                  " WHERE contaspagarfaccao.IdContasPagar =  " + IdTitulo;
            return cn.ExecuteStringSqlToList(new ContasPagarFaccaoView(), SQL);
        }

        public IEnumerable<ContasPagarView> GetTitulosComAtiviadeFaccao(int IdFaccao, DateTime DataInicial,DateTime DataFinal)
        {
            var cn = new DapperConnection<ContasPagarView>();
            string SQL = String.Empty;

            SQL = " SELECT * from contaspagar " +
                  " WHERE contaspagar.IdFornecedor = " + IdFaccao +
                  " AND contaspagar.Notinha = 1 AND SUBSTRING(dataVencimento,1,10) BETWEEN '" + DataInicial.ToString("yyyy-MM-dd") + "'" +
                  " AND '" + DataFinal.ToString("yyyy-MM-dd") + "'";

            return cn.ExecuteStringSqlToList(new ContasPagarView(), SQL);
        }

        public IEnumerable<ContasPagarRelNotinhaFaccao> GetNotinhasFaccaoRota(int IdRota, DateTime DataInicial, DateTime DataFinal)
        {
            var cnCol = new DapperConnection<Colaborador>();
            string SQLCol = String.Empty;
            string idFaccoes = String.Empty;


            SQLCol = " SELECT Id FROM colaboradores where colaboradores.idrota = " + IdRota + " AND colaboradores.faccao = 1 ";
            var Dados = cnCol.ExecuteStringSqlToList(new Colaborador(), SQLCol);

            foreach (var itemFaccoes in Dados)
            {
                idFaccoes += itemFaccoes.Id.ToString() + ","; 
            }

            if(idFaccoes.Length != 0)
            {
                idFaccoes = idFaccoes.Remove(idFaccoes.ToString().Length - 1, 1);
            }

          


            var cn = new DapperConnection<ContasPagarRelNotinhaFaccao>();
            string SQL = String.Empty;



            SQL = " SELECT SUM(contaspagar.Vale) as Vale,SUM(contaspagar.TotalFator) as TotalFator,SUM(round(contaspagar.Saldo,2)) as Saldo, contaspagar.numtitulo as reftitulo, contaspagar.Id as NotinhaId, colaboradores.idrota as RotaId, colaboradores.id as FaccaoId, colaboradores.nome as DescFacao, " +
                  " rotavisitas.descricao as DescRota, contaspagarfaccao.DataAtividade,atividadefaccao.Descricao as DescAtividade, " +
                  " SUM(contaspagarfaccao.Quantidade) as Quantidade,contaspagarfaccao.Preco,SUM(contaspagarfaccao.Total) as Total from contaspagar " +
                  " INNER JOIN contaspagarfaccao ON contaspagarfaccao.IdContasPagar = contaspagar.Id " +
                  " INNER JOIN atividadefaccao ON atividadefaccao.Id = contaspagarfaccao.IdAtividade " +
                  " INNER JOIN colaboradores ON colaboradores.id = contaspagar.IdFornecedor " +
                  " INNER JOIN rotavisitas ON rotavisitas.id = colaboradores.idrota " +
                  "  WHERE contaspagar.IdFornecedor IN(" + idFaccoes + ")" +                  
                  " AND contaspagar.Notinha = 1 AND SUBSTRING(dataVencimento,1,10) BETWEEN '" + DataInicial.ToString("yyyy-MM-dd") + "'" +
                  " AND '" + DataFinal.ToString("yyyy-MM-dd") + "'" +
                  "  group by contaspagar.IdFornecedor,contaspagarfaccao.DataAtividade,contaspagarfaccao.IdAtividade,contaspagarfaccao.Preco " +
                  " order by contaspagar.IdFornecedor,contaspagarfaccao.DataAtividade " ;

            var Notas = cn.ExecuteStringSqlToList(new ContasPagarRelNotinhaFaccao(), SQL);




            return Notas;
            // cn.ExecuteStringSqlToList(new ContasPagarRelNotinhaFaccao(), SQL);
        }

        public IEnumerable<ContasPagarView> GetNotinhasFaccaoRotaListagem(int IdRota, DateTime DataInicial, DateTime DataFinal)
        {
            var cnCol = new DapperConnection<Colaborador>();
            string SQLCol = String.Empty;
            string idFaccoes = String.Empty;


            SQLCol = " SELECT Id FROM colaboradores where colaboradores.idrota = " + IdRota + " AND colaboradores.faccao = 1 ";
            var Dados = cnCol.ExecuteStringSqlToList(new Colaborador(), SQLCol);

            foreach (var itemFaccoes in Dados)
            {
                idFaccoes += itemFaccoes.Id.ToString() + ",";
            }

            if (idFaccoes.Length != 0)
            {
                idFaccoes = idFaccoes.Remove(idFaccoes.ToString().Length - 1, 1);
            }




            var cn = new DapperConnection<ContasPagarView>();
            string SQL = String.Empty;



            SQL = " SELECT colaboradores.id as IdFornecedor, colaboradores.nome as RazaoSocialColaborador " +
                  " from contaspagar " +
                  " INNER JOIN colaboradores ON colaboradores.id = contaspagar.IdFornecedor " +                  
                  "  WHERE contaspagar.IdFornecedor IN(" + idFaccoes + ")" +
                  " AND contaspagar.Notinha = 1 AND SUBSTRING(dataVencimento,1,10) BETWEEN '" + DataInicial.ToString("yyyy-MM-dd") + "'" +
                  " AND '" + DataFinal.ToString("yyyy-MM-dd") + "'" +
                  "  group by contaspagar.IdFornecedor " +
                  " order by colaboradores.nome ";

            var Notas = cn.ExecuteStringSqlToList(new ContasPagarView(), SQL);




            return Notas;
            // cn.ExecuteStringSqlToList(new ContasPagarRelNotinhaFaccao(), SQL);
        }

        public void UpdateAtualizaTitulo(int IdTitulo,int faccao,DateTime dataVencimento,Decimal Parcela, decimal Vale, decimal Fator1, decimal Fator2, decimal TotalFator, DateTime dataInclusao)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("UPDATE  contaspagar SET IdFornecedor = " + faccao);
            SQL.AppendLine(",Saldo = " + Parcela.ToString().Replace(",","."));
            SQL.AppendLine(",ValorParcela = " + Parcela.ToString().Replace(",", "."));
            SQL.AppendLine(",ValorParcela = " + Parcela.ToString().Replace(",", "."));
            SQL.AppendLine(",Vale = " + Vale.ToString().Replace(",", "."));
            SQL.AppendLine(",Fator1 = " + Fator1.ToString().Replace(",", "."));
            SQL.AppendLine(",Fator2 = " + Fator2.ToString().Replace(",", "."));
            SQL.AppendLine(",TotalFator = " + TotalFator.ToString().Replace(",", "."));
            SQL.AppendLine(",dataVencimento = " + "'" + dataVencimento.ToString("yyyy-MM-dd") + "'");
            SQL.AppendLine(",DataEmissao = " + "'" + dataInclusao.ToString("yyyy-MM-dd") + "'");
            SQL.AppendLine(" WHERE Id = ");
            SQL.Append(IdTitulo);

            _cn.ExecuteNonQuery(SQL.ToString());

            string SQL2 = String.Empty;

            SQL2 = "DELETE FROM contaspagarfaccao where contaspagarfaccao.IdContasPagar = " + IdTitulo;

            _cn.ExecuteNonQuery(SQL2);
        }

        public void AtualizaContadoCtp(int NovoContador)
        {
            string SQL = String.Empty;
            SQL = "UPDATE contadorescodigo set ContadorAtual = " + NovoContador + " WHERE Nome = 'ContasPagar'";
            _cn.ExecuteNonQuery(SQL);
        }


        public IEnumerable<FecharRotasView> GetFechaRotas(string Ano)
        {
            var SQL = new StringBuilder();
            var cn = new DapperConnection<FecharRotasView>();


            var DataInicioTotal = Ano + "-01-01";
            var DataFimTotal = int.Parse(Ano) + 1 + "-01-01";

            SQL.AppendLine("SELECT IFNULL(RT.id,0) as IdRota,IFNULL(RT.referencia,'') as RefRota,IFNULL(RT.descricao,'') as NomeRota");

            //loop para cada mês do ano
            for (int i = 1; i <= 12; i++)
            {
                var DataInicio = Ano + "-" + i.ToString("d2") + "-01";
                string DataFim = "";
                if (i == 12)
                {
                    DataFim = int.Parse(Ano) + 1 + "-" + "01-01";

                }
                else
                {
                    DataFim = Ano + "-" + (i + 1).ToString("d2") + "-01";
                }


                
                SQL.AppendLine(",FORMAT(SUM(IF(SUBSTRING(contaspagar.dataVencimento,1,10) >=" + "'" + DataInicio + "'" + " AND SUBSTRING(contaspagar.dataVencimento,1,10) < " + "'" + DataFim + "'" + ",(contaspagar.ValorParcela),0)),2,'de_DE') as ValorTotalM" + i);
                
            }

            SQL.AppendLine(" from contaspagar");
            SQL.AppendLine("  INNER JOIN colaboradores C on C.id = contaspagar.IdFornecedor ");
            SQL.AppendLine("  LEFT JOIN rotavisitas RT on RT.id = C.idrota");
            SQL.AppendLine(" WHERE contaspagar.Idempresa = " + Business.VestilloSession.EmpresaLogada.Id.ToString());
            SQL.AppendLine(" AND contaspagar.Ativo = 1 AND contaspagar.Notinha = 1 ");

            SQL.AppendLine(" GROUP BY RT.id ");
            return cn.ExecuteStringSqlToList(new FecharRotasView(), SQL.ToString());
        }

        public void ApagarNotinhaFaccao(int IdTitulo)
        {
            string SQL2 = String.Empty;

            SQL2 = "DELETE FROM contaspagarfaccao where contaspagarfaccao.IdContasPagar = " + IdTitulo;

            _cn.ExecuteNonQuery(SQL2);
        }


        public void AjustatNotinhasFaccaoPorRota(int IdRota, int IdAtividade,int Quantidade,decimal NovoValor, DateTime VencimentoInicial, DateTime VencimentoFinal)
        {
            var cnCol = new DapperConnection<Colaborador>();
            string SQLCol = String.Empty;
            string idFaccoes = String.Empty;
            string SQLNotas = String.Empty;
            List<int> IdsContasPagar = new List<int>();



            SQLCol = " SELECT Id FROM colaboradores where colaboradores.idrota = " + IdRota + " AND colaboradores.faccao = 1 ";
            var Dados = cnCol.ExecuteStringSqlToList(new Colaborador(), SQLCol);

            foreach (var itemFaccoes in Dados)
            {
                SQLNotas = " SELECT contaspagarfaccao.Id,contaspagarfaccao.IdContasPagar, contaspagarfaccao.IdAtividade,contaspagarfaccao.Quantidade,contaspagarfaccao.Preco,contaspagarfaccao.Total " +
                           " FROM contaspagar " +
                           " INNER JOIN contaspagarfaccao ON contaspagarfaccao.IdContasPagar = contaspagar.id " + 
                           " WHERE contaspagar.IdFornecedor = " + itemFaccoes.Id + " AND DATE(contaspagar.dataVencimento) BETWEEN '" + VencimentoInicial.ToString("yyyy-MM-dd") + "'" +
                  " AND '" + VencimentoFinal.ToString("yyyy-MM-dd") + "'" +
                  " AND contaspagarfaccao.IdAtividade = " + IdAtividade ;
                var cnNotas = new DapperConnection<ContasPagarFaccaoView>();
                var DadosNotas = cnNotas.ExecuteStringSqlToList(new ContasPagarFaccaoView(), SQLNotas);


                if(DadosNotas != null && DadosNotas.Count() > 0)
                {
                    string SQLNotasSum = String.Empty;

                    SQLNotasSum = " SELECT contaspagarfaccao.Id,contaspagarfaccao.IdContasPagar, contaspagarfaccao.IdAtividade,contaspagarfaccao.Quantidade,contaspagarfaccao.Preco,contaspagarfaccao.Total " +
                           " FROM contaspagar " +
                           " INNER JOIN contaspagarfaccao ON contaspagarfaccao.IdContasPagar = contaspagar.id " +
                           " WHERE contaspagar.IdFornecedor = " + itemFaccoes.Id + " AND DATE(contaspagar.dataVencimento) BETWEEN '" + VencimentoInicial.ToString("yyyy-MM-dd") + "'" +
                  " AND '" + VencimentoFinal.ToString("yyyy-MM-dd") + "'" +
                  " AND contaspagarfaccao.IdAtividade = " + IdAtividade + " HAVING SUM(contaspagarfaccao.Quantidade) >= " + Quantidade;

                    var cnNotasSum = new DapperConnection<ContasPagarFaccaoView>();
                    var DadosNotasSum = cnNotasSum.ExecuteStringSqlToList(new ContasPagarFaccaoView(), SQLNotasSum);
                    if (DadosNotasSum == null || DadosNotasSum.Count() <= 0)
                    {
                        continue;
                    }


                    foreach (var itemNotas in DadosNotas)
                    {
                        string SQLUpAtividades = String.Empty;
                        var cnUpNotas = new DapperConnection<ContasPagarFaccaoView>();


                        decimal precoMilheiro = 0;
                        decimal NovoTotal = 0;

                       var dadosAtv = new AtividadeFaccaoService().GetServiceFactory().GetById(IdAtividade);

                        if (dadosAtv.Milheiro)
                        {
                            precoMilheiro = NovoValor / 1000;
                            NovoTotal = precoMilheiro * itemNotas.Quantidade;
                        }
                        else
                        {
                            NovoTotal = NovoValor * itemNotas.Quantidade;
                        }


                        
                        SQLUpAtividades = "update contaspagarfaccao set contaspagarfaccao.Preco = " + NovoValor.ToString().Replace(",", ".") + ",contaspagarfaccao.Total = " + NovoTotal.ToString().Replace(",",".") + " WHERE contaspagarfaccao.Id = " + itemNotas.Id;
                        cnUpNotas.ExecuteNonQuery(SQLUpAtividades);

                        if(IdsContasPagar != null && IdsContasPagar.Count > 0)
                        {
                            if(!IdsContasPagar.Contains(itemNotas.IdContasPagar))
                            {
                                IdsContasPagar.Add(itemNotas.IdContasPagar);
                            }
                            
                        }
                        else
                        {
                            IdsContasPagar.Add(itemNotas.IdContasPagar);
                        }
                        
                    }
                }

            }


            foreach (var IdContasPagar in IdsContasPagar)
            {
                string SqlSumAtv = String.Empty;
                string SqlSumdesc = String.Empty;
                string SqlUpdateNt= String.Empty;
                decimal TotalAtv = 0;
                decimal TotalDescontos = 0;
                decimal TotalTitulo = 0;

                var cnTtAtv = new DapperConnection<ContasPagarFaccaoView>();
                var cnTDesc = new DapperConnection<ContasPagar>();

                SqlSumAtv = " select SUM(contaspagarfaccao.Total) as Total from contaspagarfaccao where contaspagarfaccao.IdContasPagar = " + IdContasPagar;
                var cpF = new ContasPagarFaccaoView();

                cnTtAtv.ExecuteToModel(ref cpF, SqlSumAtv);

                TotalAtv = cpF.Total;

                SqlSumdesc = " select contaspagar.Vale,contaspagar.TotalFator from contaspagar where contaspagar.Id = " + IdContasPagar;
                var cp = new ContasPagar();
                cnTDesc.ExecuteToModel(ref cp, SqlSumdesc);
                TotalDescontos = cp.Vale + cp.TotalFator;

                TotalTitulo = decimal.Round(TotalAtv - TotalDescontos,4);


                SqlUpdateNt = "update contaspagar set contaspagar.ValorParcela = " + TotalTitulo.ToString().Replace(",",".") + ",saldo =  " + TotalTitulo.ToString().Replace(",", ".") + " where contaspagar.id = " + IdContasPagar;
                cnTDesc.ExecuteNonQuery(SqlUpdateNt);

            }

        }
    }
}
