using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Business.Service;
using Vestillo.Connection;
using Vestillo.Lib;

namespace Vestillo.Business.Repositories
{
    public class ItemPedidoVendaRepository: GenericRepository<ItemPedidoVenda>
    {
        public ItemPedidoVendaRepository()
            : base(new DapperConnection<ItemPedidoVenda>())
        {
        }

        public class ItensVendaCerta
        {
            public int IdProduto { get; set; }            
            public int Idcor { get; set; }
            public string DescCor { get; set; }
            public string AbvCor { get; set; }            
            public string RefProduto { get; set; }
            public string DescProduto { get; set; }
            public string segmento { get; set; }
            public string colecao { get; set; }
            public string catalogo { get; set; }
        }

        public IEnumerable<ItemPedidoVendaView> GetByIdView(int pedidoId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT I.*,");
            SQL.AppendLine("P.Referencia AS ProdutoReferencia,");
            SQL.AppendLine("P.referenciacli AS RefCliente,");
            SQL.AppendLine("P.Descricao AS ProdutoDescricao,");
            SQL.AppendLine("T.Descricao AS TamanhoDescricao,");
            SQL.AppendLine("C.Descricao AS CorDescricao,");
            SQL.AppendLine("T.Abreviatura AS TamanhoReferencia,");
            SQL.AppendLine("C.Abreviatura AS CorReferencia,");
            SQL.AppendLine("tm.referencia as RefTipoMovimentacao,");
            SQL.AppendLine("tm.descricao as DescricaoTipoMovimentacao");
            SQL.AppendLine("FROM 	itenspedidovenda I");
            SQL.AppendLine("INNER JOIN produtos P ON P.Id = I.ProdutoId	");
            SQL.AppendLine("LEFT JOIN tamanhos T ON T.Id = I.TamanhoId ");
            SQL.AppendLine("LEFT JOIN cores C ON C.Id = I.CorId ");
            SQL.AppendLine("LEFT JOIN TipoMovimentacoes tm ON tm.id = I.TipoMovimentacaoId");
            SQL.AppendLine("WHERE	I.PedidoVendaId = ");
            SQL.Append(pedidoId);
            SQL.Append(" ORDER BY P.Referencia, C.Descricao,T.id");

            var cn = new DapperConnection<ItemPedidoVendaView>();
            return cn.ExecuteStringSqlToList(new ItemPedidoVendaView(), SQL.ToString());
        }

        public IEnumerable<ItemPedidoVendaView> GetAllView()
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT I.*,");
            SQL.AppendLine("P.Referencia AS ProdutoReferencia,");
            SQL.AppendLine("P.referenciacli AS RefCliente,");
            SQL.AppendLine("P.Descricao AS ProdutoDescricao,");
            SQL.AppendLine("T.Descricao AS TamanhoDescricao,");
            SQL.AppendLine("C.Descricao AS CorDescricao,");
            SQL.AppendLine("T.Abreviatura AS TamanhoReferencia,");
            SQL.AppendLine("C.Abreviatura AS CorReferencia,");
            SQL.AppendLine("tm.referencia as RefTipoMovimentacao,");
            SQL.AppendLine("tm.descricao as DescricaoTipoMovimentacao");
            SQL.AppendLine("FROM 	itenspedidovenda I");
            SQL.AppendLine("INNER JOIN produtos P ON P.Id = I.ProdutoId	");
            SQL.AppendLine("LEFT JOIN tamanhos T ON T.Id = I.TamanhoId ");
            SQL.AppendLine("LEFT JOIN cores C ON C.Id = I.CorId ");
            SQL.AppendLine("LEFT JOIN TipoMovimentacoes tm ON tm.id = I.TipoMovimentacaoId");
            SQL.Append(" ORDER BY P.Referencia, C.Descricao,T.id");

            var cn = new DapperConnection<ItemPedidoVendaView>();
            return cn.ExecuteStringSqlToList(new ItemPedidoVendaView(), SQL.ToString());
        }

