using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;
using Vestillo.Lib;

namespace Vestillo.Business.Repositories
{
    public class ItemTabelaPrecoRepository: GenericRepository<ItemTabelaPreco>
    {
        public ItemTabelaPrecoRepository()
            : base(new DapperConnection<ItemTabelaPreco>())
        {
        
        }

        public  IEnumerable<ItemTabelaPrecoView> GetItensTabelaPrecoView(int tabelaPrecoId)
        {
            var SQL = new StringBuilder();

            SQL.AppendLine("SELECT	ITP.Id,	ITP.TabelaPrecoId, ITP.ProdutoId, ITP.CustoMedio, ITP.PrecoSugerido, ITP.PrecoVenda, ITP.Lucro, ");
            SQL.AppendLine("P.Descricao AS ProdutoDescricao, P.Referencia AS ProdutoReferencia, P.PrecoCompra AS ProdutoPrecoCompra, C.Descricao AS Catalogo, u.Descricao as UM, ");
            SQL.AppendLine("P.Ipi AS ProdutoIpi, P.Icms AS ProdutoIcms, P.Lucro AS ProdutoLucro, P.TipoCustoFornecedor AS ProdutoTipoCustoFornecedor, P.TipoCalculoPreco AS ProdutoTipoCalculoPreco ");
            SQL.AppendLine("FROM ItensTabelaPreco ITP");
            SQL.AppendLine("    INNER JOIN Produtos P ON P.Id  = ITP.ProdutoId");
            SQL.AppendLine("    LEFT JOIN Catalogo C ON C.Id  = P.IdCatalogo");
            SQL.AppendLine("    LEFT JOIN unidademedidas u ON u.Id  = P.IdUniMedida");
            SQL.AppendLine("WHERE ITP.TabelaPrecoId = ");
            SQL.Append(tabelaPrecoId);

            var cn = new DapperConnection<ItemTabelaPrecoView>();
            return cn.ExecuteStringSqlToList( new ItemTabelaPrecoView(), SQL.ToString());
        }

        public IEnumerable<ItemTabelaPrecoView> GetItensTabelaPrecoComGradeView(int tabelaPrecoId, int catalogoId)
        {
            var SQL = new StringBuilder();

            SQL.AppendLine("SELECT	ITP.Id,	ITP.TabelaPrecoId, ITP.ProdutoId, ITP.CustoMedio, ITP.PrecoSugerido, ITP.PrecoVenda, ITP.Lucro, ");
            SQL.AppendLine("P.Descricao AS ProdutoDescricao, P.Referencia AS ProdutoReferencia, P.PrecoCompra AS ProdutoPrecoCompra, ");
            SQL.AppendLine("P.Ipi AS ProdutoIpi, P.Icms AS ProdutoIcms, P.Lucro AS ProdutoLucro, P.TipoCustoFornecedor AS ProdutoTipoCustoFornecedor, P.TipoCalculoPreco AS ProdutoTipoCalculoPreco, ");
            SQL.AppendLine("C.Abreviatura as Cor, C.Id as CorId, I.imagem as Imagem, CO.Descricao AS Colecao");
            SQL.AppendLine("FROM ItensTabelaPreco ITP");
            SQL.AppendLine("    INNER JOIN Produtos P ON P.Id  = ITP.ProdutoId");
            SQL.AppendLine("    LEFT JOIN Estoque E ON P.Id  = E.ProdutoId");
            SQL.AppendLine("    LEFT JOIN Cores C ON C.Id = E.CorId");
            SQL.AppendLine("    LEFT JOIN Imagens I ON P.Id = I.IdProduto");
            SQL.AppendLine("    LEFT JOIN Colecoes CO ON CO.Id = P.IdColecao");
            SQL.AppendLine("WHERE ITP.TabelaPrecoId = ");
            SQL.Append(tabelaPrecoId);
            SQL.AppendLine(" AND P.IdCatalogo = ");
            SQL.Append(catalogoId);
            SQL.AppendLine(" AND (SELECT SUM(PDE.inutilizado) FROM ProdutoDetalhes PDE WHERE PDE.idCor = E.CorId AND PDE.IdProduto = ITP.ProdutoId, AND PDE.IdTamanho = E.TamanhoId  Group by PDE.IdProduto, PDE.idCor, PDE.idTamanho) = 0");
            SQL.AppendLine(" Group by P.id, C.id");
            SQL.AppendLine(" Order by P.IdColecao, P.id, C.Id");

            var cn = new DapperConnection<ItemTabelaPrecoView>();
            return cn.ExecuteStringSqlToList(new ItemTabelaPrecoView(), SQL.ToString());
        }


