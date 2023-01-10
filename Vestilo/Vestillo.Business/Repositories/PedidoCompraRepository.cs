using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;
using Vestillo.Lib;
using Vestillo.Business.Models.Views;
using System.Data;

namespace Vestillo.Business.Repositories
{
    public class PedidoCompraRepository: GenericRepository<PedidoCompra>
    {
        string modificaWhere = "";

        public PedidoCompraRepository()
            : base(new DapperConnection<PedidoCompra>())
        {
        }

        public PedidoCompraView GetByIdView(int id)
        {
            var cn = new DapperConnection<PedidoCompraView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	p.*,");
            SQL.AppendLine("Fornecedor.referencia as RefFornecedor,");
            SQL.AppendLine("Fornecedor.nome as NomeFornecedor,");
            //SQL.AppendLine("Fornecedor.Contato as Contato,");
            SQL.AppendLine("transportadora.referencia as RefTransportadora,");
            SQL.AppendLine("transportadora.nome as NomeTransportadora");
            SQL.AppendLine("FROM 	pedidoCompra p");
            SQL.AppendLine("INNER JOIN colaboradores Fornecedor ON Fornecedor.id = p.FornecedorId");
            SQL.AppendLine("LEFT JOIN colaboradores transportadora ON transportadora.id = p.TransportadoraId");
            SQL.AppendLine("WHERE p.Id = ");
            SQL.Append(id);
            
            PedidoCompraView ret = new PedidoCompraView();

            cn.ExecuteToModel(ref ret, SQL.ToString());

            if (ret != null)
            {
                var itemPedidoCompraRepository = new ItemPedidoCompraRepository();
                ret.Itens = itemPedidoCompraRepository.GetByPedido(ret.Id).ToList();
            }

            return ret;
        }

        public IEnumerable<PedidoCompraView> GetAllView()
        {
            var cn = new DapperConnection<PedidoCompraView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	p.*,IF( ISNULL((p.PrevisaoFornecedor)),'', CONCAT(MID(IFNULL(date(p.PrevisaoFornecedor),''),9,2),'/', MID(IFNULL(date(p.PrevisaoFornecedor),''),6,2),'/',MID(IFNULL(date(p.PrevisaoFornecedor),''),1,4))) as DataFornecedor,");
            SQL.AppendLine("Fornecedor.referencia as RefFornecedor,");
            SQL.AppendLine("Fornecedor.nome as NomeFornecedor,");
            SQL.AppendLine("transportadora.referencia as RefTransportadora,");
            SQL.AppendLine("transportadora.nome as NomeTransportadora, ");
            SQL.AppendLine(" IFNULL(SUM(i.Qtd * i.Preco), 0) AS Total, ");
            SQL.AppendLine(" IFNULL(SUM((i.Qtd - i.QtdAtendida) * i.Preco), 0) AS ValorFaltante, ");
            SQL.AppendLine(" IFNULL(( SELECT COUNT(CONTASPAGAR.id) AS Prazo FROM CONTASPAGAR ");
            SQL.AppendLine("        WHERE CONTASPAGAR.IdPedidoCompra = p.Id   ");
            SQL.AppendLine("        GROUP BY CONTASPAGAR.IdPedidoCompra ), 0) AS Prazo  ");
            SQL.AppendLine("FROM 	pedidoCompra p");
            SQL.AppendLine("INNER JOIN colaboradores Fornecedor ON Fornecedor.id = p.FornecedorId");
            SQL.AppendLine("LEFT JOIN colaboradores transportadora ON transportadora.id = p.TransportadoraId");
            SQL.AppendLine(" LEFT JOIN itenspedidocompra i ON i.pedidocompraid = p.id ");
            SQL.AppendLine("WHERE ");
            SQL.Append(FiltroEmpresa("", "p"));
            SQL.AppendLine(" GROUP BY p.id ");

            return cn.ExecuteStringSqlToList(new PedidoCompraView(), SQL.ToString());
        }