        public IEnumerable<ConsultaPedidoVendaRelatorio> GetPedidoParaRelatorio(FiltroPedidoVendaRelatorio filtro)
        {

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT	P.Descricao as DescProduto, P.Referencia as RefProduto, C.Abreviatura AS CorProduto,PV.DataEmissao, P.referenciacli AS RefProdutoCliente, PD.referenciagradecli AS RefGradeCliente,");
            SQL.AppendLine("T.Abreviatura AS TamanhoProduto, IFNULL(T.Id, 0) as TamanhoId, PV.CodigoPedidoCliente, I.ReferenciaPedidoCliente, I.SeqPedCliente, I.Preco,");
            SQL.AppendLine(" PV.ClienteId, CLI.RazaoSocial AS RazaoCliente, CLI.CNPJCPF, CLI.InscEstadual, CONCAT(CLI.Endereco, CLI.Numero) AS Endereco, CLI.CEP, CLI.DDD, CLI.Telefone, CLI.Bairro, CLI.IdEstado, CLI.IdMunicipio, CLI.obscliente, ");
            SQL.AppendLine(" PV.VendedorId, IFNULL(VEN.Referencia, '') AS RefVendedor, IFNULL(VEN.RazaoSocial, '') AS RazaoVendedor, ");
            SQL.AppendLine(" PV.id AS PedidoId, PV.PrevisaoEntrega AS DataEntrega, PV.Status, PV.RotaId, PV.Obs,PV.DescPercent,PV.DescValor, ");
            SQL.AppendLine(" IF(pv.TipoFrete = 0, ' ', IF(pv.TipoFrete = 1, 'CIF', 'FOB')) as TipoFrete, ");

            if (filtro.Agrupar == "Pedido")
            {
                SQL.AppendLine("SUM(I.Qtd * I.Preco) AS ValorTotal, SUM(IL.QtdFaturada * I.Preco) AS ValorEntregue, SUM((I.Qtd - IFNULL(IL.QtdFaturada, 0)) * I.Preco) AS ValorEntregar,");
            }

            SQL.AppendLine("SUM(I.Qtd) AS Quantidade, SUM(IL.QtdEmpenhada) AS Empenhado, SUM(IL.QtdFaturada) AS QuantidadeAtendida, SUM(IF(PV.Status = 4,I.Qtd - IFNULL(IL.QtdFaturada,0), 0)) AS SaldoCancelado, PV.Referencia As Pedido");            
            SQL.AppendLine("FROM 	ItensPedidoVenda I");
            SQL.AppendLine("INNER JOIN PedidoVenda PV ON PV.Id = I.PedidoVendaId");
            SQL.AppendLine("INNER JOIN Produtos P ON P.Id = I.ProdutoId");
            SQL.AppendLine("INNER JOIN colaboradores CLI ON CLI.Id = PV.ClienteId");
            SQL.AppendLine("LEFT JOIN colaboradores VEN ON VEN.Id = PV.VendedorId");
            SQL.AppendLine("LEFT JOIN Tamanhos T ON T.Id = I.TamanhoId");
            SQL.AppendLine("LEFT JOIN Cores C ON C.Id = I.CorId");
            SQL.AppendLine("LEFT JOIN ProdutoDetalhes PD ON C.Id = PD.IdCor AND T.id = PD.IdTamanho AND P.Id = PD.IdProduto");
            SQL.AppendLine("LEFT JOIN ItensLiberacaoPedidoVenda IL ON I.Id = IL.ItemPedidoVendaId");
            SQL.AppendLine("WHERE " + FiltroEmpresa("PV.EmpresaId"));

            if (filtro.Pedido != null && filtro.Pedido.Count() > 0)
                SQL.AppendLine(" AND I.PedidoVendaId in (" + string.Join(",", filtro.Pedido.ToArray()) + ")");

            if (filtro.Cliente != null && filtro.Cliente.Count() > 0)
                SQL.AppendLine(" AND PV.ClienteId in (" + string.Join(",", filtro.Cliente.ToArray()) + ")");

            if (filtro.Vendedor != null && filtro.Vendedor.Count() > 0)
                SQL.AppendLine(" AND PV.VendedorId in (" + string.Join(",", filtro.Vendedor.ToArray()) + ")");

            if (filtro.Produtos != null && filtro.Produtos.Count() > 0)
                SQL.AppendLine(" AND I.ProdutoId in (" + string.Join(",", filtro.Produtos.ToArray()) + ")");

            if (filtro.Cor != null && filtro.Cor.Count() > 0)
                SQL.AppendLine(" AND I.CorId in (" + string.Join(",", filtro.Cor.ToArray()) + ")");

            if (filtro.Tamanho != null && filtro.Tamanho.Count() > 0)
                SQL.AppendLine(" AND I.TamanhoId in (" + string.Join(",", filtro.Tamanho.ToArray()) + ")");

            if (filtro.Segmento != null && filtro.Segmento.Count() > 0)
                SQL.AppendLine(" AND P.idSegmento in (" + string.Join(",", filtro.Segmento.ToArray()) + ")");

            if (filtro.Colecao != null && filtro.Colecao.Count() > 0)
                SQL.AppendLine(" AND P.idColecao in (" + string.Join(",", filtro.Colecao.ToArray()) + ")");

            if (filtro.Catalogo != null && filtro.Catalogo.Count() > 0)
                SQL.AppendLine(" AND P.IdCatalogo in (" + string.Join(",", filtro.Catalogo.ToArray()) + ")");

            if (filtro.Grupo != null && filtro.Grupo.Count() > 0)
                SQL.AppendLine(" AND P.idGrupo in (" + string.Join(",", filtro.Grupo.ToArray()) + ")");

            if (filtro.Rota != null && filtro.Rota.Count() > 0)
                SQL.AppendLine(" AND PV.RotaId in (" + string.Join(",", filtro.Rota.ToArray()) + ")");

            if (filtro.DoAno != null && filtro.DoAno != "")
                SQL.AppendLine(" AND P.ano >= " + filtro.DoAno + "");

            if (filtro.AteAno != null && filtro.AteAno != "")
                SQL.AppendLine(" AND P.ano <= " + filtro.AteAno + "");

            if (filtro.DaEmissao != null && filtro.AteEmissao != null && filtro.AteEmissao.ToString("yyyy-MM-dd") != "0001-01-01")
                SQL.AppendLine(" AND Date(PV.DataEmissao) between '" + filtro.DaEmissao.ToString("yyyy-MM-dd") + "' AND '" + filtro.AteEmissao.ToString("yyyy-MM-dd") + "'");

            if (filtro.DaEntrega != null && filtro.AteEntrega != null && filtro.AteEntrega.ToString("yyyy-MM-dd") != "0001-01-01")
                SQL.AppendLine(" AND Date(PV.PrevisaoEntrega) between '" + filtro.DaEntrega.ToString("yyyy-MM-dd") + "' AND '" + filtro.AteEntrega.ToString("yyyy-MM-dd") + "'");

            if (filtro.DaLiberacao != null && filtro.AteLiberacao != null && filtro.AteLiberacao.ToString("yyyy-MM-dd") != "0001-01-01")
                SQL.AppendLine(" AND Date(IL.Data) between '" + filtro.DaLiberacao.ToString("yyyy-MM-dd") + "' AND '" + filtro.AteLiberacao.ToString("yyyy-MM-dd") + "'");

            switch (filtro.ExibirPedidos)
            {
                case 0:
                    break;
                case 1:
                    SQL.AppendLine(" AND (PV.Status = 2 OR PV.Status = 3) ");      
                    break;
                case 2:
                    SQL.AppendLine(" AND PV.Status = 4 ");
                    break;
                default:
                    SQL.AppendLine(" AND (PV.Status <> 2 AND PV.Status <> 3 AND PV.Status <> 4 ) ");
                    break;
            }

            if (filtro.Sintetico)
            {
                switch (filtro.Agrupar)
                {
                    case "Produto":
                        SQL.AppendFormat(" GROUP BY P.Id");
                        break;
                    case "Cor":
                        SQL.AppendFormat(" GROUP BY P.Id,I.CorId");
                        break;
                    case "Tamanho":
                        SQL.AppendFormat(" GROUP BY P.Id,I.TamanhoId");
                        break;
                    case "Grade":
                        SQL.AppendLine(" GROUP BY P.Id,I.CorId, I.TamanhoId");
                        break;
                    default:
                        SQL.AppendFormat(" GROUP BY P.Id, I.CorId, I.TamanhoId");
                        break;
                }
            }
            else
            {
                switch (filtro.Agrupar)
                {
                    case "Produto":
                        SQL.AppendFormat(" GROUP BY PV.Id, P.Id");
                        break;
                    case "Cor":
                        SQL.AppendFormat(" GROUP BY PV.Id, P.Id,I.CorId");
                        break;
                    case "Tamanho":
                        SQL.AppendFormat(" GROUP BY PV.Id, P.Id,I.TamanhoId");
                        break;
                    case "Pedido":
                        SQL.AppendFormat(" GROUP BY PV.Id");
                        break;
                    default:
                        SQL.AppendFormat(" GROUP BY PV.Id, P.Id, I.CorId, I.TamanhoId");
                        break;
                }
            }

            SQL.AppendLine(" ORDER BY P.Referencia, P.Descricao, C.Id, T.Id");

            var cn = new DapperConnection<ConsultaPedidoVendaRelatorio>();
            return cn.ExecuteStringSqlToList(new ConsultaPedidoVendaRelatorio(), SQL.ToString());
        }



