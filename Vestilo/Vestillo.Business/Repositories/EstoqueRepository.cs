using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Connection;
using Vestillo.Lib;
using System.Data;


namespace Vestillo.Business.Repositories
{
    public class EstoqueRepository : GenericRepository<Estoque>
    {
        public class AcertoEstoque
        {
            public int EstoqueId { get; set; }
            public int MaterialId { get; set; }
            public int TamanhoId { get; set; }
            public int CorId { get; set; }
            public decimal Empenhado { get; set; }
            public decimal EmpenhoOrdem { get; set; }
        }

        public EstoqueRepository()
            : base(new DapperConnection<Estoque>())
        {
        }

        public IEnumerable<ConsultaEstoqueView> GetEstoque()
        {
            var SQL = new StringBuilder();

            SQL.AppendLine("SELECT	E.Id, P.Referencia AS ProdutoReferencia, P.Descricao AS ProdutoDescricao,CAT.descricao as CatalogoDescricao, COL.descricao as ColecaoDescricao ,E.Saldo as Saldo,E.Empenhado as Empenhado,");
            SQL.AppendLine("E.TamanhoId, T.Abreviatura AS TamanhoAbreviatura, E.CorId, C.Abreviatura AS CorAbreviatura, P.TipoItem as TipoItem, ");
            SQL.AppendLine("A.Descricao AS AlmoxarifadoDescricao,");
            SQL.AppendLine("U.Abreviatura AS UnidadeMedidaAbreviatura");
            SQL.AppendLine("FROM 	Estoque E");
            SQL.AppendLine("INNER JOIN Almoxarifados A ON A.Id = E.AlmoxarifadoId");
            SQL.AppendLine("INNER JOIN Produtos P ON P.Id = E.ProdutoId");
            SQL.AppendLine("LEFT JOIN Tamanhos T ON T.Id = E.TamanhoId");
            SQL.AppendLine("LEFT JOIN Cores C ON C.Id = E.CorId");
            SQL.AppendLine("LEFT JOIN UnidadeMedidas U ON U.Id = P.IdUniMedida");
            SQL.AppendLine("LEFT JOIN catalogo CAT ON CAT.Id = P.IdCatalogo");
            SQL.AppendLine("LEFT JOIN colecoes COL ON COL.Id = P.Idcolecao");

            SQL.AppendLine("WHERE " + FiltroEmpresa("A.IdEmpresa"));
            SQL.AppendLine(" ORDER BY P.Descricao,E.TamanhoId, E.CorId");

            var cn = new DapperConnection<ConsultaEstoqueView>();
            return cn.ExecuteStringSqlToList(new ConsultaEstoqueView(), SQL.ToString());
        }

        public IEnumerable<ConsultaEstoqueView> GetEstoqueTelaGrade(int IdProduto,int IdAlmoxarifado)
        {
            var SQL = new StringBuilder();

            SQL.AppendLine("SELECT	E.Id, P.Referencia AS ProdutoReferencia, P.Descricao AS ProdutoDescricao,CAT.descricao as CatalogoDescricao, COL.descricao as ColecaoDescricao ,E.Saldo as Saldo,E.Empenhado as Empenhado,");
            SQL.AppendLine("E.TamanhoId, T.Abreviatura AS TamanhoAbreviatura, E.CorId, C.Abreviatura AS CorAbreviatura, P.TipoItem as TipoItem, ");
            SQL.AppendLine("A.Descricao AS AlmoxarifadoDescricao,");
            SQL.AppendLine("U.Abreviatura AS UnidadeMedidaAbreviatura");
            SQL.AppendLine("FROM 	Estoque E");
            SQL.AppendLine("INNER JOIN Almoxarifados A ON A.Id = E.AlmoxarifadoId");
            SQL.AppendLine("INNER JOIN Produtos P ON P.Id = E.ProdutoId");
            SQL.AppendLine("LEFT JOIN Tamanhos T ON T.Id = E.TamanhoId");
            SQL.AppendLine("LEFT JOIN Cores C ON C.Id = E.CorId");
            SQL.AppendLine("LEFT JOIN UnidadeMedidas U ON U.Id = P.IdUniMedida");
            SQL.AppendLine("LEFT JOIN catalogo CAT ON CAT.Id = P.IdCatalogo");
            SQL.AppendLine("LEFT JOIN colecoes COL ON COL.Id = P.Idcolecao");

            SQL.AppendLine("WHERE " + FiltroEmpresa("A.IdEmpresa"));
            SQL.AppendLine(" AND E.AlmoxarifadoId =  " + IdAlmoxarifado + " AND E.ProdutoId = " + IdProduto);
            SQL.AppendLine(" ORDER BY P.Descricao,E.TamanhoId, E.CorId");

            var cn = new DapperConnection<ConsultaEstoqueView>();
            return cn.ExecuteStringSqlToList(new ConsultaEstoqueView(), SQL.ToString());
        }


