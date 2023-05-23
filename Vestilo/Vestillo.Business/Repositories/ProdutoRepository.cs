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
    public class ProdutoRepository : GenericRepository<Produto>
    {
        public ProdutoRepository()
            : base(new DapperConnection<Produto>())
        {

        }

        public Produto GetByReferencia(string referencia)
        {
            var p = new Produto();
            _cn.ExecuteToModel("Referencia = '" + referencia + "'", ref p);
            return p;
        }

        public Produto GetByReferenciaComFichaTecnica(string referencia, bool fichaTecnicaCompleta)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT  P.*");
            SQL.AppendLine("FROM 	produtos P");
            SQL.AppendLine("WHERE   P.referencia = '" + referencia + "' And P.Ativo = 1");
            SQL.AppendLine("        AND " + FiltroEmpresa("", "P"));
            SQL.AppendLine("        AND (SELECT COUNT(*) FROM fichatecnica F WHERE F.ProdutoId = P.Id) ");



            if (fichaTecnicaCompleta)
            {
                SQL.Append(" > 0 ");
                SQL.AppendLine("        AND (SELECT COUNT(*) FROM fichatecnicadomaterial F WHERE F.ProdutoId = P.Id) ");
                SQL.Append(" > 0 ");
            }
            else
                SQL.Append(" > 0 ");

            SQL.AppendLine("ORDER BY P.Referencia");

            return _cn.ExecuteStringSqlToList(new Produto(), SQL.ToString()).FirstOrDefault();
        }

        public IEnumerable<Produto> GetListProdutosQuePossuemGradeENaoPossuemFichaTecnica()
        {
            Produto m = new Produto();
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT  P.*, CONCAT(P.Referencia, ' - ', P.Descricao) AS Descricao");
            SQL.AppendLine("FROM    Produtos P");
            SQL.AppendLine("WHERE   P.Id IN (SELECT PD.IdProduto FROM ProdutoDetalhes PD WHERE (PD.Inutilizado IS NULL OR PD.Inutilizado = 0))");
            SQL.AppendLine("        AND P.Ativo = 1");
            SQL.AppendLine("        AND P.TipoItem = 0");
            SQL.AppendLine("        AND (SELECT COUNT(*) FROM fichatecnicadomaterial FM WHERE FM.ProdutoId = P.Id) = 0");
            SQL.AppendLine("        AND " + FiltroEmpresa("", "P"));
            SQL.AppendLine("ORDER BY P.Referencia");

            return _cn.ExecuteStringSqlToList(new Produto(), SQL.ToString());
        }

        public IEnumerable<Produto> GetListMateriasPrimasQuePossuemGrade()
        {
            Produto m = new Produto();
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT  P.*, CONCAT(P.Referencia, ' - ', P.Descricao) AS Descricao");
            SQL.AppendLine("FROM    Produtos P");
            SQL.AppendLine("WHERE   P.Id IN (SELECT PD.IdProduto FROM ProdutoDetalhes PD WHERE (PD.Inutilizado IS NULL OR PD.Inutilizado = 0))");
            SQL.AppendLine("        AND P.Ativo = 1");
            SQL.AppendLine("        AND P.TipoItem IN(1,2)");
            SQL.AppendLine("        AND " + FiltroEmpresa("", "P"));
            //SQL.AppendLine("        AND (SELECT COUNT(*) FROM fichatecnicadomaterial FM WHERE FM.ProdutoId = P.Id) = 0");
            SQL.AppendLine("ORDER BY P.Referencia");

            return _cn.ExecuteStringSqlToList(new Produto(), SQL.ToString());
        }

        public IEnumerable<Produto> GetListPorTipoAtivo(Produto.enuTipoItem tipo)
        {
            Produto m = new Produto();
            var SQL = new StringBuilder();

            return _cn.ExecuteToList(m, "TipoItem = " + (int)tipo + " And ativo = 1");

        }

        public Produto GetByReferenciaFornecedor(string referencia)
        {
            var p = new Produto();
            _cn.ExecuteToModel("  RefFornecedor like '%" + referencia + "%'", ref p);
            return p;
        }

        public IEnumerable<Produto> GetListPorReferencia(string referencia)
        {


            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT   produtos.*,");
            SQL.AppendLine(" IFNULL(colecoes.descricao,'') as Colecao,IFNULL(segmentos.descricao,'') as Segmento,IFNULL(grupoprodutos.descricao,'') as Grupo ");
            SQL.AppendLine("FROM 	produtos ");
            SQL.AppendLine(" LEFT JOIN grupoprodutos ON grupoprodutos.id = produtos.IdGrupo ");
            SQL.AppendLine(" LEFT JOIN colecoes ON colecoes.id = produtos.Idcolecao ");
            SQL.AppendLine(" LEFT JOIN segmentos ON segmentos.id = produtos.Idsegmento ");
            SQL.AppendLine("WHERE " + FiltroEmpresa("produtos.IdEmpresa") + " AND produtos.referencia like '%" + referencia + "%' And produtos.Ativo = 1");
            SQL.AppendLine("ORDER BY produtos.Referencia");

            return _cn.ExecuteStringSqlToList(new Produto(), SQL.ToString());
        }

        public IEnumerable<Produto> GetListPorReferenciaComFichaTecnica(string referencia, bool fichaTecnicaCompleta)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT  P.*");
            SQL.AppendLine("FROM 	produtos P");
            SQL.AppendLine("WHERE " + FiltroEmpresa("P.IdEmpresa") + " AND P.referencia like '%" + referencia + "%' And P.Ativo = 1");
            SQL.AppendLine("        AND (SELECT COUNT(*) FROM fichatecnica F WHERE F.ProdutoId = P.Id) ");

            if (fichaTecnicaCompleta)
            {
                SQL.Append(" > 0 ");
                SQL.AppendLine("        AND (SELECT COUNT(*) FROM fichatecnicadomaterial F WHERE F.ProdutoId = P.Id) ");
                SQL.Append(" > 0 ");
            }
            else
                SQL.Append(" > 0 ");

            SQL.AppendLine("ORDER BY P.Referencia");

            return _cn.ExecuteStringSqlToList(new Produto(), SQL.ToString());
        }

        public IEnumerable<Produto> GetListPorReferenciaSemFichaTecnica(string referencia)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT  P.*");
            SQL.AppendLine("FROM 	produtos P");
            SQL.AppendLine("WHERE " + FiltroEmpresa("P.IdEmpresa") + " AND  P.referencia like '%" + referencia + "%' And P.Ativo = 1");
            SQL.AppendLine("        AND (SELECT COUNT(*) FROM fichatecnica F WHERE F.ProdutoId = P.Id) ");
            SQL.Append(" = 0 ");

            SQL.AppendLine("ORDER BY P.Referencia");

            return _cn.ExecuteStringSqlToList(new Produto(), SQL.ToString());
        }

        public IEnumerable<Produto> GetListPorReferenciaSemFichaTecnicaMaterial(string referencia)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT  P.*");
            SQL.AppendLine("FROM 	produtos P");
            SQL.AppendLine("WHERE " + FiltroEmpresa("P.IdEmpresa") + " AND  P.referencia like '%" + referencia + "%' And P.Ativo = 1");
            SQL.AppendLine("        AND (SELECT COUNT(*) FROM fichatecnicadomaterial F WHERE F.ProdutoId = P.Id) ");
            SQL.Append(" = 0 ");

            SQL.AppendLine("ORDER BY P.Referencia");

            return _cn.ExecuteStringSqlToList(new Produto(), SQL.ToString());
        }

        //public IEnumerable<OrdemProducaoMaterialView> GetListPorIdComFichaTecnicaMaterial(List<int> produtosId, int ordemId)
        //{
        //    var SQL = new StringBuilder();
        //    SQL.AppendLine("SELECT OPM.QuantidadeEmpenhada, OPM.QuantidadeBaixada, PM.Referencia as MaterialReferencia, PM.Descricao as MaterialDescricao, FI.materiaPrimaId, FI.materiaPrimaId as MateriaPrimaOriginalId, (SUM(FI.quantidade*IOP.Quantidade)) as QuantidadeNecessaria,");
        //    SQL.AppendLine("C.Abreviatura as CorDescricao, T.Abreviatura as TamanhoDescricao, ");
        //    SQL.AppendLine("C.ID as CorId, T.Id as TamanhoId, AF.abreviatura as ArmazemDescricao, AF.id as ArmazemId, OP.id as OrdemProducaoId, UMD.abreviatura as UM, OP.Referencia as OrdemProducaoReferencia,");
        //    SQL.AppendLine("FR.cor_produto_id as CorProdutoId, FR.tamanho_produto_id as TamanhoProdutoId");
        //    SQL.AppendLine("FROM 	produtos P");
        //    SQL.AppendLine("INNER JOIN itensordemproducao IOP ON IOP.ProdutoId = P.Id ");
        //    SQL.AppendLine("INNER JOIN fichatecnicadomaterial F ON F.ProdutoId = P.Id ");
        //    SQL.AppendLine("INNER JOIN fichatecnicadomaterialitem FI ON FI.fichatecnicaid = F.Id ");
        //    SQL.AppendLine("INNER JOIN fichatecnicadomaterialrelacao FR ON ");
        //    SQL.AppendLine("(FR.fichatecnicaid = F.Id AND FR.materiaPrimaId = FI.materiaPrimaId AND FR.cor_produto_id = IOP.CorId AND FR.tamanho_produto_id = IOP.TamanhoId) ");
        //    SQL.AppendLine("INNER JOIN produtos PM ON FI.materiaprimaid = PM.Id ");
        //    SQL.AppendLine("INNER JOIN cores C ON C.id = FR.cor_materiaprima_id ");
        //    SQL.AppendLine("INNER JOIN tamanhos T ON T.id = FR.tamanho_materiaprima_id ");
        //    SQL.AppendLine("INNER JOIN ordemproducao OP ON OP.id = IOP.OrdemProducaoId ");
        //    SQL.AppendLine("INNER JOIN almoxarifados AF ON AF.id = OP.AlmoxarifadoId ");
        //    SQL.AppendLine("INNER JOIN unidademedidas UMD ON UMD.id = PM.IdUniMedida ");
        //    SQL.AppendLine("LEFT JOIN ordemproducaomateriais OPM ON (OPM.OrdemProducaoId = IOP.OrdemProducaoId AND FR.cor_materiaprima_id = OPM.CorId AND FR.tamanho_materiaprima_id = OPM.TamanhoId AND OPM.materiaPrimaId = FR.materiaPrimaId)");
        //    SQL.AppendLine("WHERE   P.Id ");
        //    SQL.Append(" IN( ");
        //    SQL.Append(string.Join(",", produtosId));
        //    SQL.Append(") AND IOP.OrdemProducaoId = " + ordemId);
        //    SQL.AppendLine(" GROUP BY  FI.materiaPrimaId, FR.cor_materiaprima_id, FR.tamanho_materiaprima_id ");
        //    SQL.AppendLine("ORDER BY P.Referencia");
        //    var cn = new DapperConnection<OrdemProducaoMaterialView>();
        //    return cn.ExecuteStringSqlToList(new OrdemProducaoMaterialView(), SQL.ToString());
        //}

        //public IEnumerable<OrdemProducaoMaterialView> GetListPorIdComFichaTecnicaMaterial(ItemOrdemProducaoView item, int ordemId)
        //{
        //    var SQL = new StringBuilder();
        //    SQL.AppendLine("SELECT OPM.QuantidadeEmpenhada, OPM.QuantidadeBaixada, PM.Referencia as MaterialReferencia, PM.Descricao as MaterialDescricao, FI.materiaPrimaId, FI.materiaPrimaId as MateriaPrimaOriginalId, (SUM(FI.quantidade*IOP.Quantidade)) as QuantidadeNecessaria,");
        //    SQL.AppendLine("C.Abreviatura as CorDescricao, T.Abreviatura as TamanhoDescricao, ");
        //    SQL.AppendLine("C.ID as CorId, T.Id as TamanhoId, AF.abreviatura as ArmazemDescricao, AF.id as ArmazemId, OP.id as OrdemProducaoId, UMD.abreviatura as UM, OP.Referencia as OrdemProducaoReferencia,");
        //    SQL.AppendLine("FR.cor_produto_id as CorProdutoId, FR.tamanho_produto_id as TamanhoProdutoId");
        //    SQL.AppendLine("FROM 	produtos P");
        //    SQL.AppendLine("INNER JOIN itensordemproducao IOP ON IOP.ProdutoId = P.Id ");
        //    SQL.AppendLine("INNER JOIN fichatecnicadomaterial F ON F.ProdutoId = P.Id ");
        //    SQL.AppendLine("INNER JOIN fichatecnicadomaterialitem FI ON FI.fichatecnicaid = F.Id ");
        //    SQL.AppendLine("INNER JOIN fichatecnicadomaterialrelacao FR ON ");
        //    SQL.AppendLine("(FR.fichatecnicaid = F.Id AND FR.materiaPrimaId = FI.materiaPrimaId AND FR.cor_produto_id = IOP.CorId AND FR.tamanho_produto_id = IOP.TamanhoId) ");
        //    SQL.AppendLine("INNER JOIN produtos PM ON FI.materiaprimaid = PM.Id ");
        //    SQL.AppendLine("INNER JOIN cores C ON C.id = FR.cor_materiaprima_id ");
        //    SQL.AppendLine("INNER JOIN tamanhos T ON T.id = FR.tamanho_materiaprima_id ");
        //    SQL.AppendLine("INNER JOIN ordemproducao OP ON OP.id = IOP.OrdemProducaoId ");
        //    SQL.AppendLine("INNER JOIN almoxarifados AF ON AF.id = OP.AlmoxarifadoId ");
        //    SQL.AppendLine("INNER JOIN unidademedidas UMD ON UMD.id = PM.IdUniMedida ");
        //    SQL.AppendLine("LEFT JOIN ordemproducaomateriais OPM ON (OPM.OrdemProducaoId = IOP.OrdemProducaoId AND FR.cor_materiaprima_id = OPM.CorId AND FR.tamanho_materiaprima_id = OPM.TamanhoId AND OPM.materiaPrimaId = FR.materiaPrimaId)");
        //    SQL.AppendLine("WHERE   P.Id = ");
        //    SQL.Append(" IN( ");
        //    SQL.Append(item.ProdutoId + " AND IOP.CorId = " + item.CorId + "  AND IOP.TamanhoId = " + item.TamanhoId);
        //    SQL.Append(" AND IOP.OrdemProducaoId = " + ordemId);
        //    SQL.AppendLine(" GROUP BY  FI.materiaPrimaId, FR.cor_materiaprima_id, FR.tamanho_materiaprima_id ");
        //    SQL.AppendLine("ORDER BY P.Referencia");
        //    var cn = new DapperConnection<OrdemProducaoMaterialView>();
        //    return cn.ExecuteStringSqlToList(new OrdemProducaoMaterialView(), SQL.ToString());
        //}

        public IEnumerable<Produto> GetListPorDescricao(string desc)
        {

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT   produtos.*,");
            SQL.AppendLine(" IFNULL(colecoes.descricao,'') as Colecao,IFNULL(segmentos.descricao,'') as Segmento,IFNULL(grupoprodutos.descricao,'') as Grupo ");
            SQL.AppendLine("FROM 	produtos ");
            SQL.AppendLine(" LEFT JOIN grupoprodutos ON grupoprodutos.id = produtos.IdGrupo ");
            SQL.AppendLine(" LEFT JOIN colecoes ON colecoes.id = produtos.Idcolecao ");
            SQL.AppendLine(" LEFT JOIN segmentos ON segmentos.id = produtos.Idsegmento ");
            SQL.AppendLine("WHERE " + FiltroEmpresa("produtos.IdEmpresa") + " AND  produtos.descricao like '%" + desc + "%' And produtos.Ativo = 1");
            SQL.AppendLine("ORDER BY produtos.Referencia");

            return _cn.ExecuteStringSqlToList(new Produto(), SQL.ToString());


        }

        public IEnumerable<Produto> GetListPorDescricaoSemFichaTecnica(string desc)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT  P.*");
            SQL.AppendLine("FROM 	produtos P");
            SQL.AppendLine("WHERE " + FiltroEmpresa("P.IdEmpresa") + " AND  P.descricao like '%" + desc + "%' And P.Ativo = 1");
            SQL.AppendLine("        AND (SELECT COUNT(*) FROM fichatecnica F WHERE F.ProdutoId = P.Id) ");
            SQL.Append(" = 0 ");

            SQL.AppendLine("ORDER BY P.Referencia");

            return _cn.ExecuteStringSqlToList(new Produto(), SQL.ToString());
        }

        public IEnumerable<Produto> GetListPorDescricaoSemFichaTecnicaMaterial(string desc)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT  P.*");
            SQL.AppendLine("FROM 	produtos P");
            SQL.AppendLine("WHERE " + FiltroEmpresa("P.IdEmpresa") + " AND  P.descricao like '%" + desc + "%' And P.Ativo = 1");
            SQL.AppendLine("        AND (SELECT COUNT(*) FROM fichatecnicadomaterial F WHERE F.ProdutoId = P.Id) ");
            SQL.Append(" = 0 ");

            SQL.AppendLine("ORDER BY P.Referencia");

            return _cn.ExecuteStringSqlToList(new Produto(), SQL.ToString());
        }

        public IEnumerable<Produto> GetListPorFornecedor(string forncedor)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT P.*, IFNULL(colecoes.descricao,'') as Colecao,IFNULL(segmentos.descricao,'') as Segmento,IFNULL(grupoprodutos.descricao,'') as Grupo");
            SQL.AppendLine("FROM 	produtos P");
            SQL.AppendLine("INNER JOIN produtofornecedorprecos PF ON PF.IdProduto = P.Id ");
            SQL.AppendLine("INNER JOIN colaboradores F ON F.Id = PF.IdFornecedor ");

            SQL.AppendLine(" LEFT JOIN grupoprodutos ON grupoprodutos.id = P.IdGrupo ");
            SQL.AppendLine(" LEFT JOIN colecoes ON colecoes.id = P.Idcolecao ");
            SQL.AppendLine(" LEFT JOIN segmentos ON segmentos.id = P.Idsegmento ");

            SQL.AppendLine("WHERE   " + FiltroEmpresa("P.IdEmpresa"));
            SQL.AppendLine("AND F.Nome like '%" + forncedor + "%' And P.Ativo = 1");
            SQL.AppendLine("GROUP BY P.Id");
            SQL.AppendLine("ORDER BY P.Referencia");

            return _cn.ExecuteStringSqlToList(new Produto(), SQL.ToString());
        }

        public IEnumerable<Produto> GetListPorFornecedorSemFichaTecnica(string forncedor)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT  P.*");
            SQL.AppendLine("FROM 	produtos P");
            SQL.AppendLine("INNER JOIN produtofornecedorprecos PF ON PF.IdProduto = P.Id ");
            SQL.AppendLine("INNER JOIN colaboradores F ON F.Id = PF.IdFornecedor ");
            SQL.AppendLine("AND " + FiltroEmpresa("P.IdEmpresa") + " AND F.Nome like '%" + forncedor + "%' And P.Ativo = 1");
            SQL.AppendLine("        AND (SELECT COUNT(*) FROM fichatecnica F WHERE F.ProdutoId = P.Id) ");
            SQL.Append(" = 0 ");
            SQL.AppendLine("GROUP BY P.Id");
            SQL.AppendLine("ORDER BY P.Referencia");

            return _cn.ExecuteStringSqlToList(new Produto(), SQL.ToString());
        }

        public IEnumerable<Produto> GetListPorFornecedorSemFichaTecnicaMaterial(string forncedor)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT  P.*");
            SQL.AppendLine("FROM 	produtos P");
            SQL.AppendLine("INNER JOIN produtofornecedorprecos PF ON PF.IdProduto = P.Id ");
            SQL.AppendLine("INNER JOIN colaboradores F ON F.Id = PF.IdFornecedor ");
            SQL.AppendLine("AND " + FiltroEmpresa("P.IdEmpresa") + " AND F.Nome like '%" + forncedor + "%' And P.Ativo = 1");
            SQL.AppendLine("        AND (SELECT COUNT(*) FROM fichatecnicadomaterial F WHERE F.ProdutoId = P.Id) ");
            SQL.Append(" = 0 ");
            SQL.AppendLine("GROUP BY P.Id");
            SQL.AppendLine("ORDER BY P.Referencia");

            return _cn.ExecuteStringSqlToList(new Produto(), SQL.ToString());
        }

        public IEnumerable<Produto> GetListPorDescricaoComFichaTecnica(string desc, bool comFichaTecnicaCompleta)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT  P.*");
            SQL.AppendLine("FROM 	produtos P");
            SQL.AppendLine("WHERE " + FiltroEmpresa("P.IdEmpresa") + " AND  P.descricao like '%" + desc + "%' And P.Ativo = 1");
            SQL.AppendLine("        AND (SELECT COUNT(*) FROM fichatecnica F WHERE F.ProdutoId = P.Id) ");

            if (comFichaTecnicaCompleta)
            {
                SQL.Append(" > 0 ");
                SQL.AppendLine("        AND (SELECT COUNT(*) FROM fichatecnicadomaterial F WHERE F.ProdutoId = P.Id) ");
                SQL.Append(" > 0 ");
            }
            else
                SQL.Append(" > 0 ");

            SQL.AppendLine("ORDER BY P.Referencia");

            return _cn.ExecuteStringSqlToList(new Produto(), SQL.ToString());
        }

        public IEnumerable<Produto> GetAllAtivos()
        {
            Produto m = new Produto();
            return _cn.ExecuteStringSqlToList(m, "SELECT * FROM Produtos WHERE ativo = 1 ORDER BY Referencia");
        }

        public IEnumerable<Produto> GetProdutosLiberados(string referencia)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT  P.Id, P.Referencia, CONCAT(P.Referencia, ' - ', P.Descricao) AS Descricao, P.QtdPacote As QtdPacote");
            SQL.AppendLine("FROM 	produtos P");
            SQL.AppendLine("INNER JOIN itensordemproducao IOP ON IOP.ProdutoId = P.Id ");
            SQL.AppendLine("INNER JOIN ordemproducao ON ordemproducao.id = IOP.OrdemProducaoId ");
            SQL.AppendLine("WHERE   " + FiltroEmpresa("P.IdEmpresa"));
            SQL.AppendLine(" AND ordemproducao.Status <> 6 ");
            SQL.AppendLine("AND IOP.Status = 1");
            SQL.AppendLine("AND P.referencia like '%" + referencia + "%'");
            SQL.AppendLine("AND ( SELECT (SUM(IP.Quantidade) - SUM(IP.QuantidadeAtendida)) FROM itensordemproducao IP WHERE IP.ProdutoId = P.Id GROUP BY IP.ProdutoId) > 0");
            SQL.AppendLine("GROUP BY P.Id");
            SQL.AppendLine("ORDER BY P.Referencia");

            return _cn.ExecuteStringSqlToList(new Produto(), SQL.ToString());
        }

        public IEnumerable<Produto> GetProdutosParaManutencaoFichaTecnica(bool comFicha, bool semFicha)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT  P.Id, P.Referencia, CONCAT(P.Referencia, ' - ', P.Descricao) AS Descricao");
            SQL.AppendLine("FROM 	produtos P");
            SQL.AppendLine("WHERE   " + FiltroEmpresa("P.IdEmpresa"));
            SQL.AppendLine("        AND (P.TipoItem = 0 OR P.TipoItem = 2)");
            SQL.AppendLine("        AND P.Ativo = 1");

            if (!(comFicha && semFicha))
            {
                SQL.AppendLine("        AND (SELECT COUNT(*) FROM fichatecnica F WHERE F.ProdutoId = P.Id) ");

                if (comFicha)
                    SQL.Append(" > 0 ");
                else
                    SQL.Append(" = 0 ");
            }

            SQL.AppendLine("ORDER BY P.Referencia");

            return _cn.ExecuteStringSqlToList(new Produto(), SQL.ToString());
        }

        public IEnumerable<Produto> GetListById(int id)
        {
            Produto m = new Produto();
            return _cn.ExecuteToList(m, "id = " + id + " And ativo = 1");
        }

        public Produto GetByUnicoCodBarras(string CodBarras)
        {
            var p = new Produto();
            _cn.ExecuteToModel("CodBarrasUnico = '" + CodBarras + "'", ref p);
            return p;
        }

        public IEnumerable<MovimentarEstoqueView> GetProdutoPraMovimentarEstoque(string busca, bool buscarPorId, int almoxarifadoId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT  P.Id AS ProdutoId,");
            SQL.AppendLine("		P.Referencia As ProdutoReferencia,");
            SQL.AppendLine("		P.Descricao As ProdutoDescricao,");
            SQL.AppendLine("        T.Id As TamanhoId,");
            SQL.AppendLine("        T.Abreviatura As TamanhoAbreviatura,");
            SQL.AppendLine("        C.Id AS CorId,");
            SQL.AppendLine("        C.Abreviatura As CorAbreviatura,");
            SQL.AppendLine("        U.id AS IdUniMedida,");
            SQL.AppendLine("        U.abreviatura AS UnidMedidaAbreviatura,");
            SQL.AppendLine("        CASE WHEN E.Saldo IS NULL THEN 0 ELSE  E.Saldo END AS Saldo,");
            SQL.AppendLine("        1 AS Qtd");
            SQL.AppendLine("FROM 	produtos P");
            SQL.AppendLine("	LEFT JOIN produtodetalhes PD ON PD.IdProduto = P.Id");
            SQL.AppendLine("    LEFT JOIN tamanhos T ON T.Id = PD.IdTamanho");
            SQL.AppendLine("    LEFT JOIN cores C ON C.Id = PD.Idcor");
            SQL.AppendLine("    LEFT JOIN unidademedidas U ON U.id = P.IdUniMedida");
            SQL.AppendLine("    LEFT JOIN estoque E ON E.ProdutoId = P.Id AND IFNULL(E.CorId, 0) = IFNULL(C.Id, 0) AND IFNULL(E.TamanhoId, 0) = IFNULL(T.Id, 0) AND E.AlmoxarifadoId = " + almoxarifadoId.ToString());
            if (!buscarPorId)
                SQL.AppendLine("WHERE (PD.CodBarras = '" + busca + "' OR P.CodBarrasUnico = '" + busca + "' OR P.Referencia = '" + busca + "')");
            else
                SQL.AppendLine("WHERE P.Id = " + busca);



            SQL.AppendLine("      AND P.Ativo = 1 ");
            SQL.AppendLine("       AND (IFNULL(C.Id, 0) != 999 AND IFNULL(T.Id, 0) != 999) ");
            SQL.AppendLine("      AND  IFNULL(PD.Inutilizado, 0) = 0 ");
            SQL.AppendLine("      AND " + FiltroEmpresa("P.IdEmpresa"));
            SQL.AppendLine("ORDER BY P.Referencia , T.Id, C.Abreviatura");

            var cn = new DapperConnection<MovimentarEstoqueView>();
            return cn.ExecuteStringSqlToList(new MovimentarEstoqueView(), SQL.ToString());
        }

        public IEnumerable<DevolucaoItensView> GetProdutoDevolucaoItens(string busca, bool buscarPorId, int almoxarifadoId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT  P.Id AS ProdutoId,");
            SQL.AppendLine("		P.Referencia As ProdutoReferencia,");
            SQL.AppendLine("		P.Descricao As ProdutoDescricao,");
            SQL.AppendLine("        T.Id As TamanhoId,");
            SQL.AppendLine("        T.Abreviatura As TamanhoAbreviatura,");
            SQL.AppendLine("        C.Id AS CorId,");
            SQL.AppendLine("        C.Abreviatura As CorAbreviatura,");
            SQL.AppendLine("        CASE WHEN E.Saldo IS NULL THEN 0 ELSE  E.Saldo END AS Saldo,");
            SQL.AppendLine("        1 AS Qtd");
            SQL.AppendLine("FROM 	produtos P");
            SQL.AppendLine("	LEFT JOIN produtodetalhes PD ON PD.IdProduto = P.Id");
            SQL.AppendLine("    LEFT JOIN tamanhos T ON T.Id = PD.IdTamanho");
            SQL.AppendLine("    LEFT JOIN cores C ON C.Id = PD.Idcor");
            SQL.AppendLine("    LEFT JOIN estoque E ON E.ProdutoId = P.Id AND IFNULL(E.CorId, 0) = IFNULL(C.Id, 0) AND IFNULL(E.TamanhoId, 0) = IFNULL(T.Id, 0) AND E.AlmoxarifadoId = " + almoxarifadoId.ToString());
            if (!buscarPorId)
                SQL.AppendLine("WHERE (PD.CodBarras = '" + busca + "' OR P.CodBarrasUnico = '" + busca + "' OR P.Referencia = '" + busca + "')");
            else
                SQL.AppendLine("WHERE P.Id = " + busca);

            SQL.AppendLine("      AND " + FiltroEmpresa("P.IdEmpresa"));
            SQL.AppendLine("ORDER BY P.Referencia , T.Id, C.Abreviatura");

            var cn = new DapperConnection<DevolucaoItensView>();
            return cn.ExecuteStringSqlToList(new DevolucaoItensView(), SQL.ToString());
        }

        public void UpdateRefFornecedor(int ItemId, string refFornecedor)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("UPDATE produtos SET ");
            SQL.AppendLine("RefFornecedor = ");
            SQL.AppendLine("if(IFNULL(produtos.RefFornecedor,'') = '', " + "'" + refFornecedor + "'");
            SQL.AppendLine(",concat(produtos.RefFornecedor," + "'," + refFornecedor + "'))");
            SQL.AppendLine(" WHERE id = ");
            SQL.Append(ItemId);

            _cn.ExecuteNonQuery(SQL.ToString());
        }

        public void UpdateItemVinculado(int ItemId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("UPDATE produtos SET ");
            SQL.AppendLine("ativo = 0");
            SQL.AppendLine(" WHERE id = ");
            SQL.Append(ItemId);
            _cn.ExecuteNonQuery(SQL.ToString());
        }

        public IEnumerable<FocoVendas> GetFocoVendas(FiltroRelatorioFocoVendas filtro, bool AguparCor)
        {
            var cn = new DapperConnection<FocoVendas>();
            var SQL = new StringBuilder();


            SQL.AppendLine(" SELECT p.id as ProdutoId, p.Referencia,cat.descricao as Catalogo,col.descricao as Colecao,segmentos.descricao as segmento, c.Abreviatura AS cor, t.Abreviatura AS tamanho, SUM(IFNULL(e.Saldo, 0)) AS Saldo, c.id as CorId, t.id as TamanhoId, a.id as AlmoxarifadoId ");
            SQL.AppendLine(" FROM produtos p ");
            SQL.AppendLine(" INNER JOIN catalogo cat on cat.id = p.IdCatalogo ");
            SQL.AppendLine(" INNER JOIN colecoes col on col.id = p.Idcolecao ");
            SQL.AppendLine(" INNER JOIN produtodetalhes pd ON(pd.idProduto = p.id) ");
            SQL.AppendLine(" INNER JOIN cores c ON(c.Id = pd.idCor) ");
            SQL.AppendLine(" INNER JOIN tamanhos t ON(t.Id = pd.idTamanho) ");
            SQL.AppendLine(" INNER JOIN almoxarifados a ON a.Id = 3 ");
            SQL.AppendLine(" INNER JOIN estoque e ON(e.ProdutoId = p.Id AND e.corId = pd.idCor AND e.tamanhoId = pd.idTamanho) ");
            SQL.AppendLine(" LEFT JOIN segmentos on segmentos.id = p.Idsegmento ");
            SQL.AppendLine(" WHERE (a.idempresa = " + VestilloSession.EmpresaLogada.Id + ")");
            SQL.AppendLine(" AND e.almoxarifadoid = " + filtro.idAlmoxarifado);
            SQL.AppendLine(" AND e.Saldo >= " + filtro.saldo);

            if (filtro.catalogosIds != null && filtro.catalogosIds.Length > 0)
                SQL.AppendLine("        AND p.idCatalogo IN (" + string.Join(", ", filtro.catalogosIds) + ")");

            if (filtro.colecoesIds != null && filtro.colecoesIds.Length > 0)
                SQL.AppendLine("        AND p.idColecao IN (" + string.Join(", ", filtro.colecoesIds) + ")");



            if (filtro.corIds != null && filtro.corIds.Length > 0)
                SQL.AppendLine("        AND pd.idCor IN (" + string.Join(", ", filtro.corIds) + ")");

            if (filtro.tamanhosIds != null && filtro.tamanhosIds.Length > 0)
                SQL.AppendLine("        AND pd.idTamanho IN (" + string.Join(", ", filtro.tamanhosIds) + ")");




            if (AguparCor == false)
            {
                SQL.AppendLine(" GROUP BY p.id, pd.idCor, pd.idTamanho");
            }
            else
            {
                SQL.AppendLine(" GROUP BY p.id, pd.idCor");
            }

            SQL.AppendLine(" ORDER BY segmentos.descricao,cat.descricao,col.descricao, P.Referencia ,c.Abreviatura,t.Id ");

            return cn.ExecuteStringSqlToList(new FocoVendas(), SQL.ToString());



            /*
            SQLPrincipal.AppendLine("SELECT p.id as ProdutoId, s.descricao as Segmento , i.id as ImagemId, p.Referencia, c.Abreviatura AS cor, t.Abreviatura AS tamanho, IFNULL(e.Saldo, 0) AS Saldo, ");
            SQLPrincipal.AppendLine("(SUM(IFNULL(n.quantidade, 0)) - (SUM(IFNULL(n.Qtddevolvida, 0)) + IFNULL(d.Quantidade, 0))) as QtdFaturada, SUM(IFNULL(e.Empenhado, 0)) as QtdEmpenhada, ");
            SQLPrincipal.AppendLine("IFNULL(pd.TotalOp, 0) AS TotalOp, c.id as CorId, t.id as TamanhoId, a.id as AlmoxarifadoId");
            SQLPrincipal.AppendLine("FROM produtos p ");
            SQLPrincipal.AppendLine("INNER JOIN produtodetalhes pd ON (pd.idProduto = p.id) ");
            SQLPrincipal.AppendLine("INNER JOIN segmentos s ON (s.Id = p.idSegmento) ");
            SQLPrincipal.AppendLine("INNER JOIN cores c ON (c.Id = pd.idCor) ");
            SQLPrincipal.AppendLine("INNER JOIN tamanhos t ON (t.Id = pd.idTamanho) ");
            SQLPrincipal.AppendLine("INNER JOIN almoxarifados a ON (a.Id = p.IdAlmoxarifado) ");
            SQLPrincipal.AppendLine("LEFT JOIN estoque e ON (e.ProdutoId = p.Id AND e.corId = pd.idCor AND e.tamanhoId = pd.idTamanho and e.almoxarifadoid = a.id) ");
            SQLPrincipal.AppendLine("LEFT JOIN nfeitens n ON (n.idItem = p.id AND n.idcor = pd.idcor AND n.idtamanho = pd.idtamanho) ");
            SQLPrincipal.AppendLine("LEFT JOIN devolucaoitens d ON (d.idItem = p.id AND d.idcor = pd.idcor AND d.idtamanho = pd.idtamanho) ");
            SQLPrincipal.AppendLine("LEFT JOIN imagens i ON (i.idProduto = p.Id) ");
            SQLPrincipal.AppendLine(" WHERE (a.idempresa IS NULL OR a.idempresa = " + VestilloSession.EmpresaLogada.Id + ")");
            
            if (filtro.catalogosIds != null && filtro.catalogosIds.Length > 0)
                SQLPrincipal.AppendLine("        AND p.idCatalogo IN (" + string.Join(", ", filtro.catalogosIds) + ")");
            if (filtro.colecoesIds != null && filtro.colecoesIds.Length > 0)
                SQLPrincipal.AppendLine("        AND p.idColecao IN (" + string.Join(", ", filtro.colecoesIds) + ")");
            
            if (filtro.corIds != null && filtro.corIds.Length > 0)
                SQLPrincipal.AppendLine("        AND pd.idCor IN (" + string.Join(", ", filtro.corIds) + ")");
            if (filtro.tamanhosIds != null && filtro.tamanhosIds.Length > 0)
                SQLPrincipal.AppendLine("        AND pd.idTamanho IN (" + string.Join(", ", filtro.tamanhosIds) + ")");
            SQLPrincipal.AppendLine(" GROUP BY p.id, pd.idCor, pd.idTamanho");
            
            DataTable dt1 = Vestillo.Core.Connection.VestilloConnection.ExecToDataTable(SQLPrincipal.ToString());
            var SQLSecundario = new StringBuilder();
            SQLSecundario.AppendLine("SELECT p.id as ProdutoId, s.descricao as Segmento ,  p.Referencia, c.Abreviatura AS cor, t.Abreviatura AS tamanho, ");
            SQLSecundario.AppendLine("IFNULL(ilp.QtdNaoAtendida, 0) AS NaoAtendida, c.id as CorId, t.id as TamanhoId, a.id as AlmoxarifadoId");
            SQLSecundario.AppendLine("FROM produtos p ");
            SQLSecundario.AppendLine("INNER JOIN produtodetalhes pd ON (pd.idProduto = p.id) ");
            SQLSecundario.AppendLine("INNER JOIN segmentos s ON (s.Id = p.idSegmento) ");
            SQLSecundario.AppendLine("INNER JOIN cores c ON (c.Id = pd.idCor) ");
            SQLSecundario.AppendLine("INNER JOIN tamanhos t ON (t.Id = pd.idTamanho) ");
            SQLSecundario.AppendLine("INNER JOIN almoxarifados a ON (a.Id = p.IdAlmoxarifado) ");
            SQLSecundario.AppendLine("LEFT JOIN itenspedidovenda ip ON (ip.ProdutoId = p.id AND ip.CorId = pd.idcor AND ip.TamanhoId = pd.idtamanho) ");
            SQLSecundario.AppendLine("LEFT JOIN itensliberacaopedidovenda ilp ON (ilp.ItemPedidoVendaID = ip.id) ");
            SQLSecundario.AppendLine(" WHERE (a.idempresa IS NULL OR a.idempresa = " + VestilloSession.EmpresaLogada.Id + ")");
           
            if (filtro.catalogosIds != null && filtro.catalogosIds.Length > 0)
                SQLSecundario.AppendLine("        AND p.idCatalogo IN (" + string.Join(", ", filtro.catalogosIds) + ")");
            if (filtro.colecoesIds != null && filtro.colecoesIds.Length > 0)
                SQLSecundario.AppendLine("        AND p.idColecao IN (" + string.Join(", ", filtro.colecoesIds) + ")");
           
            if (filtro.corIds != null && filtro.corIds.Length > 0)
                SQLSecundario.AppendLine("        AND pd.idCor IN (" + string.Join(", ", filtro.corIds) + ")");
            if (filtro.tamanhosIds != null && filtro.tamanhosIds.Length > 0)
                SQLSecundario.AppendLine("        AND pd.idTamanho IN (" + string.Join(", ", filtro.tamanhosIds) + ")");
            SQLSecundario.AppendLine(" GROUP BY p.id, pd.idCor, pd.idTamanho");
            DataTable dt2 = Vestillo.Core.Connection.VestilloConnection.ExecToDataTable(SQLSecundario.ToString());
            
            List<FocoVendas> listFocoVenda = null;
            listFocoVenda = new List<FocoVendas>();
            listFocoVenda.Clear();
            FocoVendas item = new FocoVendas();
            ImagemRepository imgRepository = new ImagemRepository();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                item = new FocoVendas();
                item.segmento = dt1.Rows[i]["Segmento"].ToString();                
                item.referencia = dt1.Rows[i]["Referencia"].ToString(); 
                item.cor = dt1.Rows[i]["cor"].ToString(); 
                item.tamanho = dt1.Rows[i]["tamanho"].ToString(); 
                item.saldo = dt1.Rows[i]["Saldo"].ToDecimal();
                //imagem
                imgRepository = new ImagemRepository();
                var imagem = imgRepository.GetImagem("id", dt1.Rows[i]["ImagemId"].ToInt());
                foreach (var img in imagem)
                {
                    if(img.tipo == Imagem.TipoDeImagem.ProdutoAcabado)
                        item.imagem = img.imagem;
                }
                var qtdFaturada = dt1.Rows[i]["QtdFaturada"].ToDecimal();
                var qtdEmpenhada = dt1.Rows[i]["QtdEmpenhada"].ToDecimal();
                var totalFabricado = qtdFaturada + qtdEmpenhada + item.saldo;
                for (int j = 0; j < dt2.Rows.Count; j++) {
                    var naoAtendida = dt2.Rows[i]["NaoAtendida"].ToDecimal();
                    if (dt1.Rows[i]["ProdutoId"].ToInt() == dt2.Rows[i]["ProdutoId"].ToInt() &&
                        dt1.Rows[i]["CorId"].ToInt() == dt2.Rows[i]["CorId"].ToInt() &&
                        dt1.Rows[i]["TamanhoId"].ToInt() == dt2.Rows[i]["TamanhoId"].ToInt() &&
                        dt1.Rows[i]["AlmoxarifadoId"].ToInt() == dt2.Rows[i]["AlmoxarifadoId"].ToInt())
                    {
                        if (totalFabricado == 0)
                        {
                            item.venda = 0;
                            item.giro = 0;
                        }
                        else
                        {
                            item.venda = (qtdFaturada + qtdEmpenhada + naoAtendida) / (totalFabricado);
                            item.giro = (item.saldo) / (totalFabricado);
                        }
                        break;
                       
                    }
                } 
                listFocoVenda.Add(item);
            }
            */



        }


        /*public IEnumerable<FocoVendas> GetFocoVendas(FiltroRelatorioFocoVendas filtro)
        {
            var cn = new DapperConnection<FocoVendas>();
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT s.descricao as Segmento , i.imagem, p.Referencia, c.Abreviatura AS cor, t.Abreviatura AS tamanho, IFNULL(e.Saldo, 0) AS Saldo, ");
            SQL.AppendLine("IFNULL(((SUM(IFNULL(n.quantidade, 0)) - (SUM(IFNULL(n.Qtddevolvida, 0)) + IFNULL(d.Quantidade, 0))) + SUM(IFNULL(e.Empenhado, 0)) + SUM(IFNULL(ilp.QtdNaoAtendida, 0))) / ");
            SQL.AppendLine("(IFNULL(pd.TotalOp, 0)), 0) AS Venda, ");
            SQL.AppendLine("IFNULL( (IFNULL(pd.TotalOp, 0) - ((SUM(IFNULL(n.quantidade, 0)) - (SUM(IFNULL(n.Qtddevolvida, 0)) + IFNULL(d.Quantidade, 0))) + SUM(IFNULL(e.Empenhado, 0)) + SUM(IFNULL(ilp.QtdNaoAtendida, 0))) ) /  ");
            SQL.AppendLine("(IFNULL(pd.TotalOp, 0)), 0) AS Giro  ");
            SQL.AppendLine("FROM produtos p ");
            SQL.AppendLine("INNER JOIN produtodetalhes pd ON (pd.idProduto = p.id) ");
            SQL.AppendLine("INNER JOIN segmentos s ON (s.Id = p.idSegmento) ");
            SQL.AppendLine("INNER JOIN cores c ON (c.Id = pd.idCor) ");
            SQL.AppendLine("INNER JOIN tamanhos t ON (t.Id = pd.idTamanho) ");
            SQL.AppendLine("INNER JOIN almoxarifados a ON (a.Id = p.IdAlmoxarifado) ");
            SQL.AppendLine("LEFT JOIN nfeitens n ON (n.idItem = p.id AND n.idcor = pd.idcor AND n.idtamanho = pd.idtamanho) ");
            SQL.AppendLine("LEFT JOIN devolucaoitens d ON (d.idItem = p.id AND d.idcor = pd.idcor AND d.idtamanho = pd.idtamanho) ");
            SQL.AppendLine("LEFT JOIN estoque e ON (e.ProdutoId = p.Id AND e.corId = pd.idCor AND e.tamanhoId = pd.idTamanho and e.almoxarifadoid = a.id)) ");
            SQL.AppendLine("LEFT JOIN itenspedidovenda ip ON (ip.ProdutoId = p.id AND ip.CorId = pd.idcor AND ip.TamanhoId = pd.idtamanho)");
            SQL.AppendLine("LEFT JOIN itensliberacaopedidovenda ilp ON (ilp.ItemPedidoVendaID = ip.id) ");
            SQL.AppendLine("LEFT JOIN imagens i ON (i.idProduto = p.Id) ");
            SQL.AppendLine(" WHERE (a.idempresa IS NULL OR a.idempresa = " + VestilloSession.EmpresaLogada.Id + ")");
            if (filtro.segmentosIds != null && filtro.segmentosIds.Length > 0)
                SQL.AppendLine(" AND p.idSegmento IN (" + string.Join(", ", filtro.segmentosIds) + ")");            
            if (filtro.catalogosIds != null && filtro.catalogosIds.Length > 0)
                SQL.AppendLine("        AND p.idCatalogo IN (" + string.Join(", ", filtro.catalogosIds) + ")");
            if (filtro.colecoesIds != null && filtro.colecoesIds.Length > 0)
                SQL.AppendLine("        AND p.idColecao IN (" + string.Join(", ", filtro.colecoesIds) + ")");
            if (filtro.gruposIds != null && filtro.gruposIds.Length > 0)
                SQL.AppendLine("        AND p.idGrupo IN (" + string.Join(", ", filtro.gruposIds) + ")");
            if (filtro.corIds != null && filtro.corIds.Length > 0)
                SQL.AppendLine("        AND pd.idCor IN (" + string.Join(", ", filtro.corIds) + ")");
            if (filtro.tamanhosIds != null && filtro.tamanhosIds.Length > 0)
                SQL.AppendLine("        AND pd.idTamanho IN (" + string.Join(", ", filtro.tamanhosIds) + ")");
            
            SQL.AppendLine(" GROUP BY p.id, pd.idCor, pd.idTamanho");
            return cn.ExecuteStringSqlToList(new FocoVendas(), SQL.ToString());
        }*/


        public IEnumerable<ProdutoEtiqueta> GetProdutosParaEtiqueta(FiltroRelatorioEtiquetaProduto filtro)
        {
            var cn = new DapperConnection<ProdutoEtiqueta>();
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT p.descricao as DescProduto , p.Referencia as RefProduto, c.Abreviatura AS AbvCor, t.Abreviatura AS AbvTamanho, ");
            SQL.AppendLine("c.Descricao AS DescCor, t.Descricao AS DescTamanho, s.Descricao as Segmento, co.Descricao as Colecao, g.Nome as Grupo,");
            SQL.AppendLine("p.Ano AS Ano, pd.codbarras as codbarras, p.precovenda as preco");
            SQL.AppendLine("FROM produtos p ");
            SQL.AppendLine("LEFT JOIN produtodetalhes pd ON (pd.idProduto = p.id) ");
            SQL.AppendLine("LEFT JOIN segmentos s ON (s.Id = p.idSegmento) ");
            SQL.AppendLine("LEFT JOIN colecoes co ON (co.Id = p.idColecao) ");
            SQL.AppendLine("LEFT JOIN grupos g ON (g.Id = p.idGrupo) ");
            SQL.AppendLine("LEFT JOIN cores c ON (c.Id = pd.idCor) ");
            SQL.AppendLine("LEFT JOIN tamanhos t ON (t.Id = pd.idTamanho) ");
            SQL.AppendLine("LEFT JOIN imagens i ON (i.idProduto = p.Id) ");
            SQL.AppendLine("LEFT JOIN catalogo ca ON (ca.Id = p.IdCatalogo) ");

            if (filtro.produtosIds != null && filtro.produtosIds.Length > 0)
                SQL.AppendLine("WHERE p.id IN (" + string.Join(", ", filtro.produtosIds) + ")");
            else
                SQL.AppendLine("WHERE p.id = p.id ");

            if (filtro.segmentosIds != null && filtro.segmentosIds.Length > 0)
                SQL.AppendLine("        AND p.idSegmento IN (" + string.Join(", ", filtro.segmentosIds) + ")");

            if (filtro.colecoesIds != null && filtro.colecoesIds.Length > 0)
                SQL.AppendLine("        AND p.idColecao IN (" + string.Join(", ", filtro.colecoesIds) + ")");

            if (filtro.gruposIds != null && filtro.gruposIds.Length > 0)
                SQL.AppendLine("        AND p.idGrupo IN (" + string.Join(", ", filtro.gruposIds) + ")");

            if (filtro.corIds != null && filtro.corIds.Length > 0)
                SQL.AppendLine("        AND pd.idCor IN (" + string.Join(", ", filtro.corIds) + ")");

            if (filtro.tamanhosIds != null && filtro.tamanhosIds.Length > 0)
                SQL.AppendLine("        AND pd.idTamanho IN (" + string.Join(", ", filtro.tamanhosIds) + ")");

            if (filtro.doAno != "0000" || filtro.ateAno != "9999")
                SQL.AppendLine("        AND YEAR(p.ano) BETWEEN  '" + filtro.doAno + "' AND '" + filtro.ateAno + "' ");

            SQL.AppendLine(" GROUP BY p.id, pd.idCor, pd.idTamanho");

            return cn.ExecuteStringSqlToList(new ProdutoEtiqueta(), SQL.ToString());
        }

        public IEnumerable<ProdutoEtiqueta> GetProdutosParaEtiquetaOrdem(FiltroRelatorioEtiquetaProdutoOrdem filtro)
        {
            var cn = new DapperConnection<ProdutoEtiqueta>();
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT p.descricao as DescProduto , p.Referencia as RefProduto, c.Abreviatura AS AbvCor, t.Abreviatura AS AbvTamanho, ");
            SQL.AppendLine("c.Descricao AS DescCor, t.Descricao AS DescTamanho, s.Descricao as Segmento, co.Descricao as Colecao, g.Nome as Grupo,");
            SQL.AppendLine("p.Ano AS Ano, pd.codbarras as codbarras, iop.Quantidade as Quantidade, OP.Referencia AS Ordem, p.PrecoVenda as Preco");
            SQL.AppendLine("FROM itensordemproducao iop ");
            SQL.AppendLine("INNER JOIN produtos p ON (p.Id = iop.ProdutoId) ");
            SQL.AppendLine("INNER JOIN produtodetalhes pd ON (pd.idProduto = iop.ProdutoId AND pd.idCor = iop.CorId AND pd.idTamanho = iop.TamanhoId) ");
            SQL.AppendLine("INNER JOIN ordemproducao op ON (op.Id = iop.OrdemProducaoId) ");
            SQL.AppendLine("INNER JOIN cores c ON (c.Id = pd.idCor) ");
            SQL.AppendLine("INNER JOIN tamanhos t ON (t.Id = pd.idTamanho) ");
            SQL.AppendLine("LEFT JOIN segmentos s ON (s.Id = p.idSegmento) ");
            SQL.AppendLine("LEFT JOIN colecoes co ON (co.Id = p.idColecao) ");
            SQL.AppendLine("LEFT JOIN grupos g ON (g.Id = p.idGrupo) ");
            SQL.AppendLine("LEFT JOIN imagens i ON (i.idProduto = p.Id) ");
            SQL.AppendLine("LEFT JOIN catalogo ca ON (ca.Id = p.IdCatalogo) ");
            SQL.AppendLine("WHERE ");
            if (filtro.ordemStatus == 0)
            {
                SQL.Append("  op.Status != 0 ");
            }
            else if (filtro.ordemStatus == 1)
            {
                SQL.Append("  op.Status = 5 ");
            }
            else if (filtro.ordemStatus == 2)
            {
                SQL.Append(" op.Status != 5 ");
            }
            else if (filtro.ordemStatus == 3)
            {
                SQL.Append("  op.Status = 6 ");
            }


            if (filtro.ordensIds != null && filtro.ordensIds.Length > 0)
                SQL.AppendLine("        AND op.Id IN (" + string.Join(", ", filtro.ordensIds) + ")");

            if (filtro.daEmissao != "" || filtro.ateEmissao != "")
                SQL.AppendLine("        AND op.DataEmissao BETWEEN  '" + filtro.daEmissao + "' AND '" + filtro.ateEmissao + "' ");

            return cn.ExecuteStringSqlToList(new ProdutoEtiqueta(), SQL.ToString());
        }


        public IEnumerable<ProdutoEtiqueta> GetProdutosParaEtiquetaPedido(FiltroRelatorioEtiquetaProdutoPedidoVenda filtro)
        {
            var cn = new DapperConnection<ProdutoEtiqueta>();
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT p.descricao as DescProduto , p.Referencia as RefProduto, c.Abreviatura AS AbvCor, t.Abreviatura AS AbvTamanho, ");
            SQL.AppendLine("c.Descricao AS DescCor, t.Descricao AS DescTamanho, s.Descricao as Segmento, co.Descricao as Colecao, g.Nome as Grupo,");
            SQL.AppendLine("p.Ano AS Ano, pd.codbarras as codbarras, ilpv.Qtd as Quantidade, ipv.Preco as Preco");
            SQL.AppendLine("FROM itenspedidovenda ipv ");
            SQL.AppendLine("INNER JOIN itensliberacaopedidovenda ilpv ON (ipv.Id = ilpv.ItemPedidoVendaId) ");
            SQL.AppendLine("INNER JOIN produtos p ON (p.Id = ipv.ProdutoId) ");
            SQL.AppendLine("INNER JOIN produtodetalhes pd ON (pd.idProduto = ipv.ProdutoId AND pd.idCor = ipv.CorId AND pd.idTamanho = ipv.TamanhoId) ");
            SQL.AppendLine("INNER JOIN pedidovenda pv ON (pv.Id = ipv.PedidoVendaId) ");
            SQL.AppendLine("INNER JOIN cores c ON (c.Id = pd.idCor) ");
            SQL.AppendLine("INNER JOIN tamanhos t ON (t.Id = pd.idTamanho) ");
            SQL.AppendLine("LEFT JOIN segmentos s ON (s.Id = p.idSegmento) ");
            SQL.AppendLine("LEFT JOIN colecoes co ON (co.Id = p.idColecao) ");
            SQL.AppendLine("LEFT JOIN grupos g ON (g.Id = p.idGrupo) ");
            SQL.AppendLine("LEFT JOIN imagens i ON (i.idProduto = p.Id) ");
            SQL.AppendLine("LEFT JOIN catalogo ca ON (ca.Id = p.IdCatalogo) ");
            SQL.AppendLine("WHERE ");
            SQL.AppendLine("pv.Id IN (" + string.Join(", ", filtro.pedidosIds) + ")");

            if (filtro.clientesIds != null && filtro.clientesIds.Length > 0)
                SQL.AppendLine("        AND pv.ClienteId IN (" + string.Join(", ", filtro.clientesIds) + ")");

            if (filtro.vendedoresIds != null && filtro.vendedoresIds.Length > 0)
                SQL.AppendLine("        AND pv.VendedorId IN (" + string.Join(", ", filtro.vendedoresIds) + ")");

            if (filtro.daLiberacao != "" || filtro.ateLiberacao != "")
                SQL.AppendLine("        AND ilpv.Data BETWEEN  '" + filtro.daLiberacao + "' AND '" + filtro.ateLiberacao + "' ");


            return cn.ExecuteStringSqlToList(new ProdutoEtiqueta(), SQL.ToString());
        }

        public IEnumerable<ProdutoEtiqueta> GetProdutosParaEtiquetaPacote(FiltroRelatorioEtiquetaProdutoPacote filtro)
        {
            var cn = new DapperConnection<ProdutoEtiqueta>();
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT p.descricao as DescProduto , p.Referencia as RefProduto, c.Abreviatura AS AbvCor, t.Abreviatura AS AbvTamanho, ");
            SQL.AppendLine("c.Descricao AS DescCor, t.Descricao AS DescTamanho, s.Descricao as Segmento, co.Descricao as Colecao, g.Nome as Grupo,");
            SQL.AppendLine("p.Ano AS Ano, pd.codbarras as codbarras, pa.Quantidade as Quantidade, pa.Referencia as Pacote, p.PrecoVenda as Preco");
            SQL.AppendLine("FROM pacotes pa ");
            SQL.AppendLine("INNER JOIN produtos p ON (p.Id = pa.ProdutoId) ");
            SQL.AppendLine("INNER JOIN produtodetalhes pd ON (pd.idProduto = pa.ProdutoId AND pd.idCor = pa.CorId AND pd.idTamanho = pa.TamanhoId) ");
            SQL.AppendLine("INNER JOIN cores c ON (c.Id = pd.idCor) ");
            SQL.AppendLine("INNER JOIN tamanhos t ON (t.Id = pd.idTamanho) ");
            SQL.AppendLine("LEFT JOIN segmentos s ON (s.Id = p.idSegmento) ");
            SQL.AppendLine("LEFT JOIN colecoes co ON (co.Id = p.idColecao) ");
            SQL.AppendLine("LEFT JOIN grupos g ON (g.Id = p.idGrupo) ");
            SQL.AppendLine("LEFT JOIN imagens i ON (i.idProduto = p.Id) ");
            SQL.AppendLine("LEFT JOIN catalogo ca ON (ca.Id = p.IdCatalogo) ");
            if (filtro.pacotesIds != null && filtro.pacotesIds.Length > 0)
            {
                SQL.AppendLine("WHERE ");
                SQL.AppendLine("pa.Id IN (" + string.Join(", ", filtro.pacotesIds) + ")");
            }

            return cn.ExecuteStringSqlToList(new ProdutoEtiqueta(), SQL.ToString());
        }

        public IEnumerable<ProdutoEtiqueta> GetProdutosParaEtiquetaComposicao(FiltroRelatorioEtiquetaComposicao filtro)
        {
            var cn = new DapperConnection<ProdutoEtiqueta>();
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT p.descricao as DescProduto , p.Referencia as RefProduto, c.Abreviatura AS AbvCor, t.Abreviatura AS AbvTamanho, ");
            SQL.AppendLine("c.Descricao AS DescCor, t.Descricao AS DescTamanho, p.Id AS ProdutoId ");
            SQL.AppendLine("FROM produtos p ");
            SQL.AppendLine("LEFT JOIN produtodetalhes pd ON (pd.idProduto = p.id) ");
            SQL.AppendLine("LEFT JOIN cores c ON (c.Id = pd.idCor) ");
            SQL.AppendLine("LEFT JOIN tamanhos t ON (t.Id = pd.idTamanho) ");
            SQL.AppendLine("LEFT JOIN segmentos s ON (s.Id = p.idSegmento) ");
            SQL.AppendLine("LEFT JOIN colecoes co ON (co.Id = p.idColecao) ");

            if (filtro.produtosIds != null && filtro.produtosIds.Length > 0)
                SQL.AppendLine("WHERE p.id IN (" + string.Join(", ", filtro.produtosIds) + ")");
            else
                SQL.AppendLine("WHERE p.id = p.id ");

            if (filtro.tamanhosIds != null && filtro.tamanhosIds.Length > 0)
                SQL.AppendLine("        AND pd.idTamanho IN (" + string.Join(", ", filtro.tamanhosIds) + ")");

            if (filtro.segmentosIds != null && filtro.segmentosIds.Length > 0)
                SQL.AppendLine("        AND p.idSegmento IN (" + string.Join(", ", filtro.segmentosIds) + ")");

            if (filtro.colecoesIds != null && filtro.colecoesIds.Length > 0)
                SQL.AppendLine("        AND p.idColecao IN (" + string.Join(", ", filtro.colecoesIds) + ")");

            SQL.AppendLine(" GROUP BY p.id,  pd.idTamanho");

            return cn.ExecuteStringSqlToList(new ProdutoEtiqueta(), SQL.ToString());
        }

        public IEnumerable<OrdemProducaoMaterialView> GetListPorIdComFichaTecnicaMaterial(List<int> produtosId, int ordemId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT OPM.QuantidadeEmpenhada, OPM.QuantidadeBaixada, PM.Referencia as MaterialReferencia, PM.Descricao as MaterialDescricao, FI.materiaPrimaId, FI.materiaPrimaId as MateriaPrimaOriginalId, (SUM(FI.quantidade*IOP.Quantidade)) as QuantidadeNecessaria,");
            SQL.AppendLine("C.Abreviatura as CorDescricao, T.Abreviatura as TamanhoDescricao, ");
            SQL.AppendLine("C.ID as CorId, T.Id as TamanhoId, AF.abreviatura as ArmazemDescricao, AF.id as ArmazemId, OP.id as OrdemProducaoId, UMD.abreviatura as UM, OP.Referencia as OrdemProducaoReferencia,");
            SQL.AppendLine("FR.cor_produto_id as CorProdutoId, FR.tamanho_produto_id as TamanhoProdutoId, F.possuiquebra, FI.DestinoId AS Destino, FI.sequencia");
            SQL.AppendLine("FROM 	produtos P");
            SQL.AppendLine("INNER JOIN itensordemproducao IOP ON IOP.ProdutoId = P.Id ");
            SQL.AppendLine("INNER JOIN fichatecnicadomaterial F ON F.ProdutoId = P.Id ");
            SQL.AppendLine("INNER JOIN fichatecnicadomaterialitem FI ON FI.fichatecnicaid = F.Id ");
            SQL.AppendLine("INNER JOIN fichatecnicadomaterialrelacao FR ON ");
            SQL.AppendLine("(FR.fichatecnicaid = F.Id AND FR.materiaPrimaId = FI.materiaPrimaId AND FR.cor_produto_id = IOP.CorId AND FR.tamanho_produto_id = IOP.TamanhoId AND FR.fichatecnicaitemId = FI.Id) ");
            SQL.AppendLine("INNER JOIN produtos PM ON FI.materiaprimaid = PM.Id ");
            SQL.AppendLine("INNER JOIN cores C ON C.id = FR.cor_materiaprima_id ");
            SQL.AppendLine("INNER JOIN tamanhos T ON T.id = FR.tamanho_materiaprima_id ");
            SQL.AppendLine("INNER JOIN ordemproducao OP ON OP.id = IOP.OrdemProducaoId ");
            SQL.AppendLine("INNER JOIN almoxarifados AF ON AF.id = OP.AlmoxarifadoId ");
            SQL.AppendLine("INNER JOIN unidademedidas UMD ON UMD.id = PM.IdUniMedida ");
            SQL.AppendLine("LEFT JOIN ordemproducaomateriais OPM ON (OPM.OrdemProducaoId = IOP.OrdemProducaoId AND FR.cor_materiaprima_id = OPM.CorId AND FR.tamanho_materiaprima_id = OPM.TamanhoId AND OPM.materiaPrimaId = FR.materiaPrimaId)");
            SQL.AppendLine("WHERE   P.Id ");
            SQL.Append(" IN( ");
            SQL.Append(string.Join(",", produtosId));
            SQL.Append(") AND IOP.OrdemProducaoId = " + ordemId);
            SQL.AppendLine(" GROUP BY  FI.materiaPrimaId, FR.cor_materiaprima_id, FR.tamanho_materiaprima_id ");
            SQL.AppendLine("ORDER BY FI.sequencia");
            var cn = new DapperConnection<OrdemProducaoMaterialView>();
            return cn.ExecuteStringSqlToList(new OrdemProducaoMaterialView(), SQL.ToString());
        }

        public IEnumerable<OrdemProducaoMaterialView> GetListPorIdComFichaTecnicaMaterialSemOP(List<int> produtosId, int ordemId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT PM.Referencia as MaterialReferencia, PM.Descricao as MaterialDescricao, FI.materiaPrimaId, FI.materiaPrimaId as MateriaPrimaOriginalId, (SUM(FI.quantidade*IOP.Quantidade)) as QuantidadeNecessaria,");
            SQL.AppendLine("C.Abreviatura as CorDescricao, T.Abreviatura as TamanhoDescricao, ");
            SQL.AppendLine("C.ID as CorId, T.Id as TamanhoId, AF.abreviatura as ArmazemDescricao, AF.id as ArmazemId, OP.id as OrdemProducaoId, UMD.abreviatura as UM, OP.Referencia as OrdemProducaoReferencia,");
            SQL.AppendLine("FR.cor_produto_id as CorProdutoId, FR.tamanho_produto_id as TamanhoProdutoId, F.possuiquebra, FI.DestinoId AS Destino, FI.sequencia");
            SQL.AppendLine("FROM 	produtos P");
            SQL.AppendLine("INNER JOIN itensordemproducao IOP ON IOP.ProdutoId = P.Id ");
            SQL.AppendLine("INNER JOIN fichatecnicadomaterial F ON F.ProdutoId = P.Id ");
            SQL.AppendLine("INNER JOIN fichatecnicadomaterialitem FI ON FI.fichatecnicaid = F.Id ");
            SQL.AppendLine("INNER JOIN fichatecnicadomaterialrelacao FR ON ");
            SQL.AppendLine("(FR.fichatecnicaid = F.Id AND FR.materiaPrimaId = FI.materiaPrimaId AND FR.cor_produto_id = IOP.CorId AND FR.tamanho_produto_id = IOP.TamanhoId AND FR.fichatecnicaitemId = FI.Id) ");
            SQL.AppendLine("INNER JOIN produtos PM ON FI.materiaprimaid = PM.Id ");
            SQL.AppendLine("INNER JOIN cores C ON C.id = FR.cor_materiaprima_id ");
            SQL.AppendLine("INNER JOIN tamanhos T ON T.id = FR.tamanho_materiaprima_id ");
            SQL.AppendLine("INNER JOIN ordemproducao OP ON OP.id = IOP.OrdemProducaoId ");
            SQL.AppendLine("INNER JOIN almoxarifados AF ON AF.id = OP.AlmoxarifadoId ");
            SQL.AppendLine("INNER JOIN unidademedidas UMD ON UMD.id = PM.IdUniMedida ");
            SQL.AppendLine("WHERE   P.Id ");
            SQL.Append(" IN( ");
            SQL.Append(string.Join(",", produtosId));
            SQL.Append(") AND IOP.OrdemProducaoId = " + ordemId);
            SQL.AppendLine(" GROUP BY  FI.materiaPrimaId, FR.cor_materiaprima_id, FR.tamanho_materiaprima_id ");
            SQL.AppendLine("ORDER BY FI.sequencia");
            var cn = new DapperConnection<OrdemProducaoMaterialView>();
            return cn.ExecuteStringSqlToList(new OrdemProducaoMaterialView(), SQL.ToString());
        }

        public IEnumerable<OrdemProducaoMaterialView> GetListPorIdComFichaTecnicaMaterial(ItemOrdemProducaoView item, int ordemId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT PM.Referencia as MaterialReferencia, PM.Descricao as MaterialDescricao, FI.materiaPrimaId, FI.materiaPrimaId as MateriaPrimaOriginalId, (SUM(FI.quantidade*IOP.Quantidade)) as QuantidadeNecessaria,");
            SQL.AppendLine("C.Abreviatura as CorDescricao, T.Abreviatura as TamanhoDescricao, IOP.id as ItemOrdemProducaoId, ");
            SQL.AppendLine("C.ID as CorId, C.ID as CorOriginalId, T.Id as TamanhoId, T.Id as TamanhoOriginalId, AF.abreviatura as ArmazemDescricao, AF.id as ArmazemId, OP.id as OrdemProducaoId, UMD.abreviatura as UM, OP.Referencia as OrdemProducaoReferencia,");
            SQL.AppendLine("FR.cor_produto_id as CorProdutoId, FR.tamanho_produto_id as TamanhoProdutoId, FI.DestinoId, FI.sequencia, IFNULL(opm.EmpenhoProducao, 0) as EmpenhoProducao");
            SQL.AppendLine("FROM 	produtos P");
            SQL.AppendLine("INNER JOIN itensordemproducao IOP ON IOP.ProdutoId = P.Id ");
            SQL.AppendLine("INNER JOIN fichatecnicadomaterial F ON F.ProdutoId = P.Id ");
            SQL.AppendLine("INNER JOIN fichatecnicadomaterialitem FI ON FI.fichatecnicaid = F.Id ");
            SQL.AppendLine("INNER JOIN fichatecnicadomaterialrelacao FR ON ");
            SQL.AppendLine("(FR.fichatecnicaid = F.Id AND FR.materiaPrimaId = FI.materiaPrimaId AND FR.cor_produto_id = IOP.CorId AND FR.tamanho_produto_id = IOP.TamanhoId AND FR.fichatecnicaitemId = FI.Id) ");
            SQL.AppendLine("INNER JOIN produtos PM ON FI.materiaprimaid = PM.Id ");
            SQL.AppendLine("INNER JOIN cores C ON C.id = FR.cor_materiaprima_id ");
            SQL.AppendLine("INNER JOIN tamanhos T ON T.id = FR.tamanho_materiaprima_id ");
            SQL.AppendLine("INNER JOIN ordemproducao OP ON OP.id = IOP.OrdemProducaoId ");
            SQL.AppendLine("INNER JOIN almoxarifados AF ON AF.id = OP.AlmoxarifadoId ");
            SQL.AppendLine("INNER JOIN unidademedidas UMD ON UMD.id = PM.IdUniMedida ");
            SQL.AppendLine("LEFT JOIN ordemproducaomateriais OPM ON (OPM.itemordemproducaoid = IOP.Id AND FR.cor_materiaprima_id = OPM.CorId AND FR.tamanho_materiaprima_id = OPM.TamanhoId AND OPM.materiaPrimaId = FR.materiaPrimaId )");
            SQL.AppendLine("WHERE   P.Id = ");
            SQL.Append(item.ProdutoId + " AND IOP.CorId = " + item.CorId + "  AND IOP.TamanhoId = " + item.TamanhoId);
            SQL.Append(" AND IOP.Id = " + item.Id);
            SQL.Append(" AND IOP.OrdemProducaoId = " + ordemId);
            SQL.AppendLine(" GROUP BY  IOP.ID, FI.materiaPrimaId, FR.cor_materiaprima_id, FR.tamanho_materiaprima_id ");
            SQL.AppendLine(" ORDER BY PM.Referencia, C.Abreviatura, T.id");
            var cn = new DapperConnection<OrdemProducaoMaterialView>();
            return cn.ExecuteStringSqlToList(new OrdemProducaoMaterialView(), SQL.ToString());
        }

        public IEnumerable<CompraMaterial> GetListPorIdComFichaTecnicaMaterial(FiltroRelatorioCompraMaterial filtro)
        {
            var SQL = new StringBuilder();
            //SQL.AppendLine("SELECT (SELECT SUM(OPM.QuantidadeNecessaria - OPM.QuantidadeBaixada) FROM ordemproducaomateriais OPM ");
            //SQL.AppendLine("WHERE (FR.cor_materiaprima_id = OPM.CorId AND FR.tamanho_materiaprima_id = OPM.TamanhoId AND OPM.materiaPrimaId = FR.materiaPrimaId)) as OrdemProducaoLiberado, ");
            //SQL.AppendLine("PM.Referencia as Referencia, PM.Descricao as Material,");
            //SQL.AppendLine("C.Abreviatura as Cor, T.Abreviatura as Tamanho, (E.Saldo + E.Empenhado) as Fisico");
            //SQL.AppendLine("FROM 	produtos P");
            //SQL.AppendLine("INNER JOIN itensordemproducao IOP ON IOP.ProdutoId = P.Id  "); //AND IOP.Status <> 0
            //SQL.AppendLine("INNER JOIN fichatecnicadomaterial F ON F.ProdutoId = P.Id ");
            //SQL.AppendLine("INNER JOIN fichatecnicadomaterialitem FI ON FI.fichatecnicaid = F.Id ");
            //SQL.AppendLine("INNER JOIN fichatecnicadomaterialrelacao FR ON ");
            //SQL.AppendLine("(FR.fichatecnicaid = F.Id AND FR.materiaPrimaId = FI.materiaPrimaId AND FR.cor_produto_id = IOP.CorId AND FR.tamanho_produto_id = IOP.TamanhoId AND FR.fichatecnicaitemId = FI.Id) ");
            //SQL.AppendLine("INNER JOIN produtos PM ON FI.materiaprimaid = PM.Id ");
            //SQL.AppendLine("INNER JOIN cores C ON C.id = FR.cor_materiaprima_id ");
            //SQL.AppendLine("INNER JOIN tamanhos T ON T.id = FR.tamanho_materiaprima_id ");
            //SQL.AppendLine("INNER JOIN ordemproducao OP ON OP.id = IOP.OrdemProducaoId ");
            //SQL.AppendLine("LEFT JOIN estoque E ON (E.CorId = FR.cor_materiaprima_id AND E.TamanhoId = FR.tamanho_materiaprima_id AND E.ProdutoId = FR.materiaPrimaId)");
            ////SQL.AppendLine("WHERE   OP.Semana IN (" + string.Join(", ", filtro.semanas) + ")");
            ////SQL.Append(" IN( ");
            ////SQL.Append(item.ProdutoId + " AND IOP.CorId = " + item.CorId + "  AND IOP.TamanhoId = " + item.TamanhoId);
            ////SQL.Append(" AND IOP.OrdemProducaoId = " + ordemId);
            //SQL.AppendLine(" GROUP BY  FI.materiaPrimaId, FR.cor_materiaprima_id, FR.tamanho_materiaprima_id ");
            //SQL.AppendLine("ORDER BY PM.Referencia");

            SQL.AppendLine(" SELECT OrdemProducaoLiberado, Referencia, Material, Cor, Tamanho, Fisico FROM");
            SQL.AppendLine("(( SELECT SUM(OPM.QuantidadeNecessaria - OPM.QuantidadeBaixada) as OrdemProducaoLiberado, ");
            SQL.AppendLine("    PM.Referencia as Referencia, PM.Descricao as Material,");
            SQL.AppendLine("    C.Abreviatura as Cor, T.Abreviatura as Tamanho, (E.Saldo + E.Empenhado) as Fisico, ");
            SQL.AppendLine("    OPM.materiaPrimaId, OPM.corid, OPM.tamanhoid ");
            SQL.AppendLine("    FROM 	itensordemproducao IOP");
            SQL.AppendLine("    INNER JOIN ordemproducao OP ON OP.id = IOP.OrdemProducaoId  ");
            SQL.AppendLine("    INNER JOIN ordemproducaomateriais OPM ON OP.id = OPM.OrdemProducaoId AND OPM.ItemOrdemProducaoId = IOP.id  ");
            SQL.AppendLine("    INNER JOIN produtos PM ON OPM.materiaprimaid = PM.Id ");
            SQL.AppendLine("    INNER JOIN cores C ON C.id = OPM.corid AND C.id <> 999");
            SQL.AppendLine("    INNER JOIN tamanhos T ON T.id = OPM.tamanhoid AND T.id <> 999 ");
            SQL.AppendLine("    LEFT JOIN estoque E ON (E.CorId = OPM.corid AND E.TamanhoId = OPM.tamanhoid AND E.ProdutoId = OPM.materiaPrimaId)");
            SQL.AppendLine("    WHERE OP.Status <> 6 ");
            SQL.AppendLine("    GROUP BY  OPM.materiaPrimaId, OPM.corid, OPM.tamanhoid ");
            SQL.AppendLine("    ORDER BY PM.Referencia )");
            SQL.AppendLine(" UNION ALL ");
            SQL.AppendLine("( SELECT 0 as OrdemProducaoLiberado, ");
            SQL.AppendLine("    PM.Referencia as Referencia, PM.Descricao as Material,");
            SQL.AppendLine("    C.Abreviatura as Cor, T.Abreviatura as Tamanho, (E.Saldo + E.Empenhado) as Fisico,");
            SQL.AppendLine("    f.materiaprimaid, f.cor_materiaprima_Id as corid, f.tamanho_materiaprima_Id as tamanhoid");
            SQL.AppendLine("    FROM 	itensordemproducao IOP");
            SQL.AppendLine("    INNER JOIN ordemproducao OP ON OP.id = IOP.OrdemProducaoId  ");
            SQL.AppendLine("    INNER JOIN fichatecnicadomaterialrelacao f ON f.produtoId = IOP.ProdutoId AND IOP.CorId = f.cor_produto_id AND IOP.TamanhoId = f.tamanho_produto_Id");
            SQL.AppendLine("    INNER JOIN produtos PM ON f.materiaprimaid = PM.Id ");
            SQL.AppendLine("    INNER JOIN cores C ON C.id = f.cor_materiaprima_Id AND C.id <> 999");
            SQL.AppendLine("    INNER JOIN tamanhos T ON T.id = f.tamanho_materiaprima_Id AND T.id <> 999 ");
            SQL.AppendLine("    LEFT JOIN estoque E ON (E.CorId = f.cor_materiaprima_Id AND E.TamanhoId = f.tamanho_materiaprima_Id AND E.ProdutoId = f.materiaprimaid)");
            SQL.AppendLine("    WHERE IOP.Status = 0 AND OP.Status <> 6");
            SQL.AppendLine("    GROUP BY  f.materiaprimaid, f.cor_materiaprima_Id, f.tamanho_materiaprima_Id ");
            SQL.AppendLine("    ORDER BY PM.Referencia )) AS t");
            SQL.AppendLine(" GROUP BY materiaPrimaId, corid, tamanhoid ");
            SQL.AppendLine(" ORDER BY Referencia ");

            var cn = new DapperConnection<CompraMaterial>();
            //return cn.ExecuteStringSqlToList(new CompraMaterial(), SQL.ToString());
            var ret = cn.ExecuteStringSqlToList(new CompraMaterial(), SQL.ToString()).ToList();

            var semana1 = new OrdemProducaoMaterialRepository().GetListCompraMaterialSemana1(filtro.semana1).ToList();
            var semana2 = new OrdemProducaoMaterialRepository().GetListCompraMaterialSemana(filtro.semana2).ToList();
            var semana3 = new OrdemProducaoMaterialRepository().GetListCompraMaterialSemana(filtro.semana3).ToList();
            var semana4 = new OrdemProducaoMaterialRepository().GetListCompraMaterialSemana(filtro.semana4).ToList();
            var semana5 = new OrdemProducaoMaterialRepository().GetListCompraMaterialSemana(filtro.semana5).ToList();
            var semana6 = new OrdemProducaoMaterialRepository().GetListCompraMaterialSemana(filtro.semana6).ToList();
            var semana7 = new OrdemProducaoMaterialRepository().GetListCompraMaterialSemana(filtro.semana7).ToList();
            var semana8 = new OrdemProducaoMaterialRepository().GetListCompraMaterialSemana(filtro.semana8).ToList();

            ret.ForEach(r =>
            {
                var sem1 = semana1.Find(s => s.Material == r.Material && s.Cor == r.Cor && s.Tamanho == r.Tamanho);
                if (sem1 != null)
                {
                    r.Consumo1 = sem1.Consumo;
                    r.Compra1 = sem1.Compra;
                }

                var sem2 = semana2.Find(s => s.Material == r.Material && s.Cor == r.Cor && s.Tamanho == r.Tamanho);
                if (sem2 != null)
                {
                    r.Consumo2 = sem2.Consumo;
                    r.Compra2 = sem2.Compra;
                }

                var sem3 = semana3.Find(s => s.Material == r.Material && s.Cor == r.Cor && s.Tamanho == r.Tamanho);
                if (sem3 != null)
                {
                    r.Consumo3 = sem3.Consumo;
                    r.Compra3 = sem3.Compra;
                }

                var sem4 = semana4.Find(s => s.Material == r.Material && s.Cor == r.Cor && s.Tamanho == r.Tamanho);
                if (sem4 != null)
                {
                    r.Consumo4 = sem4.Consumo;
                    r.Compra4 = sem4.Compra;
                }

                var sem5 = semana5.Find(s => s.Material == r.Material && s.Cor == r.Cor && s.Tamanho == r.Tamanho);
                if (sem5 != null)
                {
                    r.Consumo5 = sem5.Consumo;
                    r.Compra5 = sem5.Compra;
                }

                var sem6 = semana6.Find(s => s.Material == r.Material && s.Cor == r.Cor && s.Tamanho == r.Tamanho);
                if (sem6 != null)
                {
                    r.Consumo6 = sem6.Consumo;
                    r.Compra6 = sem6.Compra;
                }

                var sem7 = semana7.Find(s => s.Material == r.Material && s.Cor == r.Cor && s.Tamanho == r.Tamanho);
                if (sem7 != null)
                {
                    r.Consumo7 = sem7.Consumo;
                    r.Compra7 = sem7.Compra;
                }

                var sem8 = semana8.Find(s => s.Material == r.Material && s.Cor == r.Cor && s.Tamanho == r.Tamanho);
                if (sem8 != null)
                {
                    r.Consumo8 = sem8.Consumo;
                    r.Compra8 = sem8.Compra;
                }
            });

            return ret;
        }

        public bool ExisteEAnDuplicado()
        {
            bool Duplicado = false;
            string SQL = String.Empty;
            var cn = new DapperConnection<ProdutoDetalhe>();


            SQL = " SELECT codbarras,COUNT(*) " +
                  "  from produtodetalhes " +
                  "  group by codbarras " +
                  "  having count(*) > 1 and codbarras<> ''";

            var dados = cn.ExecuteStringSqlToList(new ProdutoDetalhe(), SQL.ToString()).ToList();

            if (dados != null && dados.Count > 0)
            {
                Duplicado = true;
            }

            return Duplicado;
        }

        public bool ProdutoJaCadastrado(string RefProduto)
        {
            bool Duplicado = false;
            string SQL = String.Empty;
            var cn = new DapperConnection<Produto>();


            SQL = " SELECT * from produtos where Referencia = " + "'" + RefProduto + "'" + " AND Idempresa = " + VestilloSession.EmpresaLogada.Id;

            var dados = cn.ExecuteStringSqlToList(new Produto(), SQL.ToString()).ToList();

            if (dados != null && dados.Count > 0)
            {
                Duplicado = true;
            }

            return Duplicado;
        }

        public void IncluirAlterarProdutoProtheus(int IdProduto, decimal TempoTotal, decimal TempoInterno)
        {

            try
            {
                string SQL = String.Empty;
                var cn = new DapperConnection<TemposProtheus>();

                SQL = " SELECT CONCAT(produtos.Referencia,'-',cores.Abreviatura,'-',tamanhos.Abreviatura) as Produto  from produtodetalhes " +
                      " INNER JOIN cores ON cores.Id = produtodetalhes.Idcor " +
                      "  INNER JOIN tamanhos ON tamanhos.Id = produtodetalhes.IdTamanho " +
                      "  INNER JOIN produtos ON produtos.Id = produtodetalhes.IdProduto " +
                      "  WHERE produtodetalhes.IdProduto = " + IdProduto;

                var dados = cn.ExecuteStringSqlToList(new TemposProtheus(), SQL.ToString()).ToList();

                if (dados != null && dados.Count > 0)
                {
                    foreach (var item in dados)
                    {
                        string SQL2 = String.Empty;
                        SQL2 = " SELECT * FROM TemposProtheus where TemposProtheus.Produto =  " + "'" + item.Produto + "'";
                        var dados2 = cn.ExecuteStringSqlToList(new TemposProtheus(), SQL2.ToString()).ToList();

                        string SQL3 = String.Empty;
                        if (dados2 != null && dados2.Count > 0)
                        {

                            SQL3 = " DELETE FROM temposprotheus  WHERE Produto = " + "'" + item.Produto + "'";
                            cn.ExecuteNonQuery(SQL3);

                            SQL3 = "  insert into temposprotheus (Produto, TempoTotal, TempoMenosInterno, Status) " +
                                   " values( " +
                                  "'" + item.Produto + "'" + "," + TempoTotal.ToString().Replace(",", ".") + "," + TempoInterno.ToString().Replace(",", ".") + ",0)";
                            cn.ExecuteNonQuery(SQL3);
                        }
                        else
                        {
                            SQL3 = "  insert into temposprotheus (Produto, TempoTotal, TempoMenosInterno, Status) " +
                                   " values( " +
                                  "'" + item.Produto + "'" + "," + TempoTotal.ToString().Replace(",", ".") + "," + TempoInterno.ToString().Replace(",", ".") + ",0)";
                            cn.ExecuteNonQuery(SQL3);
                        }

                    }
                }


            }
            catch (Exception ex)
            {
                throw new Vestillo.Lib.VestilloException(ex);
            }


        }

        public void UpdateDescricaoMarketPlace(int ItemId, string DescMarketPlace, string EanPai)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("UPDATE produtos SET ");
            SQL.AppendLine("DescricaoMarketPlace = ");
            SQL.Append("'" + DescMarketPlace + "'");
            SQL.AppendLine(" , CodigoBarrarEcommerce = ");
            SQL.Append("'" + EanPai + "'");
            SQL.AppendLine(" WHERE id = ");
            SQL.Append(ItemId);
            _cn.ExecuteNonQuery(SQL.ToString());
        }

        public void UpdateGradeMarketPlace(int ItemId, int CorId, int TamanhoId, string EanFilho)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("update produtodetalhes set ");
            SQL.AppendLine("codbarras  = ");
            SQL.Append("'" + EanFilho + "'");
            SQL.AppendLine(" WHERE IdProduto = ");
            SQL.Append(ItemId);
            SQL.AppendLine(" AND Idcor  = ");
            SQL.Append(CorId);
            SQL.AppendLine(" AND IdTamanho  = ");
            SQL.Append(TamanhoId);
            _cn.ExecuteNonQuery(SQL.ToString());
        }

        public void UpdateNotaEntrada(int ItemId, decimal Compra, decimal Lucro, decimal Venda)
        {
            Venda = decimal.Round(Venda, 2);
            var SQL = new StringBuilder();
            SQL.AppendLine("update produtos set ");
            SQL.AppendLine("PrecoCompra  = ");
            SQL.Append(Compra.ToString().Replace(",", "."));
            SQL.AppendLine(",lucro  = ");
            SQL.Append(Lucro.ToString().Replace(",", "."));
            SQL.AppendLine(",PrecoVenda  = ");
            SQL.Append(Venda.ToString().Replace(",", "."));
            SQL.AppendLine(" WHERE Id = ");
            SQL.Append(ItemId);

            _cn.ExecuteNonQuery(SQL.ToString());
        }


        public IEnumerable<Produto> GetListPorFiltros(int tipoItem, string referencia, string descricao, string colecao)
        {
            var sql = new StringBuilder();
            string filtro = string.Empty;

            sql.AppendLine("select produtos.* from produtos left join colecoes on colecoes.id = produtos.Idcolecao ");

            validarTipoDoItemDoProduto(tipoItem, ref filtro);
            validarReferenciaDoProduto(referencia, ref filtro);
            validarDescricaoDoProduto(descricao, ref filtro);
            validarColecaoDoProduto(colecao, ref filtro);

            sql.AppendLine(filtro);

            return _cn.ExecuteStringSqlToList(new Produto(), sql.ToString());
        }

        public IEnumerable<Produto> GetListMaterialPorFiltros(int tipoItem, string referencia, string descricao, string grupo, string fornecedor)
        {
            var sql = new StringBuilder();
            string filtro = string.Empty;

            if (!string.IsNullOrEmpty(fornecedor))
            {
                sql.AppendLine("select distinct produtos.* from produtos  ");
            }
            else
            {
                sql.AppendLine("select  produtos.* from produtos  ");
            }
            if (!string.IsNullOrEmpty(grupo))
            {
                sql.AppendLine(" left join grupoprodutos on (grupoprodutos.id=produtos.IdGrupo) ");
            }
            if (!string.IsNullOrEmpty(fornecedor))
            {
                sql.AppendLine("left join produtofornecedorprecos  on (produtofornecedorprecos.IdProduto = produtos.Id) inner join colaboradores  on (colaboradores.id = produtofornecedorprecos.IdFornecedor and colaboradores.ativo=1)");
            }
            validarTipoDoItemDoProduto(tipoItem, ref filtro);
            validarReferenciaDoProduto(referencia, ref filtro);
            validarDescricaoDoProduto(descricao, ref filtro);
            validarGrupoDeProduto(grupo, ref filtro);
            validarFornecedor(fornecedor, ref filtro);

            sql.AppendLine(filtro);

            return _cn.ExecuteStringSqlToList(new Produto(), sql.ToString());
        }

        public IEnumerable<Produto> GetListGrupoDeProduto(string grupo)
        {
            var sql = new StringBuilder();
            string filtro = string.Empty;
            sql.AppendLine("select  produtos.* from produtos  ");
            sql.AppendLine(" join grupoprodutos on (grupoprodutos.id=produtos.IdGrupo) ");
            validarGrupoDeProduto(grupo, ref filtro, true);
            sql.AppendLine(filtro);

            return _cn.ExecuteStringSqlToList(new Produto(), sql.ToString());
        }

        private void montarFiltro(ref string filtro, string sql)
        {
            var str = string.Empty;
            if (string.IsNullOrEmpty(filtro))
                filtro += " where ";
            else
                filtro += " and ";

            filtro += sql;
        }


        private void validarTipoDoItemDoProduto(int tipoItem, ref string filtro)
        {
            if (tipoItem != -1)
            {
                var sql = string.Format(" produtos.tipoitem={0}", tipoItem);
                montarFiltro(ref filtro, sql);
            }
        }

        private void validarReferenciaDoProduto(string referencia, ref string filtro)
        {
            if (!string.IsNullOrEmpty(referencia))
            {
                var sql = "produtos.referencia like'%" + referencia + "%'";
                montarFiltro(ref filtro, sql);
            }
        }

        private void validarDescricaoDoProduto(string descricao, ref string filtro)
        {
            if (!string.IsNullOrEmpty(descricao))
            {
                var sql = " produtos.descricao like '%" + descricao + "%'";
                montarFiltro(ref filtro, sql);
            }
        }

        private void validarGrupoDeProduto(string grupo, ref string filtro, bool igual = false)
        {
            if (!string.IsNullOrEmpty(grupo))
            {
                string sql = string.Empty;
                if (!igual)
                    sql = " grupoprodutos.descricao  like '%" + grupo + "%'";
                else
                    sql = " grupoprodutos.descricao='" + grupo + "'";
                montarFiltro(ref filtro, sql);
            }
        }

        private void validarFornecedor(string fornecedor, ref string filtro)
        {
            if (!string.IsNullOrEmpty(fornecedor))
            {
                var sql = "colaboradores.Nome like '%" + fornecedor + "%'";
                montarFiltro(ref filtro, sql);
            }
        }

        private void validarColecaoDoProduto(string colecao, ref string filtro)
        {
            if (!string.IsNullOrEmpty(colecao))
            {
                var sql = "colecoes.descricao like '%" + colecao + "%'";
                montarFiltro(ref filtro, sql);
            }
        }

    }
}