        public IEnumerable<ConsultaPedidoVendaRelatorio> GetPedidoParaRelatorioExibeReferencias(FiltroPedidoVendaRelatorio filtro)
        {

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT	P.Descricao as DescProduto, P.Referencia as RefProduto, C.Abreviatura AS CorProduto,PV.DataEmissao, P.referenciacli AS RefProdutoCliente, PD.referenciagradecli AS RefGradeCliente,");
            SQL.AppendLine("T.Abreviatura AS TamanhoProduto, IFNULL(T.Id, 0) as TamanhoId, PV.CodigoPedidoCliente, I.ReferenciaPedidoCliente, I.SeqPedCliente, I.Preco,");
            SQL.AppendLine(" PV.ClienteId, CLI.RazaoSocial AS RazaoCliente, CLI.CNPJCPF, CLI.InscEstadual, CONCAT(CLI.Endereco, CLI.Numero) AS Endereco, CLI.CEP, CLI.DDD, CLI.Telefone, CLI.Bairro, CLI.IdEstado, CLI.IdMunicipio, CLI.obscliente, ");
            SQL.AppendLine(" PV.VendedorId, IFNULL(VEN.Referencia, '') AS RefVendedor, IFNULL(VEN.RazaoSocial, '') AS RazaoVendedor, ");
            SQL.AppendLine(" PV.id AS PedidoId, PV.PrevisaoEntrega AS DataEntrega, PV.Status, PV.RotaId, PV.Obs,PV.DescPercent,PV.DescValor, ");
            SQL.AppendLine(" IF(pv.TipoFrete = 0, ' ', IF(pv.TipoFrete = 1, 'CIF', 'FOB')) as TipoFrete, ");

            if (filtro.Agrupar == "Pedido")
            {
                SQL.AppendLine("SUM(I.Qtd * I.Preco) AS ValorTotal, SUM(IL.QtdFaturada * I.Preco) AS ValorEntregue, SUM((I.Qtd - IFNULL(IL.QtdFaturada, 0)) * I.Preco) AS ValorEntregar,");
            }

            SQL.AppendLine("SUM(I.Qtd) AS Quantidade, SUM(IL.QtdEmpenhada) AS Empenhado, SUM(IL.QtdFaturada) AS QuantidadeAtendida, SUM(IF(PV.Status = 4,I.Qtd - IFNULL(IL.QtdFaturada,0), 0)) AS SaldoCancelado, PV.Referencia As Pedido");
            SQL.AppendLine("FROM 	ItensPedidoVenda I");
            SQL.AppendLine("INNER JOIN PedidoVenda PV ON PV.Id = I.PedidoVendaId");
            SQL.AppendLine("INNER JOIN Produtos P ON P.Id = I.ProdutoId");
            SQL.AppendLine("INNER JOIN colaboradores CLI ON CLI.Id = PV.ClienteId");
            SQL.AppendLine("LEFT JOIN colaboradores VEN ON VEN.Id = PV.VendedorId");
            SQL.AppendLine("LEFT JOIN Tamanhos T ON T.Id = I.TamanhoId");
            SQL.AppendLine("LEFT JOIN Cores C ON C.Id = I.CorId");
            SQL.AppendLine("LEFT JOIN ProdutoDetalhes PD ON C.Id = PD.IdCor AND T.id = PD.IdTamanho AND P.Id = PD.IdProduto");
            SQL.AppendLine("LEFT JOIN ItensLiberacaoPedidoVenda IL ON I.Id = IL.ItemPedidoVendaId");
            SQL.AppendLine("WHERE " + FiltroEmpresa("PV.EmpresaId"));

            if (filtro.Pedido != null && filtro.Pedido.Count() > 0)
                SQL.AppendLine(" AND I.PedidoVendaId in (" + string.Join(",", filtro.Pedido.ToArray()) + ")");

            if (filtro.Cliente != null && filtro.Cliente.Count() > 0)
                SQL.AppendLine(" AND PV.ClienteId in (" + string.Join(",", filtro.Cliente.ToArray()) + ")");

            if (filtro.Vendedor != null && filtro.Vendedor.Count() > 0)
                SQL.AppendLine(" AND PV.VendedorId in (" + string.Join(",", filtro.Vendedor.ToArray()) + ")");

            if (filtro.Produtos != null && filtro.Produtos.Count() > 0)
                SQL.AppendLine(" AND I.ProdutoId in (" + string.Join(",", filtro.Produtos.ToArray()) + ")");

            if (filtro.Cor != null && filtro.Cor.Count() > 0)
                SQL.AppendLine(" AND I.CorId in (" + string.Join(",", filtro.Cor.ToArray()) + ")");

            if (filtro.Tamanho != null && filtro.Tamanho.Count() > 0)
                SQL.AppendLine(" AND I.TamanhoId in (" + string.Join(",", filtro.Tamanho.ToArray()) + ")");

            if (filtro.Segmento != null && filtro.Segmento.Count() > 0)
                SQL.AppendLine(" AND P.idSegmento in (" + string.Join(",", filtro.Segmento.ToArray()) + ")");

            if (filtro.Colecao != null && filtro.Colecao.Count() > 0)
                SQL.AppendLine(" AND P.idColecao in (" + string.Join(",", filtro.Colecao.ToArray()) + ")");

            if (filtro.Catalogo != null && filtro.Catalogo.Count() > 0)
                SQL.AppendLine(" AND P.IdCatalogo in (" + string.Join(",", filtro.Catalogo.ToArray()) + ")");

            if (filtro.Grupo != null && filtro.Grupo.Count() > 0)
                SQL.AppendLine(" AND P.idGrupo in (" + string.Join(",", filtro.Grupo.ToArray()) + ")");

            if (filtro.Rota != null && filtro.Rota.Count() > 0)
                SQL.AppendLine(" AND PV.RotaId in (" + string.Join(",", filtro.Rota.ToArray()) + ")");

            if (filtro.DoAno != null && filtro.DoAno != "")
                SQL.AppendLine(" AND P.ano >= " + filtro.DoAno + "");

            if (filtro.AteAno != null && filtro.AteAno != "")
                SQL.AppendLine(" AND P.ano <= " + filtro.AteAno + "");

            if (filtro.DaEmissao != null && filtro.AteEmissao != null && filtro.AteEmissao.ToString("yyyy-MM-dd") != "0001-01-01")
                SQL.AppendLine(" AND Date(PV.DataEmissao) between '" + filtro.DaEmissao.ToString("yyyy-MM-dd") + "' AND '" + filtro.AteEmissao.ToString("yyyy-MM-dd") + "'");

            if (filtro.DaEntrega != null && filtro.AteEntrega != null && filtro.AteEntrega.ToString("yyyy-MM-dd") != "0001-01-01")
                SQL.AppendLine(" AND Date(PV.PrevisaoEntrega) between '" + filtro.DaEntrega.ToString("yyyy-MM-dd") + "' AND '" + filtro.AteEntrega.ToString("yyyy-MM-dd") + "'");

            if (filtro.DaLiberacao != null && filtro.AteLiberacao != null && filtro.AteLiberacao.ToString("yyyy-MM-dd") != "0001-01-01")
                SQL.AppendLine(" AND Date(IL.Data) between '" + filtro.DaLiberacao.ToString("yyyy-MM-dd") + "' AND '" + filtro.AteLiberacao.ToString("yyyy-MM-dd") + "'");

            switch (filtro.ExibirPedidos)
            {
                case 0:
                    break;
                case 1:
                    SQL.AppendLine(" AND (PV.Status = 2 OR PV.Status = 3) ");
                    break;
                case 2:
                    SQL.AppendLine(" AND PV.Status = 4 ");
                    break;
                default:
                    SQL.AppendLine(" AND (PV.Status <> 2 AND PV.Status <> 3 AND PV.Status <> 4 ) ");
                    break;
            }

            if (filtro.Sintetico)
            {
                switch (filtro.Agrupar)
                {
                    case "Produto":
                        SQL.AppendFormat(" GROUP BY  PV.Id,P.Id");
                        break;
                    case "Cor":
                        SQL.AppendFormat(" GROUP BY  PV.Id,P.Id,I.CorId");
                        break;
                    case "Tamanho":
                        SQL.AppendFormat(" GROUP BY  PV.Id,P.Id,I.TamanhoId");
                        break;
                    case "Grade":
                        SQL.AppendLine(" GROUP BY  PV.Id,P.Id,I.CorId, I.TamanhoId");
                        break;
                    default:
                        SQL.AppendFormat(" GROUP BY  PV.Id,P.Id, I.CorId, I.TamanhoId");
                        break;
                }
            }
            else
            {
                switch (filtro.Agrupar)
                {
                    case "Produto":
                        SQL.AppendFormat(" GROUP BY PV.Id, P.Id");
                        break;
                    case "Cor":
                        SQL.AppendFormat(" GROUP BY PV.Id, P.Id,I.CorId");
                        break;
                    case "Tamanho":
                        SQL.AppendFormat(" GROUP BY PV.Id, P.Id,I.TamanhoId");
                        break;
                    case "Pedido":
                        SQL.AppendFormat(" GROUP BY PV.Id");
                        break;
                    default:
                        SQL.AppendFormat(" GROUP BY PV.Id, P.Id, I.CorId, I.TamanhoId");
                        break;
                }
            }

            SQL.AppendLine(" ORDER BY P.Referencia, P.Descricao, C.Id, T.Id");

            var cn = new DapperConnection<ConsultaPedidoVendaRelatorio>();
            return cn.ExecuteStringSqlToList(new ConsultaPedidoVendaRelatorio(), SQL.ToString());
        }