        public IEnumerable<ConsultaEstoqueView> GetEstoqueRelatorio(FiltroEstoqueRelatorio filtro)
        {
            var SQL = new StringBuilder();

            SQL.AppendLine("SELECT	E.Id, P.Referencia AS ProdutoReferencia, P.Descricao AS ProdutoDescricao,CAT.descricao as CatalogoDescricao,  SUM(IFNULL(E.Saldo,0)) as Saldo, SUM(IFNULL(E.Empenhado,0)) as Empenhado,");
            SQL.AppendLine("E.TamanhoId, T.Abreviatura AS TamanhoAbreviatura, E.CorId, C.Abreviatura AS CorAbreviatura, CAT.descricao as Catalogo,");

            if (!filtro.SoEstoque)
            {
                SQL.AppendLine("SUM(IFNULL((SELECT SUM(IOP.Quantidade - IOP.QuantidadeProduzida) FROM ItensOrdemProducao IOP");
                SQL.AppendLine(" JOIN OrdemProducao OP ON OP.id = IOP.OrdemProducaoId ");
                SQL.AppendLine("WHERE " + FiltroEmpresa("OP.EmpresaId"));
                SQL.AppendLine(" AND IOP.ProdutoId = E.ProdutoId AND IOP.CorId = E.CorId AND IOP.TamanhoId = E.TamanhoId AND OP.Status <> 6 ");
                SQL.AppendLine("GROUP BY P.Descricao,E.TamanhoId, E.CorId),0)) AS EmProducao,");

                SQL.AppendLine("SUM(IFNULL((SELECT SUM(IPC.QTD - IPC.QTDATENDIDA) FROM ItensPedidoCompra IPC");
                SQL.AppendLine("INNER JOIN PedidoCompra PC ON PC.id = IPC.PedidoCompraId");
                SQL.AppendLine("WHERE " + FiltroEmpresa("PC.EmpresaId"));
                SQL.AppendLine(" AND IPC.ProdutoId = E.ProdutoId AND IPC.CorId = E.CorId AND IPC.TamanhoId = E.TamanhoId AND PC.Status <> 4");
                SQL.AppendLine("GROUP BY P.Descricao,E.TamanhoId, E.CorId),0)) AS PedidoCompra,");
            }

            SQL.AppendLine("A.Descricao AS AlmoxarifadoDescricao, P.TipoItem, U.Abreviatura AS UnidadeMedidaAbreviatura, CO.descricao as Colecao, GR.descricao as Grupo ");
            SQL.AppendLine("FROM Estoque E	");
            SQL.AppendLine("INNER JOIN Produtos P ON P.Id = E.ProdutoId");
            SQL.AppendLine("INNER JOIN Almoxarifados A ON A.Id = E.AlmoxarifadoId");
            SQL.AppendLine("LEFT JOIN UnidadeMedidas U ON U.Id = P.IdUniMedida");
            SQL.AppendLine("LEFT JOIN Tamanhos T ON T.Id = E.TamanhoId");
            SQL.AppendLine("LEFT JOIN Cores C ON C.Id = E.CorId");
            SQL.AppendLine("LEFT JOIN catalogo CAT ON CAT.Id = P.IdCatalogo");
            SQL.AppendLine("LEFT JOIN colecoes CO ON CO.Id = P.IdColecao");
            SQL.AppendLine("LEFT JOIN grupoprodutos GR ON GR.Id = P.IdGrupo");
            SQL.AppendLine("WHERE " + FiltroEmpresa("A.IdEmpresa"));

            if (filtro.Produtos != null && filtro.Produtos.Count() > 0)
                SQL.AppendLine(" AND P.Id in (" + string.Join(",", filtro.Produtos.ToArray()) + ")");

            if (filtro.Almoxarifado != null && filtro.Almoxarifado.Count() > 0)
                SQL.AppendLine(" AND E.AlmoxarifadoId in (" + string.Join(",", filtro.Almoxarifado.ToArray()) + ")");

            if (filtro.Cor != null && filtro.Cor.Count() > 0)
                SQL.AppendLine(" AND E.CorId in (" + string.Join(",", filtro.Cor.ToArray()) + ")");

            if (filtro.Tamanho != null && filtro.Tamanho.Count() > 0)
                SQL.AppendLine(" AND E.TamanhoId in (" + string.Join(",", filtro.Tamanho.ToArray()) + ")");


            if (filtro.Colecao != null && filtro.Colecao.Count() > 0)
                SQL.AppendLine(" AND P.idColecao in (" + string.Join(",", filtro.Colecao.ToArray()) + ")");


            if (filtro.Grupo != null && filtro.Grupo.Count() > 0)
                SQL.AppendLine(" AND P.idGrupo in (" + string.Join(",", filtro.Grupo.ToArray()) + ")");


            if (filtro.Catalogo != null && filtro.Catalogo.Count() > 0)
                SQL.AppendLine(" AND P.idCatalogo in (" + string.Join(",", filtro.Catalogo.ToArray()) + ")");

            switch (filtro.Tipo)
            {
                case 1:
                    SQL.AppendLine(" AND P.TipoItem <> 1");
                    break;
                case 2:
                    SQL.AppendLine(" AND P.TipoItem <> 0");
                    break;
                default:
                    break;
            }
            if (filtro.DiferenteZero)
            {
                SQL.AppendLine(" AND E.Saldo > 0");
            }

            switch (filtro.Agrupar)
            {
                case 0:
                    SQL.AppendLine(" GROUP BY P.Id");
                    break;
                case 1:
                    SQL.AppendLine("GROUP BY P.Id, E.CorId");
                    break;
                case 2:
                    SQL.AppendLine("GROUP BY P.Id, E.TamanhoId");
                    break;
                default:
                    SQL.AppendLine(" GROUP BY P.Id, E.TamanhoId, E.CorId, E.AlmoxarifadoId");
                    break;
            }

            SQL.AppendLine(" ORDER BY P.Descricao, E.TamanhoId, E.CorId"); 

            var cn = new DapperConnection<ConsultaEstoqueView>();
            return cn.ExecuteStringSqlToList(new ConsultaEstoqueView(), SQL.ToString());
        }

        public Estoque GetEstoque(int almoxerifadoId, int produtoId, int corId, int tamanhoId, bool ProcuraPorEmpresa)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT	E.* ");
            SQL.AppendLine("FROM 	Estoque E");
            SQL.AppendLine("INNER JOIN Produtos P ON P.Id = E.ProdutoId");
            SQL.AppendLine("INNER JOIN Almoxarifados A ON A.Id = E.AlmoxarifadoId");
            SQL.AppendLine("LEFT JOIN Tamanhos T ON T.Id = E.TamanhoId");
            SQL.AppendLine("LEFT JOIN Cores C ON C.Id = E.CorId");
            SQL.AppendLine("WHERE ");
            if (ProcuraPorEmpresa)
            {
                SQL.AppendLine(" (A.IdEmpresa = " + VestilloSession.EmpresaLogada.Id  + " OR ISNULL(A.IdEmpresa) ) AND ");
            }
            
            SQL.AppendLine(" E.AlmoxarifadoId = " + almoxerifadoId.ToString());
            SQL.AppendLine("AND E.ProdutoId = " + produtoId.ToString());

            if (corId > 0)
            {
                SQL.AppendLine("AND E.CorId = " + corId.ToString());
            }
            else
            {
                SQL.AppendLine("AND E.CorId IS NULL");
            }