        public IEnumerable<PedidoCompraView> GetListPorReferencia(string referencia, string parametrosDaBusca)
        {
            if (parametrosDaBusca == "SOMENTE PARA FATURAR")
            {
                modificaWhere = " n.Status = 1 OR n.Status = 2  AND "; // Pedido Aberto ou faturado parcial
            }

            var cn = new DapperConnection<PedidoCompraView>();
            var p = new PedidoCompraView();
                      

            var SQL = new Select()
                .Campos("n.id as id, n.Status,n.referencia as Referencia, cli.nome as NomeFornecedor ")
                .From("pedidoCompra n")
                .LeftJoin("colaboradores cli", "cli.id = n.FornecedorId")
                .Where(modificaWhere + "n.referencia like '%" + referencia + "%' And  " + FiltroEmpresa("", "n"));


            return cn.ExecuteStringSqlToList(p, SQL.ToString());
        }

        public IEnumerable<PedidoCompraView> GetListPorFornecedor(string Fornecedor, string parametrosDaBusca)
        {
            if (parametrosDaBusca == "SOMENTE PARA FATURAR")
            {
                modificaWhere = " n.Status = 1 OR n.Status = 2  AND "; // Pedido Aberto ou faturado parcial
            }

            var cn = new DapperConnection<PedidoCompraView>();
            var p = new PedidoCompraView();


           
            var SQL = new Select()
                .Campos("n.id as id,n.referencia as Referencia, cli.nome as NomeFornecedor ")
                .From("pedidoCompra n")
                .InnerJoin("colaboradores cli", "cli.id = n.FornecedorId")
                .Where(modificaWhere + "cli.nome like '%" + Fornecedor + "%' And  " + FiltroEmpresa("", "n"));


            return cn.ExecuteStringSqlToList(p, SQL.ToString());
        }

        public void UpdateStatus(int pedidoCompraId, enumStatusPedidoCompra status)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("UPDATE pedidoCompra SET ");
            SQL.AppendLine("Status = ");
            SQL.Append((int)status);
            SQL.AppendLine(" WHERE id = ");
            SQL.Append(pedidoCompraId);

            _cn.ExecuteNonQuery(SQL.ToString());
        }


        public void UpdateOpsDoRelatorio(int pedidoCompraId, string RefOps)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("UPDATE pedidoCompra SET ");
            SQL.AppendLine("OrdensReferencia = ");
            SQL.Append("'" + RefOps + "'");
            SQL.AppendLine(" WHERE id = ");
            SQL.Append(pedidoCompraId);