        public IEnumerable<ItemTabelaPrecoView> GetItensTabelaPrecoComGradeView(int tabelaPrecoId, List<int> catalogoIds, List<int> almoxarifadoIds)
        {
            var SQL = new StringBuilder();

            SQL.AppendLine("SELECT	ITP.Id,	ITP.TabelaPrecoId, ITP.ProdutoId, ITP.CustoMedio, ITP.PrecoSugerido, ITP.PrecoVenda, ITP.Lucro, ");
            SQL.AppendLine("P.Descricao AS ProdutoDescricao, P.Referencia AS ProdutoReferencia, P.PrecoCompra AS ProdutoPrecoCompra, ");
            SQL.AppendLine("P.Ipi AS ProdutoIpi, P.Icms AS ProdutoIcms, P.Lucro AS ProdutoLucro, P.TipoCustoFornecedor AS ProdutoTipoCustoFornecedor, P.TipoCalculoPreco AS ProdutoTipoCalculoPreco, ");
            SQL.AppendLine("C.Abreviatura as Cor, C.Id as CorId, I.imagem as Imagem, CO.Descricao AS Colecao");
            SQL.AppendLine("FROM ItensTabelaPreco ITP");
            SQL.AppendLine("    INNER JOIN Produtos P ON P.Id  = ITP.ProdutoId");
            SQL.AppendLine("    LEFT JOIN TABELASPRECO TP ON TP.ID = ITP.TABELAPRECOID");

            int empresaLogada = VestilloSession.EmpresaLogada.Id;

            SQL.AppendLine("    LEFT JOIN ALMOXARIFADOS A ON A.IDEMPRESA = " + empresaLogada);

            //SQL.AppendLine("    LEFT JOIN ALMOXARIFADOS A ON A.IDEMPRESA = TP.EMPRESAID");

            SQL.AppendLine("    LEFT JOIN Estoque E ON P.Id  = E.ProdutoId AND E.ALMOXARIFADOID = A.ID");
            SQL.AppendLine("    INNER JOIN Cores C ON C.Id = E.CorId");
            SQL.AppendLine("    LEFT JOIN Imagens I ON P.Id = I.IdProduto");
            SQL.AppendLine("    LEFT JOIN Colecoes CO ON CO.Id = P.IdColecao");
            SQL.AppendLine("WHERE ITP.TabelaPrecoId = ");
            SQL.Append(tabelaPrecoId);
            SQL.AppendLine(" AND " + FiltroEmpresa("TP.EMPRESAID"));
            SQL.AppendLine(" AND P.IdCatalogo");
            SQL.Append(" in ( " + string.Join(",", catalogoIds) + " )");
            SQL.AppendLine(" AND A.id");
            SQL.Append(" in ( " + string.Join(",", almoxarifadoIds) + " )"); //filtra por almoxarifado da empresa logada
            SQL.AppendLine(" AND (E.id IS NULL OR (SELECT SUM(PDE.inutilizado) FROM ProdutoDetalhes PDE WHERE PDE.idCor = E.CorId AND PDE.IdProduto = ITP.ProdutoId AND PDE.IdTamanho = E.TamanhoId  Group by PDE.IdProduto, PDE.idCor, PDE.idTamanho) = 0" +
                ") ");
            SQL.AppendLine(" Group by P.id, C.id");
            SQL.AppendLine(" Order by P.IdColecao, P.id, C.Id");

            var cn = new DapperConnection<ItemTabelaPrecoView>();
            return cn.ExecuteStringSqlToList(new ItemTabelaPrecoView(), SQL.ToString());
        }

