using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Core.Models;
using Vestillo.Core.Connection;
using System.Data;

namespace Vestillo.Core.Repositories
{
    public class ClienteRepository : GenericRepository<Cliente>
    {


        public ClienteDadosFinanceiroView GetDadosFinanceiro(int clienteId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT	");
            sql.AppendLine("	(IFNULL((SELECT SUM(NFE.totalnota) FROM NFE WHERE NFE.IdColaborador = C.Id), 0) +");
            sql.AppendLine("	 IFNULL((SELECT SUM(NFCE.total) FROM NFCE WHERE NFCE.IdCliente = C.Id), 0))	AS TotalCompra,");
            sql.AppendLine("	(SELECT MAX(NFE.totalnota) FROM NFE WHERE NFE.IdColaborador = C.Id) AS MaiorCompra,");
            sql.AppendLine("	(SELECT MAX(NFE.DataInclusao) FROM NFE WHERE NFE.IdColaborador = C.Id) AS UltimaCompra,");
            sql.AppendLine("	C.DataPrimeiraCompra,");
            sql.AppendLine("	(SELECT SUM(CR.Saldo) FROM contasReceber CR WHERE CR.IdCliente = C.Id AND CR.Ativo = 1 AND CR.Prefixo <> 'NEG' ) AS SaldoDevedor,");
            sql.AppendLine("	(SELECT SUM(CR.ValorPago) FROM contasReceber CR WHERE CR.IdCliente = C.Id) AS TotalPago");
            sql.AppendLine("FROM 	Colaboradores C");
            sql.AppendLine("WHERE   C.Id = " + clienteId.ToString());


            ClienteDadosFinanceiroView dadosFinanceiro =  VestilloConnection.ExecSQLToModel<ClienteDadosFinanceiroView>(sql.ToString());

            if (dadosFinanceiro != null)
            {
                sql = new StringBuilder();
                sql.AppendLine("SELECT	");
                sql.AppendLine("		TP.Descricao AS TipoDocumento,");
                sql.AppendLine("		SUM(CR.ValorPago) AS TotalPago,");
                sql.AppendLine("		SUM(CR.Saldo) AS TotalAberto");
                sql.AppendLine("FROM 	ContasReceber CR	");
                sql.AppendLine("	LEFT JOIN tipodocumentos TP ON TP.Id = CR.IdTipoDocumento	");
                sql.AppendLine("WHERE CR.Ativo = 1 AND CR.Prefixo <> 'NEG' AND	CR.IdCliente = " + clienteId.ToString());
                sql.AppendLine("GROUP BY TP.Descricao");
                sql.AppendLine(" UNION ALL	");
                sql.AppendLine("SELECT	");
                sql.AppendLine("		TP.Descricao AS TipoDocumento,");
                sql.AppendLine("		SUM(CR.ValorPago) AS TotalPago,");
                sql.AppendLine("		SUM(CR.Saldo) AS TotalAberto");
                sql.AppendLine("FROM 	ContasReceber CR	");
                sql.AppendLine("	LEFT JOIN tipodocumentos TP ON TP.Id = CR.IdTipoDocumento	");
                sql.AppendLine("WHERE CR.Ativo = 0 AND (CR.IdTipoDocumento = 10  OR CR.IdTipoDocumento = 11)AND CR.Prefixo <> 'NEG' AND	CR.IdCliente = " + clienteId.ToString());
                sql.AppendLine("GROUP BY TP.Descricao");


                dadosFinanceiro.PagamentosEmAberto = VestilloConnection.ExecSQLToList<ClienteDadosFinanceiroView.Titulos>(sql.ToString());

                

                sql = new StringBuilder();
                sql.AppendLine("SELECT	");
                sql.AppendLine("    R.Id  AS Id");
                sql.AppendLine("    ,R.NumTitulo  AS Titulo");
                sql.AppendLine("    ,R.Prefixo");
                sql.AppendLine("    ,R.Parcela");
                sql.AppendLine("    ,TP.descricao AS TipoDocumento");
                sql.AppendLine("    ,R.dataVencimento AS Vencimento");
                sql.AppendLine("    ,R.ValorPago");
                sql.AppendLine("    ,R.ValorParcela");
                sql.AppendLine("    ,R.DataEmissao AS Emissao");
                sql.AppendLine("    ,R.Saldo");
                sql.AppendLine("    ,IF(R.DataVencimento < now(),'ATRASADO','EM DIA') as Situacao");
                sql.AppendLine("FROM 	ContasReceber R	");
                sql.AppendLine("    LEFT JOIN TipoDocumentos TP ON TP.id = R.IdTipoDocumento");
                sql.AppendLine("WHERE R.Ativo = 1 AND R.Prefixo <> 'NEG' AND R.Saldo > 0 AND	R.IdCliente = " + clienteId.ToString());
                sql.AppendLine("ORDER BY R.dataVencimento DESC");

                dadosFinanceiro.ParcelasEmAberto = VestilloConnection.ExecSQLToList<ClienteDadosFinanceiroView.Parcelas>(sql.ToString());

                sql = new StringBuilder();
                sql.AppendLine("SELECT	");
                sql.AppendLine("    R.Id  AS Id");
                sql.AppendLine("    ,R.NumTitulo  AS Titulo");
                sql.AppendLine("    ,R.Prefixo");
                sql.AppendLine("    ,R.Parcela");
                sql.AppendLine("    ,TP.descricao AS TipoDocumento");
                sql.AppendLine("    ,R.dataVencimento AS Vencimento");
                sql.AppendLine("    ,R.ValorPago");
                sql.AppendLine("    ,R.ValorParcela");
                sql.AppendLine("    ,R.DataEmissao AS Emissao");
                sql.AppendLine("    ,R.Saldo");
                sql.AppendLine("	,R.DataPagamento AS Pagamento");
                sql.AppendLine("	,IF(DATEDIFF (R.DataPagamento,R.dataVencimento) < 0,0,DATEDIFF (R.DataPagamento,R.dataVencimento)) as DiasAtraso");
                sql.AppendLine("FROM 	ContasReceber R	");
                sql.AppendLine("    LEFT JOIN TipoDocumentos TP ON TP.id = R.IdTipoDocumento");
                sql.AppendLine("WHERE R.Saldo = 0 AND R.Prefixo <> 'NEG' AND R.ValorPago > 0 AND R.IdCliente = " + clienteId.ToString());
                sql.AppendLine("ORDER BY R.DataPagamento DESC");

                dadosFinanceiro.ParcelasPagas= VestilloConnection.ExecSQLToList<ClienteDadosFinanceiroView.Parcelas>(sql.ToString());

                sql = new StringBuilder();
                sql.AppendLine("SELECT	");
                sql.AppendLine("    Id, NumeroCheque, Valor, DataVencimento, Status");                
                sql.AppendLine("FROM 	cheques	");
                sql.AppendLine("WHERE ColaboradorId = " + clienteId.ToString());
                sql.AppendLine("ORDER BY DataVencimento");

                dadosFinanceiro.Cheques = VestilloConnection.ExecSQLToList<ClienteDadosFinanceiroView.ChequeCliente>(sql.ToString());
            }

            return dadosFinanceiro;
        }

