using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Connection;
using System.Data;
using Vestillo.Business.Controllers;
using Vestillo.Business.Service;

namespace Vestillo.Business.Repositories
{
    public class ItemOrdemProducaoRepository : GenericRepository<ItemOrdemProducao>
    {
        public ItemOrdemProducaoRepository()
            : base(new DapperConnection<ItemOrdemProducao>())
        {
        }

        public IEnumerable<ItemOrdemProducaoView> GetByPedido(int ordemId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT I.*,");
            SQL.AppendLine("P.Referencia AS ProdutoReferencia,");
            SQL.AppendLine("P.Descricao AS ProdutoDescricao,");
            SQL.AppendLine("P.QtdPacote AS QtdPacote,");
            SQL.AppendLine("T.Abreviatura AS TamanhoDescricao,");
            SQL.AppendLine("C.Abreviatura AS CorDescricao, ");
            SQL.AppendLine("PV.Referencia AS PedidoReferencia");
            SQL.AppendLine("FROM 	itensordemproducao I");
            SQL.AppendLine("INNER JOIN produtos P ON P.Id = I.ProdutoId	");
            SQL.AppendLine("LEFT JOIN tamanhos T ON T.Id = I.TamanhoId ");
            SQL.AppendLine("LEFT JOIN cores C ON C.Id = I.CorId ");
            SQL.AppendLine("LEFT JOIN pedidovenda PV ON PV.Id = I.PedidoVendaId ");
            SQL.AppendLine("WHERE	I.OrdemProducaoId = ");
            SQL.Append(ordemId);

            var cn = new DapperConnection<ItemOrdemProducaoView>();
            return cn.ExecuteStringSqlToList(new ItemOrdemProducaoView(), SQL.ToString());
        }

        public IEnumerable<ItemOrdemProducaoView> GetByPedidoVenda(int pedidoId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT I.*,");
            SQL.AppendLine("P.Referencia AS ProdutoReferencia,");
            SQL.AppendLine("P.Descricao AS ProdutoDescricao,");
            SQL.AppendLine("P.QtdPacote AS QtdPacote,");
            SQL.AppendLine("T.Abreviatura AS TamanhoDescricao,");
            SQL.AppendLine("C.Abreviatura AS CorDescricao, ");
            SQL.AppendLine("PV.Referencia AS PedidoReferencia");
            SQL.AppendLine("FROM 	itensordemproducao I");
            SQL.AppendLine("INNER JOIN produtos P ON P.Id = I.ProdutoId	");
            SQL.AppendLine("LEFT JOIN tamanhos T ON T.Id = I.TamanhoId ");
            SQL.AppendLine("LEFT JOIN cores C ON C.Id = I.CorId ");
            SQL.AppendLine("LEFT JOIN pedidovenda PV ON PV.Id = I.PedidoVendaId ");
            SQL.AppendLine("WHERE	I.PedidoVendaId = ");
            SQL.Append(pedidoId);

            var cn = new DapperConnection<ItemOrdemProducaoView>();
            return cn.ExecuteStringSqlToList(new ItemOrdemProducaoView(), SQL.ToString());
        }

        public IEnumerable<ItemOrdemProducaoView> GetByOrdem(int ordemId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT I.*,");
            SQL.AppendLine("P.Referencia AS ProdutoReferencia,");
            SQL.AppendLine("P.Descricao AS ProdutoDescricao,");
            SQL.AppendLine("T.Abreviatura AS TamanhoDescricao,");
            SQL.AppendLine("C.Abreviatura AS CorDescricao, ");
            SQL.AppendLine("PV.Referencia AS PedidoReferencia,");
            SQL.AppendLine(" OP.Referencia as OrdemProducaoReferencia, ");
            SQL.AppendLine(" OP.AlmoxarifadoId as AlmoxarifadoId ");
            SQL.AppendLine("FROM 	itensordemproducao I");
            SQL.AppendLine("INNER JOIN produtos P ON P.Id = I.ProdutoId	");
            SQL.AppendLine("LEFT JOIN tamanhos T ON T.Id = I.TamanhoId ");
            SQL.AppendLine("LEFT JOIN cores C ON C.Id = I.CorId ");
            SQL.AppendLine("LEFT JOIN pedidovenda PV ON PV.Id = I.PedidoVendaId ");
            SQL.AppendLine("INNER JOIN ordemproducao OP ON OP.Id = I.OrdemProducaoId ");
            SQL.AppendLine("WHERE	I.OrdemProducaoId = ");
            SQL.Append(ordemId);

            var cn = new DapperConnection<ItemOrdemProducaoView>();
            return cn.ExecuteStringSqlToList(new ItemOrdemProducaoView(), SQL.ToString());
        }

        public IEnumerable<ItemOrdemProducaoView> GetByOrdemBalanco(int ordemId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT I.*,");
            SQL.AppendLine("SUM(i.Quantidade) AS Quantidade,");
            SQL.AppendLine("P.Referencia AS ProdutoReferencia,");
            SQL.AppendLine("P.Descricao AS ProdutoDescricao,");
            SQL.AppendLine("T.Abreviatura AS TamanhoDescricao,");
            SQL.AppendLine("C.Abreviatura AS CorDescricao, ");
            SQL.AppendLine("PV.Referencia AS PedidoReferencia,");
            SQL.AppendLine(" OP.Referencia as OrdemProducaoReferencia, ");
            SQL.AppendLine(" OP.AlmoxarifadoId as AlmoxarifadoId ");
            SQL.AppendLine("FROM 	itensordemproducao I");
            SQL.AppendLine("INNER JOIN produtos P ON P.Id = I.ProdutoId	");
            SQL.AppendLine("LEFT JOIN tamanhos T ON T.Id = I.TamanhoId ");
            SQL.AppendLine("LEFT JOIN cores C ON C.Id = I.CorId ");
            SQL.AppendLine("LEFT JOIN pedidovenda PV ON PV.Id = I.PedidoVendaId ");
            SQL.AppendLine("INNER JOIN ordemproducao OP ON OP.Id = I.OrdemProducaoId ");
            SQL.AppendLine("WHERE	I.OrdemProducaoId = ");
            SQL.Append(ordemId);
            SQL.Append(" GROUP BY I.ProdutoId,I.Status");

            var cn = new DapperConnection<ItemOrdemProducaoView>();
            return cn.ExecuteStringSqlToList(new ItemOrdemProducaoView(), SQL.ToString());
        }

        public IEnumerable<ItemOrdemProducaoView> GetByProduto(int produtoId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT I.id, I.OrdemProducaoId, I.ProdutoId, I.CorId, I.TamanhoId, (SUM(I.Quantidade) - SUM(I.QuantidadeAtendida)) AS Quantidade, OP.DataEntrada,");
            SQL.AppendLine("P.Referencia AS ProdutoReferencia,");
            SQL.AppendLine("P.Descricao AS ProdutoDescricao,");
            SQL.AppendLine("T.Descricao AS TamanhoDescricao,");
            SQL.AppendLine("C.Descricao AS CorDescricao,");
            SQL.AppendLine("OP.DataEntrada AS DataEntrada,");
            SQL.AppendLine("OP.Referencia AS OrdemProducaoReferencia");
            SQL.AppendLine("FROM 	itensordemproducao I");
            SQL.AppendLine("INNER JOIN produtos P ON P.Id = I.ProdutoId	");
            SQL.AppendLine("INNER JOIN ordemproducao OP ON OP.Id = I.OrdemProducaoId ");
            SQL.AppendLine("LEFT JOIN tamanhos T ON T.Id = I.TamanhoId ");
            SQL.AppendLine("LEFT JOIN cores C ON C.Id = I.CorId ");
            SQL.AppendLine("WHERE	I.ProdutoId = ");
            SQL.Append(produtoId);
            SQL.AppendLine(" AND " + FiltroEmpresa("EmpresaId", "OP"));
            SQL.AppendLine(" AND	(I.Status = 1 OR I.Status = 2) AND OP.Status <> 6 ");
            SQL.AppendLine("GROUP BY I.OrdemProducaoId, I.Id, I.ProdutoId, I.TamanhoId, I.CorId");

            var cn = new DapperConnection<ItemOrdemProducaoView>();
            return cn.ExecuteStringSqlToList(new ItemOrdemProducaoView(), SQL.ToString());
        }

        public IEnumerable<ItemOrdemProducaoView> GetByProduto(int produtoId, int corId, int tamanhoId, int almoxarifadoId, bool FiltraStatus = false)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT I.id, I.OrdemProducaoId, I.ProdutoId, I.CorId, I.TamanhoId, (SUM(I.Quantidade) - SUM(I.QuantidadeProduzida)) AS Quantidade, OP.DataEntrada,");
            SQL.AppendLine("I.PedidoVendaId, P.Referencia AS ProdutoReferencia,");
            SQL.AppendLine("P.Descricao AS ProdutoDescricao,");
            SQL.AppendLine("T.Descricao AS TamanhoDescricao,");
            SQL.AppendLine("C.Descricao AS CorDescricao,");
            SQL.AppendLine("OP.DataEntrada AS DataEntrada,");
            SQL.AppendLine("OP.Referencia AS OrdemProducaoReferencia");
            SQL.AppendLine("FROM 	itensordemproducao I");
            SQL.AppendLine("INNER JOIN produtos P ON P.Id = I.ProdutoId	");
            SQL.AppendLine("INNER JOIN ordemproducao OP ON OP.Id = I.OrdemProducaoId ");
            SQL.AppendLine("LEFT JOIN tamanhos T ON T.Id = I.TamanhoId ");
            SQL.AppendLine("LEFT JOIN cores C ON C.Id = I.CorId ");
            SQL.AppendLine("WHERE	I.ProdutoId = ");
            SQL.Append(produtoId);
            SQL.AppendLine(" AND	OP.AlmoxarifadoId = " + almoxarifadoId);

            if(corId != 0)
                SQL.AppendLine(" AND	I.CorId = " + corId);
            if (tamanhoId != 0)
                SQL.AppendLine(" AND	I.TamanhoId = " + tamanhoId);

            if (FiltraStatus)
                SQL.AppendLine(" AND	OP.status <> 6 ");

            SQL.AppendLine("GROUP BY I.OrdemProducaoId, I.Id, I.ProdutoId, I.TamanhoId, I.CorId, OP.AlmoxarifadoId");

            var cn = new DapperConnection<ItemOrdemProducaoView>();
            return cn.ExecuteStringSqlToList(new ItemOrdemProducaoView(), SQL.ToString());
        }