        public IEnumerable<ItemTabelaPrecoView> GetItensTabelaPrecoComGradeEstoque(int tabelaPrecoId, List<int> catalogoIds, List<int> tamanhoIds, List<int> almoxarifadoIds)
        {
            var SQL = new StringBuilder();

            SQL.AppendLine("SELECT p.id as ProdutoId, p.Referencia AS ProdutoReferencia, p.Descricao AS ProdutoDescricao, col.Descricao AS Colecao, cat.Descricao AS Catalogo, ");
            SQL.AppendLine("        itp.PrecoVenda , t.Abreviatura AS Tamanho, c.Abreviatura AS Cor, c.id as CorId, t.Id as TamanhoId, i.imagem as Imagem, ");
            SQL.AppendLine("        SUM(IFNULL(e.Saldo, 0)) AS estoque ");
            SQL.AppendLine(" FROM itenstabelapreco itp ");
            SQL.AppendLine(" INNER JOIN produtos p ON p.id = itp.produtoid ");
            SQL.AppendLine(" INNER JOIN produtodetalhes pd ON pd.idproduto = p.id ");
            SQL.AppendLine(" LEFT JOIN cores c ON c.id = pd.idcor ");
            SQL.AppendLine(" LEFT JOIN tamanhos t ON t.id = pd.idtamanho ");
            SQL.AppendLine(" LEFT JOIN colecoes col ON col.id = p.idcolecao ");
            SQL.AppendLine(" LEFT JOIN catalogo cat ON cat.Id = p.idcatalogo ");
            SQL.AppendLine(" LEFT JOIN imagens i ON i.IdProduto = p.Id ");
            SQL.AppendLine(" LEFT JOIN estoque e ON e.produtoid = p.id AND c.id = e.corid AND t.id = e.tamanhoid AND e.AlmoxarifadoId IN ( " + string.Join(",", almoxarifadoIds) + " ) ");
            SQL.AppendLine(" WHERE itp.tabelaprecoid = ");
            SQL.Append(tabelaPrecoId);
            SQL.AppendLine(" AND p.idCatalogo IN ( " + string.Join(",", catalogoIds) + " ) AND p.ativo = 1 ");
            SQL.AppendLine(" AND pd.IdTamanho IN( " + string.Join(",", tamanhoIds) + " ) AND pd.inutilizado = 0 ");
            SQL.AppendLine(" GROUP BY p.id, c.id, t.id ");
            SQL.AppendLine(" Order by P.IdCatalogo, P.IdColecao, P.id, C.Id");

            var cn = new DapperConnection<ItemTabelaPrecoView>();
            return cn.ExecuteStringSqlToList(new ItemTabelaPrecoView(), SQL.ToString());
        }