        public IEnumerable<ClienteCarteiraView> ListDadosCarteiraClientes(int[] vendedores, int tipoConsulta)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("		C.Id");
            sql.AppendLine("        ,C.RazaoSocial");
            sql.AppendLine("        ,C.Bairro");  
            sql.AppendLine("        ,C.LimiteCompra");
            sql.AppendLine("        ,C.DataPrimeiraCompra");
            sql.AppendLine("        ,C.email");
            sql.AppendLine("        ,IF(C.TipoCliente = 0,'Cliente','Lead') as TipoCliente");
            sql.AppendLine("        ,V1.Nome AS Vendedor1");
            sql.AppendLine("        ,V2.Nome AS Vendedor2");
            sql.AppendLine("        ,C.DataCadastro");
            sql.AppendLine("        ,R.descricao AS Rota");
            sql.AppendLine("        ,E.Abreviatura AS UF");
            sql.AppendLine("        ,M.municipio AS Cidade");
            sql.AppendLine("        ,UC.DataInclusao AS UltimaCompra");
            sql.AppendLine("        ,UC.TotalNota AS ValorUltimaCompra");
            sql.AppendLine("        ,(SELECT SUM(Nfe.totalprodutos) - SUM(Nfe.ValDesconto) FROM Nfe WHERE Nfe.IdColaborador = C.Id AND Nfe.Tipo = 0 AND nfe.StatusNota <> 2 AND DATE_FORMAT(Nfe.DataInclusao, '%Y') = DATE_FORMAT(SYSDATE(), '%Y') -1) AS ValorAcumuladoAnoPassado");
            sql.AppendLine("        ,ROUND((SELECT SUM(nfeitens.quantidade) - SUM(nfeitens.Qtddevolvida) FROM Nfe INNER JOIN nfeitens ON nfeitens.IdNfe = Nfe.Id WHERE Nfe.IdColaborador = C.Id AND Nfe.Tipo = 0 AND nfe.StatusNota <> 2 AND DATE_FORMAT(Nfe.DataInclusao, '%Y') = DATE_FORMAT(SYSDATE(), '%Y') -1),0) AS AcumuladoPecasAnoPassado");