        public IEnumerable<ItemPedidoVendaView> GetByPedido(int pedidoId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT I.*,");
            SQL.AppendLine("P.Referencia AS ProdutoReferencia,");
            SQL.AppendLine("P.referenciacli AS RefCliente,");            
            SQL.AppendLine("P.Descricao AS ProdutoDescricao,");            
            SQL.AppendLine("T.Descricao AS TamanhoDescricao,");
            SQL.AppendLine("C.Descricao AS CorDescricao,");
            SQL.AppendLine("T.Abreviatura AS TamanhoReferencia,");
            SQL.AppendLine("C.Abreviatura AS CorReferencia,");
            SQL.AppendLine("tm.referencia as RefTipoMovimentacao,");
            SQL.AppendLine("tm.descricao as DescricaoTipoMovimentacao");
            SQL.AppendLine("FROM 	itenspedidovenda I");
            SQL.AppendLine("INNER JOIN produtos P ON P.Id = I.ProdutoId	");
            SQL.AppendLine("LEFT JOIN tamanhos T ON T.Id = I.TamanhoId ");
            SQL.AppendLine("LEFT JOIN cores C ON C.Id = I.CorId ");
            SQL.AppendLine("LEFT JOIN TipoMovimentacoes tm ON tm.id = I.TipoMovimentacaoId");
            SQL.AppendLine("WHERE	I.PedidoVendaId = ");
            SQL.Append(pedidoId);
            SQL.Append(" ORDER BY P.Referencia, C.Descricao,T.id");

            var cn = new DapperConnection<ItemPedidoVendaView>();
            return cn.ExecuteStringSqlToList(new ItemPedidoVendaView(), SQL.ToString());
        }