        public IEnumerable<OrdemProducaoStatusRel> GetByFiltro(FiltroOrdemProducao filtro)
        {
            var cn = new DapperConnection<OrdemProducaoStatusRel>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT I.OrdemProducaoId, I.ProdutoId, I.CorId, I.TamanhoId, (I.Quantidade) as Quantidade,");
            SQL.AppendLine("(I.QuantidadeAtendida) as  QuantidadeAtendida, (I.QuantidadeProduzida) as QuantidadeProduzida,");
            SQL.AppendLine("OP.Referencia AS OrdemProducaoReferencia,");
            SQL.AppendLine("OP.DataEmissao AS Emissao,");
            SQL.AppendLine("OP.Liberacao AS Liberacao,");
            SQL.AppendLine("GP.Data AS Pacote,");
            SQL.AppendLine("min(PA.DataEntrada) AS Producao,");
            SQL.AppendLine("max(PA.DataSaida) AS Estoque,");
            SQL.AppendLine("P.Referencia AS ProdutoReferencia,");
            SQL.AppendLine("P.Descricao AS ProdutoDescricao,");
            SQL.AppendLine("T.Abreviatura AS TamanhoDescricao,");
            SQL.AppendLine("C.Abreviatura AS CorDescricao, ");
            SQL.AppendLine("(IFNULL(I.Quantidade, 0) - IFNULL(IL.Quantidade, 0)) AS QtdNaoLiberada, ");
            SQL.AppendLine("(IFNULL(IL.Quantidade, 0) - SUM(IFNULL(PA.Quantidade, 0))) AS QtdLiberada,");
            SQL.AppendLine("((SUM(IFNULL(PA.Quantidade, 0)) - IFNULL(I.QuantidadeProduzida, 0)) -(SUM(IF(IFNULL(PA.DataEntrada, '1-1-0001')  > '1-1-0001', (IFNULL(PA.Quantidade, 0)), 0))- IFNULL(I.QuantidadeProduzida, 0))) AS QtdPacote,");
            SQL.AppendLine("(SUM(IF(IFNULL(PA.DataEntrada, '1-1-0001')  > '1-1-0001', (IFNULL(PA.Quantidade, 0)), 0))- IFNULL(I.QuantidadeProduzida, 0)) AS QtdProducao, ");

            if (VestilloSession.FinalizaPacoteFaccao)
                SQL.AppendLine("( SUM(IFNULL(PA.qtdproduzida, 0)) - SUM(IFNULL(PA.qtddefeito, 0)) ) AS QtdEstoque, ");
            else
                SQL.AppendLine("(IFNULL(I.QuantidadeProduzida, 0) - SUM(IFNULL(PA.qtddefeito, 0))) AS QtdEstoque, ");

            SQL.AppendLine("SUM(PA.qtddefeito) AS QtdDefeito ");
            SQL.AppendLine("FROM 	itensordemproducao I");
            SQL.AppendLine("INNER JOIN produtos P ON P.Id = I.ProdutoId	");
            SQL.AppendLine("INNER JOIN ordemproducao OP ON OP.Id = I.OrdemProducaoId	");
            SQL.AppendLine("LEFT JOIN itemliberacaoordemproducao IL ON I.Id = IL.ItemOrdemProducaoId	");
            SQL.AppendLine("LEFT JOIN pacotes PA ON I.Id = PA.ItemOrdemProducaoId	");
            SQL.AppendLine("LEFT JOIN grupopacote GP ON GP.Id = PA.GrupoPacoteId	");
            SQL.AppendLine("LEFT JOIN tamanhos T ON T.Id = I.TamanhoId ");
            SQL.AppendLine("LEFT JOIN cores C ON C.Id = I.CorId ");
            SQL.AppendLine("LEFT JOIN pedidovenda PV ON PV.Id = I.PedidoVendaId ");
            SQL.AppendLine("WHERE 1=1 ");

            if (filtro.OrdensProducao != null && filtro.OrdensProducao.Count() > 0)
                SQL.AppendLine(" AND I.OrdemProducaoId IN (" + string.Join(", ", filtro.OrdensProducao) + ")");

            if (filtro.Produtos != null && filtro.Produtos.Count() > 0)
                SQL.AppendLine("        AND I.ProdutoId IN (" + string.Join(", ", filtro.Produtos) + ")");

            if (filtro.Cores != null && filtro.Cores.Count() > 0)
                SQL.AppendLine("        AND I.CorId IN (" + string.Join(", ", filtro.Cores) + ")");

            if (filtro.Tamanhos != null && filtro.Tamanhos.Count() > 0)
                SQL.AppendLine("        AND I.TamanhoId IN (" + string.Join(", ", filtro.Tamanhos) + ")");

            if (filtro.Grupo != null && filtro.Grupo.Count() > 0)
                SQL.AppendLine("        AND P.IdGrupo IN (" + string.Join(", ", filtro.Grupo) + ")");

            if (filtro.Colecao != null && filtro.Colecao.Count() > 0)
                SQL.AppendLine("        AND P.IdColecao IN (" + string.Join(", ", filtro.Colecao) + ")");

            if (filtro.Catalogo != null && filtro.Catalogo.Count() > 0)
                SQL.AppendLine("        AND P.IdCatalogo IN (" + string.Join(", ", filtro.Catalogo) + ")");

            if (filtro.Segmento != null && filtro.Segmento.Count() > 0)
                SQL.AppendLine("        AND P.IdSegmento IN (" + string.Join(", ", filtro.Segmento) + ")");



            if (filtro.NaoLiberada || filtro.Liberada || filtro.Finalizada)
            {
                SQL.AppendLine(" AND (");

                if (filtro.NaoLiberada)
                    SQL.Append(" OP.Status IN (0,1)");

                if (filtro.Liberada && filtro.NaoLiberada)
                    SQL.Append(" OR OP.Status IN (2,3,4,5,8,9,10)");
                else if (filtro.Liberada)
                    SQL.Append("OP.Status IN (2,3,4,5,8,9,10)");

                if (filtro.Finalizada && (filtro.NaoLiberada || filtro.Liberada))
                    SQL.Append(" OR OP.Status = 6 ");
                else if (filtro.Finalizada)
                    SQL.Append(" OP.Status = 6 ");

                SQL.Append(")");
            }
            else
            {
                SQL.AppendLine(" AND OP.Status = -1");
            }

            SQL.AppendLine("GROUP BY I.OrdemProducaoId, I.Id, I.ProdutoId, I.CorId, I.TamanhoId");

            return cn.ExecuteStringSqlToList(new OrdemProducaoStatusRel(), SQL.ToString());
        }

        public IEnumerable<OrdemProducaoStatusRel> GetBySetorFiltro(FiltroOrdemProducao filtro)
        {
            var cn = new DapperConnection<OrdemProducaoStatusRel>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT *, OO.Data AS DataSetor, GO.SetorId AS Setor, SE.Abreviatura AS SetorAbreviatura, SE.Descricao AS SetorDescricao  FROM ");
            SQL.AppendLine("(SELECT I.OrdemProducaoId, I.ProdutoId, I.CorId, I.TamanhoId, (I.Quantidade) as Quantidade,");
            SQL.AppendLine("(I.QuantidadeAtendida) as  QuantidadeAtendida, (I.QuantidadeProduzida) as QuantidadeProduzida,");
            SQL.AppendLine("OP.Referencia AS OrdemProducaoReferencia,");
            SQL.AppendLine("OP.DataEmissao AS Emissao,");
            SQL.AppendLine("OP.Liberacao AS Liberacao,");
            SQL.AppendLine("GP.Data AS Pacote,");
            SQL.AppendLine("PA.DataEntrada AS Producao,");
            SQL.AppendLine("PA.DataSaida AS Estoque,");
            SQL.AppendLine("P.Referencia AS ProdutoReferencia,");
            SQL.AppendLine("P.Descricao AS ProdutoDescricao,");
            SQL.AppendLine("T.Abreviatura AS TamanhoDescricao,");
            SQL.AppendLine("C.Abreviatura AS CorDescricao,  PA.Id As PAID, PA.GrupoPacoteId as GrupoPacoteId,");
            //SQL.AppendLine("OO.Data AS DataSetor,");
            //SQL.AppendLine("GO.SetorId AS Setor, ");
            //SQL.AppendLine("SE.Abreviatura AS SetorAbreviatura, ");
            //SQL.AppendLine("SE.Descricao AS SetorDescricao, ");
            SQL.AppendLine("(IFNULL(I.QuantidadeProduzida, 0) - SUM(IFNULL(PA.qtddefeito, 0))) AS QtdEstoque, ");
            SQL.AppendLine("SUM(PA.qtddefeito) AS QtdDefeito ");
            SQL.AppendLine("FROM 	itensordemproducao I");
            SQL.AppendLine("INNER JOIN produtos P ON P.Id = I.ProdutoId	");
            SQL.AppendLine("INNER JOIN ordemproducao OP ON OP.Id = I.OrdemProducaoId	");
            SQL.AppendLine("LEFT JOIN itemliberacaoordemproducao IL ON I.Id = IL.ItemOrdemProducaoId	");
            SQL.AppendLine("LEFT JOIN pacotes PA ON I.Id = PA.ItemOrdemProducaoId	");
            SQL.AppendLine("LEFT JOIN grupopacote GP ON GP.Id = PA.GrupoPacoteId	");
            SQL.AppendLine("LEFT JOIN tamanhos T ON T.Id = I.TamanhoId ");
            SQL.AppendLine("LEFT JOIN cores C ON C.Id = I.CorId ");
            //SQL.AppendLine("LEFT JOIN grupooperacoes GO ON GO.GrupoPacoteId = PA.GrupoPacoteId	");
            //SQL.AppendLine("LEFT JOIN operacaooperadora OO ON  OO.Pacoteid = PA.Id AND GO.OperacaoPadraoId = OO.OperacaoId AND GO.Sequencia = OO.sequencia");
            //SQL.AppendLine("LEFT JOIN setores SE ON SE.Id = GO.SetorId ");
            SQL.AppendLine("WHERE 1=1 ");

            if (filtro.OrdensProducao != null && filtro.OrdensProducao.Count() > 0)
                SQL.AppendLine(" AND I.OrdemProducaoId IN (" + string.Join(", ", filtro.OrdensProducao) + ")");

            if (filtro.Produtos != null && filtro.Produtos.Count() > 0)
                SQL.AppendLine("        AND I.ProdutoId IN (" + string.Join(", ", filtro.Produtos) + ")");

            if (filtro.Cores != null && filtro.Cores.Count() > 0)
                SQL.AppendLine("        AND I.CorId IN (" + string.Join(", ", filtro.Cores) + ")");

            if (filtro.Tamanhos != null && filtro.Tamanhos.Count() > 0)
                SQL.AppendLine("        AND I.TamanhoId IN (" + string.Join(", ", filtro.Tamanhos) + ")");

            //if (filtro.Setor != null && filtro.Setor.Count() > 0)
            //    SQL.AppendLine("        AND GO.SetorId IN (" + string.Join(", ", filtro.Setor) + ")");

            if (filtro.NaoLiberada || filtro.Liberada || filtro.Finalizada)
            {
                SQL.AppendLine(" AND (");

                if (filtro.NaoLiberada)
                    SQL.Append(" OP.Status IN (0,1)");

                if (filtro.Liberada && filtro.NaoLiberada)
                    SQL.Append(" OR OP.Status IN (2,3,4,5,8,9,10)");
                else if (filtro.Liberada)
                    SQL.Append("OP.Status IN (2,3,4,5,8,9,10)");

                if (filtro.Finalizada && (filtro.NaoLiberada || filtro.Liberada))
                    SQL.Append(" OR OP.Status = 6 ");
                else if (filtro.Finalizada)
                    SQL.Append(" OP.Status = 6 ");

                SQL.Append(")");
            }
            else
            {
                SQL.AppendLine(" AND OP.Status = -1");
            }

            SQL.AppendLine("GROUP BY I.OrdemProducaoId, I.ProdutoId, I.CorId, I.TamanhoId) AS ordem");
            //switch (filtro.Agrupar)
            //{
            //    case "produto":
            //        SQL.AppendLine("GROUP BY I.OrdemProducaoId, GO.SetorId, I.ProdutoId");
            //        break;
            //    case "cor":
            //        SQL.AppendLine("GROUP BY I.OrdemProducaoId, GO.SetorId, I.ProdutoId, I.CorId");
            //        break;
            //    case "tamanho":
            //        SQL.AppendLine("GROUP BY I.OrdemProducaoId, GO.SetorId, I.ProdutoId, I.TamanhoId");
            //        break;
            //    default:
            //        SQL.AppendLine("GROUP BY I.OrdemProducaoId, GO.SetorId, I.ProdutoId, I.CorId, I.TamanhoId");
            //        break;
            //}
            
            SQL.AppendLine("LEFT JOIN grupooperacoes GO ON GO.GrupoPacoteId = Ordem.GrupoPacoteId ");
            SQL.AppendLine("LEFT JOIN operacaooperadora OO ON  OO.Pacoteid = Ordem.PAID AND GO.OperacaoPadraoId = OO.OperacaoId AND GO.Sequencia = OO.sequencia ");
            SQL.AppendLine("LEFT JOIN setores SE ON SE.Id = GO.SetorId ");
            SQL.AppendLine("WHERE 1=1 ");
            if (filtro.Setor != null && filtro.Setor.Count() > 0)
                SQL.AppendLine("        AND GO.SetorId IN (" + string.Join(", ", filtro.Setor) + ")");
	        SQL.AppendLine("GROUP BY Ordem.OrdemProducaoId, GO.SetorId, Ordem.ProdutoId, Ordem.CorId, Ordem.TamanhoId ");

            return cn.ExecuteStringSqlToList(new OrdemProducaoStatusRel(), SQL.ToString());
        }