            sql.AppendLine("        ,(SELECT SUM(Nfe.totalprodutos) - SUM(Nfe.ValDesconto) FROM Nfe WHERE Nfe.IdColaborador = C.Id AND Nfe.Tipo = 0 AND nfe.StatusNota <> 2 AND DATE_FORMAT(Nfe.DataInclusao, '%Y') = DATE_FORMAT(SYSDATE(), '%Y')) AS ValorAcumuladoAno");
            sql.AppendLine("        ,ROUND((SELECT SUM(nfeitens.quantidade) - SUM(nfeitens.Qtddevolvida) FROM Nfe INNER JOIN nfeitens ON nfeitens.IdNfe = Nfe.Id WHERE Nfe.IdColaborador = C.Id AND Nfe.Tipo = 0 AND nfe.StatusNota <> 2 AND DATE_FORMAT(Nfe.DataInclusao, '%Y') = DATE_FORMAT(SYSDATE(), '%Y')),0) AS AcumuladoPecasAno");
            sql.AppendLine("        ,(SELECT COUNT(*) FROM Nfe WHERE Nfe.IdColaborador = C.Id AND Nfe.Tipo = 0 AND nfe.StatusNota <> 2) AS QtdFaturamento");
            sql.AppendLine("        ,(SELECT MAX(A.DataCriacao) FROM Atividades A WHERE A.ClienteId = C.Id) AS DataUltimaAtividadeInserida");
            sql.AppendLine("        ,(SELECT MAX(A.DataAtividade) FROM Atividades A WHERE A.ClienteId = C.Id) AS DataUltimoAgendamento");
            sql.AppendLine("FROM 	Colaboradores C");
            sql.AppendLine("	LEFT JOIN Colaboradores V1 ON V1.Id = C.IdVendedor");
            sql.AppendLine("    LEFT JOIN Colaboradores V2 ON V2.Id = C.IdVendedor2");
            sql.AppendLine("    LEFT JOIN rotavisitas	R  ON R.id = C.Idrota");
            sql.AppendLine("    LEFT JOIN estados		E  ON E.Id = C.IdEstado");
            sql.AppendLine("    LEFT JOIN municipiosibge M ON M.id = C.idmunicipio");
            sql.AppendLine("    LEFT JOIN NFe			UC ON UC.Id = (SELECT Id FROM Nfe WHERE Nfe.IdColaborador = C.Id ORDER BY Nfe.DataInclusao DESC LIMIT 1) ");
            sql.AppendLine("WHERE 	C.Cliente = 1");
            sql.AppendLine("        AND " + FiltroEmpresa("IdEmpresa", "C"));
            sql.AppendLine("        AND C.idvendedor IN (" + string.Join(", ", vendedores) + ")");