        public IEnumerable<ItemPedidoVendaView> GetByPedidoLiberacao(int pedidoId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT I.*,");
            SQL.AppendLine("P.Referencia AS ProdutoReferencia,");
            SQL.AppendLine("P.referenciacli AS RefCliente,");       
            SQL.AppendLine("P.Descricao AS ProdutoDescricao,");
            SQL.AppendLine("T.Descricao AS TamanhoDescricao,");
            SQL.AppendLine("C.Descricao AS CorDescricao,");
            SQL.AppendLine("T.Abreviatura AS TamanhoReferencia,");
            SQL.AppendLine("C.Abreviatura AS CorReferencia,");
            SQL.AppendLine("SUM(IL.QtdEmpenhada) AS QtdEmpenhada,");
            SQL.AppendLine("SUM(IL.QtdNaoAtendida) AS QtdNaoAtendida,");
            SQL.AppendLine("tm.referencia as RefTipoMovimentacao,");
            SQL.AppendLine("tm.descricao as DescricaoTipoMovimentacao");
            SQL.AppendLine("FROM 	itenspedidovenda I");
            SQL.AppendLine("INNER JOIN produtos P ON P.Id = I.ProdutoId	");
            SQL.AppendLine("LEFT JOIN itensliberacaopedidovenda IL ON I.Id = IL.ItemPedidoVendaId	");
            SQL.AppendLine("LEFT JOIN tamanhos T ON T.Id = I.TamanhoId ");
            SQL.AppendLine("LEFT JOIN cores C ON C.Id = I.CorId ");
            SQL.AppendLine("LEFT JOIN TipoMovimentacoes tm ON tm.id = I.TipoMovimentacaoId");
            SQL.AppendLine("WHERE	I.PedidoVendaId = ");
            SQL.Append(pedidoId);
            SQL.Append(" GROUP BY I.Id, I.ProdutoId, I.CorId, I.TamanhoId");
            SQL.Append(" ORDER BY P.Referencia, C.Descricao,T.id");

            var cn = new DapperConnection<ItemPedidoVendaView>();
            return cn.ExecuteStringSqlToList(new ItemPedidoVendaView(), SQL.ToString());
        }

        public IEnumerable<ItemPedidoVendaView> GetByPedidoAgrupado(int pedidoId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT I.Id, I.PedidoVendaId, I.TipoMovimentacaoId, I.ProdutoId, I.TamanhoId, I.CorId, I.UnidadeMedidaId, SUM(I.Qtd) AS Qtd,");
            SQL.AppendLine(" I.UnidadeMedida2Id, I.QtdUnidadeMedida2, I.Preco, I.PrecoUnidadeMedida2, I.ReferenciaPedidoCliente, I.SeqPedCliente,");
            SQL.AppendLine("P.Referencia AS ProdutoReferencia,");
            SQL.AppendLine("P.referenciacli AS RefCliente,");       
            SQL.AppendLine("P.Descricao AS ProdutoDescricao,");
            SQL.AppendLine("T.Descricao AS TamanhoDescricao,");
            SQL.AppendLine("C.Descricao AS CorDescricao,");
            SQL.AppendLine("T.Abreviatura AS TamanhoReferencia,");
            SQL.AppendLine("C.Abreviatura AS CorReferencia,");
            SQL.AppendLine("tm.referencia as RefTipoMovimentacao,");
            SQL.AppendLine("tm.descricao as DescricaoTipoMovimentacao");
            SQL.AppendLine("FROM 	itenspedidovenda I");
            SQL.AppendLine("INNER JOIN produtos P ON P.Id = I.ProdutoId	");
            SQL.AppendLine("LEFT JOIN tamanhos T ON T.Id = I.TamanhoId ");
            SQL.AppendLine("LEFT JOIN cores C ON C.Id = I.CorId ");
            SQL.AppendLine("LEFT JOIN TipoMovimentacoes tm ON tm.id = I.TipoMovimentacaoId");
            SQL.AppendLine("WHERE	I.PedidoVendaId = ");
            SQL.Append(pedidoId);
            SQL.Append(" GROUP BY I.ProdutoId, I.CorId, I.TamanhoId");
            SQL.Append(" ORDER BY P.Referencia, C.Descricao,T.id");

            var cn = new DapperConnection<ItemPedidoVendaView>();
            return cn.ExecuteStringSqlToList(new ItemPedidoVendaView(), SQL.ToString());
        }

        public IEnumerable<ItemPedidoVenda> GetItens(int produtoId, int corId, int tamanhoId, int almoxarifadoId)
        {
            var SQL = new Select()
                .Campos("ip.*")
                .From("itenspedidovenda ip")
                .InnerJoin("pedidovenda p", "p.id = ip.PedidoVendaId")
                .Where("ip.Produtoid = " + produtoId + " and ip.CorId = " + corId + " and ip.TamanhoId = " + tamanhoId + " and p.AlmoxarifadoId = " + almoxarifadoId + " and p.Status = 1;");

            var cn = new DapperConnection<ItemPedidoVenda>();
            var tm = new ItemPedidoVenda();
            return cn.ExecuteStringSqlToList(tm, SQL.ToString());
        }

        public IEnumerable<ItemPedidoVenda> GetItensLiberadosSemEmpenho(int produtoId, int corId, int tamanhoId, int almoxarifadoId)
        {
            var SQL = new Select()
                .Campos("ip.Id, (il.Qtd - il.QtdNaoAtendida - il.QtdEmpenhada - il.QtdFaturada) as Qtd")
                .From("itenspedidovenda ip")
                .InnerJoin("pedidovenda p", "p.id = ip.PedidoVendaId")
                .InnerJoin("itensliberacaopedidovenda il", "ip.id = il.ItemPedidoVendaID")
                .Where("ip.Produtoid = " + produtoId + " and ip.CorId = " + corId + " and ip.TamanhoId = " + tamanhoId + " and p.AlmoxarifadoId = " + almoxarifadoId + " AND il.SemEmpenho = 1;");


            var cn = new DapperConnection<ItemPedidoVenda>();
            var tm = new ItemPedidoVenda();
            return cn.ExecuteStringSqlToList(tm, SQL.ToString());
        }

        public IEnumerable<ItemLiberacaoPedidoVenda> GetItensZerarEmpenho(int produtoId, int corId, int tamanhoId, int almoxarifadoId)
        {
            var SQL = new Select()
                .Campos("il.*")
                .From("itenspedidovenda ip")
                .InnerJoin("pedidovenda p", "p.id = ip.PedidoVendaId")
                .InnerJoin("itensliberacaopedidovenda il", "il.itemPedidoVendaId = ip.Id")
                .Where("ip.Produtoid = " + produtoId + " and ip.CorId = " + corId + " and ip.TamanhoId = " + tamanhoId + " and p.AlmoxarifadoId = " + almoxarifadoId + " AND il.QtdEmpenhada > 0; ");

            var cn = new DapperConnection<ItemLiberacaoPedidoVenda>();
            var tm = new ItemLiberacaoPedidoVenda();
            return cn.ExecuteStringSqlToList(tm, SQL.ToString());
        }