        public IEnumerable<OrdemProducaoStatusRel> GetByFiltroOrdem(FiltroOrdemProducao filtro)
        {
            var cn = new DapperConnection<OrdemProducaoStatusRel>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT I.OrdemProducaoId, I.ProdutoId, I.CorId, I.TamanhoId, ");
            if (filtro.Relatorio == 0)
            {
                SQL.AppendLine(" SUM(I.Quantidade) as Quantidade, SUM(I.QuantidadeAtendida) as  QuantidadeAtendida, SUM(I.QuantidadeProduzida) as QuantidadeProduzida, SUM(I.QuantidadeAvaria) as QuantidadeAvaria,");
            }
            else
            {
                if(VestilloSession.FinalizaPacoteFaccao && !filtro.NotaEntradaFaccao)
                    SQL.AppendLine(" (I.Quantidade) as Quantidade, (I.QuantidadeAtendida) as  QuantidadeAtendida, SUM(pacotes.qtdproduzida) as QuantidadeProduzida, (I.QuantidadeAvaria) as QuantidadeAvaria,");
                else
                    SQL.AppendLine(" (I.Quantidade) as Quantidade, (I.QuantidadeAtendida) as  QuantidadeAtendida, (I.QuantidadeProduzida) as QuantidadeProduzida, (I.QuantidadeAvaria) as QuantidadeAvaria, ");
            }
            
            SQL.AppendLine("OP.Referencia AS OrdemProducaoReferencia,");
            SQL.AppendLine("OP.DataEmissao AS Emissao,");
            SQL.AppendLine("OP.Liberacao AS Liberacao,");
            SQL.AppendLine("OP.DataPrevisaoCorte AS Corte,");
            SQL.AppendLine("P.Referencia AS ProdutoReferencia,");
            SQL.AppendLine("P.Descricao AS ProdutoDescricao,");
            SQL.AppendLine("T.Abreviatura AS TamanhoDescricao,");
            SQL.AppendLine("C.Abreviatura AS CorDescricao, ");
            SQL.AppendLine("FT.TempoTotal AS Tempo ");
            if (filtro.Relatorio != 0 && !filtro.NotaEntradaFaccao)
            {
                SQL.AppendLine(",  SUM(pacotes.qtddefeito) as qtddefeito  ");
            }
            else if(filtro.Relatorio != 0)
            {
                SQL.AppendLine(",  (I.QuantidadeDefeito) as QtdDefeito  ");
            }
            SQL.AppendLine("FROM 	itensordemproducao I");
            SQL.AppendLine("INNER JOIN produtos P ON P.Id = I.ProdutoId	");
            SQL.AppendLine("INNER JOIN ordemproducao OP ON OP.Id = I.OrdemProducaoId	");
            SQL.AppendLine("INNER JOIN fichatecnica FT ON FT.ProdutoId = I.ProdutoId	");
            if (filtro.Relatorio != 0 && !filtro.NotaEntradaFaccao)
            {
                SQL.AppendLine("LEFT JOIN pacotes ON pacotes.itemordemproducaoid =  I.Id ");
            }
            SQL.AppendLine("LEFT JOIN tamanhos T ON T.Id = I.TamanhoId ");
            SQL.AppendLine("LEFT JOIN cores C ON C.Id = I.CorId ");
            SQL.AppendLine("WHERE ");

            if (filtro.OrdensProducao != null && filtro.OrdensProducao.Count() > 0)
            {
                SQL.AppendLine("I.OrdemProducaoId IN (" + string.Join(", ", filtro.OrdensProducao) + ")");
            }
            else
            {
                SQL.AppendLine("I.OrdemProducaoId <> 0 ");
            }


            if (filtro.Produtos != null && filtro.Produtos.Count() > 0)
                SQL.AppendLine("        AND I.ProdutoId IN (" + string.Join(", ", filtro.Produtos) + ")");

            if (filtro.Cores != null && filtro.Cores.Count() > 0)
                SQL.AppendLine("        AND I.CorId IN (" + string.Join(", ", filtro.Cores) + ")");

            if (filtro.Tamanhos != null && filtro.Tamanhos.Count() > 0)
                SQL.AppendLine("        AND I.TamanhoId IN (" + string.Join(", ", filtro.Tamanhos) + ")");

            if (filtro.Grupo != null && filtro.Grupo.Count() > 0)
                SQL.AppendLine("        AND P.IdGrupo IN (" + string.Join(", ", filtro.Grupo) + ")");

            if (filtro.Colecao != null && filtro.Colecao.Count() > 0)
                SQL.AppendLine("        AND P.IdColecao IN (" + string.Join(", ", filtro.Colecao) + ")");

            if (filtro.Catalogo != null && filtro.Catalogo.Count() > 0)
                SQL.AppendLine("        AND P.IdCatalogo IN (" + string.Join(", ", filtro.Catalogo) + ")");

            if (filtro.Segmento != null && filtro.Segmento.Count() > 0)
                SQL.AppendLine("        AND P.IdSegmento IN (" + string.Join(", ", filtro.Segmento) + ")");

            if (filtro.Almoxarifado != null && filtro.Almoxarifado.Count > 0)
                SQL.AppendLine("        AND OP.AlmoxarifadoId IN (" + string.Join(", ", filtro.Almoxarifado) + ")");

            if ((filtro.DoAno != "" && filtro.DoAno != "0000") || (filtro.AteAno != "" && filtro.AteAno != "9999"))
                SQL.AppendLine("        AND P.ano BETWEEN  '" + filtro.DoAno + "' AND '" + filtro.AteAno + "' ");

            if (filtro.DaEmissao != "" || filtro.AteEmissao != "")
                SQL.AppendLine("        AND date(op.DataEmissao) BETWEEN  '" + filtro.DaEmissao + "' AND '" + filtro.AteEmissao + "' ");

            switch (filtro.Exibir)
            {
                case 0:
                    break;
                case 1:
                    SQL.Append(" AND OP.Status IN (2,3,4,5,8,9,10)");
                    break;
                case 2:
                    SQL.Append(" AND OP.Status IN (0,1)");
                    break;
                case 3:
                    SQL.Append(" AND OP.Status = 6 ");
                    break;
            }

            if (filtro.Relatorio == 0)
            {
                SQL.AppendLine("GROUP BY I.ProdutoId, I.CorId, I.TamanhoId");
            }
            else
            {
                SQL.AppendLine("GROUP BY I.OrdemProducaoId, I.ProdutoId, I.CorId, I.TamanhoId");
            }


            return cn.ExecuteStringSqlToList(new OrdemProducaoStatusRel(), SQL.ToString());
        }

        public IEnumerable<OrdemProducaoMaterialView> GetByOrdensParaPedidoCompra(List<int> idsOdens)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT MaterialReferencia, MaterialDescricao, materiaPrimaId, SUM(quantidadenecessaria) as quantidadenecessaria, SUM(quantidadeempenhada) as quantidadeempenhada, SUM(quantidadebaixada) as quantidadebaixada, SUM(Lancamento) as Lancamento,");
            SQL.AppendLine("CorDescricao, TamanhoDescricao, CorId, TamanhoId, ArmazemDescricao, ArmazemId, UM FROM ( ");

            SQL.AppendLine("(SELECT PM.Referencia as MaterialReferencia, PM.Descricao as MaterialDescricao, FI.materiaPrimaId, ((FI.quantidade*IOP.Quantidade)) as quantidadenecessaria, 0 as quantidadeempenhada, 0 as quantidadebaixada, ((FI.quantidade*IOP.Quantidade)) as Lancamento,");
            SQL.AppendLine("C.Abreviatura as CorDescricao, T.Abreviatura as TamanhoDescricao, ");
            SQL.AppendLine("C.ID as CorId, T.Id as TamanhoId, OP.Referencia as OrdemProducaoReferencia, AF.abreviatura as ArmazemDescricao, AF.id as ArmazemId, UMD.abreviatura as UM");
            SQL.AppendLine("FROM 	produtos P");
            SQL.AppendLine("INNER JOIN itensordemproducao IOP ON IOP.ProdutoId = P.Id ");
            SQL.AppendLine("INNER JOIN fichatecnicadomaterial F ON F.ProdutoId = P.Id ");
            SQL.AppendLine("INNER JOIN fichatecnicadomaterialitem FI ON FI.fichatecnicaid = F.Id ");
            SQL.AppendLine("INNER JOIN fichatecnicadomaterialrelacao FR ON ");
            SQL.AppendLine("(FR.fichatecnicaid = F.Id AND FR.materiaPrimaId = FI.materiaPrimaId AND FR.cor_produto_id = IOP.CorId AND FR.tamanho_produto_id = IOP.TamanhoId AND FR.fichatecnicaitemId = FI.Id) ");
            SQL.AppendLine("INNER JOIN produtos PM ON FI.materiaprimaid = PM.Id ");
            SQL.AppendLine("INNER JOIN cores C ON C.id = FR.cor_materiaprima_id ");
            SQL.AppendLine("INNER JOIN tamanhos T ON T.id = FR.tamanho_materiaprima_id ");
            SQL.AppendLine("INNER JOIN ordemproducao OP ON OP.Id = IOP.OrdemProducaoId ");
            SQL.AppendLine("INNER JOIN almoxarifados AF ON AF.id = OP.AlmoxarifadoId ");
            SQL.AppendLine("INNER JOIN unidademedidas UMD ON UMD.id = PM.IdUniMedida ");
            SQL.AppendLine("WHERE IOP.OrdemProducaoId IN ");
            SQL.AppendLine(" (" + string.Join(", ", idsOdens) + ")");
            SQL.AppendLine("AND	IOP.Status = 0)");