            switch (tipoConsulta)
            {
                case 1: //TotalCarteirasCliente
                    sql.AppendLine("        AND C.Ativo = 1");
                    break;

                case 2: //FaturadoUltimos12Meses
                    sql.AppendLine("		AND (SELECT COUNT(*) FROM NFe WHERE NFe.IdColaborador = C.id AND TIMESTAMPDIFF(MONTH, Nfe.DataInclusao, SYSDATE()) <= 12) > 0 ");
                    break;

                case 3: //FaturadoNoMes
                    sql.AppendLine("		AND (SELECT COUNT(*) FROM NFe WHERE NFe.IdColaborador = C.id AND DATE_FORMAT(Nfe.DataInclusao, '%m-%Y') = DATE_FORMAT(SYSDATE(), '%m-%Y')) > 0 ");
                    break;

                case 4: //Cliente
                    sql.AppendLine("		AND DATE_FORMAT(C.DataPrimeiraCompra, '%m-%Y') = DATE_FORMAT(SYSDATE(), '%m-%Y')");
                    break;
                case 5: //Cliente Pós-Venda
                    sql.AppendLine("		AND (SELECT COUNT(*) FROM atividades A WHERE A.ClienteId = C.Id AND A.TipoAtividadeId = 1 AND A.StatusAtividade = 1) > 0");
                    break;

                case 6: //Cliente Oportunidade
                    sql.AppendLine("		AND C.Ativo = 1 AND (SELECT COUNT(*) FROM atividades A WHERE A.ClienteId = C.Id AND A.TipoAtividadeId = 2 AND A.StatusAtividade = 1) > 0");
                    break;

                case 7: //Potencial
                    //sql.AppendLine("		AND (SELECT COUNT(*) FROM atividades A WHERE A.ClienteId = C.Id AND A.TipoAtividadeId = 3 AND A.StatusAtividade = 1) > 0");
                    sql.AppendLine("		AND C.Ativo = 1 AND C.LimiteCompra > 1000 AND (SELECT COUNT(*) FROM contasreceber CR WHERE CR.IdCliente = C.Id AND CR.saldo > 0) = 0 ");
                    break;

                case 8:
                    sql.AppendLine("	   AND C.TipoCliente <> 0 AND C.Ativo = 1");
                    break;

                case 9:
                    sql.AppendLine("	   AND C.TipoCliente = 1");
                    sql.AppendLine("       AND DATE_FORMAT(C.DataCadastro, '%m-%Y') = '" + DateTime.Now.ToString("MM-yyyy") + "'"); 
                    break;

                case 10:
                    sql.AppendLine("	   AND DATE_FORMAT(C.DataCadastro, '%m-%Y') = '" + DateTime.Now.ToString("MM-yyyy") + "'");
                    sql.AppendLine("       AND C.DataLead IS NOT NULL AND C.DataPrimeiraCompra IS NOT NULL"); 
                    break;

                default:
                    return new List<ClienteCarteiraView>();
            }

            sql.AppendLine("ORDER BY 2");

            return VestilloConnection.ExecSQLToList<ClienteCarteiraView>(sql.ToString());
        }