            _cn.ExecuteNonQuery(SQL.ToString());
        }

        public List<int> GetSemanas()
        {
            var cn = new DapperConnection<int>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	pc.Semana");
            SQL.AppendLine("FROM 	pedidocompra pc");
            SQL.AppendLine("WHERE ");
            SQL.Append(FiltroEmpresa("", "pc"));
            SQL.AppendLine("AND pc.Status <> 6");
            SQL.AppendLine("GROUP BY pc.Semana DESC");
            SQL.AppendLine("ORDER BY pc.Semana");

            return cn.ExecuteStringSqlToList(new Int16(), SQL.ToString()).ToList();
        }

        public IEnumerable<ConsultaPedidoCompraRelatorio> GetPedidoParaRelatorio(FiltroPedidoCompra filtro)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT	P.Descricao as DescProduto, P.Referencia as RefProduto, C.Abreviatura AS CorProduto,PV.DataEmissao, P.referenciacli AS RefProdutoCliente,");
            SQL.AppendLine("T.Abreviatura AS TamanhoProduto, T.Id AS TamanhoId, PV.FornecedorId, I.Preco, U.Abreviatura AS UM, U2.Abreviatura AS UM2, PV.Obs,");
            SQL.AppendLine(" PV.id AS PedidoId, PV.PrevisaoEntrega AS DataEntrega, PV.Status, PV.Contato,PV.OrdensReferencia as Ordens, ");
            SQL.AppendLine(" LEFT(CO.RazaoSocial,50) AS RazaoSocial, CO.Referencia as RefFornecedor,CO.CnpjCpf AS CnpjCpf, CO.InscEstadual AS InscEstadual, CO.Endereco AS Endereco,");
            SQL.AppendLine(" CO.Numero AS Numero, CO.Cep AS Cep, CO.Ddd AS Ddd, CO.Telefone AS Telefone, CO.Bairro AS Bairro, CO.IdEstado AS IdEstado, CO.IdMunicipio AS IdMunicipio,");
            //if (filtro.Agrupar == "Pedido")
            //{
            //    SQL.AppendLine("SUM(I.Qtd * I.Preco) AS ValorTotal, SUM(IL.QtdFaturada * I.Preco) AS ValorEntregue, SUM((I.Qtd - IFNULL(IL.QtdFaturada, 0)) * I.Preco) AS ValorEntregar,");
            //}

            SQL.AppendLine(" IFNULL(( SELECT COUNT(CONTASPAGAR.id) AS Prazo FROM CONTASPAGAR ");
            SQL.AppendLine(" INNER JOIN pedidocompra ON CONTASPAGAR.IdPedidoCompra = pedidocompra.id ");
            SQL.AppendLine(" WHERE I.PedidoCompraId = pedidocompra.id   ");
            SQL.AppendLine(" GROUP BY pedidocompra.id ), 0) AS Prazo,  ");

            SQL.AppendLine("SUM(I.Qtd) AS Quantidade, SUM(I.QtdAtendida) AS QuantidadeAtendida, SUM(I.Qtd * I.Preco) AS ValorTotal, PV.Referencia As Pedido");
            SQL.AppendLine("FROM 	ItensPedidoCompra I");
            SQL.AppendLine("INNER JOIN PedidoCompra PV ON PV.Id = I.PedidoCompraId");
            SQL.AppendLine("INNER JOIN Produtos P ON P.Id = I.ProdutoId");
            SQL.AppendLine("INNER JOIN Colaboradores CO ON CO.Id = PV.FornecedorId");
            SQL.AppendLine("LEFT JOIN Cores C ON C.Id = I.CorId");
            SQL.AppendLine("LEFT JOIN Tamanhos T ON T.Id = I.TamanhoId");
            SQL.AppendLine("LEFT JOIN UnidadeMedidas U ON U.Id = P.IdUniMedida");
            SQL.AppendLine("LEFT JOIN UnidadeMedidas U2 ON U2.Id = P.IdSegUniMedida");
            SQL.AppendLine("WHERE " + FiltroEmpresa("P.IdEmpresa"));

            if (filtro.Pedidos != null && filtro.Pedidos.Count() > 0)
                SQL.AppendLine(" AND I.PedidoCompraId in (" + string.Join(",", filtro.Pedidos.ToArray()) + ")");

            if (filtro.Fornecedores != null && filtro.Fornecedores.Count() > 0)
                SQL.AppendLine(" AND PV.FornecedorId in (" + string.Join(",", filtro.Fornecedores.ToArray()) + ")");

            if (filtro.Produtos != null && filtro.Produtos.Count() > 0)
                SQL.AppendLine(" AND I.ProdutoId in (" + string.Join(",", filtro.Produtos.ToArray()) + ")");

            if (filtro.DaEmissao != null && filtro.AteEmissao != null && filtro.DaEmissao != "" && filtro.AteEmissao != "")
                SQL.AppendLine(" AND Date(PV.DataEmissao) between '" + filtro.DaEmissao + "' AND '" + filtro.AteEmissao + "'");

            if (filtro.DaPrev != null && filtro.AtePrev != null && filtro.DaPrev != "" && filtro.AtePrev != "")
                SQL.AppendLine(" AND Date(PV.PrevisaoEntrega) between '" + filtro.DaPrev + "' AND '" + filtro.AtePrev + "'");

            if (filtro.Contato != null && filtro.Contato != "")
            {
                SQL.AppendLine(" AND PV.Contato like '%" + filtro.Contato + "%' ");
            }

            if (filtro.NaoAtendido || filtro.AtendidoParcial || filtro.Atendido || filtro.Finalizado)
            {
                SQL.AppendLine(" AND (");

                if (filtro.NaoAtendido)
                    SQL.Append(" PV.Status IN (1)");

                if (filtro.NaoAtendido && filtro.AtendidoParcial)
                    SQL.Append(" OR PV.Status IN (2)");
                else if (filtro.AtendidoParcial)
                    SQL.Append("PV.Status IN (2)");

                if (filtro.Atendido && (filtro.NaoAtendido || filtro.AtendidoParcial))
                    SQL.Append(" OR PV.Status = 3 ");
                else if (filtro.Atendido) 
                    SQL.Append(" PV.Status = 3 ");

                if (filtro.Finalizado && (filtro.NaoAtendido || filtro.AtendidoParcial || filtro.Atendido))
                    SQL.Append(" OR PV.Status = 4 ");
                else if (filtro.Finalizado)
                    SQL.Append(" PV.Status = 4 ");

                SQL.Append(")");
            }

            if (filtro.Relatorio == 0)
            {
                SQL.AppendFormat(" GROUP BY P.Id, I.CorId, I.TamanhoId");
            }else if (filtro.AgruparItem)
            {
                SQL.AppendFormat(" GROUP BY PV.Id, P.Id");
            }
            else
            {
                SQL.AppendFormat(" GROUP BY PV.Id, P.Id, I.CorId, I.TamanhoId");
            }  

            if (VestilloSession.UsaOrdenacaoFixa)
            {   
                 SQL.AppendLine(" ORDER BY P.Referencia, C.Abreviatura, T.Abreviatura");
            }
            else
            {
                switch (filtro.Ordenar)
                {
                    case "produto":
                        SQL.AppendLine(" ORDER BY P.Referencia, P.Descricao, C.Id, T.Id");
                        break;
                    case "descricao":
                        SQL.AppendLine(" ORDER BY P.Descricao, P.Referencia, C.Id, T.Id");
                        break;
                    case "Pedido":
                        SQL.AppendLine(" ORDER BY PV.Referencia, P.Referencia, P.Descricao, C.Id, T.Id");
                        break;
                    case "Emissao":
                        SQL.AppendLine(" ORDER BY PV.DataEmissao, PV.Referencia, P.Referencia, P.Descricao, C.Id, T.Id");
                        break;
                    case "Fornecedor":
                        SQL.AppendLine(" ORDER BY CO.Referencia, PV.Referencia, P.Referencia, P.Descricao, C.Id, T.Id");
                        break;
                    case "Status":
                        SQL.AppendLine(" ORDER BY PV.Status, PV.Referencia, P.Referencia, P.Descricao, C.Id, T.Id");
                        break;
                    case "Prev":
                        SQL.AppendLine(" ORDER BY PV.PrevisaoEntrega, PV.Referencia, P.Referencia, P.Descricao, C.Id, T.Id");
                        break;
                }
            }


            

            var cn = new DapperConnection<ConsultaPedidoCompraRelatorio>();
            return cn.ExecuteStringSqlToList(new ConsultaPedidoCompraRelatorio(), SQL.ToString());
        }

        public decimal GetQuantidadeComprada(DateTime? Inicio, DateTime? Fim, int materiaPrimaId, int corId, int tamanhoId)
        {
            decimal quantidade = 0;

            string where = string.Empty;
            if (Inicio != null)
                where = " AND DATE(pedidocompra.PrevisaoEntrega) >= '" + Inicio.GetValueOrDefault().Date.ToString("yyyy-MM-dd") + "'";
            if(Fim != null)
                where = where + " AND DATE(pedidocompra.PrevisaoEntrega) <= '" + Fim.GetValueOrDefault().Date.ToString("yyyy-MM-dd") + "'";

            var SQL = new Select()
                .Campos("SUM(Qtd - IFNULL(QtdAtendida,0)) AS quantidade ")
                .From("itenspedidocompra")
                .InnerJoin("pedidocompra", "pedidocompra.Id = itenspedidocompra.PedidoCompraId")
                .Where("pedidocompra.Status <> 3 AND pedidocompra.Status <> 4 " + where)
                .Where("itenspedidocompra.ProdutoId = " + materiaPrimaId + " and itenspedidocompra.CorId = " + corId + " and itenspedidocompra.TamanhoId = " + tamanhoId)
                .GroupBy("itenspedidocompra.ProdutoId, itenspedidocompra.CorId, itenspedidocompra.TamanhoId");

            DataTable dt = _cn.ExecuteToDataTable(SQL.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                quantidade = decimal.Parse(dt.Rows[0][0].ToString());
                if (quantidade < 0) quantidade = 0;
            }

            return quantidade;
        }

      
    }
}