            SQL.AppendLine("UNION ALL");

            SQL.AppendLine("(SELECT p.Referencia as MaterialReferencia, p.Descricao as MaterialDescricao, opm.materiaprimaid, (opm.quantidadenecessaria) as quantidadenecessaria,");
            SQL.AppendLine("sum(opm.quantidadeempenhada) as quantidadeempenhada, sum(opm.quantidadebaixada) as quantidadebaixada, sum(quantidadenecessaria - quantidadeempenhada - quantidadebaixada) as lancamento, ");       
            SQL.AppendLine("c.abreviatura as CorDescricao,");
            SQL.AppendLine("t.abreviatura as TamanhoDescricao, ");
            SQL.AppendLine(" opm.corid, opm.tamanhoid, "); // F.possuiquebra, FI.DestinoId AS Destino, FI.sequencia,
            SQL.AppendLine("o.Referencia as OrdemProducaoReferencia,");
            SQL.AppendLine("  a.abreviatura as ArmazemDescricao, opm.armazemid,");
            SQL.AppendLine("u.abreviatura as UM");
            SQL.AppendLine("FROM 	ordemproducaomateriais opm");
            SQL.AppendLine("INNER JOIN ordemproducao o ON o.Id = opm.OrdemProducaoId ");
            SQL.AppendLine("INNER JOIN itensordemproducao IOP ON IOP.Id = opm.ItemOrdemProducaoId ");
            SQL.AppendLine("INNER JOIN produtos p ON p.Id = opm.materiaprimaid ");
            SQL.AppendLine("INNER JOIN cores c ON c.Id = opm.corid ");
            SQL.AppendLine("INNER JOIN tamanhos t ON t.Id = opm.tamanhoid ");
            SQL.AppendLine("LEFT JOIN almoxarifados a ON a.Id = opm.armazemid ");
            SQL.AppendLine("LEFT JOIN unidademedidas u ON u.Id = p.IdUniMedida ");
            SQL.AppendLine("WHERE	opm.OrdemProducaoId IN ");
            SQL.AppendLine(" (" + string.Join(", ", idsOdens) + ")");
            SQL.AppendLine("AND	IOP.Status >= 1");
            SQL.AppendLine("GROUP BY opm.materiaPrimaId, opm.corId, opm.tamanhoId)) as om");
            SQL.AppendLine("GROUP BY materiaPrimaId, corId, tamanhoId");