        public IEnumerable<ItemTabelaPrecoView> GetItensTabelaPrecoComGradeOrdem(int tabelaPrecoId, List<int> catalogoIds, List<int> tamanhoIds, List<int> almoxarifadoIds)
        {
            int empresaLogada = VestilloSession.EmpresaLogada.Id;

            var SQL = new StringBuilder();

            SQL.AppendLine("SELECT p.Id as ProdutoId, p.Referencia AS ProdutoReferencia, p.Descricao AS ProdutoDescricao, col.Descricao AS Colecao, cat.Descricao AS Catalogo, ");
            SQL.AppendLine("        itp.PrecoVenda , t.Abreviatura AS Tamanho, c.Abreviatura AS Cor, c.id as CorId, t.id as TamanhoId, i.imagem as Imagem, ");
            SQL.AppendLine("        (SELECT SUM(IFNULL(e.saldo, 0)) FROM estoque e WHERE e.produtoid = p.id AND c.id = e.corid AND t.id = e.tamanhoid AND e.AlmoxarifadoId IN ( " + string.Join(",", almoxarifadoIds) + " )) ");
            SQL.AppendLine("            + SUM(IFNULL(iop.quantidade,0) - IFNULL(iop.quantidadeproduzida, 0)) AS estoque ");
            SQL.AppendLine(" FROM itenstabelapreco itp ");
            SQL.AppendLine(" INNER JOIN produtos p ON p.id = itp.produtoid ");
            SQL.AppendLine(" INNER JOIN produtodetalhes pd ON pd.idproduto = p.id ");
            SQL.AppendLine(" LEFT JOIN cores c ON c.id = pd.idcor ");
            SQL.AppendLine(" LEFT JOIN tamanhos t ON t.id = pd.idtamanho ");
            SQL.AppendLine(" LEFT JOIN colecoes col ON col.id = p.idcolecao ");
            SQL.AppendLine(" LEFT JOIN catalogo cat ON cat.Id = p.idcatalogo ");
            SQL.AppendLine(" LEFT JOIN imagens i ON i.IdProduto = p.Id ");
            SQL.AppendLine(" LEFT JOIN ordemproducao op ON op.almoxarifadoid IN ( " + string.Join(",", almoxarifadoIds) + " ) AND op.status <> 6 ");
            SQL.AppendLine(" LEFT JOIN itensordemproducao iop ON iop.produtoid = p.id AND c.id = iop.corid AND t.id = iop.tamanhoid AND iop.ordemproducaoid = op.id ");
            SQL.AppendLine(" WHERE itp.tabelaprecoid = ");
            SQL.Append(tabelaPrecoId);
            SQL.AppendLine(" AND p.idCatalogo IN ( " + string.Join(",", catalogoIds) + " ) AND p.ativo = 1 ");
            SQL.AppendLine(" AND pd.IdTamanho IN( " + string.Join(",", tamanhoIds) + " ) AND pd.inutilizado = 0 ");
            SQL.AppendLine(" GROUP BY p.id, c.id, t.id ");
            SQL.AppendLine(" Order by P.IdCatalogo, P.IdColecao, P.id, C.Id");

            var cn = new DapperConnection<ItemTabelaPrecoView>();
            var itensTabela =  cn.ExecuteStringSqlToList(new ItemTabelaPrecoView(), SQL.ToString()).ToList();

            itensTabela.ForEach(i =>
            {
                decimal qtdNaoAtendida = new ItemLiberacaoPedidoVendaRepository().GetQtdNaoAtendida(i.ProdutoId, i.CorId, i.TamanhoId, almoxarifadoIds);
                i.Estoque = i.Estoque - qtdNaoAtendida;

            });

            return itensTabela.AsEnumerable();
        }

        public IEnumerable<TamanhoView> GetItensTabelaPrecoTamanhos(int itemId, int corId, List<int> almoxarifadoIds)
        {
            var SQL = new StringBuilder();

            SQL.AppendLine("SELECT	T.*, E.Saldo AS Estoque ");
            SQL.AppendLine("FROM ItensTabelaPreco ITP");
            SQL.AppendLine("    INNER JOIN Produtos P ON P.Id  = ITP.ProdutoId");
            SQL.AppendLine("    LEFT JOIN TABELASPRECO TP ON TP.ID = ITP.TABELAPRECOID");


            int empresaLogada = VestilloSession.EmpresaLogada.Id;

            SQL.AppendLine("    LEFT JOIN ALMOXARIFADOS A ON A.IDEMPRESA = " + empresaLogada);

            //SQL.AppendLine("    LEFT JOIN ALMOXARIFADOS A ON A.IDEMPRESA = TP.EMPRESAID");

            SQL.AppendLine("    INNER JOIN Estoque E ON P.Id  = E.ProdutoId AND E.ALMOXARIFADOID = A.ID");
            SQL.AppendLine("    LEFT JOIN Cores C ON C.Id = E.CorId");
            SQL.AppendLine("    LEFT JOIN Tamanhos T ON T.Id = E.TamanhoId");
            SQL.AppendLine("WHERE ITP.Id = ");
            SQL.Append(itemId);
            SQL.AppendLine(" AND " + FiltroEmpresa("TP.EMPRESAID"));
            SQL.AppendLine(" AND C .Id = ");
            SQL.Append(corId);
            SQL.AppendLine(" AND A.id");
            SQL.Append(" in ( " + string.Join(",", almoxarifadoIds) + " )");
            SQL.AppendLine(" Order by T.id");

            var cn = new DapperConnection<TamanhoView>();
            return cn.ExecuteStringSqlToList(new TamanhoView(), SQL.ToString());
        }