            if (tamanhoId > 0)
            {
                SQL.AppendLine("AND E.TamanhoId = " + tamanhoId.ToString());
            }
            else
            {
                SQL.AppendLine("AND E.TamanhoId IS NULL");
            }
          

            
            var ret = new Estoque();
            _cn.ExecuteToModel(ref ret, SQL.ToString());
            return ret;
        }

        public IEnumerable<ConsultaEstoqueProdutoroduzidoView> GetEstoque(FichaEstoqueProdutoProduzido filtro)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT	E.Id, P.Id AS ProdutoId,P.Referencia AS ProdutoReferencia, P.Descricao AS ProdutoDescricao, E.Saldo as Disponivel,E.Empenhado as Empenhado,");
            SQL.AppendLine("E.TamanhoId, T.Abreviatura AS TamanhoAbreviatura, E.CorId, C.Abreviatura AS CorAbreviatura,PD.Inutilizado as Inutilizado,");
            SQL.AppendLine("A.Abreviatura AS AlmoxarifadoReferencia, (E.Saldo + E.Empenhado) AS Fisico,");
            //SQL.AppendLine("SUM(IFNULL(IOP.Quantidade,0)) As QuantidadeProducao, (SUM(IFNULL(IOP.Quantidade,0)) - SUM(IFNULL(IOP.Quantidade,0))) As QuantidadeProduzida,");
            SQL.AppendLine("Cl.Descricao As Colecao, G.Descricao As Grupo, A.id As AlmoxarifadoId");
            SQL.AppendLine("FROM 	Estoque E");
            SQL.AppendLine("INNER JOIN Almoxarifados A ON A.Id = E.AlmoxarifadoId");
            SQL.AppendLine("INNER JOIN Produtos P ON P.Id = E.ProdutoId");
            SQL.AppendLine("INNER JOIN produtodetalhes PD ON PD.IdProduto = E.ProdutoId AND PD.IdTamanho = E.TamanhoId AND PD.Idcor = E.CorId");
            SQL.AppendLine("LEFT JOIN Tamanhos T ON T.Id = E.TamanhoId");
            SQL.AppendLine("LEFT JOIN Cores C ON C.Id = E.CorId");
            SQL.AppendLine("LEFT JOIN GrupoProdutos G ON G.Id = P.idGrupo");
            SQL.AppendLine("LEFT JOIN Colecoes Cl ON CL.Id = P.idColecao");
            //SQL.AppendLine("LEFT JOIN itensordemproducao IOP ON IOP.CorId = E.CorId AND IOP.TamanhoId = E.TamanhoId AND IOP.ProdutoId = E.ProdutoId");
            SQL.AppendLine("WHERE " + FiltroEmpresa("A.IdEmpresa"));
            SQL.AppendLine(" AND P.TipoItem <> 1"); 

            if (filtro.Produtos != null && filtro.Produtos.Count() > 0)
                SQL.AppendLine(" AND E.ProdutoId in (" + string.Join(",", filtro.Produtos.ToArray()) + ")");

            if (filtro.Almoxarifado != null && filtro.Almoxarifado.Count() > 0)
                SQL.AppendLine(" AND E.AlmoxarifadoId in (" + string.Join(",", filtro.Almoxarifado.ToArray()) + ")");

            if (filtro.Cor != null && filtro.Cor.Count() > 0)
                SQL.AppendLine(" AND E.CorId in (" + string.Join(",", filtro.Cor.ToArray()) + ")");

            if (filtro.Tamanho != null && filtro.Tamanho.Count() > 0)
                SQL.AppendLine(" AND E.TamanhoId in (" + string.Join(",", filtro.Tamanho.ToArray()) + ")");

            if (filtro.Segmento != null && filtro.Segmento.Count() > 0)
                SQL.AppendLine(" AND P.idSegmento in (" + string.Join(",", filtro.Segmento.ToArray()) + ")");

            if (filtro.Colecao != null && filtro.Colecao.Count() > 0)
                SQL.AppendLine(" AND P.idColecao in (" + string.Join(",", filtro.Colecao.ToArray()) + ")");

            if (filtro.Grupo != null && filtro.Grupo.Count() > 0)
                SQL.AppendLine(" AND P.idGrupo in (" + string.Join(",", filtro.Grupo.ToArray()) + ")");

            if (filtro.DoAno != null && filtro.DoAno != "")
                SQL.AppendLine(" AND P.ano >= " + filtro.DoAno + "");

            if (filtro.AteAno != null && filtro.AteAno != "")
                SQL.AppendLine(" AND P.ano <= " + filtro.AteAno + "");
            //SQL.AppendLine(" GROUP BY P.Id,E.CorId, E.TamanhoId, E.AlmoxarifadoId");
            SQL.AppendLine(" ORDER BY P.Descricao,E.CorId, E.TamanhoId");