        public IEnumerable<AcaompanhamentoCatalogoClienteView> AcompanhamentoCatalogoCliente(int idClientes)
        {
            List<AcaompanhamentoCatalogoClienteView> DadosCatalogo = null;
            DadosCatalogo = new List<AcaompanhamentoCatalogoClienteView>();
            DadosCatalogo.Clear();

            int anoInicial = DateTime.Now.AddYears(-2).Year;
            int anoFinal = DateTime.Now.Year;
            string DiaMesInicial = "-01-01";
            string DiaMesFinal = "-12-31";

            string PeriodoInicial = anoInicial.ToString() + DiaMesInicial;
            string PeriodoFinal = anoFinal.ToString() + DiaMesFinal;

            int[] idCatalogos;
            List<int> retCatalogos = new List<int>();

            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" select colaboradores.id,IFNULL(catalogo.id,0) as IdCatalogo,IFNULL(catalogo.descricao,'SEM CATÁLOGO') as Catalogo,ROUND(SUM(nfeitens.quantidade) - SUM(nfeitens.Qtddevolvida),0) as  Quantidade,ROUND(SUM(nfeitens.DescontoRateado + nfeitens.DescontoBonificado + nfeitens.DescontoCofins + nfeitens.DescontoPis + nfeitens.DescontoSefaz),2) as Descontos,ROUND((NFE.frete + NFE.seguro + NFE.despesa),2) as Acrescimos,ROUND(IFNULL(IF(SUM((nfeitens.preco * (nfeitens.quantidade - nfeitens.Qtddevolvida)) - (nfeitens.DescontoRateado + nfeitens.DescontoBonificado + nfeitens.DescontoCofins + nfeitens.DescontoPis + nfeitens.DescontoSefaz ))<0,0.00,SUM((nfeitens.preco * (nfeitens.quantidade - nfeitens.Qtddevolvida)) - (nfeitens.DescontoRateado + nfeitens.DescontoBonificado + nfeitens.DescontoCofins + nfeitens.DescontoPis + nfeitens.DescontoSefaz ))),0),2) as TotalItens ");
            sql.AppendLine(" FROM nfeitens ");
            sql.AppendLine(" INNER JOIN produtos ON produtos.id = nfeitens.iditem ");
            sql.AppendLine(" LEFT JOIN catalogo  ON catalogo.id = produtos.IdCatalogo ");
            sql.AppendLine(" LEFT JOIN entrega ON entrega.id = produtos.IdEmtrega ");
            sql.AppendLine(" LEFT JOIN segmentos ON segmentos.id = produtos.Idsegmento ");
            sql.AppendLine(" INNER JOIN nfe on nfe.id = nfeitens.IdNfe ");
            sql.AppendLine(" INNER JOIN colaboradores ON colaboradores.id = NFE.IdColaborador ");
            sql.AppendLine(" LEFT JOIN municipiosibge on municipiosibge.id = colaboradores.idmunicipio ");
            sql.AppendLine(" LEFT JOIN rotavisitas on rotavisitas.id = colaboradores.idrota ");
            sql.AppendLine(" LEFT JOIN colaboradores ven ON ven.id = nfe.idvendedor ");
            sql.AppendLine(" LEFT JOIN colaboradores ven2 ON ven2.id = nfe.IdVendedor2 ");
            sql.AppendLine(" WHERE colaboradores.id = " + idClientes + " AND nfe.datainclusao BETWEEN " + "'" + PeriodoInicial + "'" + " AND " + "'" + PeriodoFinal + "'");
            sql.AppendLine(" GROUP BY colaboradores.id, catalogo.id ");
            sql.AppendLine(" ORDER BY  colaboradores.nome, nfe.datainclusao, catalogo.descricao, entrega.descricao, segmentos.descricao ");


            var dados  =  VestilloConnection.ExecSQLToList<AcaompanhamentoCatalogoClienteView>(sql.ToString());