        public IEnumerable<TamanhoView> GetItensTabelaPrecoTamanhosOrdem(int itemId, int corId, List<int> almoxarifadoIds)
        {
            int empresaLogada = VestilloSession.EmpresaLogada.Id;
            var SQL = new StringBuilder();

            SQL.AppendLine(" SELECT Id, Abreviatura, Descricao, Ativo, SUM(Estoque) AS Estoque FROM ( ");

            SQL.AppendLine("    SELECT	T.*, E.Saldo AS Estoque ");
            SQL.AppendLine("    FROM Produtos P ");
            SQL.AppendLine("        LEFT JOIN ALMOXARIFADOS A ON A.IDEMPRESA = " + empresaLogada);
            SQL.AppendLine("        INNER JOIN Estoque E ON P.Id  = E.ProdutoId AND E.ALMOXARIFADOID = A.ID ");
            SQL.AppendLine("        LEFT JOIN Cores C ON C.Id = E.CorId ");
            SQL.AppendLine("        LEFT JOIN Tamanhos T ON T.Id = E.TamanhoId ");
            SQL.AppendLine("    WHERE P.Id = ");
            SQL.Append(itemId);
            SQL.AppendLine("    AND C .Id = ");
            SQL.Append(corId);
            SQL.AppendLine("    AND A.id");
            SQL.Append(" in ( " + string.Join(",", almoxarifadoIds) + " ) ");

            SQL.Append("        UNION ALL ");

            SQL.AppendLine("    SELECT	T.*, (SUM(iop.quantidade - iop.quantidadeproduzida) -  ");
            SQL.AppendLine("                            IFNULL((SELECT SUM(qtdnaoatendida) FROM itensliberacaopedidovenda il");
            SQL.AppendLine("                            INNER JOIN itenspedidovenda ipv ON ipv.id = il.itempedidovendaid ");
            SQL.AppendLine("                            WHERE qtdnaoatendida > 0 AND ipv.produtoid = " + itemId + " AND ipv.tamanhoid = t.id AND ipv.corid = "+ corId +" AND il.almoxarifadoid = a.id  ");
            SQL.AppendLine("                ),0) ) AS Estoque  ");
            SQL.AppendLine("    FROM Produtos P ");
            SQL.AppendLine("        LEFT JOIN ALMOXARIFADOS A ON A.IDEMPRESA = " + empresaLogada);
            SQL.AppendLine("        INNER JOIN itensordemproducao iop ON iop.produtoid = p.id ");
            SQL.AppendLine("        INNER JOIN ordemproducao op ON op.id = iop.ordemproducaoid AND op.almoxarifadoid = a.id ");
            SQL.AppendLine("        LEFT JOIN Cores C ON C.Id = iop.CorId ");
            SQL.AppendLine("        LEFT JOIN Tamanhos T ON T.Id = iop.TamanhoId ");
            SQL.AppendLine("    WHERE P.Id = ");
            SQL.Append(itemId);
            SQL.AppendLine("    AND C .Id = ");
            SQL.Append(corId);
            SQL.AppendLine("    AND A.id ");
            SQL.Append(" in ( " + string.Join(",", almoxarifadoIds) + " ) ");
            SQL.AppendLine("   AND op.status <> 6  ");
            SQL.AppendLine("   GROUP BY t.id  ");


            SQL.AppendLine(" ) t WHERE ID IS NOT NULL GROUP BY Id ORDER BY Id ");

            var cn = new DapperConnection<TamanhoView>();
            return cn.ExecuteStringSqlToList(new TamanhoView(), SQL.ToString());
        }