            var cn = new DapperConnection<ConsultaEstoqueProdutoroduzidoView>();
            return cn.ExecuteStringSqlToList(new ConsultaEstoqueProdutoroduzidoView(), SQL.ToString());
        }

        public IEnumerable<ConsultaMovimentacaoEstoqueView> GetMovimentacaoEstoque(int estoqueId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	DataMovimento, Saida, Entrada, U.Nome AS UsuarioNome, ME.Observacao ");
            SQL.AppendLine("FROM 	MovimentacaoEstoque ME");
            SQL.AppendLine("INNER JOIN Usuarios U ON U.Id = ME.UsuarioId");
            SQL.AppendLine("WHERE EstoqueId = ");
            SQL.Append(estoqueId);

            var cn = new DapperConnection<ConsultaMovimentacaoEstoqueView>();
            return cn.ExecuteStringSqlToList(new ConsultaMovimentacaoEstoqueView(), SQL.ToString());
        }

        public ConsultaEstoqueView GetSaldoAtualProduto(int almoxerifadoId, int produtoId, int corId, int tamanhoId)
        {
            var SQL = new StringBuilder();

            SQL.AppendLine("SELECT	E.Id, P.Referencia AS ProdutoReferencia, P.Descricao AS ProdutoDescricao, E.Saldo,E.Empenhado as Empenhado,");
            SQL.AppendLine("E.TamanhoId, T.Abreviatura AS TamanhoAbreviatura, E.CorId, C.Abreviatura AS CorAbreviatura");
            SQL.AppendLine("FROM 	Estoque E");
            SQL.AppendLine("INNER JOIN Produtos P ON P.Id = E.ProdutoId");
            SQL.AppendLine("INNER JOIN Almoxarifados A ON A.Id = E.AlmoxarifadoId");
            SQL.AppendLine("LEFT JOIN Tamanhos T ON T.Id = E.TamanhoId");
            SQL.AppendLine("LEFT JOIN Cores C ON C.Id = E.CorId");
            SQL.AppendLine("WHERE " + FiltroEmpresa("A.IdEmpresa"));
            SQL.AppendLine("AND E.AlmoxarifadoId = " + almoxerifadoId.ToString());
            SQL.AppendLine("AND E.ProdutoId = " + produtoId.ToString());

            if (corId > 0)
            {
                SQL.AppendLine("AND E.CorId = " + corId.ToString());
            }
            else
            {
                SQL.AppendLine("AND E.CorId IS NULL");
            }

            if (tamanhoId > 0)
            {
                SQL.AppendLine("AND E.TamanhoId = " + tamanhoId.ToString());
            }
            else
            {
                SQL.AppendLine("AND E.TamanhoId IS NULL");
            }

            var consulta = new ConsultaEstoqueView();

            var cn = new DapperConnection<ConsultaEstoqueView>();
            cn.ExecuteToModel(ref consulta, SQL.ToString());

            
            return consulta;
        }

        public ConsultaEstoqueView GetSaldoAtualProdutoGestaoCompra(int produtoId, int corId, int tamanhoId)
        {
            var SQL = new StringBuilder();

            SQL.AppendLine("SELECT	E.Id, P.Referencia AS ProdutoReferencia, P.Descricao AS ProdutoDescricao, SUM(E.Saldo) as Saldo,SUM(E.Empenhado) as Empenhado,");
            SQL.AppendLine("E.TamanhoId, T.Abreviatura AS TamanhoAbreviatura, E.CorId, C.Abreviatura AS CorAbreviatura");
            SQL.AppendLine("FROM 	Estoque E");
            SQL.AppendLine("INNER JOIN Produtos P ON P.Id = E.ProdutoId");            
            SQL.AppendLine("LEFT JOIN Tamanhos T ON T.Id = E.TamanhoId");
            SQL.AppendLine("LEFT JOIN Cores C ON C.Id = E.CorId");
            SQL.AppendLine("WHERE " + FiltroEmpresa("P.IdEmpresa"));            
            SQL.AppendLine("AND E.ProdutoId = " + produtoId.ToString());
            if (corId > 0)
            {
                SQL.AppendLine("AND E.CorId = " + corId.ToString());
            }
            else
            {
                SQL.AppendLine("AND E.CorId IS NULL");
            }

            if (tamanhoId > 0)
            {
                SQL.AppendLine("AND E.TamanhoId = " + tamanhoId.ToString());
            }
            else
            {
                SQL.AppendLine("AND E.TamanhoId IS NULL");
            }

            var consulta = new ConsultaEstoqueView();

            var cn = new DapperConnection<ConsultaEstoqueView>();
            cn.ExecuteToModel(ref consulta, SQL.ToString());


            return consulta;
        }

        public IEnumerable<ConsultaMovimentacaoEstoqueView> GetMovimentacaoEstoque(int almoxarifadoId, string codigoBarras, int produtoId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT	M.DataMovimento, U.Nome AS UsuarioNome,  E.Saldo, T.Descricao AS TamanhoDescricao, T.Abreviatura AS TamanhoAbreviacao, C.Descricao AS CorDescricao, C.Abreviatura AS CorAbreviacao,");
            SQL.AppendLine("	    M.Entrada, M.Saida, P.Referencia AS ProdutoReferencia, P.Descricao AS ProdutoDescricao,");
            SQL.AppendLine("        UM.abreviatura as UnidMedidaAbreviatura");
            SQL.AppendLine("FROM 	Estoque E");
            SQL.AppendLine("    INNER JOIN MovimentacaoEstoque M ON M.EstoqueId = E.Id");
            SQL.AppendLine("    INNER JOIN Usuarios U ON U.Id = M.UsuarioId");
            SQL.AppendLine("    INNER JOIN Almoxarifados A ON A.Id = E.AlmoxarifadoId");
            SQL.AppendLine("    INNER JOIN Produtos P ON P.Id = E.ProdutoId");
            SQL.AppendLine("    LEFT JOIN Tamanhos T ON T.Id = E.TamanhoId");
            SQL.AppendLine("    LEFT JOIN Cores C ON C.Id = E.CorId");
            SQL.AppendLine("    LEFT JOIN unidademedidas UM ON UM.id = P.IdUniMedida");
            SQL.AppendLine("    LEFT JOIN ProdutoDetalhes PD ON PD.IdProduto = E.ProdutoId AND PD.IdTamanho = E.TamanhoId AND PD.IdCor = E.CorId");
            SQL.AppendLine("WHERE " + FiltroEmpresa("A.IdEmpresa"));
            SQL.AppendLine("AND E.AlmoxarifadoId = " + almoxarifadoId.ToString());
            
            if (!string.IsNullOrEmpty(codigoBarras))
                SQL.AppendLine("AND (PD.CodBarras = '" + codigoBarras + "' OR P.CodBarrasUnico = '" + codigoBarras + "' OR P.Referencia = '" + codigoBarras + "')");
            else
                SQL.AppendLine("AND E.ProdutoId = " + produtoId.ToString());
            
            SQL.AppendLine("ORDER BY DataMovimento DESC");

            

            var cn = new DapperConnection<ConsultaMovimentacaoEstoqueView>();
            return cn.ExecuteStringSqlToList(new ConsultaMovimentacaoEstoqueView(), SQL.ToString());
        }

        public IEnumerable<ConsultaEstoqueMateriaPrima> GetEstoqueMateriaPrima(FichaEstoqueMateriaPrima filtro)
        {
            string Campo = String.Empty;

            if (VestilloSession.UsaDescricaoAlternativa)
            {
                Campo = "P.DescricaoAlternativa";                
            }
            else
            {
                Campo = "P.Descricao";
                
            }

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT	E.Id, P.Id AS ProdutoId,P.Referencia AS ProdutoReferencia, " + Campo +"  AS ProdutoDescricao, ");            
            SQL.AppendLine(" E.TamanhoId, T.Abreviatura AS TamanhoAbreviatura, E.CorId, C.Abreviatura AS CorAbreviatura, ");
            SQL.AppendLine(" A.Abreviatura AS AlmoxarifadoReferencia, ");
            SQL.AppendLine(" Cl.Descricao As Colecao, G.Descricao As Grupo, A.id As AlmoxarifadoId,");
            SQL.AppendLine(" P.NCM As NCM, U.Abreviatura As UM, ");
            SQL.AppendLine(" S.Descricao as Segmento,");

            if(filtro.Relatorio == 0)
            {
                SQL.AppendLine(" ifnull(SUM(E.Saldo),0) as Disponivel, ifnull(SUM(E.Empenhado), 0) as Empenhado, ");
                SQL.AppendLine(" ifnull(SUM(E.Saldo + E.Empenhado), 0) AS Fisico ");
            }
            else
            {
                SQL.AppendLine(" ifnull(E.Saldo, 0) as Disponivel, ifnull(E.Empenhado, 0) as Empenhado, ");
                SQL.AppendLine(" ifnull((E.Saldo + E.Empenhado), 0) AS Fisico ");
            }           

            //SQL.AppendLine("SUM(IFNULL(IOP.Quantidade,0)) As QuantidadeProducao, (SUM(IFNULL(IOP.Quantidade,0)) - SUM(IFNULL(IOP.Quantidade,0))) As QuantidadeProduzida,");            
            SQL.AppendLine("FROM 	Estoque E");
            SQL.AppendLine("INNER JOIN Almoxarifados A ON A.Id = E.AlmoxarifadoId");
            SQL.AppendLine("INNER JOIN Produtos P ON P.Id = E.ProdutoId");
            SQL.AppendLine("LEFT JOIN UnidadeMedidas U ON U.Id = P.IdUniMedida");
            SQL.AppendLine("LEFT JOIN Tamanhos T ON T.Id = E.TamanhoId");
            SQL.AppendLine("LEFT JOIN Cores C ON C.Id = E.CorId");
            SQL.AppendLine("LEFT JOIN GrupoProdutos G ON G.Id = P.idGrupo");
            SQL.AppendLine("LEFT JOIN Colecoes Cl ON CL.Id = P.idColecao");
            SQL.AppendLine("LEFT JOIN Segmentos s on s.id = p.idsegmento");

            //SQL.AppendLine("LEFT JOIN itensordemproducao IOP ON IOP.CorId = E.CorId AND IOP.TamanhoId = E.TamanhoId AND IOP.ProdutoId = E.ProdutoId");
            SQL.AppendLine("WHERE " + FiltroEmpresa("A.IdEmpresa"));

            SQL.AppendLine("AND (P.TipoItem = 1 OR P.TipoItem = 2) ");

            if (filtro.Produtos != null && filtro.Produtos.Count() > 0)
                SQL.AppendLine(" AND E.ProdutoId in (" + string.Join(",", filtro.Produtos.ToArray()) + ")");

            if (filtro.Almoxarifado != null && filtro.Almoxarifado.Count() > 0)
                SQL.AppendLine(" AND E.AlmoxarifadoId in (" + string.Join(",", filtro.Almoxarifado.ToArray()) + ")");

            if (filtro.Cor != null && filtro.Cor.Count() > 0)
                SQL.AppendLine(" AND E.CorId in (" + string.Join(",", filtro.Cor.ToArray()) + ")");

            if (filtro.Tamanho != null && filtro.Tamanho.Count() > 0)
                SQL.AppendLine(" AND E.TamanhoId in (" + string.Join(",", filtro.Tamanho.ToArray()) + ")");

            if (filtro.Segmento != null && filtro.Segmento.Count() > 0)
                SQL.AppendLine(" AND P.IdSegmento in (" + string.Join(",", filtro.Segmento.ToArray()) + ")");

            if (filtro.Relatorio == 0)
            {
                SQL.AppendLine(" GROUP BY E.ProdutoId ");
                if(filtro.OrdenarPor == 0)
                {
                    SQL.AppendLine(" ORDER BY P.Referencia");
                }
                else {
                    SQL.AppendLine(" ORDER BY P.Descricao");
                }
                
            }
            else
            {
                if (filtro.OrdenarPor == 0)
                {
                    SQL.AppendLine(" ORDER BY P.Referencia,E.CorId, E.TamanhoId");
                }
                else
                {
                    SQL.AppendLine(" ORDER BY P.Descricao,E.CorId, E.TamanhoId");
                }
               
            }

            var cn = new DapperConnection<ConsultaEstoqueMateriaPrima>();
            return cn.ExecuteStringSqlToList(new ConsultaEstoqueMateriaPrima(), SQL.ToString());
        }

         public ConsultaEstoqueView GetEmpenhoAtualProduto(int almoxerifadoId, int produtoId, int corId, int tamanhoId)
         {
             var SQL = new StringBuilder();

             SQL.AppendLine("SELECT e.Id, p.Referencia AS ProdutoReferencia, p.Descricao AS ProdutoDescricao, e.Saldo, e.Empenhado, (IFNULL(e.Empenhado, 0) - SUM(IFNULL(opm.quantidadeempenhada, 0))) AS EmpenhadoLiberado,");
             SQL.AppendLine("e.TamanhoId, t.Abreviatura AS TamanhoAbreviatura, e.CorId, c.Abreviatura AS CorAbreviatura");
             SQL.AppendLine("FROM 	Estoque e");
             SQL.AppendLine("INNER JOIN Produtos p ON p.Id = e.ProdutoId");
             SQL.AppendLine("INNER JOIN Almoxarifados A ON A.Id = E.AlmoxarifadoId");
             SQL.AppendLine("LEFT JOIN ordemproducaomateriais opm ON");
             SQL.AppendLine("opm.OrdemProducaoId = (Select opm2.OrdemProducaoId from ordemproducaomateriais opm2 ");
             SQL.AppendLine("INNER JOIN ordemproducao op ON (op.id = opm2.OrdemProducaoId) ");
             SQL.AppendLine("where opm2.MateriaPrimaId = e.ProdutoId AND opm2.CorId = e.CorId  ");
             SQL.AppendLine("AND opm2.TamanhoId = e.TamanhoId AND op.Status <> 6 AND opm.Id = opm2.Id AND opm2.ItemOrdemProducaoId > 0) ");
             SQL.AppendLine("LEFT JOIN Tamanhos t ON t.Id = e.TamanhoId");
             SQL.AppendLine("LEFT JOIN Cores c ON c.Id = e.CorId");
             //SQL.AppendLine("WHERE opm.MateriaPrimaId = e.ProdutoId AND opm.CorId = e.CorId AND opm.TamanhoId = e.TamanhoId AND op.Status <> 6");
             SQL.AppendLine("WHERE " + FiltroEmpresa("A.IdEmpresa"));
             SQL.AppendLine("AND e.almoxarifadoId = " + almoxerifadoId.ToString());
             SQL.AppendLine("AND e.ProdutoId = " + produtoId.ToString());

             if (corId > 0)
             {
                 SQL.AppendLine("AND e.CorId = " + corId.ToString());
             }
             else
             {
                 SQL.AppendLine("AND e.CorId IS NULL");
             }

             if (tamanhoId > 0)
             {
                 SQL.AppendLine("AND e.TamanhoId = " + tamanhoId.ToString());
             }
             else
             {
                 SQL.AppendLine("AND e.TamanhoId IS NULL");
             }

             SQL.AppendLine("GROUP BY e.ProdutoId, e.CorId, e.TamanhoId");
             var consulta = new ConsultaEstoqueView();

             var cn = new DapperConnection<ConsultaEstoqueView>();
             cn.ExecuteToModel(ref consulta, SQL.ToString());

             return consulta;
         }

        public IEnumerable<ConsultaEstoqueView> GetEstoqueByEmpresa(List<int> idEmpresas, int? tipoProduto)
        {
            var SQL = new StringBuilder();

            SQL.AppendLine("SELECT	E.Produtoid, E.Id, P.Referencia AS ProdutoReferencia, P.Descricao AS ProdutoDescricao,CAT.descricao as CatalogoDescricao, COL.descricao as ColecaoDescricao ,E.Saldo as Saldo,E.Empenhado as Empenhado,");
            SQL.AppendLine("E.TamanhoId, T.Abreviatura AS TamanhoAbreviatura, E.CorId, C.Abreviatura AS CorAbreviatura,");
            SQL.AppendLine("A.Descricao AS AlmoxarifadoDescricao, A.id as AlmoxarifadoId,");
            SQL.AppendLine("S.Descricao as Segmento, ");
            SQL.AppendLine("En.descricao as Entrega");            
            SQL.AppendLine("FROM 	Estoque E");
            SQL.AppendLine("INNER JOIN Almoxarifados A ON A.Id = E.AlmoxarifadoId");
            SQL.AppendLine("INNER JOIN Produtos P ON P.Id = E.ProdutoId");
            SQL.AppendLine("LEFT JOIN Tamanhos T ON T.Id = E.TamanhoId");
            SQL.AppendLine("LEFT JOIN Cores C ON C.Id = E.CorId");
            SQL.AppendLine("LEFT JOIN UnidadeMedidas U ON U.Id = P.IdUniMedida");
            SQL.AppendLine("LEFT JOIN catalogo CAT ON CAT.Id = P.IdCatalogo");
            SQL.AppendLine("LEFT JOIN colecoes COL ON COL.Id = P.Idcolecao");
            SQL.AppendLine("LEFT JOIN segmentos S on s.id = P.Idsegmento");
            SQL.AppendLine("LEFT JOIN entrega EN on En.Id = P.IdEmtrega");

            SQL.AppendLine(" WHERE P.ativo = 1");

            if (idEmpresas != null && idEmpresas.Count() > 0)
                SQL.AppendLine(" AND A.idempresa in (" + string.Join(",", idEmpresas.ToArray()) + ")");

            if (tipoProduto != null)
            {
                if (tipoProduto != 2)
                    SQL.AppendLine(" AND p.TipoItem = " + tipoProduto);
            }
                

            SQL.AppendLine(" ORDER BY P.Descricao,E.TamanhoId, E.CorId");

            var cn = new DapperConnection<ConsultaEstoqueView>();
            return cn.ExecuteStringSqlToList(new ConsultaEstoqueView(), SQL.ToString());
        }

        public IEnumerable<ConsultaEstoqueRelatorioView> GetConsultaEstoqueRelatorio(List<int> idEmpresas, int? tipoProduto, bool faturado, DateTime? daData, DateTime? ateData)
        {
            var sqlFaturado = new StringBuilder();
            sqlFaturado.AppendLine("select nfe.idAlmoxarifado as AlmoxarifadoId, nfeitens.iditem,nfeitens.idcor,nfeitens.idtamanho,SUM(nfeitens.quantidade - nfeitens.Qtddevolvida) as QuantidadeFaturada");
            sqlFaturado.AppendLine("FROM nfeitens ");
            sqlFaturado.AppendLine("INNER JOIN produtos ON produtos.id = nfeitens.iditem");
            sqlFaturado.AppendLine("INNER JOIN nfe on nfe.id = nfeitens.IdNfe");
            sqlFaturado.AppendLine("INNER JOIN tipomovimentacoes on tipomovimentacoes.id = nfeitens.IdTipoMov");
            sqlFaturado.AppendLine("WHERE IFNULL(NFE.StatusNota,0) <> 2  AND (NFE.Tipo = 0 OR NFE.Tipo = 7) AND tipomovimentacoes.atualizaestoque = 1");
            if (idEmpresas != null && idEmpresas.Count() > 0)
                sqlFaturado.AppendLine("AND NFE.Idempresa in (" + string.Join(",", idEmpresas.ToArray()) + ")");
            if (faturado)
            {
                if (daData != null && ateData != null)
                {
                    sqlFaturado.AppendLine("AND nfe.DataInclusao BETWEEN ' " + daData.GetValueOrDefault().Date.ToString("yyyy-MM-dd HH:mm:ss") + " ' AND ' " + ateData.GetValueOrDefault().Date.ToString("yyyy-MM-dd HH:mm:ss") + " ' ");
                }
            }
            sqlFaturado.AppendLine("GROUP BY nfeitens.iditem,nfeitens.idcor,nfeitens.idtamanho, nfe.idAlmoxarifado");

            DataTable dt1 = Vestillo.Core.Connection.VestilloConnection.ExecToDataTable(sqlFaturado.ToString());

            var sqlNaoAtendido = new StringBuilder();
            sqlNaoAtendido.AppendLine("SELECT  A.id as AlmoxarifadoId, IV.corid, iV.tamanhoid, IV.PRODUTOID, SUM(I.QtdNaoAtendida) AS NaoAtendido");
            sqlNaoAtendido.AppendLine(" FROM  itensliberacaopedidovenda I");
            sqlNaoAtendido.AppendLine("INNER JOIN itenspedidovenda IV on I.ITEMPEDIDOVENDAID = IV.ID");
            sqlNaoAtendido.AppendLine("INNER JOIN almoxarifados a on a.id = i.almoxarifadoid");
            if (idEmpresas != null && idEmpresas.Count() > 0)
                sqlNaoAtendido.AppendLine(" where A.idempresa in (" + string.Join(",", idEmpresas.ToArray()) + ")");
            sqlNaoAtendido.AppendLine(" group by IV.PRODUTOID, IV.corid, iV.tamanhoid, A.id ");

            DataTable dt2 = Vestillo.Core.Connection.VestilloConnection.ExecToDataTable(sqlNaoAtendido.ToString());

            var estoques = GetEstoqueByEmpresa(idEmpresas, tipoProduto).ToList();

            List<ConsultaEstoqueRelatorioView> consultaEstoque = null;
            consultaEstoque = new List<ConsultaEstoqueRelatorioView>();
            consultaEstoque.Clear();

            ConsultaEstoqueRelatorioView item = new ConsultaEstoqueRelatorioView();

            foreach (var estoque in estoques)
            {
                item = new ConsultaEstoqueRelatorioView();
                item.RefProduto = estoque.ProdutoReferencia;
                item.DescProduto = estoque.ProdutoDescricao;
                item.CorAbreviatura = estoque.CorAbreviatura;
                item.TamAbreviatura = estoque.TamanhoAbreviatura;
                item.Colecao = estoque.ColecaoDescricao;
                item.Segmento = estoque.Segmento;
                item.Catalogo = estoque.CatalogoDescricao;
                item.Entrega = estoque.Entrega;
                item.Saldo = estoque.Saldo;
                item.Empenhado = estoque.Empenhado;
                item.AlmoxarifadoDescricao = estoque.AlmoxarifadoDescricao;

                item.Faturado = 0;                
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    if(estoque.TamanhoId == dt1.Rows[i]["idtamanho"].ToInt() &&
                        estoque.CorId == dt1.Rows[i]["idcor"].ToInt() &&
                        estoque.Produtoid == dt1.Rows[i]["iditem"].ToInt() &&
                        estoque.AlmoxarifadoId == dt1.Rows[i]["AlmoxarifadoId"].ToInt())
                    {
                        item.Faturado += Convert.ToDecimal(dt1.Rows[i]["QuantidadeFaturada"]);
                    }
                }

                item.NaoAtendido = 0;

                for (int j = 0; j < dt2.Rows.Count; j++)
                {
                    if (estoque.TamanhoId == dt2.Rows[j]["tamanhoid"].ToInt() &&
                        estoque.CorId == dt2.Rows[j]["corid"].ToInt() &&
                        estoque.Produtoid == dt2.Rows[j]["PRODUTOID"].ToInt() &&
                        estoque.AlmoxarifadoId == dt2.Rows[j]["AlmoxarifadoId"].ToInt())
                    {
                        item.NaoAtendido += Convert.ToDecimal(dt2.Rows[j]["NaoAtendido"]);
                    }
                }

                consultaEstoque.Add(item);

            }
            

            return consultaEstoque;
        }


        public void DeletaMovEstoque(int IdProduto)
        {
            StringBuilder IdEstoque = new StringBuilder();
            List<int> IdsEstoque = new List<int>();
            string SQL = String.Empty;

            SQL = "SELECT  estoque.Id as id from estoque where estoque.ProdutoId = " + IdProduto;
           

            var crtEstoque = new Estoque();
            var dadosEstoque = _cn.ExecuteStringSqlToList(crtEstoque, SQL.ToString());                       
           
            if(dadosEstoque != null && dadosEstoque.Count() > 0)
            {
                foreach (var item in dadosEstoque)
                {
                    if (!IdsEstoque.Contains(item.Id))
                    {
                        IdEstoque.Append(item.Id + ",");
                    }
                    
                }

                if(IdEstoque.Length > 0)
                {
                    IdEstoque.Remove(IdEstoque.Length - 1, 1);
                }
                
                SQL = "DELETE from movimentacaoestoque where movimentacaoestoque.EstoqueId IN " + "(" + IdEstoque + ")";
                _cn.ExecuteNonQuery(SQL);

                SQL = "DELETE from estoque where estoque.id IN " + "(" + IdEstoque + ")";
                _cn.ExecuteNonQuery(SQL);
            }



        }


        public void AcertaEstoquePlanilha(int AlmoxarifadoId,int ProdutoId,int CorId,int TamanhoId, decimal SaldoAtual)
        {
            var data = DateTime.Now;
            string DataAtual = data.ToString("yyyy-MM-dd HH:mm:ss");


            string SQL = "UPDATE estoque set  Saldo = " + SaldoAtual.ToString().Replace(",",".") + ",DataAlteracao = " + "'" +  DataAtual + "'" + " WHERE AlmoxarifadoId = " + AlmoxarifadoId + " AND ProdutoId = " + ProdutoId +
                " AND CorId = " + CorId + " AND TamanhoId = " + TamanhoId;
            _cn.ExecuteNonQuery(SQL);
        }

        public MovimentacaoEstoque GetUltimaMovimentacaoByPacote(int idPacote)
        {
            var SQL = new StringBuilder();
            var consulta = new MovimentacaoEstoque();

            SQL.AppendLine("SELECT * FROM movimentacaoestoque WHERE IdPacote = " + idPacote + " ORDER BY DataMovimento DESC LIMIT 1");

            var cn = new DapperConnection<MovimentacaoEstoque>();
            cn.ExecuteToModel(ref consulta, SQL.ToString());

            return consulta;
        }

        public Estoque GetSaldoProduto(int produtoId, int corId, int tamanhoId, int idAlmoxarifado)
        {
            var SQL = new StringBuilder();

            SQL.AppendLine("SELECT	E.ProdutoId, SUM(E.Saldo) AS Saldo,SUM(E.Empenhado) as Empenhado,");
            SQL.AppendLine(" E.TamanhoId, E.CorId");
            SQL.AppendLine("FROM 	Estoque E");
            SQL.AppendLine("INNER JOIN Produtos P ON P.Id = E.ProdutoId");
            SQL.AppendLine("INNER JOIN Almoxarifados A ON A.Id = E.AlmoxarifadoId");
            SQL.AppendLine("LEFT JOIN Tamanhos T ON T.Id = E.TamanhoId");
            SQL.AppendLine("LEFT JOIN Cores C ON C.Id = E.CorId");
            SQL.AppendLine("WHERE " + FiltroEmpresa("A.IdEmpresa"));
            if(idAlmoxarifado > 0)
                SQL.AppendLine("AND E.AlmoxarifadoId = " + idAlmoxarifado.ToString());

            SQL.AppendLine("AND E.ProdutoId = " + produtoId.ToString());

            if (corId > 0)
            {
                SQL.AppendLine("AND E.CorId = " + corId.ToString());
            }
            else
            {
                SQL.AppendLine("AND E.CorId IS NULL");
            }

            if (tamanhoId > 0)
            {
                SQL.AppendLine("AND E.TamanhoId = " + tamanhoId.ToString());
            }
            else
            {
                SQL.AppendLine("AND E.TamanhoId IS NULL");
            }

            var consulta = new Estoque();

            var cn = new DapperConnection<Estoque>();
            cn.ExecuteToModel(ref consulta, SQL.ToString());

            return consulta;
            
        }

        public  void AcertaEmepnhoOrdemEstoque()
        {          
            string SQL = String.Empty;
            try
            {
                SQL = " select estoque.id as EstoqueId,estoque.ProdutoId as MaterialId,estoque.CorId as CorId,estoque.TamanhoId as TamanhoId, estoque.Empenhado, IFNULL(sum(ordemproducaomateriais.quantidadeempenhada) + sum(EmpenhoProducao),0) as EmpenhoOrdem from estoque " +
                  " left join ordemproducaomateriais on ordemproducaomateriais.materiaprimaid = estoque.ProdutoId and ordemproducaomateriais.corid = estoque.CorId and ordemproducaomateriais.tamanhoid = estoque.TamanhoId and ordemproducaomateriais.armazemid = estoque.AlmoxarifadoId " +
                  "  group by estoque.AlmoxarifadoId,estoque.ProdutoId, estoque.CorId, estoque.TamanhoId";


                var crtEstoque = new AcertoEstoque();
                var cn = new DapperConnection<AcertoEstoque>();
                var dadosEstoque = cn.ExecuteStringSqlToList(crtEstoque, SQL.ToString());

                var ItensDiferentes = dadosEstoque.Where(x => x.Empenhado != x.EmpenhoOrdem);

                foreach (var item in ItensDiferentes)
                {
                    SQL = "UPDATE estoque set estoque.empenhado = " + item.EmpenhoOrdem.ToString().Replace(",",".") + " WHERE estoque.id = " + item.EstoqueId + " AND estoque.ProdutoId = " + item.MaterialId + " AND estoque.CorId = " + item.CorId + " AND estoque.TamanhoId = " + item.TamanhoId;
                    _cn.ExecuteNonQuery(SQL);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            





        }

        /* public IEnumerable<ConsultaEstoqueRelatorioView> GetConsultaEstoqueRelatorio(List<int> idEmpresas)
         {
             var SQL = new StringBuilder();
             SQL.AppendLine("SELECT P.Referencia AS RefProduto,P.Descricao as DescProduto, C.Abreviatura as CorAbreviatura, T.abreviatura as TamAbreviatura, ");
             SQL.AppendLine("CO.descricao as Colecao, S.Descricao as Segmento, CA.descricao AS Catalogo, En.descricao as Entrega, IFNULL(SUM(E.Saldo), 0) as Saldo, ");
             SQL.AppendLine("E.Empenhado as Empenhado, IFNULL(SUM(I.QtdFaturada), 0) AS Faturado, IFNULL(SUM(I.QtdNaoAtendida), 0) AS NaoAtendido");
             SQL.AppendLine("FROM 	Estoque E");
             SQL.AppendLine("INNER JOIN produtos P on E.ProdutoId = P.ID");
             SQL.AppendLine("INNER JOIN cores C on C.id = E.CorId");
             SQL.AppendLine("INNER JOIN tamanhos T on T.id = E.TamanhoId"); 
             SQL.AppendLine("LEFT join almoxarifados A on A.id = E.AlmoxarifadoId");
             SQL.AppendLine("LEFT JOIN segmentos S on s.id = P.Idsegmento");
             SQL.AppendLine("LEFT JOIN catalogo CA on CA.id = P.IdCatalogo");
             SQL.AppendLine("LEFT JOIN entrega EN on En.Id = P.IdEmtrega");
             SQL.AppendLine("LEFT JOIN colecoes CO on CO.Id = P.Idcolecao");
             SQL.AppendLine("LEFT JOIN itenspedidovenda IV on IV.PRODUTOID = E.PRODUTOID AND IV.CorId = E.CorId AND IV.tamanhoId = E.TamanhoId");
             SQL.AppendLine("LEFT JOIN itensliberacaopedidovenda I on I.ITEMPEDIDOVENDAID = IV.ID");

             if (idEmpresas != null && idEmpresas.Count() > 0)
                 SQL.AppendLine(" WHERE A.idempresa in (" + string.Join(",", idEmpresas.ToArray()) + ")");

             SQL.AppendLine(" GROUP BY P.Id, C.Id, T.Id");

             //SQL.AppendLine(" ORDER BY P.Descricao,E.CorId, E.TamanhoId");

             var cn = new DapperConnection<ConsultaEstoqueRelatorioView>();
             return cn.ExecuteStringSqlToList(new ConsultaEstoqueRelatorioView(), SQL.ToString());
         }*/
    }
}