            var cn = new DapperConnection<OrdemProducaoMaterialView>();
            return cn.ExecuteStringSqlToList(new OrdemProducaoMaterialView(), SQL.ToString());
        }

        public IEnumerable<OrdemCompra> GetByOrdensParaCompra(List<int> idsOdens, List<int> idsGrupos)
        {
            string Campo1 = String.Empty;
            string Campo2 = String.Empty;

            if (VestilloSession.UsaDescricaoAlternativa)
            {
                Campo1 = "PM.DescricaoAlternativa";
                Campo2 = "P.DescricaoAlternativa";
            }
            else
            {
                Campo1 = "PM.Descricao";
                Campo2 = "P.Descricao";
            }

            var SQL = new StringBuilder(); // COM ESTOQUE
            SQL.AppendLine("SELECT MaterialReferencia, MaterialDescricao, materiaPrimaId, SUM(IFNULL(quantidadenecessaria, 0)) as quantidadenecessaria,SUM(IFNULL(quantidadeempenhada, 0)) as quantidadeempenhada,SUM(IFNULL(quantidadebaixada, 0)) as quantidadebaixada,");
            SQL.AppendLine("CorDescricao, TamanhoDescricao, CorId, TamanhoId, ArmazemDescricao, ArmazemId, UM, MateriaPrimaOriginalId, CorOriginalId, TamanhoOriginalId, Grupo FROM ( ");

            SQL.AppendLine("(SELECT PM.Referencia as MaterialReferencia, " + Campo1 + " as MaterialDescricao , FI.materiaPrimaId, ((FI.quantidade*IOP.Quantidade)) as quantidadenecessaria, 0 as quantidadeempenhada, 0 as quantidadebaixada,");
            SQL.AppendLine("C.Abreviatura as CorDescricao, T.Abreviatura as TamanhoDescricao, ");
            SQL.AppendLine("C.ID as CorId, T.Id as TamanhoId, AF.abreviatura as ArmazemDescricao, AF.id as ArmazemId, UMD.abreviatura as UM, FI.materiaPrimaId as MateriaPrimaOriginalId, C.ID  as CorOriginalId, T.Id as TamanhoOriginalId");
            SQL.AppendLine(", gp.descricao as Grupo ");
            SQL.AppendLine("FROM 	produtos P");
            SQL.AppendLine("INNER JOIN itensordemproducao IOP ON IOP.ProdutoId = P.Id ");
            SQL.AppendLine("INNER JOIN fichatecnicadomaterial F ON F.ProdutoId = P.Id ");
            SQL.AppendLine("INNER JOIN fichatecnicadomaterialitem FI ON FI.fichatecnicaid = F.Id ");
            SQL.AppendLine("INNER JOIN fichatecnicadomaterialrelacao FR ON ");
            SQL.AppendLine("(FR.fichatecnicaid = F.Id AND FR.materiaPrimaId = FI.materiaPrimaId AND FR.cor_produto_id = IOP.CorId AND FR.tamanho_produto_id = IOP.TamanhoId AND FR.fichatecnicaitemId = FI.Id) ");
            SQL.AppendLine("INNER JOIN produtos PM ON FI.materiaprimaid = PM.Id ");
            SQL.AppendLine("INNER JOIN cores C ON C.id = FR.cor_materiaprima_id and c.id <> 999 ");
            SQL.AppendLine("INNER JOIN tamanhos T ON T.id = FR.tamanho_materiaprima_id and t.id <> 999 ");
            SQL.AppendLine("INNER JOIN ordemproducao OP ON OP.Id = IOP.OrdemProducaoId ");
            SQL.AppendLine("INNER JOIN almoxarifados AF ON AF.id = OP.AlmoxarifadoId ");
            SQL.AppendLine("INNER JOIN unidademedidas UMD ON UMD.id = PM.IdUniMedida ");
            SQL.AppendLine("LEFT JOIN grupoprodutos gp ON gp.id = pm.IdGrupo ");
            SQL.AppendLine("WHERE IOP.Status = 0  ");

            if (idsOdens != null && idsOdens.Count() > 0)
                SQL.AppendLine("        AND  IOP.OrdemProducaoId IN (" + string.Join(", ", idsOdens) + ")");

            if (idsGrupos != null && idsGrupos.Count() > 0)
                SQL.AppendLine("        AND  pm.IdGrupo IN (" + string.Join(", ", idsGrupos) + ")");

            SQL.AppendLine(" )");

            SQL.AppendLine("UNION ALL");

            SQL.AppendLine("(SELECT p.Referencia as MaterialReferencia, " + Campo2 + " as MaterialDescricao ,  opm.materiaPrimaId, (opm.quantidadenecessaria) as quantidadenecessaria,");
            SQL.AppendLine("(opm.quantidadeempenhada) as quantidadeempenhada, (opm.quantidadebaixada) as quantidadebaixada,");
            SQL.AppendLine("c.abreviatura as CorDescricao,");
            SQL.AppendLine("t.abreviatura as TamanhoDescricao,");
            SQL.AppendLine(" opm.corid, opm.tamanhoid, "); // F.possuiquebra, FI.DestinoId AS Destino, FI.sequencia,
            SQL.AppendLine("a.abreviatura as ArmazemDescricao, opm.armazemid,");
            SQL.AppendLine("u.abreviatura as UM, ");            
            SQL.AppendLine("opm.materiaPrimaoriginalId, opm.cororiginalid, opm.tamanhooriginalid");
            SQL.AppendLine(", g.descricao as Grupo ");
            SQL.AppendLine("FROM 	ordemproducaomateriais opm");
            SQL.AppendLine("INNER JOIN ordemproducao o ON o.Id = opm.OrdemProducaoId ");
            SQL.AppendLine("INNER JOIN itensordemproducao IOP ON IOP.Id = opm.ItemOrdemProducaoId ");
            SQL.AppendLine("INNER JOIN produtos p ON p.Id = opm.materiaprimaid ");
            SQL.AppendLine("INNER JOIN cores c ON c.Id = opm.corid and c.id <> 999 ");
            SQL.AppendLine("INNER JOIN tamanhos t ON t.Id = opm.tamanhoid and t.id <> 999 ");
            SQL.AppendLine("LEFT JOIN almoxarifados a ON a.Id = opm.armazemid ");
            SQL.AppendLine("LEFT JOIN unidademedidas u ON u.Id = p.IdUniMedida ");
            SQL.AppendLine("LEFT JOIN grupoprodutos g ON g.id = p.IdGrupo ");
            SQL.AppendLine("WHERE IOP.Status > 0 ");

            if (idsOdens != null && idsOdens.Count() > 0)
                SQL.AppendLine("        AND opm.OrdemProducaoId IN (" + string.Join(", ", idsOdens) + ")");

            if (idsGrupos != null && idsGrupos.Count() > 0)
                SQL.AppendLine("        AND p.IdGrupo IN (" + string.Join(", ", idsGrupos) + ")");

            SQL.AppendLine(" )) as om");

            SQL.AppendLine("GROUP BY materiaPrimaId, corId, tamanhoId");

            var cn = new DapperConnection<OrdemCompra>();
            return cn.ExecuteStringSqlToList(new OrdemCompra(), SQL.ToString());
        }

        public void LimparVinculoPedidoOrdem(int idItemPedido) //id da tabela itensliberacaopedidovenda
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("UPDATE itensordemproducao SET SeqLiberacaoPedido = 0, PedidoVendaId = 0 ");
            SQL.AppendLine("WHERE SeqLiberacaoPedido = ");
            SQL.Append(idItemPedido);

            _cn.ExecuteNonQuery(SQL.ToString());
        }

        //DAQUI PRA BAIXO Gestão de compras

        public IEnumerable<GestaoOrdemCompra> GetByGestaoOrdemCompra(List<int> idsOdens, List<int> idsMateriaPrima,DateTime DaInclusao, DateTime AteInclusao)
        {
            var Valor = "'" + DaInclusao.ToString("yyyy-MM-dd") + "' AND '" + AteInclusao.ToString("yyyy-MM-dd") + "'";

            var SQL = new StringBuilder(); 
            SQL.AppendLine("SELECT MaterialReferencia, MaterialDescricao, materiaPrimaId, SUM(IFNULL(quantidadenecessaria, 0)) as quantidadenecessaria,SUM(IFNULL(quantidadeempenhada, 0)) as quantidadeempenhada,SUM(IFNULL(quantidadebaixada, 0)) as quantidadebaixada,");
            SQL.AppendLine("CorDescricao, TamanhoDescricao, CorId, TamanhoId, ArmazemDescricao, ArmazemId, UM,SE, MateriaPrimaOriginalId, CorOriginalId, TamanhoOriginalId FROM ( ");

            SQL.AppendLine("(SELECT PM.Referencia as MaterialReferencia, PM.Descricao as MaterialDescricao, FI.materiaPrimaId, ((FI.quantidade*IOP.Quantidade)) as quantidadenecessaria, 0 as quantidadeempenhada, 0 as quantidadebaixada,");
            SQL.AppendLine("C.Abreviatura as CorDescricao, T.Abreviatura as TamanhoDescricao, ");
            SQL.AppendLine("C.ID as CorId, T.Id as TamanhoId, AF.abreviatura as ArmazemDescricao, AF.id as ArmazemId, UMD.abreviatura as UM,SEG.descricao as SE, FI.materiaPrimaId as MateriaPrimaOriginalId, C.ID  as CorOriginalId, T.Id as TamanhoOriginalId");
            SQL.AppendLine("FROM 	produtos P");
            SQL.AppendLine("INNER JOIN itensordemproducao IOP ON IOP.ProdutoId = P.Id ");
            SQL.AppendLine("INNER JOIN fichatecnicadomaterial F ON F.ProdutoId = P.Id ");
            SQL.AppendLine("INNER JOIN fichatecnicadomaterialitem FI ON FI.fichatecnicaid = F.Id ");
            SQL.AppendLine("INNER JOIN fichatecnicadomaterialrelacao FR ON ");
            SQL.AppendLine("(FR.fichatecnicaid = F.Id AND FR.materiaPrimaId = FI.materiaPrimaId AND FR.cor_produto_id = IOP.CorId AND FR.tamanho_produto_id = IOP.TamanhoId AND FR.fichatecnicaitemId = FI.Id) ");
            SQL.AppendLine("INNER JOIN produtos PM ON FI.materiaprimaid = PM.Id ");
            SQL.AppendLine("LEFT JOIN segmentos SEG on SEG.id = PM.Idsegmento");
            SQL.AppendLine("INNER JOIN cores C ON C.id = FR.cor_materiaprima_id ");
            SQL.AppendLine("INNER JOIN tamanhos T ON T.id = FR.tamanho_materiaprima_id ");
            SQL.AppendLine("INNER JOIN ordemproducao OP ON OP.Id = IOP.OrdemProducaoId ");
            SQL.AppendLine("INNER JOIN almoxarifados AF ON AF.id = OP.AlmoxarifadoId ");
            SQL.AppendLine("INNER JOIN unidademedidas UMD ON UMD.id = PM.IdUniMedida ");
            SQL.AppendLine("WHERE  OP.Status  <> 6 AND ");
            if (idsOdens.Count > 0)
            {
                SQL.AppendLine(" IOP.OrdemProducaoId IN ");
                SQL.AppendLine(" (" + string.Join(", ", idsOdens) + ") AND ");
            }

            if (idsMateriaPrima.Count > 0)
            {
                SQL.AppendLine(" FI.materiaPrimaId IN ");
                SQL.AppendLine(" (" + string.Join(", ", idsMateriaPrima) + ") AND ");
            }

            
            SQL.AppendLine("	IOP.Status = 0 AND  SUBSTRING(OP.DataEmissao, 1, 10) BETWEEN " + Valor + ")" );

            SQL.AppendLine("UNION ALL");

            SQL.AppendLine("(SELECT  p.Referencia as MaterialReferencia, p.Descricao as MaterialDescricao, opm.materiaPrimaId, ");
            //SQL.AppendLine(" IF(IF(opm.quantidadebaixada > 0, opm.quantidadenecessaria - opm.quantidadebaixada, opm.quantidadenecessaria - opm.quantidadeempenhada) < 0, 0, IF(opm.quantidadebaixada > 0, opm.quantidadenecessaria - opm.quantidadebaixada, opm.quantidadenecessaria - opm.quantidadeempenhada)) as quantidadenecessaria, ");
            SQL.AppendLine(" IF( (quantidadenecessaria - quantidadebaixada - quantidadeempenhada) < 0, 0, (quantidadenecessaria - quantidadebaixada - quantidadeempenhada) ) as quantidadenecessaria, ");
            SQL.AppendLine("(opm.quantidadeempenhada) as quantidadeempenhada, (opm.quantidadebaixada) as quantidadebaixada,");
            SQL.AppendLine("c.abreviatura as CorDescricao,");
            SQL.AppendLine("t.abreviatura as TamanhoDescricao,");
            SQL.AppendLine(" opm.corid, opm.tamanhoid, "); 
            SQL.AppendLine("a.abreviatura as ArmazemDescricao, opm.armazemid,");
            SQL.AppendLine("u.abreviatura as UM, S.descricao as SE,");
            SQL.AppendLine("opm.materiaPrimaoriginalId, opm.cororiginalid, opm.tamanhooriginalid");
            SQL.AppendLine("FROM 	ordemproducaomateriais opm");
            SQL.AppendLine("INNER JOIN ordemproducao o ON o.Id = opm.OrdemProducaoId ");
            SQL.AppendLine("INNER JOIN itensordemproducao IOP ON IOP.Id = opm.ItemOrdemProducaoId ");
            SQL.AppendLine("INNER JOIN produtos p ON p.Id = opm.materiaprimaid ");
            SQL.AppendLine("INNER JOIN cores c ON c.Id = opm.corid ");
            SQL.AppendLine("INNER JOIN tamanhos t ON t.Id = opm.tamanhoid ");
            SQL.AppendLine("LEFT JOIN segmentos S on S.id = p.Idsegmento");
            SQL.AppendLine("LEFT JOIN almoxarifados a ON a.Id = opm.armazemid ");
            SQL.AppendLine("INNER JOIN unidademedidas u ON u.Id = p.IdUniMedida ");
            SQL.AppendLine("WHERE  o.Status  <> 6 AND ");

            if (idsOdens.Count > 0)
            {
                SQL.AppendLine("	opm.OrdemProducaoId IN ");
                SQL.AppendLine(" (" + string.Join(", ", idsOdens) + ") AND ");

            }

            if (idsMateriaPrima.Count > 0)
            {
                SQL.AppendLine(" opm.materiaprimaid IN ");
                SQL.AppendLine(" (" + string.Join(", ", idsMateriaPrima) + ") AND ");
            }



           SQL.AppendLine("	IOP.Status > 0 AND  SUBSTRING(o.DataEmissao, 1, 10) BETWEEN " + Valor + ")) as om");

            SQL.AppendLine("GROUP BY materiaPrimaId, corId, tamanhoId");

            var cn = new DapperConnection<GestaoOrdemCompra>();
            return cn.ExecuteStringSqlToList(new GestaoOrdemCompra(), SQL.ToString());
        }

        public decimal GetByQuantidadeNecessariaTotal(int idMateriaPrima, int idCor, int idTamanho)
        {
            decimal Emproducao = 0;
            decimal Aberto = 0;
            decimal total = 0;
            string SQL = String.Empty;

            //" SELECT IF(IF(SUM(quantidadebaixada) > 0, SUM(quantidadenecessaria) - SUM(quantidadebaixada),SUM(quantidadenecessaria) - SUM(quantidadeempenhada)) <0,0,IF(SUM(quantidadebaixada) > 0, SUM(quantidadenecessaria) - SUM(quantidadebaixada),SUM(quantidadenecessaria) - SUM(quantidadeempenhada))) as quantidadenecessariaTotal "
            SQL = " SELECT IF( (SUM(quantidadenecessaria) - SUM(quantidadebaixada) - SUM(quantidadeempenhada)) < 0, 0, (SUM(quantidadenecessaria) - SUM(quantidadebaixada) - SUM(quantidadeempenhada)) ) as quantidadenecessariaTotal " +
                  " FROM ordemproducaomateriais " +
                  " INNER JOIN itensordemproducao IOP ON IOP.Id = ordemproducaomateriais.ItemOrdemProducaoId " +
                  " INNER JOIN  ordemproducao ON ordemproducao.id = ordemproducaomateriais.ordemproducaoid " +
                  " WHERE ordemproducao.status <> 6 AND IOP.Status > 0 AND ordemproducaomateriais.materiaprimaid = " + idMateriaPrima +
                  " AND ordemproducaomateriais.corid = " + idCor + " AND ordemproducaomateriais.tamanhoid = " + idTamanho;

            DataTable dt = _cn.ExecuteToDataTable(SQL.ToString());
            if(dt != null && dt.Rows.Count > 0)
            {
                Emproducao = decimal.Parse("0" + dt.Rows[0][0].ToString());
            }
            

            SQL = String.Empty;

            SQL = " SELECT SUM(((FI.quantidade*IOP.Quantidade))) as quantidadenecessariaTotal " +
                  " FROM produtos P " +
                  "   INNER JOIN itensordemproducao IOP ON IOP.ProdutoId = P.Id " +
                  "   INNER JOIN fichatecnicadomaterial F ON F.ProdutoId = P.Id " +
                  "   INNER JOIN fichatecnicadomaterialitem FI ON FI.fichatecnicaid = F.Id " +
                  "   INNER JOIN fichatecnicadomaterialrelacao FR ON " +
                  "   (FR.fichatecnicaid = F.Id AND FR.materiaPrimaId = FI.materiaPrimaId AND FR.cor_produto_id = IOP.CorId AND FR.tamanho_produto_id = IOP.TamanhoId AND FR.fichatecnicaitemId = FI.Id) " +
                  "   INNER JOIN produtos PM ON FI.materiaprimaid = PM.Id " +
                  "   LEFT JOIN segmentos SEG on SEG.id = PM.Idsegmento " +
                  "   INNER JOIN cores C ON C.id = FR.cor_materiaprima_id " +
                  "   INNER JOIN tamanhos T ON T.id = FR.tamanho_materiaprima_id " +
                  "   INNER JOIN ordemproducao OP ON OP.Id = IOP.OrdemProducaoId " +
                  "   INNER JOIN almoxarifados AF ON AF.id = OP.AlmoxarifadoId " +
                  "   INNER JOIN unidademedidas UMD ON UMD.id = PM.IdUniMedida " +
                  "   WHERE OP.status <> 6 AND " +

                  "  IOP.Status = 0 AND FR.materiaPrimaId = " + idMateriaPrima +
                  "  AND FR.cor_materiaprima_Id = " + idCor +
                  "  AND FR.tamanho_materiaprima_Id = "  + idTamanho +
                  "  GROUP BY FR.materiaPrimaId, FR.cor_materiaprima_Id, FR.tamanho_materiaprima_Id  ";

            DataTable dtAberto = _cn.ExecuteToDataTable(SQL.ToString());
            if(dtAberto != null && dtAberto.Rows.Count > 0)
            {
                Aberto = decimal.Parse("0" + dtAberto.Rows[0][0].ToString());
            }
            


            total = Emproducao + Aberto;
            return total;
        }

        public decimal GetByQuantidadeEmPEdidoPorOrdem(List<int> IdsOps,int IdMateriaPrima, int idCor, int idTamanho)
        {
            decimal Parcial = 0;            
            decimal total = 0;
            string SQL = String.Empty;
            List<int> IdsPedidos = new List<int>();

            SQL = "select ordemproducao.Referencia from ordemproducao where ordemproducao.id IN (" + string.Join(", ", IdsOps) + ")";
            DataTable OPS = _cn.ExecuteToDataTable(SQL.ToString());
            if (OPS != null && OPS.Rows.Count > 0)
            {
                SQL = String.Empty;

                for (int i = 0; i < OPS.Rows.Count; i++)
                {
                    SQL = " select SUM(IFNULL(itenspedidocompra.Qtd,0) - IFNULL(itenspedidocompra.QtdAtendida,0)) as EmPedido,IFNULL(pedidocompra.id ,0) as IdPedido from pedidocompra " +
                     " INNER JOIN itenspedidocompra ON itenspedidocompra.PedidoCompraId = pedidocompra.Id " +
                     " where LOCATE(" + "'" + OPS.Rows[i]["Referencia"].ToString() + "'" + ",OrdensReferencia)" +
                     " AND itenspedidocompra.ProdutoId = " + IdMateriaPrima +
                     " AND itenspedidocompra.CorId = " + idCor +
                     " AND itenspedidocompra.TamanhoId = " + idTamanho +
                     " AND itenspedidocompra.Qtd - itenspedidocompra.QtdAtendida > 0 AND  pedidocompra.Status <> 4 " +
                     " GROUP by IFNULL(pedidocompra.id ,0) ";

                     DataTable Pedido = _cn.ExecuteToDataTable(SQL.ToString());

                    for (int p = 0; p < Pedido.Rows.Count; p++)
                    {
                        if (int.Parse(Pedido.Rows[p]["IdPedido"].ToString()) == 0)
                        {
                            Parcial += decimal.Parse("0" + Pedido.Rows[p]["EmPedido"].ToString());
                        }
                        else
                        {
                            var ExistePedido = IdsPedidos.Where(x => x == int.Parse(Pedido.Rows[p]["IdPedido"].ToString())).ToList();
                            if (ExistePedido.Count == 0)
                            {
                                IdsPedidos.Add(int.Parse(Pedido.Rows[p]["IdPedido"].ToString()));
                                Parcial += decimal.Parse("0" + Pedido.Rows[p]["EmPedido"].ToString());
                            }

                        }

                    }

                }
            }

            total = Parcial;

            return total;
        }

        public IEnumerable<OrdemProducaoView> GetByOrdensDoEmpenhoTotal(int IdMmateriaprima, int Idcor, int Idtamanho)
        {
            
            var SQL = new StringBuilder();

            
            
            SQL.AppendLine("select ordemproducao.status,ordemproducao.Referencia,");
            SQL.AppendLine("ordemproducao.DataEmissao,SUM(ordemproducaomateriais.quantidadeempenhada) as quantidadeCuringa ");
            SQL.AppendLine(" from ordemproducaomateriais  "); // F.possuiquebra, FI.DestinoId AS Destino, FI.sequencia,
            SQL.AppendLine(" INNER JOIN ordemproducao ON ordemproducao.id = ordemproducaomateriais.ordemproducaoid ");
            SQL.AppendLine(" where ordemproducaomateriais.quantidadeempenhada > 0 ");
            //SQL.AppendLine(" AND ordemproducao.Status <> 6 ");
            SQL.AppendLine(" AND ordemproducaomateriais.materiaprimaid = " + IdMmateriaprima);
            SQL.AppendLine(" AND ordemproducaomateriais.corid = " + Idcor);
            SQL.AppendLine(" AND ordemproducaomateriais.tamanhoid = " + Idtamanho);
            SQL.AppendLine(" group by ordemproducao.id,ordemproducaomateriais.materiaprimaid,ordemproducaomateriais.corid,ordemproducaomateriais.tamanhoid  ");
            SQL.AppendLine(" order by ordemproducaomateriais.ordemproducaoid ");
            

            var cn = new DapperConnection<OrdemProducaoView>();
            return cn.ExecuteStringSqlToList(new OrdemProducaoView(), SQL.ToString());
        }

        public IEnumerable<OrdemProducaoView> GetByOrdensDoNecessarioTotal(int IdMmateriaprima, int Idcor, int Idtamanho)
        {

            List<OrdemProducaoView> MontaTotal = null;
            MontaTotal = new List<OrdemProducaoView>();
            MontaTotal.Clear();
            OrdemProducaoView it = new OrdemProducaoView();
            var SQL = new StringBuilder();


            SQL.AppendLine("SELECT ordemproducao.Status,ordemproducao.Referencia, ordemproducao.DataEmissao, ");
            //SQL.AppendLine(" IF(IF(SUM(ordemproducaomateriais.quantidadebaixada) > 0, SUM(ordemproducaomateriais.quantidadenecessaria) - SUM(ordemproducaomateriais.quantidadebaixada),SUM(ordemproducaomateriais.quantidadenecessaria) - SUM(ordemproducaomateriais.quantidadeempenhada)) <0,0,IF(SUM(ordemproducaomateriais.quantidadebaixada) > 0, SUM(ordemproducaomateriais.quantidadenecessaria) - SUM(ordemproducaomateriais.quantidadebaixada),SUM(ordemproducaomateriais.quantidadenecessaria) - SUM(ordemproducaomateriais.quantidadeempenhada))) as quantidadeCuringa ");// SUM(ordemproducaomateriais.quantidadenecessaria) as quantidadeCuringa ");
            SQL.AppendLine(" IF((SUM(ordemproducaomateriais.quantidadenecessaria) - SUM(ordemproducaomateriais.quantidadebaixada) - SUM(ordemproducaomateriais.quantidadeempenhada)) < 0,0,(SUM(ordemproducaomateriais.quantidadenecessaria) - SUM(ordemproducaomateriais.quantidadebaixada) - SUM(ordemproducaomateriais.quantidadeempenhada))) as quantidadeCuringa ");
            SQL.AppendLine("from ordemproducaomateriais  ");           
            SQL.AppendLine(" INNER JOIN itensordemproducao IOP ON IOP.Id = ordemproducaomateriais.ItemOrdemProducaoId  ");
            SQL.AppendLine(" INNER JOIN ordemproducao ON ordemproducao.id = IOP.ordemproducaoid ");
            SQL.AppendLine(" WHERE ordemproducao.Status <> 6 AND IOP.Status > 0 ");
            SQL.AppendLine(" AND ordemproducaomateriais.materiaprimaid =  " + IdMmateriaprima);
            SQL.AppendLine(" AND ordemproducaomateriais.corid = " + Idcor);
            SQL.AppendLine(" AND ordemproducaomateriais.tamanhoid = " + Idtamanho);
            SQL.AppendLine(" group by ordemproducao.id,ordemproducaomateriais.materiaprimaid,ordemproducaomateriais.corid,ordemproducaomateriais.tamanhoid ");
            SQL.AppendLine(" order by ordemproducao.id ");


            var cn = new DapperConnection<OrdemProducaoView>();

            var dadosProduzindo = cn.ExecuteStringSqlToList(new OrdemProducaoView(), SQL.ToString());

            foreach (var itemProduzindo in dadosProduzindo)
            {
                it = new OrdemProducaoView();
                it.Status = itemProduzindo.Status;
                it.Referencia = itemProduzindo.Referencia;
                it.DataEmissao = itemProduzindo.DataEmissao;
                it.quantidadeCuringa = itemProduzindo.quantidadeCuringa;
                MontaTotal.Add(it);
            }


            SQL = new StringBuilder();

            SQL.AppendLine(" SELECT OP.Status,OP.Referencia, OP.DataEmissao,SUM(((FI.quantidade*IOP.Quantidade))) as quantidadeCuringa ");
            SQL.AppendLine(" FROM produtos P ");
            SQL.AppendLine("  INNER JOIN itensordemproducao IOP ON IOP.ProdutoId = P.Id  "); // F.possuiquebra, FI.DestinoId AS Destino, FI.sequencia,
            SQL.AppendLine("  INNER JOIN fichatecnicadomaterial F ON F.ProdutoId = P.Id  ");
            SQL.AppendLine("  INNER JOIN fichatecnicadomaterialitem FI ON FI.fichatecnicaid = F.Id ");
            SQL.AppendLine("  INNER JOIN fichatecnicadomaterialrelacao FR ON    (FR.fichatecnicaid = F.Id AND FR.materiaPrimaId = FI.materiaPrimaId AND FR.cor_produto_id = IOP.CorId AND FR.tamanho_produto_id = IOP.TamanhoId AND FR.fichatecnicaitemId = FI.Id)  ");
            SQL.AppendLine(" INNER JOIN produtos PM ON FI.materiaprimaid = PM.Id");
            SQL.AppendLine(" INNER JOIN ordemproducao OP ON OP.Id = IOP.OrdemProducaoId");
            SQL.AppendLine(" WHERE  OP.status <> 6 AND  IOP.Status = 0 ");

            SQL.AppendLine(" AND FR.materiaPrimaId =  " + IdMmateriaprima);
            SQL.AppendLine(" AND FR.cor_materiaprima_Id = " + Idcor);
            SQL.AppendLine(" AND FR.tamanho_materiaprima_Id = " + Idtamanho);
            SQL.AppendLine(" GROUP BY OP.id,FR.materiaPrimaId, FR.cor_materiaprima_Id, FR.tamanho_materiaprima_Id  ");
            SQL.AppendLine(" ORDER by OP.id ");


            var cn2 = new DapperConnection<OrdemProducaoView>();

            var dadosAbertos = cn2.ExecuteStringSqlToList(new OrdemProducaoView(), SQL.ToString());

            foreach (var itemAberto in dadosAbertos)
            {
                var ExisteOp = MontaTotal.Where(x => x.Referencia == itemAberto.Referencia).ToList();
                if (ExisteOp != null && ExisteOp.Count > 0)
                {
                    ExisteOp[0].quantidadeCuringa += itemAberto.quantidadeCuringa;
                }
                else
                {
                    it = new OrdemProducaoView();
                    it.Status = itemAberto.Status;
                    it.Referencia = itemAberto.Referencia;
                    it.DataEmissao = itemAberto.DataEmissao;
                    it.quantidadeCuringa = itemAberto.quantidadeCuringa;
                    MontaTotal.Add(it);
                }
            }





            return MontaTotal;

            //return cn.ExecuteStringSqlToList(new OrdemProducaoView(), SQL.ToString());
        }


        //necessario do filto


        public IEnumerable<OrdemProducaoView> GetByOrdensDoNecessarioDoFiltro(List<int> idsOdens, int IdMmateriaprima, int Idcor, int Idtamanho, DateTime DaInclusao, DateTime AteInclusao)
        {
            var Valor = "'" + DaInclusao.ToString("yyyy-MM-dd") + "' AND '" + AteInclusao.ToString("yyyy-MM-dd") + "'";

            var SQL = new StringBuilder(); 
            
            SQL.AppendLine("SELECT IdOP,referencia,Status ,DataEmissao, SUM(IFNULL(quantidadenecessaria, 0)) as quantidadecuringa  FROM ( ");

            SQL.AppendLine("(SELECT OP.id as IdOP,OP.referencia,OP.status,DataEmissao,PM.Referencia as MaterialReferencia, PM.Descricao as MaterialDescricao, FI.materiaPrimaId, ((FI.quantidade*IOP.Quantidade)) as quantidadenecessaria, 0 as quantidadeempenhada, 0 as quantidadebaixada,");
            SQL.AppendLine("C.Abreviatura as CorDescricao, T.Abreviatura as TamanhoDescricao, ");
            SQL.AppendLine("C.ID as CorId, T.Id as TamanhoId, AF.abreviatura as ArmazemDescricao, AF.id as ArmazemId, UMD.abreviatura as UM,SEG.descricao as SE, FI.materiaPrimaId as MateriaPrimaOriginalId, C.ID  as CorOriginalId, T.Id as TamanhoOriginalId");
            SQL.AppendLine("FROM 	produtos P");
            SQL.AppendLine("INNER JOIN itensordemproducao IOP ON IOP.ProdutoId = P.Id ");
            SQL.AppendLine("INNER JOIN fichatecnicadomaterial F ON F.ProdutoId = P.Id ");
            SQL.AppendLine("INNER JOIN fichatecnicadomaterialitem FI ON FI.fichatecnicaid = F.Id ");
            SQL.AppendLine("INNER JOIN fichatecnicadomaterialrelacao FR ON ");
            SQL.AppendLine("(FR.fichatecnicaid = F.Id AND FR.materiaPrimaId = FI.materiaPrimaId AND FR.cor_produto_id = IOP.CorId AND FR.tamanho_produto_id = IOP.TamanhoId AND FR.fichatecnicaitemId = FI.Id) ");
            SQL.AppendLine("INNER JOIN produtos PM ON FI.materiaprimaid = PM.Id ");
            SQL.AppendLine("LEFT JOIN segmentos SEG on SEG.id = PM.Idsegmento");
            SQL.AppendLine("INNER JOIN cores C ON C.id = FR.cor_materiaprima_id ");
            SQL.AppendLine("INNER JOIN tamanhos T ON T.id = FR.tamanho_materiaprima_id ");
            SQL.AppendLine("INNER JOIN ordemproducao OP ON OP.Id = IOP.OrdemProducaoId ");
            SQL.AppendLine("INNER JOIN almoxarifados AF ON AF.id = OP.AlmoxarifadoId ");
            SQL.AppendLine("INNER JOIN unidademedidas UMD ON UMD.id = PM.IdUniMedida ");
            SQL.AppendLine("WHERE  OP.Status  <> 6 AND ");
            if (idsOdens.Count > 0)
            {
                SQL.AppendLine(" IOP.OrdemProducaoId IN ");
                SQL.AppendLine(" (" + string.Join(", ", idsOdens) + ") AND ");
            }
                        
            SQL.AppendLine(" FR.materiaPrimaId = " + IdMmateriaprima + " AND FR.cor_materiaprima_Id = " + Idcor + " AND FR.tamanho_materiaprima_Id = " + Idtamanho);


            SQL.AppendLine("	AND IOP.Status = 0 AND  SUBSTRING(OP.DataEmissao, 1, 10) BETWEEN " + Valor + ")");

            SQL.AppendLine("UNION ALL");

            SQL.AppendLine("(SELECT  o.id as IdOP,o.referencia,o.status,DataEmissao ,p.Referencia as MaterialReferencia, p.Descricao as MaterialDescricao, opm.materiaPrimaId, ");
            //SQL.AppendLine(" IF(IF(opm.quantidadebaixada > 0, opm.quantidadenecessaria - opm.quantidadebaixada, opm.quantidadenecessaria - opm.quantidadeempenhada) < 0, 0, IF(opm.quantidadebaixada > 0, opm.quantidadenecessaria - opm.quantidadebaixada, opm.quantidadenecessaria - opm.quantidadeempenhada)) as quantidadenecessaria, ");
            SQL.AppendLine(" IF((opm.quantidadenecessaria - opm.quantidadebaixada - opm.quantidadeempenhada) < 0,0,(opm.quantidadenecessaria - opm.quantidadebaixada - opm.quantidadeempenhada)) as quantidadenecessaria, ");
            SQL.AppendLine("(opm.quantidadeempenhada) as quantidadeempenhada, (opm.quantidadebaixada) as quantidadebaixada,");
            SQL.AppendLine("c.abreviatura as CorDescricao,");
            SQL.AppendLine("t.abreviatura as TamanhoDescricao,");
            SQL.AppendLine(" opm.corid, opm.tamanhoid, "); // F.possuiquebra, FI.DestinoId AS Destino, FI.sequencia,
            SQL.AppendLine("a.abreviatura as ArmazemDescricao, opm.armazemid,");
            SQL.AppendLine("S.descricao as SE,u.abreviatura as UM,");
            SQL.AppendLine("opm.materiaPrimaoriginalId, opm.cororiginalid, opm.tamanhooriginalid");
            SQL.AppendLine("FROM 	ordemproducaomateriais opm");
            SQL.AppendLine("INNER JOIN ordemproducao o ON o.Id = opm.OrdemProducaoId ");
            SQL.AppendLine("INNER JOIN itensordemproducao IOP ON IOP.Id = opm.ItemOrdemProducaoId ");
            SQL.AppendLine("INNER JOIN produtos p ON p.Id = opm.materiaprimaid ");
            SQL.AppendLine("INNER JOIN cores c ON c.Id = opm.corid ");
            SQL.AppendLine("INNER JOIN tamanhos t ON t.Id = opm.tamanhoid ");
            SQL.AppendLine("LEFT JOIN segmentos S on S.id = p.Idsegmento");
            SQL.AppendLine("LEFT JOIN almoxarifados a ON a.Id = opm.armazemid ");
            SQL.AppendLine("LEFT JOIN unidademedidas u ON u.Id = p.IdUniMedida ");
            SQL.AppendLine("WHERE  o.Status  <> 6 AND ");

            if (idsOdens.Count > 0)
            {
                SQL.AppendLine("	opm.OrdemProducaoId IN ");
                SQL.AppendLine(" (" + string.Join(", ", idsOdens) + ") AND ");

            }

            
            SQL.AppendLine(" opm.materiaprimaid = " + IdMmateriaprima + " AND opm.corid = " + Idcor + " AND opm.tamanhoid = " + Idtamanho);
            
            SQL.AppendLine("	AND IOP.Status > 0 AND  SUBSTRING(o.DataEmissao, 1, 10) BETWEEN " + Valor + ")) as om");

            SQL.AppendLine("GROUP BY IdOP,materiaPrimaId, corId, tamanhoId");

            var cn = new DapperConnection<OrdemProducaoView>();
            return cn.ExecuteStringSqlToList(new OrdemProducaoView(), SQL.ToString());
        }


        //TODOS OS PEDIDOS COM O ITEM
        public IEnumerable<OrdemProducaoView> GetByPedidosMaterialGestaoCompra(int IdMaterial,int IdCor,int IdTamanho)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT PC.status,PC.referencia,PC.DataEmissao,I.Id, I.PedidoCompraId,");
            SQL.AppendLine("I.TipoMovimentacaoId, I.ProdutoId,");
            SQL.AppendLine("I.TamanhoId, I.CorId, ");
            SQL.AppendLine("I.Qtd - I.QtdAtendida as quantidadecuringa, ");
            SQL.AppendLine("(I.Qtd) AS Qtd, (I.QtdAtendida) AS QtdAtendida,");
            SQL.AppendLine("P.Descricao AS ProdutoDescricao,");
            SQL.AppendLine("P.Referencia AS ProdutoReferencia,");
            SQL.AppendLine("T.Descricao AS TamanhoDescricao,");
            SQL.AppendLine("C.Descricao AS CorDescricao");
            SQL.AppendLine("FROM 	itenspedidoCompra I");
            SQL.AppendLine("INNER JOIN pedidoCompra PC ON PC.Id = I.PedidoCompraId");
            SQL.AppendLine("INNER JOIN produtos P ON P.Id = I.ProdutoId	");
            SQL.AppendLine("LEFT JOIN tamanhos T ON T.Id = I.TamanhoId ");
            SQL.AppendLine("LEFT JOIN cores C ON C.Id = I.CorId ");
            SQL.AppendLine("WHERE	I.ProdutoId = " + IdMaterial);
            SQL.Append(" AND I.CorId = " + IdCor);
            SQL.Append(" AND I.TamanhoId = " + IdTamanho);
            SQL.Append(" AND I.Qtd - I.QtdAtendida > 0 AND  PC.Status <> 4");
            SQL.AppendLine(" GROUP BY I.id");

            var cn = new DapperConnection<OrdemProducaoView>();

            return cn.ExecuteStringSqlToList(new OrdemProducaoView(), SQL.ToString());
        }



        public IEnumerable<OrdemProducaoView> GetByExibirPedidoPorOrdem(List<int> IdsOps, int IdMateriaPrima, int idCor, int idTamanho)
        {
            List<OrdemProducaoView> MontaTotal = null;
            MontaTotal = new List<OrdemProducaoView>();
            MontaTotal.Clear();
            OrdemProducaoView it = new OrdemProducaoView();
            string SQL = String.Empty;

            SQL = "select ordemproducao.Referencia from ordemproducao where ordemproducao.id IN (" + string.Join(", ", IdsOps) + ")";
            DataTable OPS = _cn.ExecuteToDataTable(SQL.ToString());
            if (OPS != null && OPS.Rows.Count > 0)
            {
                SQL = String.Empty;

                for (int i = 0; i < OPS.Rows.Count; i++)
                {
                    SQL = " select pedidocompra.Referencia,pedidocompra.DataEmissao,IFNULL(SUM(itenspedidocompra.Qtd - itenspedidocompra.QtdAtendida),0) as quantidadeCuringa from pedidocompra " +
                     " INNER JOIN itenspedidocompra ON itenspedidocompra.PedidoCompraId = pedidocompra.Id " +
                     " where LOCATE(" + "'" + OPS.Rows[i]["Referencia"].ToString() + "'" + ",OrdensReferencia)" +
                     " AND itenspedidocompra.ProdutoId = " + IdMateriaPrima +
                     " AND itenspedidocompra.CorId = " + idCor +
                     " AND itenspedidocompra.TamanhoId = " + idTamanho +
                     " AND itenspedidocompra.Qtd - itenspedidocompra.QtdAtendida > 0 AND  pedidocompra.Status <> 4 " +
                     " GROUP BY pedidocompra.id,itenspedidocompra.ProdutoId,itenspedidocompra.CorId,itenspedidocompra.TamanhoId ";
                    var cn = new DapperConnection<OrdemProducaoView>();

                    var dados = cn.ExecuteStringSqlToList(new OrdemProducaoView(), SQL.ToString());
                    foreach (var item in dados)
                    {

                        it = new OrdemProducaoView();
                        it.Referencia = item.Referencia;
                        it.DataEmissao = item.DataEmissao;
                        it.quantidadeCuringa = item.quantidadeCuringa;

                        List<OrdemProducaoView> filtro = new List<OrdemProducaoView>();
                        filtro = MontaTotal;
                        var ExistePedido = filtro.Where(x => x.Referencia  == item.Referencia).ToList();
                        if (ExistePedido.Count == 0)
                        {
                            MontaTotal.Add(it);
                        }                          
                       
                    }


                }
            }

            return MontaTotal;

        }

        public IEnumerable<OrdemProducaoView> GetByExibeNotaEntrada(int IdMatPrima, int IdCor, int IdTamanho, DateTime DaInclusao, DateTime AteInclusao)
        {
            var Valor = "'" + DaInclusao.ToString("yyyy-MM-dd") + "' AND '" + AteInclusao.ToString("yyyy-MM-dd") + "'";

            var SQL = new StringBuilder();
            SQL.AppendLine("select notaentrada.Referencia,notaentrada.DataInclusao as DataEmissao,SUM(notaentradaitens.quantidade) as quantidadeCuringa FROM notaentradaitens");
            SQL.AppendLine("INNER JOIN notaentrada ON notaentrada.id = notaentradaitens.IdNota");
            SQL.AppendLine("WHERE notaentradaitens.iditem = " + IdMatPrima);
            SQL.AppendLine("AND notaentradaitens.idcor = " + IdCor);
            SQL.AppendLine("AND  notaentradaitens.idtamanho = " + IdTamanho);
            SQL.AppendLine("AND   SUBSTRING(notaentrada.DataInclusao, 1, 10) BETWEEN " + Valor);
            SQL.AppendLine("GROUP BY notaentradaitens.IdNota,notaentradaitens.iditem,notaentradaitens.idcor,notaentradaitens.idtamanho");


            var cn = new DapperConnection<OrdemProducaoView>();

            return cn.ExecuteStringSqlToList(new OrdemProducaoView(), SQL.ToString());
        }


        public bool PedidoExisteEmOutrasOps(int IdPedido, int IdOp)
        {
            var cn = new DapperConnection<OrdemProducaoView>();
            string SQL = String.Empty;
            bool ExisteEmOutrasOps = false;
            OrdemProducaoView ret = new OrdemProducaoView();

            SQL = " SELECT * FROM itensordemproducao where itensordemproducao.PedidoVendaId = " + IdPedido + " AND itensordemproducao.OrdemProducaoId <> " + IdOp;

            cn.ExecuteToModel(ref ret, SQL.ToString());

            if (ret != null)
            {
                ExisteEmOutrasOps = true;
            }

            return ExisteEmOutrasOps;
        }


        public bool MudaDataPrevisao(DataTable  Itens)
        {
            string SQL = String.Empty;
            DateTime Valor = new DateTime();
            try
            {
                for (int i = 0; i < Itens.Rows.Count; i++)
                {
                    Valor =  Convert.ToDateTime(Itens.Rows[i]["NovaData"]);
                    var NovaData = "'" + Valor.ToString("yyyy-MM-dd") +  "'";

                    SQL = "UPDATE itensordemproducao SET DataPrevisao = "  + NovaData + " WHERE itensordemproducao.id = " + Itens.Rows[i]["IdItem"];
                    _cn.ExecuteNonQuery(SQL);
                }
                
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }

        public IEnumerable<ItemOrdemProducaoView> GetByMudaData(int ordemId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT I.*,");
            SQL.AppendLine("P.Referencia AS ProdutoReferencia,");
            SQL.AppendLine("P.Descricao AS ProdutoDescricao,");
            SQL.AppendLine("P.QtdPacote AS QtdPacote,");
            SQL.AppendLine("T.Abreviatura AS TamanhoDescricao,");
            SQL.AppendLine("C.Abreviatura AS CorDescricao, ");
            SQL.AppendLine("IFNULL(PC.datasaida,'Permite') as StatusPacote");
            SQL.AppendLine("FROM 	itensordemproducao I");
            SQL.AppendLine("INNER JOIN produtos P ON P.Id = I.ProdutoId	");
            SQL.AppendLine("LEFT JOIN tamanhos T ON T.Id = I.TamanhoId ");
            SQL.AppendLine("LEFT JOIN cores C ON C.Id = I.CorId ");
            SQL.AppendLine("LEFT JOIN pacotes PC ON PC.itemordemproducaoid = I.id ");
            SQL.AppendLine("WHERE	I.OrdemProducaoId = ");
            SQL.Append(ordemId);
            SQL.AppendLine(" GROUP BY I.id ");

            var cn = new DapperConnection<ItemOrdemProducaoView>();
            return cn.ExecuteStringSqlToList(new ItemOrdemProducaoView(), SQL.ToString());
        }

        public void AlimentarDadosDaOrdemVinculada(List<NotaEntradaFaccaoItens> itensNota)
        {
            string SQL = String.Empty;
            var cn = new DapperConnection<ItemOrdemProducao>();

            foreach (var item in itensNota)
            {
                SQL = "UPDATE itensordemproducao SET itensordemproducao.QuantidadeProduzida = QuantidadeProduzida + " + item.quantidadeProduzida.ToString().Replace(",", ".") + " ,itensordemproducao.QuantidadeAvaria =  QuantidadeAvaria + " +
                item.quantidadeAvaria.ToString().Replace(",", ".") + ", itensordemproducao.QuantidadeDefeito = QuantidadeDefeito + " + item.quantidadeDefeito.ToString().Replace(",", ".") + " WHERE itensordemproducao.id = " + item.IdItemNaOrdem;
                cn.ExecuteNonQuery(SQL);
            }
        }


        public void TrataExclusaoNotaFaccao(int IdItem, decimal Produzida,decimal Avaria,decimal Defeito, bool ZeraAvariaDefeito)
        {
            string SQL = String.Empty;
            string AvariaDefeito = String.Empty;

            //if(ZeraAvariaDefeito)
            //AvariaDefeito = " ,QuantidadeAvaria = 0,QuantidadeDefeito = 0 ";

            var cn = new DapperConnection<ItemOrdemProducao>();

            SQL = "UPDATE itensordemproducao SET itensordemproducao.QuantidadeProduzida = QuantidadeProduzida - " + Produzida.ToString().Replace(",",".") + " ,itensordemproducao.QuantidadeAvaria =  QuantidadeAvaria - " +
                Avaria.ToString().Replace(",", ".") + ", itensordemproducao.QuantidadeDefeito = QuantidadeDefeito - " + Defeito.ToString().Replace(",", ".") + " WHERE itensordemproducao.id = " + IdItem;
            cn.ExecuteNonQuery(SQL);

        }


        public void FinalizaOrdemTela(List<ItensOrdemFinalizacaoView> itensDaOp) //Winner
        {
            string SQL = String.Empty;
            var cn = new DapperConnection<ItemOrdemProducao>();

            try
            {

                foreach (var item in itensDaOp)
                {
                    decimal TotalGeral = 0;
                    TotalGeral = item.Total + item.DefeitoItem;

                    SQL = "UPDATE itensordemproducao SET itensordemproducao.QuantidadeProduzida = QuantidadeProduzida + " + TotalGeral.ToString().Replace(",", ".") + ", QuantidadeDefeito = QuantidadeDefeito + " + item.DefeitoItem.ToString().Replace(",", ".") + " WHERE itensordemproducao.id = " + item.IdItemNaOp;
                    cn.ExecuteNonQuery(SQL);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }


        }

        public void EstornaFinalizacaoOrdemTela(List<ItensOrdemFinalizacaoView> itensDaOp) //Winner
        {
            string SQL = String.Empty;
            var cn = new DapperConnection<ItemOrdemProducao>();

            try
            {

                foreach (var item in itensDaOp)
                {
                    decimal TotalGeral = 0;
                    TotalGeral = item.Quantidade;

                    SQL = "UPDATE itensordemproducao SET itensordemproducao.QuantidadeProduzida = QuantidadeProduzida - " + TotalGeral.ToString().Replace(",", ".") + ", QuantidadeDefeito = QuantidadeDefeito - " + item.DefeitoItem.ToString().Replace(",", ".") + " WHERE itensordemproducao.id = " + item.IdItemNaOp;
                    cn.ExecuteNonQuery(SQL);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public void FinalizaItemOrdemTela(List<ItensOrdemFinalizacaoView> itensDaOp) //Honey Be
        {
            string SQL = String.Empty;
            var cn = new DapperConnection<ItemOrdemProducao>();
            int Finalizado = 0;
            string OperacaoLog = String.Empty;
            try
            {

                foreach (var item in itensDaOp)
                {

                    if(item.ItemFinalizado == 1)
                    {
                        Finalizado = 1;
                    }
                    else
                    {
                        Finalizado = 0;
                    }
                    SQL = "UPDATE itensordemproducao SET itensordemproducao.ItemFinalizado = " + Finalizado  + " WHERE itensordemproducao.id = " + item.IdItemNaOp;
                    cn.ExecuteNonQuery(SQL);
                    if(Finalizado == 1)
                    {
                        OperacaoLog = "Desistência de produção ";
                    }
                    else
                    {
                        OperacaoLog = "Estornou Desistência ";
                    }

                    var registro = new Log();
                    registro.Data = DateTime.Now;
                    registro.DescricaoOperacao = OperacaoLog + "do item de id igual a " + item.IdItemNaOp;
                    registro.EmpresaId = VestilloSession.EmpresaLogada.Id;
                    registro.Modulo = "Itens Ordem Produção";
                    registro.UsuarioId = VestilloSession.UsuarioLogado.Id;
                    new LogService().GetServiceFactory().Save(ref registro);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
    }
}