        public IEnumerable<ItemTabelaPreco> GetItensTabelaPreco(int tabelaPrecoId)
        {
            var SQL = new StringBuilder();

            SQL.AppendLine("SELECT	* ");
            SQL.AppendLine("FROM ItensTabelaPreco ITP");
            SQL.AppendLine("WHERE ITP.TabelaPrecoId = ");
            SQL.Append(tabelaPrecoId);

            var cn = new DapperConnection<ItemTabelaPreco>();
            return cn.ExecuteStringSqlToList(new ItemTabelaPreco(), SQL.ToString());
        }

        public ItemTabelaPrecoView GetItemTabelaPrecoView(int tabelaPrecoId, int produtoId)
        {
            var SQL = new StringBuilder();
            
            SQL.AppendLine("SELECT	ITP.Id,	ITP.TabelaPrecoId, ITP.ProdutoId, ITP.CustoMedio, ITP.PrecoSugerido, ITP.PrecoVenda, ITP.Lucro, ");
            SQL.AppendLine("P.Descricao AS ProdutoDescricao, P.Referencia AS ProdutoReferencia, P.PrecoCompra AS ProdutoPrecoCompra, ");
            SQL.AppendLine("P.Ipi AS ProdutoIpi, P.Icms AS ProdutoIcms, P.Lucro AS ProdutoLucro, P.TipoCustoFornecedor AS ProdutoTipoCustoFornecedor, P.TipoCalculoPreco AS ProdutoTipoCalculoPreco ");
            SQL.AppendLine("FROM ItensTabelaPreco ITP");
            SQL.AppendLine("    INNER JOIN Produtos P ON P.Id  = ITP.ProdutoId");
            SQL.AppendLine("WHERE ITP.TabelaPrecoId = ");
            SQL.Append(tabelaPrecoId);
            SQL.AppendLine(" AND  ITP.ProdutoId = ");
            SQL.Append(produtoId);

            var cn = new DapperConnection<ItemTabelaPrecoView>();
            var ret = new ItemTabelaPrecoView();
            cn.ExecuteToModel(ref ret, SQL.ToString());
            return ret;
        }

        public IEnumerable<ItemTabelaPreco> GetListByProduto(int produtoId)
        {
            var SQL = new StringBuilder();

            SQL.AppendLine("SELECT	ITP.* ");
            SQL.AppendLine("FROM ItensTabelaPreco ITP");
            SQL.AppendLine(" INNER JOIN TabelasPreco T ON T.Id = ITP.TabelaPrecoId ");
            SQL.AppendLine("WHERE  " + FiltroEmpresa("T.EmpresaId") + " AND ITP.ProdutoId = ");
            SQL.Append(produtoId);
           
            return _cn.ExecuteStringSqlToList(new ItemTabelaPreco(), SQL.ToString());
        }

        public void DeleteByTabelaPreco(int tabelaPrecoId)
        {
            string SQL = "DELETE FROM ItensTabelaPreco WHERE TabelaPrecoId = " + tabelaPrecoId.ToString();
            _cn.ExecuteNonQuery(SQL);
        }

        public void DeleteByTabelaPrecoEProduto(int tabelaPrecoId, int produtoId)
        {
            string SQL = "DELETE FROM ItensTabelaPreco WHERE TabelaPrecoId = " + tabelaPrecoId.ToString() + " AND ProdutoId = " + produtoId;
            _cn.ExecuteNonQuery(SQL);
        }

    }
}