        public IEnumerable<ItemPedidoVendaView> GetByPedidoAgrupadoView(int pedidoId, bool AgrupaTamanho)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT SUM(I.Qtd) AS Qtd,P.Referencia AS ProdutoReferencia,");
            if (AgrupaTamanho == false)
            {
                SQL.AppendLine("P.Descricao AS ProdutoDescricao, ");
            }
            else
            {
                SQL.AppendLine("CONCAT(P.Descricao,' - Tamanho: ',t.Abreviatura) AS ProdutoDescricao, ");
            }
            SQL.AppendLine(" I.Preco as Preco ");           
            SQL.AppendLine("FROM 	itenspedidovenda I");
            SQL.AppendLine("INNER JOIN produtos P ON P.Id = I.ProdutoId	");
            if (AgrupaTamanho == true)
            {
                SQL.AppendLine("INNER JOIN tamanhos T ON T.Id = I.TamanhoId ");
            }
            SQL.AppendLine("WHERE	I.PedidoVendaId = ");
            SQL.Append(pedidoId);
            if(AgrupaTamanho == false)
            {
                SQL.Append(" GROUP BY I.ProdutoId");
            }
            else
            {
                SQL.Append(" GROUP BY I.ProdutoId,I.TamanhoId");
            }
            
            SQL.Append(" ORDER BY P.Referencia");