            AcaompanhamentoCatalogoClienteView itemDaLista = new AcaompanhamentoCatalogoClienteView();
            foreach (var item in dados)
            {
                var SQLPedidoLiberado = String.Empty;
                itemDaLista = new AcaompanhamentoCatalogoClienteView();
                itemDaLista.Catalogo = item.Catalogo;
                itemDaLista.Quantidade = item.Quantidade;
                itemDaLista.Descontos = item.Descontos;
                itemDaLista.Acrescimos = item.Acrescimos;
                itemDaLista.TotalItens = item.TotalItens;
                if (item.IdCatalogo > 0)
                {
                    SQLPedidoLiberado = " SELECT ROUND((SUM(itensliberacaopedidovenda.Qtd) - SUM(itensliberacaopedidovenda.QtdFaturada)),0) as Atender from pedidovenda " +
                                  " INNER JOIN itenspedidovenda ON itenspedidovenda.PedidoVendaId = pedidovenda.Id " +
                                  " INNER JOIN produtos ON produtos.id = itenspedidovenda.ProdutoId " +
                                  " INNER JOIN catalogo ON catalogo.id = produtos.IdCatalogo " +
                                  " INNER JOIN itensliberacaopedidovenda ON itensliberacaopedidovenda.ItemPedidoVendaID = itenspedidovenda.Id " +
                                  " Where (pedidovenda.Status = 2 OR pedidovenda.Status = 6 OR pedidovenda.Status = 7 OR pedidovenda.Status = 8) AND produtos.IdCatalogo =  " + item.IdCatalogo +
                                  " AND pedidovenda.ClienteId = " + idClientes + 
                                  " GROUP BY produtos.IdCatalogo ";
                    DataTable dt = VestilloConnection.ExecToDataTable(SQLPedidoLiberado.ToString());

                    if(dt.Rows.Count > 0)
                    {
                        itemDaLista.PedidoLiberado = decimal.Parse("0" + dt.Rows[0][0].ToString());
                    }
                    else
                    {
                        itemDaLista.PedidoLiberado = 0;
                    }
                    
                }
                else
                {
                    itemDaLista.PedidoLiberado = 0;
                }

                retCatalogos.Add(item.IdCatalogo);



                DadosCatalogo.Add(itemDaLista);
            }

            idCatalogos = retCatalogos.ToArray();

           if(dados != null && dados.Count() > 0 )
           {
                var SqlCatalogos = "SELECT * from catalogo WHERE catalogo.id not IN(" + string.Join(", ", idCatalogos) + ")";
                DataTable dtCatalogo = VestilloConnection.ExecToDataTable(SqlCatalogos.ToString());
                for (int i = 0; i < dtCatalogo.Rows.Count; i++)
                {
                    int IdCatalogo = Convert.ToInt32(dtCatalogo.Rows[i]["Id"]);
                    var SQLPedidoLiberadoCatNovo = " SELECT ROUND((SUM(itensliberacaopedidovenda.Qtd) - SUM(itensliberacaopedidovenda.QtdFaturada)),0) as Atender from pedidovenda " +
                         " INNER JOIN itenspedidovenda ON itenspedidovenda.PedidoVendaId = pedidovenda.Id " +
                         " INNER JOIN produtos ON produtos.id = itenspedidovenda.ProdutoId " +
                         " INNER JOIN catalogo ON catalogo.id = produtos.IdCatalogo " +
                         " INNER JOIN itensliberacaopedidovenda ON itensliberacaopedidovenda.ItemPedidoVendaID = itenspedidovenda.Id " +
                         " Where (pedidovenda.Status = 2 OR pedidovenda.Status = 6 OR pedidovenda.Status = 7 OR pedidovenda.Status = 8) AND produtos.IdCatalogo =  " + IdCatalogo +
                         " AND pedidovenda.ClienteId = " + idClientes +
                         " GROUP BY produtos.IdCatalogo ";
                    DataTable dt = VestilloConnection.ExecToDataTable(SQLPedidoLiberadoCatNovo.ToString());

                    if (dt.Rows.Count > 0)
                    {
                        string NomeCatalogo = dtCatalogo.Rows[i]["descricao"].ToString();

                        itemDaLista = new AcaompanhamentoCatalogoClienteView();
                        itemDaLista.Catalogo = NomeCatalogo;
                        itemDaLista.Quantidade = 0;
                        itemDaLista.Descontos = 0;
                        itemDaLista.Acrescimos = 0;
                        itemDaLista.TotalItens = 0;
                        itemDaLista.PedidoLiberado = decimal.Parse("0" + dt.Rows[0][0].ToString());
                        DadosCatalogo.Add(itemDaLista);
                    }

                }
            }
           

            



            return DadosCatalogo;
        }

        
    }
}