            var cn = new DapperConnection<ItemPedidoVendaView>();
            return cn.ExecuteStringSqlToList(new ItemPedidoVendaView(), SQL.ToString());
        }

        public IEnumerable<ItemPedidoVendaView> GetItensConferenciaByBusca(int almoxarifadoId, List<int> produtosIds)
        {
            var SQL = new StringBuilder();

            SQL.AppendLine(" SELECT itenspedidovenda.*, ");
            SQL.AppendLine("    pedidovenda.referencia as RefPedido, produtos.Referencia as ProdutoReferencia, cores.Descricao as CorDescricao, tamanhos.Descricao as TamanhoDescricao ");
            SQL.AppendLine(" FROM itenspedidovenda ");
            SQL.AppendLine("    INNER JOIN itensliberacaopedidovenda on itenspedidovenda.id = itensliberacaopedidovenda.ItemPedidoVendaID ");
            SQL.AppendLine("    INNER JOIN pedidovenda on pedidovenda.id = itenspedidovenda.pedidovendaid ");
            SQL.AppendLine("    INNER JOIN produtos on produtos.id = itenspedidovenda.ProdutoId ");
            SQL.AppendLine("    LEFT JOIN cores on itenspedidovenda.CorId = cores.id ");
            SQL.AppendLine("    LEFT JOIN tamanhos on itenspedidovenda.TamanhoId = tamanhos.id ");
            SQL.AppendLine(" WHERE itensliberacaopedidovenda.QtdConferencia > 0 AND itensliberacaopedidovenda.QtdConferencia <> itensliberacaopedidovenda.QtdConferida ");
            SQL.AppendLine("    AND pedidovenda.status = 7 AND pedidovenda.AlmoxarifadoId = " + almoxarifadoId);
            SQL.AppendLine("    AND itenspedidovenda.ProdutoId IN (" + string.Join(",", produtosIds.ToArray()) + ")");
            SQL.AppendLine(" ORDER BY itenspedidovenda.produtoid, itenspedidovenda.corid, itenspedidovenda.tamanhoid ");

            var cn = new DapperConnection<ItemPedidoVendaView>();
            return cn.ExecuteStringSqlToList(new ItemPedidoVendaView(), SQL.ToString());
        }

        public IEnumerable<RelPedidoPorCliente> PedidoPorCliente(FiltroRelPedidoCliente filtro)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT	P.Id as ProdutoId, P.Descricao as DescProduto, P.Referencia as RefProduto, C.Abreviatura AS CorProduto,PV.DataEmissao, P.referenciacli AS RefProdutoCliente, PD.referenciagradecli AS RefGradeCliente,");
            SQL.AppendLine("T.Abreviatura AS TamanhoProduto, IFNULL(T.Id, 0) as TamanhoId, PV.CodigoPedidoCliente, I.ReferenciaPedidoCliente, I.SeqPedCliente, I.Preco,");
            SQL.AppendLine(" PV.ClienteId, CLI.RazaoSocial AS RazaoCliente, CLI.CNPJCPF, CLI.InscEstadual, CONCAT(CLI.Endereco, CLI.Numero) AS Endereco, CLI.CEP, CLI.DDD, CLI.Telefone, CLI.Bairro, CLI.IdEstado, CLI.IdMunicipio, CLI.obscliente, ");            
            SQL.AppendLine(" PV.id AS PedidoId, PV.PrevisaoEntrega AS DataEntrega, PV.Status, PV.RotaId, PV.Obs,PV.DescPercent,PV.DescValor, ");
            SQL.AppendLine(" IF(pv.TipoFrete = 0, ' ', IF(pv.TipoFrete = 1, 'CIF', 'FOB')) as TipoFrete, ");
            SQL.AppendLine(" cat.descricao as Catalogo,col.descricao as Colecao,segmentos.descricao as segmento, ");




            SQL.AppendLine("SUM(I.Qtd) AS Quantidade, SUM(IL.QtdEmpenhada) AS Empenhado, SUM(IL.QtdFaturada) AS QuantidadeAtendida, SUM(IF(PV.Status = 4,I.Qtd - IFNULL(IL.QtdFaturada,0), 0)) AS SaldoCancelado, PV.Referencia As Pedido");
            SQL.AppendLine("FROM 	ItensPedidoVenda I");
            SQL.AppendLine("INNER JOIN PedidoVenda PV ON PV.Id = I.PedidoVendaId");
            SQL.AppendLine("INNER JOIN Produtos P ON P.Id = I.ProdutoId");
            SQL.AppendLine(" INNER JOIN catalogo cat on cat.id = P.IdCatalogo ");
            SQL.AppendLine(" INNER JOIN colecoes col on col.id = P.Idcolecao ");
            SQL.AppendLine(" LEFT JOIN segmentos on segmentos.id = P.Idsegmento ");
            SQL.AppendLine("INNER JOIN colaboradores CLI ON CLI.Id = PV.ClienteId");
            SQL.AppendLine("INNER JOIN almoxarifados ALM ON ALM.Id = PV.AlmoxarifadoId");
            SQL.AppendLine("LEFT JOIN Tamanhos T ON T.Id = I.TamanhoId");
            SQL.AppendLine("LEFT JOIN Cores C ON C.Id = I.CorId");
            SQL.AppendLine("LEFT JOIN ProdutoDetalhes PD ON C.Id = PD.IdCor AND T.id = PD.IdTamanho AND P.Id = PD.IdProduto");
            SQL.AppendLine("LEFT JOIN ItensLiberacaoPedidoVenda IL ON I.Id = IL.ItemPedidoVendaId");
            SQL.AppendLine("WHERE " + FiltroEmpresa("PV.EmpresaId"));

            if (filtro.pedidosIds != null && filtro.pedidosIds.Count() > 0)
                SQL.AppendLine(" AND I.PedidoVendaId in (" + string.Join(",", filtro.pedidosIds.ToArray()) + ")");

           
           SQL.AppendLine(" AND PV.ClienteId = " + filtro.clientesId);
            SQL.AppendLine(" AND PV.AlmoxarifadoId = " + filtro.idAlmoxarifado);


            if (filtro.corIds != null && filtro.corIds.Count() > 0)
                SQL.AppendLine(" AND I.CorId in (" + string.Join(",", filtro.corIds.ToArray()) + ")");

            if (filtro.tamanhosIds != null && filtro.tamanhosIds.Count() > 0)
                SQL.AppendLine(" AND I.TamanhoId in (" + string.Join(",", filtro.tamanhosIds.ToArray()) + ")");


            if (filtro.colecoesIds != null && filtro.colecoesIds.Count() > 0)
                SQL.AppendLine(" AND P.idColecao in (" + string.Join(",", filtro.colecoesIds.ToArray()) + ")");

            if (filtro.catalogosIds != null && filtro.catalogosIds.Count() > 0)
                SQL.AppendLine(" AND P.IdCatalogo in (" + string.Join(",", filtro.catalogosIds.ToArray()) + ")");           


            if (filtro.DaEmissao != null && filtro.AteEmissao != null && filtro.AteEmissao.ToString("yyyy-MM-dd") != "0001-01-01")
                SQL.AppendLine(" AND Date(PV.DataEmissao) between '" + filtro.DaEmissao.ToString("yyyy-MM-dd") + "' AND '" + filtro.AteEmissao.ToString("yyyy-MM-dd") + "'");

            SQL.AppendFormat(" GROUP BY P.Id, I.CorId, I.TamanhoId");

            /*
            switch (filtro.ExibirPedidos)
            {
                case 0:
                    break;
                case 1:
                    SQL.AppendLine(" AND (PV.Status = 2 OR PV.Status = 3) ");
                    break;
                case 2:
                    SQL.AppendLine(" AND PV.Status = 4 ");
                    break;
                default:
                    SQL.AppendLine(" AND (PV.Status <> 2 AND PV.Status <> 3 AND PV.Status <> 4 ) ");
                    break;
            }
            */

            

            SQL.AppendLine(" ORDER BY P.IdCatalogo,P.idColecao, P.Referencia, C.Abreviatura, T.Id");

            var cn = new DapperConnection<RelPedidoPorCliente>();
            return cn.ExecuteStringSqlToList(new RelPedidoPorCliente(), SQL.ToString());
        }

        public IEnumerable<RelVendaCerta> VendaCerta(FiltroRelVendaCerta filtro)
        {
            List<RelVendaCerta> ItensNaoCompradoCliente = new List<RelVendaCerta>();

            var SQL = new StringBuilder();
            string SqlColecao = String.Empty;
            string SqlComprasCliente = String.Empty;

            SqlColecao = " SELECT produtos.Id as IdProduto, produtos.Referencia as RefProduto,produtos.Descricao as DescProduto,produtodetalhes.Idcor as Idcor, " +
                         "   cores.Abreviatura as AbvCor,cores.Descricao as DescCor,segmentos.descricao as segmento,colecoes.descricao as colecao,catalogo.descricao as catalogo " +
                         "   FROM produtos " +
                         "   INNER JOIN produtodetalhes ON produtodetalhes.IdProduto = produtos.Id " +
                         "   INNER JOIN cores ON cores.Id = produtodetalhes.Idcor " +
                         "   INNER JOIN tamanhos on tamanhos.Id = produtodetalhes.IdTamanho " +
                         "   INNER JOIN catalogo on catalogo.Id = produtos.IdCatalogo " +
                         "   LEFT JOIN colecoes on colecoes.Id = produtos.Idcolecao " +
                         "   LEFT JOIN segmentos on segmentos.Id = produtos.Idsegmento " +
                         "   WHERE IFNULL(produtos.IdCatalogo,0) = " + filtro.catalogosId + " AND produtos.ativo = 1  AND produtodetalhes.Inutilizado = 0 " +
                         "   Group By produtodetalhes.IdProduto, produtodetalhes.Idcor " ;
            var cnCatalogo = new DapperConnection<ItensVendaCerta>();
            var DadosCatalogo = cnCatalogo.ExecuteStringSqlToList(new ItensVendaCerta(), SqlColecao);


            string IdClientes = string.Join(",", filtro.clientesIds.ToArray());

            string[] NovosIdsCLientes = IdClientes.ToString().Split(',');
            foreach (var itensCatalogo in DadosCatalogo)
            {

                foreach (var itemClientesSelecao in NovosIdsCLientes)
                {
                    int IdCliente = Convert.ToInt32(itemClientesSelecao);

                    var DadosCLi = new ColaboradorService().GetServiceFactory().GetById(IdCliente);

              

                    SqlComprasCliente = " SELECT itenspedidovenda.ProdutoId as ProdutoId,itenspedidovenda.CorId as CorId from pedidovenda " +
                                        " INNER JOIN itenspedidovenda ON itenspedidovenda.PedidoVendaId = pedidovenda.Id " +
                                        " WHERE pedidovenda.ClienteId = " + IdCliente + " AND itenspedidovenda.ProdutoId = " + itensCatalogo.IdProduto + " AND itenspedidovenda.CorId = " + itensCatalogo.Idcor + 
                                        " AND Date(pedidovenda.DataEmissao) between '" + filtro.DaEmissao.ToString("yyyy-MM-dd") + "' AND '" + filtro.AteEmissao.ToString("yyyy-MM-dd") + "'" +                                        
                                        " GROUP BY itenspedidovenda.ProdutoId,itenspedidovenda.CorId ";
                    var cnItensComprados = new DapperConnection<ItemPedidoVenda>();
                    var ItensComprados = cnItensComprados.ExecuteStringSqlToList(new ItemPedidoVenda(), SqlComprasCliente);

                    if(ItensComprados == null || ItensComprados.Count() <= 0)
                    {
                        var ItemSemCompra = new RelVendaCerta();
                        ItemSemCompra.catalogo = itensCatalogo.catalogo;
                        ItemSemCompra.ClienteId = IdCliente;
                        ItemSemCompra.RazaoCliente = DadosCLi.RazaoSocial;
                        ItemSemCompra.colecao = itensCatalogo.colecao;
                        ItemSemCompra.CorProduto = itensCatalogo.DescCor;
                        ItemSemCompra.ProdutoId = itensCatalogo.IdProduto;
                        ItemSemCompra.RefProduto = itensCatalogo.RefProduto;
                        ItemSemCompra.DescProduto = itensCatalogo.DescProduto;
                        ItemSemCompra.segmento = itensCatalogo.segmento;
                        ItensNaoCompradoCliente.Add(ItemSemCompra);
                    }
                }
                

            }                        
            return ItensNaoCompradoCliente;
        }

        public IEnumerable<RelVendaCerta> VendaCertaGrupos(FiltroRelVendaCerta filtro)
        {
            List<RelVendaCerta> ItensNaoCompradoCliente = new List<RelVendaCerta>();

            var SQL = new StringBuilder();
            string SqlColecao = String.Empty;
            var SqlComprasCliente = new StringBuilder();

            SqlColecao = " SELECT produtos.Id as IdProduto, produtos.Referencia as RefProduto,produtos.Descricao as DescProduto,produtodetalhes.Idcor as Idcor, " +
                         "   cores.Abreviatura as AbvCor,cores.Descricao as DescCor,segmentos.descricao as segmento,colecoes.descricao as colecao,catalogo.descricao as catalogo " +
                         "   FROM produtos " +
                         "   INNER JOIN produtodetalhes ON produtodetalhes.IdProduto = produtos.Id " +
                         "   INNER JOIN cores ON cores.Id = produtodetalhes.Idcor " +
                         "   INNER JOIN tamanhos on tamanhos.Id = produtodetalhes.IdTamanho " +
                         "   INNER JOIN catalogo on catalogo.Id = produtos.IdCatalogo " +
                         "   LEFT JOIN colecoes on colecoes.Id = produtos.Idcolecao " +
                         "   LEFT JOIN segmentos on segmentos.Id = produtos.Idsegmento " +
                         "   WHERE IFNULL(produtos.IdCatalogo,0) = " + filtro.catalogosId + " AND produtos.ativo = 1 AND produtodetalhes.Inutilizado = 0 " +
                         "   Group By produtodetalhes.IdProduto, produtodetalhes.Idcor ";
            var cnCatalogo = new DapperConnection<ItensVendaCerta>();
            var DadosCatalogo = cnCatalogo.ExecuteStringSqlToList(new ItensVendaCerta(), SqlColecao);


            string IdClientes = string.Join(",", filtro.clientesIds.ToArray());

            string[] NovosIdsCLientes = IdClientes.ToString().Split(',');
            foreach (var itensCatalogo in DadosCatalogo)
            {
                SqlComprasCliente = new StringBuilder();
                SqlComprasCliente.AppendLine(" SELECT itenspedidovenda.ProdutoId as ProdutoId,itenspedidovenda.CorId as CorId from pedidovenda ");
                SqlComprasCliente.AppendLine(" INNER JOIN itenspedidovenda ON itenspedidovenda.PedidoVendaId = pedidovenda.Id ");
                SqlComprasCliente.AppendLine(" WHERE pedidovenda.ClienteId IN (" + string.Join(",", filtro.clientesIds.ToArray()) + ")");
                SqlComprasCliente.AppendLine(" AND itenspedidovenda.ProdutoId = " + itensCatalogo.IdProduto + " AND itenspedidovenda.CorId = " + itensCatalogo.Idcor);
                SqlComprasCliente.AppendLine(" AND Date(pedidovenda.DataEmissao) between '" + filtro.DaEmissao.ToString("yyyy-MM-dd") + "' AND '" + filtro.AteEmissao.ToString("yyyy-MM-dd") + "'");
                SqlComprasCliente.AppendLine(" GROUP BY itenspedidovenda.ProdutoId,itenspedidovenda.CorId ");

                var cnItensComprados = new DapperConnection<ItemPedidoVenda>();
                var ItensComprados = cnItensComprados.ExecuteStringSqlToList(new ItemPedidoVenda(), SqlComprasCliente.ToString());

                if (ItensComprados == null || ItensComprados.Count() <= 0)
                {
                    var ItemSemCompra = new RelVendaCerta();
                    ItemSemCompra.catalogo = itensCatalogo.catalogo;
                    ItemSemCompra.ClienteId = 0;
                    ItemSemCompra.RazaoCliente = "";
                    ItemSemCompra.colecao = itensCatalogo.colecao;
                    ItemSemCompra.CorProduto = itensCatalogo.DescCor;
                    ItemSemCompra.ProdutoId = itensCatalogo.IdProduto;
                    ItemSemCompra.RefProduto = itensCatalogo.RefProduto;
                    ItemSemCompra.DescProduto = itensCatalogo.DescProduto;
                    ItemSemCompra.segmento = itensCatalogo.segmento;
                    ItensNaoCompradoCliente.Add(ItemSemCompra);
                }

            }
            return ItensNaoCompradoCliente;
        }

        public IEnumerable<Colaborador> ClienteVendaCertaGrupo(FiltroRelVendaCerta filtro)
        {
            var SQL = new StringBuilder();
            var cnClientes = new DapperConnection<Colaborador>();

            SQL.AppendLine("SELECT colaboradores.razaosocial from colaboradores  ");
            SQL.AppendLine(" WHERE colaboradores.Id IN (" + string.Join(",", filtro.clientesIds.ToArray()) + ")");

            
            return cnClientes.ExecuteStringSqlToList(new Colaborador(), SQL.ToString());
        }

    }
}
