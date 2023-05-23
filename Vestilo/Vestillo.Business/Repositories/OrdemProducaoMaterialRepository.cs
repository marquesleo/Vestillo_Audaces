using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class OrdemProducaoMaterialRepository: GenericRepository<OrdemProducaoMaterial>
    {
        public OrdemProducaoMaterialRepository()
            : base(new DapperConnection<OrdemProducaoMaterial>())
        {
        }

        public IEnumerable<OrdemProducaoMaterialView> GetByOrdemIdView(int ordemId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT opm.armazemid, opm.corid, opm.cororiginalid, opm.tamanhoid, opm.tamanhooriginalid, sum(opm.quantidadenecessaria) as quantidadenecessaria,");
            SQL.AppendLine(" sum(opm.quantidadeempenhada) as quantidadeempenhada, sum(opm.quantidadebaixada) as quantidadebaixada, opm.materiaprimaid, opm.materiaprimaoriginalid,opm.OrdemProducaoId,IF(sum(opm.quantidadenecessaria) - sum(opm.quantidadebaixada) < 0, 0,sum(opm.quantidadenecessaria) - sum(opm.quantidadebaixada))  as Falta,");
            SQL.AppendLine(" p.Referencia as MaterialReferencia,");
            SQL.AppendLine(" p.Descricao as MaterialDescricao,");
            SQL.AppendLine(" c.abreviatura as CorDescricao,");
            SQL.AppendLine(" t.abreviatura as TamanhoDescricao,");
            SQL.AppendLine(" o.Referencia as OrdemProducaoReferencia,");
            SQL.AppendLine(" o.Status as OrdemProducaoStatus,");
            SQL.AppendLine(" a.abreviatura as ArmazemDescricao,");
            SQL.AppendLine(" u.abreviatura as UM, ");
            SQL.AppendLine(" sum( IFNULL(opm.EmpenhoProducao, 0)) as EmpenhoLivre ");
            SQL.AppendLine("FROM 	ordemproducaomateriais opm");
            SQL.AppendLine("INNER JOIN ordemproducao o ON o.Id = opm.OrdemProducaoId ");
            SQL.AppendLine("INNER JOIN itensordemproducao IOP ON IOP.Id = opm.ItemOrdemProducaoId ");
            SQL.AppendLine("INNER JOIN produtos p ON p.Id = opm.materiaprimaid ");
            SQL.AppendLine("INNER JOIN cores c ON c.Id = opm.corid ");
            SQL.AppendLine("INNER JOIN tamanhos t ON t.Id = opm.tamanhoid ");
            SQL.AppendLine("LEFT JOIN almoxarifados a ON a.Id = opm.armazemid ");
            SQL.AppendLine("LEFT JOIN unidademedidas u ON u.Id = p.IdUniMedida ");
            SQL.AppendLine("WHERE	opm.OrdemProducaoId = ");
            SQL.Append(ordemId);
            SQL.AppendLine(" GROUP BY opm.materiaprimaid, opm.corid, opm.tamanhoid ");
            SQL.AppendLine(" ORDER BY p.Referencia, C.Abreviatura, T.id ");


            var cn = new DapperConnection<OrdemProducaoMaterialView>();
            return cn.ExecuteStringSqlToList(new OrdemProducaoMaterialView(), SQL.ToString());
        }

        public IEnumerable<OrdemProducaoMaterialView> GetByOrdensIdView(int ordemId)
        {
            var SQL = new StringBuilder();

            SQL.AppendLine("SELECT opm.*,");
            SQL.AppendLine("p.Referencia as MaterialReferencia,");
            SQL.AppendLine("p.Descricao as MaterialDescricao,");
            SQL.AppendLine("c.abreviatura as CorDescricao,");
            SQL.AppendLine("t.abreviatura as TamanhoDescricao,");
            SQL.AppendLine("o.Referencia as OrdemProducaoReferencia,");
            SQL.AppendLine("a.abreviatura as ArmazemDescricao,");
            SQL.AppendLine("u.abreviatura as UM");
            SQL.AppendLine("FROM 	ordemproducaomateriais opm");
            SQL.AppendLine("INNER JOIN ordemproducao o ON o.Id = opm.OrdemProducaoId ");
            SQL.AppendLine("INNER JOIN itensordemproducao IOP ON IOP.Id = opm.ItemOrdemProducaoId ");
            SQL.AppendLine("INNER JOIN produtos p ON p.Id = opm.materiaprimaid ");
            SQL.AppendLine("INNER JOIN cores c ON c.Id = opm.corid ");
            SQL.AppendLine("INNER JOIN tamanhos t ON t.Id = opm.tamanhoid ");
            SQL.AppendLine("LEFT JOIN almoxarifados a ON a.Id = opm.armazemid ");
            SQL.AppendLine("LEFT JOIN unidademedidas u ON u.Id = p.IdUniMedida ");
            SQL.AppendLine("WHERE	opm.OrdemProducaoId = ");
            SQL.Append(ordemId);
            SQL.AppendLine(" ORDER BY p.Referencia, C.Abreviatura, T.id, opm.id");

            var cn = new DapperConnection<OrdemProducaoMaterialView>();
            return cn.ExecuteStringSqlToList(new OrdemProducaoMaterialView(), SQL.ToString());
        }

        public OrdemProducaoMaterialView GetEmpenhoLivreByOrdem(OrdemProducaoMaterialView ordem)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT e.Saldo,  e.Empenhado, SUM(IFNULL(opm.EmpenhoProducao, 0)) AS EmpenhoLivre ");
            SQL.AppendLine(" FROM 	estoque e ");
            SQL.AppendLine(" LEFT JOIN ordemproducaomateriais opm ON opm.MateriaPrimaId = e.ProdutoId AND opm.CorId = e.CorId AND opm.armazemid = e.almoxarifadoid ");
            SQL.AppendLine("    AND opm.TamanhoId = e.TamanhoId AND opm.OrdemProducaoId = " + ordem.OrdemProducaoId +" AND opm.ItemOrdemProducaoId > 0 ");            
            SQL.AppendLine(" INNER JOIN Almoxarifados A ON A.Id = E.AlmoxarifadoId");
            SQL.AppendLine(" WHERE " + FiltroEmpresa("A.IdEmpresa"));
            SQL.AppendLine(" AND e.produtoid = ");
            SQL.Append(ordem.MateriaPrimaId);
            SQL.AppendLine(" AND	e.corid = ");
            SQL.Append(ordem.CorId);
            SQL.AppendLine(" AND	e.tamanhoid = ");
            SQL.Append(ordem.TamanhoId);
            SQL.AppendLine(" AND	e.almoxarifadoid = ");
            SQL.Append(ordem.ArmazemId);
            SQL.AppendLine(" GROUP BY e.produtoid, e.corid, e.tamanhoid ");

            var cn = new DapperConnection<OrdemProducaoMaterialView>();
            var materialOrdem = new OrdemProducaoMaterialView();
            cn.ExecuteToModel(ref materialOrdem, SQL.ToString());
            return materialOrdem;
        }

        public IEnumerable<OrdemProducaoMaterialView> GetListByItemComFichaTecnicaMaterial(List<int> idsIOP, int ordemId, bool agruparItem)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT PM.Referencia as MaterialReferencia, PM.Descricao as MaterialDescricao, FI.materiaPrimaId, FI.materiaPrimaId as MateriaPrimaOriginalId, (SUM(FI.quantidade*IOP.Quantidade)) as QuantidadeNecessaria,");
            SQL.AppendLine("C.Abreviatura as CorDescricao, T.Abreviatura as TamanhoDescricao, IOP.id as ItemOrdemProducaoId, ");
            SQL.AppendLine("C.ID as CorId, C.ID as CorOriginalId, T.Id as TamanhoId, T.Id as TamanhoOriginalId, AF.abreviatura as ArmazemDescricao, AF.id as ArmazemId, OP.id as OrdemProducaoId, UMD.abreviatura as UM, OP.Referencia as OrdemProducaoReferencia,");
            SQL.AppendLine("FR.cor_produto_id as CorProdutoId, FR.tamanho_produto_id as TamanhoProdutoId, FI.DestinoId, FI.sequencia "); 
            
            if(agruparItem)
                SQL.AppendLine(", IFNULL(opm.EmpenhoProducao, 0) as EmpenhoProducao");

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

            if(agruparItem)
                SQL.AppendLine("LEFT JOIN ordemproducaomateriais OPM ON (OPM.itemordemproducaoid = IOP.Id AND FR.cor_materiaprima_id = OPM.CorId AND FR.tamanho_materiaprima_id = OPM.TamanhoId AND OPM.materiaPrimaId = FR.materiaPrimaId )");

            SQL.AppendLine("WHERE  ");
            SQL.Append(" IOP.Id IN ( " + string.Join(",", idsIOP) + " ) ");
            SQL.Append(" AND IOP.OrdemProducaoId = " + ordemId);

            //ESSA QUERY FOI ALTERADA incluir o AGRUPAMENTO PELO FI.sequencia , PARA QUE OS MATERIAIS IGUAL APARECEM SEPARADOS DIA 28/10 2022
            //desfeita a lateração 03/11/2022
            if (agruparItem)
            {
                if (VestilloSession.AgrupaLiberacaoOpSequencia)
                {
                    SQL.AppendLine(" GROUP BY  FI.sequencia,IOP.ID, FI.materiaPrimaId, FR.cor_materiaprima_id, FR.tamanho_materiaprima_id ");
                }
                else
                {
                    SQL.AppendLine(" GROUP BY  IOP.ID, FI.materiaPrimaId, FR.cor_materiaprima_id, FR.tamanho_materiaprima_id ");
                }
            }   
            else
            {
                if (VestilloSession.AgrupaLiberacaoOpSequencia)
                {
                    SQL.AppendLine(" GROUP BY  FI.sequencia,FI.materiaPrimaId, FR.cor_materiaprima_id, FR.tamanho_materiaprima_id ");
                }
                else
                {
                    SQL.AppendLine(" GROUP BY  FI.materiaPrimaId, FR.cor_materiaprima_id, FR.tamanho_materiaprima_id ");
                }
                    
            }

                

            SQL.AppendLine(" ORDER BY PM.Referencia, C.Abreviatura, T.id");
            var cn = new DapperConnection<OrdemProducaoMaterialView>();
            return cn.ExecuteStringSqlToList(new OrdemProducaoMaterialView(), SQL.ToString());
        }

        public IEnumerable<OrdemProducaoMaterialView> GetByOrdenEItem(int itemId, int ordemId)
        {
            //ESSA QUERY FOI ALTERADA PARA RETIRAR OS GROUP BYS, PARA QUE OS MATERIAIS IGUAL APARECEM SEPARADOS DIA 28/10/2022
            //Alteração desfeita 03/11/2022
            var SQL = new StringBuilder();

            bool QuebraManual = PossuiQuebraManual(itemId);


            SQL.AppendLine("SELECT MaterialReferencia, MaterialDescricao, materiaPrimaId, MateriaPrimaOriginalId, (SUM(QuantidadeNecessaria)) as QuantidadeNecessaria,");
            SQL.AppendLine("sum(quantidadeempenhada) as quantidadeempenhada, sum(quantidadebaixada) as quantidadebaixada,");
            SQL.AppendLine("CorDescricao, TamanhoDescricao, ");
            SQL.AppendLine("CorId, TamanhoId,  ArmazemDescricao,  ArmazemId,  OrdemProducaoId,  UM,  OrdemProducaoReferencia,");
            SQL.AppendLine(" possuiquebra, QuebraManual, Destino, sequencia FROM ( ");

            SQL.AppendLine("(SELECT PM.Referencia as MaterialReferencia, PM.Descricao as MaterialDescricao, FI.materiaPrimaId, FI.materiaPrimaId as MateriaPrimaOriginalId, (SUM(FI.quantidade*IOP.Quantidade)) as QuantidadeNecessaria,");
            SQL.AppendLine("0 as quantidadeempenhada, 0 as quantidadebaixada,");
            SQL.AppendLine("C.Abreviatura as CorDescricao, T.Abreviatura as TamanhoDescricao, ");
            SQL.AppendLine("C.ID as CorId, T.Id as TamanhoId, AF.abreviatura as ArmazemDescricao, AF.id as ArmazemId, OP.id as OrdemProducaoId, UMD.abreviatura as UM, OP.Referencia as OrdemProducaoReferencia,");
            SQL.AppendLine("F.possuiquebra, F.QuebraManual, d.descricao AS Destino, FI.sequencia");
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
            SQL.AppendLine("LEFT JOIN destinos d ON d.Id = FI.DestinoId ");
            SQL.AppendLine("WHERE   OP.Id = ");
            SQL.Append(ordemId);
            SQL.Append(" AND IOP.id = " + itemId);
            SQL.Append(" AND ( IOP.Status = 0)");
            SQL.Append(" AND ( FR.cor_materiaprima_id <> 999 AND FR.tamanho_materiaprima_id <> 999 )");

            // teste relatório 21/11/2022
            if(QuebraManual)
            {
                SQL.AppendLine(" GROUP BY  FI.sequencia, FI.materiaPrimaId, FR.cor_materiaprima_id, FR.tamanho_materiaprima_id ) ");
            }
            else
            {
                SQL.AppendLine(" GROUP BY  FI.materiaPrimaId, FR.cor_materiaprima_id, FR.tamanho_materiaprima_id ) ");
            }
            
            
            //SQL.AppendLine("ORDER BY FI.sequencia");

            SQL.AppendLine("UNION ALL");

            SQL.AppendLine(" ( SELECT p.Referencia as MaterialReferencia, ");
            SQL.AppendLine("p.Descricao as MaterialDescricao, "); //F.possuiquebra, FI.DestinoId AS Destino, FI.sequencia,
            SQL.AppendLine("opm.materiaprimaid, opm.materiaprimaoriginalid, sum(opm.quantidadenecessaria) as quantidadenecessaria, ");
            SQL.AppendLine("sum(opm.quantidadeempenhada) as quantidadeempenhada, sum(opm.quantidadebaixada) as quantidadebaixada,");
            SQL.AppendLine("c.abreviatura as CorDescricao,");
            SQL.AppendLine("t.abreviatura as TamanhoDescricao,");
            SQL.AppendLine("opm.corid, opm.tamanhoid, a.abreviatura as ArmazemDescricao,  opm.armazemid,O.id as OrdemProducaoId,");
            SQL.AppendLine("u.abreviatura as UM,");
            SQL.AppendLine("o.Referencia as OrdemProducaoReferencia, F.possuiquebra, F.QuebraManual, d.descricao AS Destino, opm.sequencia");
            SQL.AppendLine("FROM 	ordemproducaomateriais opm");
            SQL.AppendLine("INNER JOIN ordemproducao o ON o.Id = opm.OrdemProducaoId ");
            SQL.AppendLine("INNER JOIN itensordemproducao IOP ON IOP.Id = opm.ItemOrdemProducaoId ");

            //SQL.AppendLine("INNER JOIN fichatecnicadomaterialrelacao FR ON ");
            //SQL.AppendLine("(FR.produtoId = IOP.ProdutoId   AND FR.cor_produto_Id = IOP.CorId AND FR.Tamanho_produto_Id = IOP.Tamanhoid");
            //SQL.AppendLine("AND FR.materiaPrimaId = OPM.materiaprimaoriginalid AND FR.cor_materiaprima_Id = OPM.cororiginalid AND FR.Tamanho_materiaprima_Id = OPM.tamanhooriginalid) ");
            SQL.AppendLine("INNER JOIN fichatecnicadomaterial F ON (F.produtoid = IOP.ProdutoId)");
            //SQL.AppendLine("INNER JOIN fichatecnicadomaterialitem FI ON (FI.fichatecnicaid = F.Id AND FI.materiaPrimaId = OPM.materiaprimaoriginalid) ");

            SQL.AppendLine("INNER JOIN produtos p ON p.Id = opm.materiaprimaid ");
            SQL.AppendLine("INNER JOIN cores c ON c.Id = opm.corid ");
            SQL.AppendLine("INNER JOIN tamanhos t ON t.Id = opm.tamanhoid ");
            SQL.AppendLine("LEFT JOIN almoxarifados a ON a.Id = opm.armazemid ");
            SQL.AppendLine("LEFT JOIN unidademedidas u ON u.Id = p.IdUniMedida ");
            SQL.AppendLine("LEFT JOIN destinos d ON d.Id = opm.DestinoId ");
            SQL.AppendLine("WHERE	opm.OrdemProducaoId = ");
            SQL.Append(ordemId);
            SQL.AppendLine(" AND	opm.ItemOrdemProducaoId = ");
            SQL.Append(itemId);
            SQL.Append(" AND IOP.Status > 0 ");
            // teste relatório 21/11/2022
            if (QuebraManual)
            {
                SQL.AppendLine(" GROUP BY  opm.sequencia,opm.materiaPrimaId, opm.corid, opm.tamanhoid )) as opmaterial ");
            }
            else
            {
                SQL.AppendLine(" GROUP BY  opm.materiaPrimaId, opm.corid, opm.tamanhoid )) as opmaterial ");
            }
            // teste relatório 21/11/2022
            if (QuebraManual)
            {
                SQL.AppendLine(" GROUP BY  sequencia,materiaPrimaId, CorId, Tamanhoid");
            }
            else
            {
                SQL.AppendLine(" GROUP BY  materiaPrimaId, CorId, Tamanhoid");
            }
            
            SQL.AppendLine(" ORDER BY sequencia, MaterialReferencia, CorDescricao, TamanhoDescricao");


            var cn = new DapperConnection<OrdemProducaoMaterialView>();
            return cn.ExecuteStringSqlToList(new OrdemProducaoMaterialView(), SQL.ToString());
        }

        public IEnumerable<OrdemProducaoMaterialView> GetExcluir(int itemId, int ordemId)
        {
            var SQL = new StringBuilder();

            SQL.AppendLine("SELECT opm.*,");
            SQL.AppendLine("p.Referencia as MaterialReferencia, "); //F.possuiquebra, FI.DestinoId AS Destino, FI.sequencia,
            SQL.AppendLine("p.Descricao as MaterialDescricao,");
            SQL.AppendLine("c.abreviatura as CorDescricao,");
            SQL.AppendLine("t.abreviatura as TamanhoDescricao,");
            SQL.AppendLine("o.Referencia as OrdemProducaoReferencia,");
            SQL.AppendLine("a.abreviatura as ArmazemDescricao,");
            SQL.AppendLine("u.abreviatura as UM");
            SQL.AppendLine("FROM     ordemproducaomateriais opm");
            SQL.AppendLine("INNER JOIN ordemproducao o ON o.Id = opm.OrdemProducaoId ");
            SQL.AppendLine("INNER JOIN itensordemproducao IOP ON IOP.Id = opm.ItemOrdemProducaoId ");


            SQL.AppendLine("INNER JOIN produtos p ON p.Id = opm.materiaprimaid ");
            SQL.AppendLine("INNER JOIN cores c ON c.Id = opm.corid ");
            SQL.AppendLine("INNER JOIN tamanhos t ON t.Id = opm.tamanhoid ");
            SQL.AppendLine("LEFT JOIN almoxarifados a ON a.Id = opm.armazemid ");
            SQL.AppendLine("LEFT JOIN unidademedidas u ON u.Id = p.IdUniMedida ");
            SQL.AppendLine("WHERE	opm.OrdemProducaoId = ");
            SQL.Append(ordemId);
            SQL.AppendLine(" AND	opm.ItemOrdemProducaoId = ");
            SQL.Append(itemId);

            var cn = new DapperConnection<OrdemProducaoMaterialView>();
            return cn.ExecuteStringSqlToList(new OrdemProducaoMaterialView(), SQL.ToString());
        }

        public IEnumerable<OrdemProducaoMaterial> GetByOrdemView(int ordemId, int materialId, int corId, int tamanhoId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT opm.*");
            SQL.AppendLine("FROM 	ordemproducaomateriais opm");
            SQL.AppendLine("INNER JOIN ordemproducao o ON o.Id = opm.OrdemProducaoId ");
            SQL.AppendLine("INNER JOIN produtos p ON p.Id = opm.materiaprimaid ");
            SQL.AppendLine("INNER JOIN cores c ON c.Id = opm.corid ");
            SQL.AppendLine("INNER JOIN tamanhos t ON t.Id = opm.tamanhoid ");
            SQL.AppendLine("WHERE	opm.OrdemProducaoId = ");
            SQL.Append(ordemId + " AND opm.materiaprimaid = " + materialId + " AND opm.corid = " + corId + " AND opm.tamanhoid = " + tamanhoId);

            var cn = new DapperConnection<OrdemProducaoMaterialView>();
            return cn.ExecuteStringSqlToList(new OrdemProducaoMaterialView(), SQL.ToString());
        }

        public IEnumerable<OrdemProducaoMaterialEstoqueView> GetMaterialLiberacaoView()
        {
            var SQL = new StringBuilder();
            //SQL.AppendLine("SELECT SUM(IFNULL(opm.quantidadenecessaria, 0)) AS quantidadenecessaria,");
            //SQL.AppendLine("p.Referencia as MaterialReferencia,");
            //SQL.AppendLine("p.Descricao as MaterialDescricao,");
            //SQL.AppendLine("c.Descricao as CorDescricao,");
            //SQL.AppendLine("t.Descricao as TamanhoDescricao,");
            //SQL.AppendLine("o.Referencia as OrdemProducaoReferencia,");
            //SQL.AppendLine("IFNULL(e.Empenhado, 0) AS QuantidadeEmpenhada,");
            //SQL.AppendLine("(IFNULL(e.Empenhado, 0) - SUM(IFNULL(opm.quantidadenecessaria, 0))) AS Lancamento");
            //SQL.AppendLine("FROM 	ordemproducaomateriais opm");
            //SQL.AppendLine("INNER JOIN ordemproducao o ON o.Id = (SELECT o2.Id FROM ordemproducao o2 WHERE o2.id = opm.OrdemProducaoId AND o2.Status <> 6) ");
            //SQL.AppendLine("INNER JOIN produtos p ON p.Id = opm.materiaprimaid ");
            //SQL.AppendLine("INNER JOIN Estoque e ON ");
            //SQL.AppendLine("(e.ProdutoId = opm.materiaprimaid AND e.CorId = opm.CorId AND e.TamanhoId = opm.TamanhoId AND e.AlmoxarifadoId = opm.ArmazemId) ");
            //SQL.AppendLine("INNER JOIN cores c ON c.Id = opm.corid ");
            //SQL.AppendLine("INNER JOIN tamanhos t ON t.Id = opm.tamanhoid ");
            SQL.AppendLine("SELECT opm.Id, opm.CorId, opm.TamanhoId, opm.MateriaPrimaId as MaterialId, opm.ArmazemId, o.id as OrdemId, ");
            SQL.AppendLine(" CONCAT(p.Referencia, ' - ', p.Descricao) as MaterialDescricao, ");
            SQL.AppendLine(" c.Abreviatura as CorDescricao, ");
            SQL.AppendLine(" t.Abreviatura as TamanhoDescricao, ");
            SQL.AppendLine(" a.Descricao as ArmazemDescricao, ");
            SQL.AppendLine(" o.Referencia as OrdemReferencia, ");
            SQL.AppendLine(" sum(opm.quantidadenecessaria) as QuantidadeNecessaria, ");
            SQL.AppendLine(" sum(opm.quantidadeempenhada) as Empenhado, ");
            SQL.AppendLine(" sum(opm.EmpenhoProducao) as EmpenhoLivre, sum(opm.EmpenhoProducao) as Lancamento,colaboradores.razaosocial as RazaoColaborador ");
            SQL.AppendLine("FROM 	ordemproducaomateriais opm");
            SQL.AppendLine(" INNER JOIN ordemproducao o ON o.Id = opm.OrdemProducaoId ");
            SQL.AppendLine(" INNER JOIN produtos p ON p.Id = opm.materiaprimaid ");
            SQL.AppendLine(" INNER JOIN cores c ON c.Id = opm.corid ");
            SQL.AppendLine(" INNER JOIN tamanhos t ON t.Id = opm.tamanhoid ");
            SQL.AppendLine(" INNER JOIN almoxarifados a ON a.Id = opm.armazemid ");
            SQL.AppendLine(" LEFT JOIN colaboradores ON colaboradores.Id = o.IdColaborador ");
            SQL.AppendLine("GROUP BY opm.materiaprimaid, opm.CorId, opm.TamanhoId, opm.OrdemProducaoId");
            SQL.AppendLine("ORDER BY p.Referencia, c.Abreviatura, t.Id, o.Referencia");

            var cn = new DapperConnection<OrdemProducaoMaterialEstoqueView>();
            return cn.ExecuteStringSqlToList(new OrdemProducaoMaterialEstoqueView(), SQL.ToString());
        }

        public IEnumerable<CompraMaterialSemana> GetListCompraMaterialSemana(List<int> semanas)
        {
            if (semanas.Count <= 0)
                semanas.Add(-1);

            //var SQL = new StringBuilder();
            //SQL.AppendLine("SELECT (SUM(FI.quantidade*IOP.Quantidade)) as Consumo,  PM.Referencia as Referencia, PM.Descricao as Material, ");
            //SQL.AppendLine("(IPC.Qtd) as Compra, C.Abreviatura as Cor, T.Abreviatura as Tamanho");
            //SQL.AppendLine("FROM 	produtos P");
            //SQL.AppendLine("INNER JOIN itensordemproducao IOP ON IOP.ProdutoId = P.Id AND IOP.Status = 0");
            //SQL.AppendLine("INNER JOIN fichatecnicadomaterial F ON F.ProdutoId = P.Id ");
            //SQL.AppendLine("INNER JOIN fichatecnicadomaterialitem FI ON FI.fichatecnicaid = F.Id ");
            //SQL.AppendLine("INNER JOIN fichatecnicadomaterialrelacao FR ON ");
            //SQL.AppendLine("(FR.fichatecnicaid = F.Id AND FR.materiaPrimaId = FI.materiaPrimaId AND FR.cor_produto_id = IOP.CorId AND FR.tamanho_produto_id = IOP.TamanhoId) ");
            //SQL.AppendLine("INNER JOIN ordemproducao OP ON OP.id = IOP.OrdemProducaoId ");
            //SQL.AppendLine("INNER JOIN produtos PM ON FI.materiaprimaid = PM.Id ");
            //SQL.AppendLine("INNER JOIN cores C ON C.id = FR.cor_materiaprima_id ");
            //SQL.AppendLine("INNER JOIN tamanhos T ON T.id = FR.tamanho_materiaprima_id ");
            //SQL.AppendLine("LEFT JOIN itenspedidocompra IPC ON (IPC.ProdutoId = FR.materiaPrimaId AND IPC.CorId = FR.cor_materiaprima_id AND IPC.TamanhoId = FR.tamanho_materiaprima_id");
            //SQL.AppendLine("AND (select ip.id from itenspedidocompra ip inner join pedidocompra pci on ip.PedidoCompraId = pci.Id where ip.ProdutoId = FR.materiaPrimaId AND ip.CorId = FR.cor_materiaprima_id AND ip.TamanhoId = FR.tamanho_materiaprima_id AND pci.Status <> 4 AND pci.Semana IN (" + string.Join(", ", semanas) + ")))");
            ////SQL.AppendLine("LEFT JOIN ordemproducaomateriais OPM ON (FR.cor_materiaprima_id = OPM.CorId AND FR.tamanho_materiaprima_id = OPM.TamanhoId AND OPM.materiaPrimaId = FR.materiaPrimaId)");
            //SQL.AppendLine("LEFT JOIN estoque E ON (E.CorId = FR.cor_materiaprima_id AND E.TamanhoId = FR.tamanho_materiaprima_id AND E.ProdutoId = FR.materiaPrimaId)");
            //SQL.AppendLine("WHERE   OP.Semana IN (" + string.Join(", ", semanas) + ")");
            ////SQL.Append(" IN( ");
            ////SQL.Append(item.ProdutoId + " AND IOP.CorId = " + item.CorId + "  AND IOP.TamanhoId = " + item.TamanhoId);
            ////SQL.Append(" AND IOP.OrdemProducaoId = " + ordemId);
            //SQL.AppendLine(" GROUP BY  FI.materiaPrimaId, FR.cor_materiaprima_id, FR.tamanho_materiaprima_id ");
            //SQL.AppendLine("ORDER BY P.Referencia");
            //var cn = new DapperConnection<CompraMaterialSemana>();
            //var compras = cn.ExecuteStringSqlToList(new CompraMaterialSemana(), SQL.ToString());
            //if (compras.Count() > 0)
            //{
            //    return compras;
            //}
            //else
            //{
            //    SQL = new StringBuilder();
            //    SQL.AppendLine("SELECT PM.Referencia as Referencia, PM.Descricao as Material, ");
            //    SQL.AppendLine("(IPC.Qtd) as Compra, C.Abreviatura as Cor, T.Abreviatura as Tamanho");
            //    SQL.AppendLine("FROM 	produtos P");
            //    SQL.AppendLine("INNER JOIN produtodetalhes PD ON PD.IdProduto = P.ID ");
            //    SQL.AppendLine("INNER JOIN fichatecnicadomaterial F ON F.ProdutoId = P.Id ");
            //    SQL.AppendLine("INNER JOIN fichatecnicadomaterialitem FI ON FI.fichatecnicaid = F.Id ");
            //    SQL.AppendLine("INNER JOIN fichatecnicadomaterialrelacao FR ON ");
            //    SQL.AppendLine("(FR.fichatecnicaid = F.Id AND FR.materiaPrimaId = FI.materiaPrimaId AND FR.cor_produto_id = PD.IdCor AND FR.tamanho_produto_id = PD.IdTamanho) ");
            //    SQL.AppendLine("INNER JOIN produtos PM ON FI.materiaprimaid = PM.Id ");
            //    SQL.AppendLine("INNER JOIN cores C ON C.id = FR.cor_materiaprima_id ");
            //    SQL.AppendLine("INNER JOIN tamanhos T ON T.id = FR.tamanho_materiaprima_id ");
            //    SQL.AppendLine("LEFT JOIN itenspedidocompra IPC ON (IPC.ProdutoId = FR.materiaPrimaId AND IPC.CorId = FR.cor_materiaprima_id AND IPC.TamanhoId = FR.tamanho_materiaprima_id");
            //    SQL.AppendLine("AND (select ip.id from itenspedidocompra ip inner join pedidocompra pci on ip.PedidoCompraId = pci.Id where ip.ProdutoId = FR.materiaPrimaId AND ip.CorId = FR.cor_materiaprima_id AND ip.TamanhoId = FR.tamanho_materiaprima_id AND pci.Status <> 4 AND pci.Semana IN (" + string.Join(", ", semanas) + ")))");
            //    //SQL.AppendLine("LEFT JOIN ordemproducaomateriais OPM ON (FR.cor_materiaprima_id = OPM.CorId AND FR.tamanho_materiaprima_id = OPM.TamanhoId AND OPM.materiaPrimaId = FR.materiaPrimaId)");
            //    SQL.AppendLine("LEFT JOIN estoque E ON (E.CorId = FR.cor_materiaprima_id AND E.TamanhoId = FR.tamanho_materiaprima_id AND E.ProdutoId = FR.materiaPrimaId)");
            //    SQL.AppendLine("WHERE   IPC.QTD > 0");
            //    //SQL.Append(" IN( ");
            //    //SQL.Append(item.ProdutoId + " AND IOP.CorId = " + item.CorId + "  AND IOP.TamanhoId = " + item.TamanhoId);
            //    //SQL.Append(" AND IOP.OrdemProducaoId = " + ordemId);
            //    SQL.AppendLine(" GROUP BY  FI.materiaPrimaId, FR.cor_materiaprima_id, FR.tamanho_materiaprima_id ");
            //    SQL.AppendLine("ORDER BY P.Referencia");

            //    return cn.ExecuteStringSqlToList(new CompraMaterialSemana(), SQL.ToString());
            //}

            var SQL = new StringBuilder();

            SQL.AppendLine("SELECT SUM(Consumo) as Consumo, Referencia, Material, SUM(Compra) as compra, Cor, Tamanho FROM ");

            SQL.AppendLine("((SELECT (FI.quantidade*IOP.Quantidade) as Consumo,  PM.Referencia as Referencia, PM.Descricao as Material, ");
            SQL.AppendLine("0 as Compra, C.Abreviatura as Cor, T.Abreviatura as Tamanho ");

            SQL.AppendLine("FROM itensordemproducao IOP  ");
            SQL.AppendLine("INNER JOIN fichatecnicadomaterial F ON F.ProdutoId = IOP.ProdutoId  ");
            SQL.AppendLine("INNER JOIN fichatecnicadomaterialitem FI ON FI.fichatecnicaid = F.Id   ");
            SQL.AppendLine("INNER JOIN fichatecnicadomaterialrelacao FR ON   ");
            SQL.AppendLine("(FR.fichatecnicaid = F.Id AND FR.materiaPrimaId = FI.materiaPrimaId AND FR.cor_produto_id = IOP.CorId AND FR.tamanho_produto_id = IOP.TamanhoId AND FR.fichatecnicaitemid = FI.id) ");
            SQL.AppendLine("INNER JOIN ordemproducao OP ON OP.id = IOP.OrdemProducaoId ");
            SQL.AppendLine("INNER JOIN produtos PM ON FI.materiaprimaid = PM.Id ");
            SQL.AppendLine("INNER JOIN cores C ON C.id = FR.cor_materiaprima_id ");
            SQL.AppendLine("INNER JOIN tamanhos T ON T.id = FR.tamanho_materiaprima_id ");
            SQL.AppendLine("LEFT JOIN estoque E ON (E.CorId = FR.cor_materiaprima_id AND E.TamanhoId = FR.tamanho_materiaprima_id AND E.ProdutoId = FR.materiaPrimaId)");
            SQL.AppendLine("WHERE   OP.Semana in (" + string.Join(", ", semanas) + ") AND IOP.Status = 0 AND OP.Status <> 6");
            SQL.AppendLine("GROUP BY  FI.materiaPrimaId, IOP.id, FR.cor_materiaprima_id, FR.tamanho_materiaprima_id ORDER BY PM.Referencia, IOP.id)");

            SQL.AppendLine("UNION ALL ");

            SQL.AppendLine("(SELECT 0 as Consumo, P.Referencia as Referencia, P.Descricao as Material, ");
            SQL.AppendLine("sum(I.Qtd - I.QtdAtendida) as Compra, C.Abreviatura as Cor, T.Abreviatura as Tamanho ");

            SQL.AppendLine("FROM itenspedidoCompra I  ");
            SQL.AppendLine("INNER JOIN pedidoCompra PC ON PC.Id = I.PedidoCompraId  ");
            SQL.AppendLine("INNER JOIN produtos P ON P.Id = I.ProdutoId	");
            SQL.AppendLine("LEFT JOIN tamanhos T ON T.Id = I.TamanhoId ");
            SQL.AppendLine("LEFT JOIN cores C ON C.Id = I.CorId  ");
            SQL.AppendLine("WHERE I.Qtd - I.QtdAtendida > 0 AND  PC.Status <> 4  ");
            SQL.AppendLine("AND PC.semana in (" + string.Join(", ", semanas) + ") GROUP BY p.id, c.id, t.id)) as co  ");
            SQL.AppendLine("GROUP BY Referencia, Cor, Tamanho; ");

            var cn = new DapperConnection<CompraMaterialSemana>();


            //return cn.ExecuteStringSqlToList(new CompraMaterialSemana(), SQL.ToString());

            //SQL.AppendLine("SELECT (SUM(FI.quantidade*IOP.Quantidade)) as Consumo,  PM.Referencia as Referencia, PM.Descricao as Material, ");
            //SQL.AppendLine("C.Abreviatura as Cor, T.Abreviatura as Tamanho, ");

            //SQL.AppendLine("(SELECT sum(I.Qtd - I.QtdAtendida) AS Qtd");
            //SQL.AppendLine("FROM 	itenspedidoCompra I");
            //SQL.AppendLine("INNER JOIN pedidoCompra PC ON PC.Id = I.PedidoCompraId");
            //SQL.AppendLine("INNER JOIN produtos P1 ON P1.Id = I.ProdutoId	");
            //SQL.AppendLine("LEFT JOIN tamanhos T1 ON T1.Id = I.TamanhoId 	");
            //SQL.AppendLine("LEFT JOIN cores C1 ON C1.Id = I.CorId 	");
            //SQL.AppendLine("WHERE I.Qtd - I.QtdAtendida > 0 AND  PC.Status <> 4 ");
            //SQL.AppendLine("AND p1.id = FI.materiaprimaid AND c1.id = FR.cor_materiaprima_id  AND t1.id = FR.tamanho_materiaprima_id	");
            //SQL.AppendLine("AND PC.semana >=" + semanas.LastOrDefault() + " GROUP BY p1.id, c1.id, t1.id) as Compra	");

            //SQL.AppendLine("FROM itensordemproducao IOP  ");
            //SQL.AppendLine("INNER JOIN fichatecnicadomaterial F ON F.ProdutoId = IOP.ProdutoId  ");
            //SQL.AppendLine("INNER JOIN fichatecnicadomaterialitem FI ON FI.fichatecnicaid = F.Id   ");
            //SQL.AppendLine("INNER JOIN fichatecnicadomaterialrelacao FR ON   ");
            //SQL.AppendLine("(FR.fichatecnicaid = F.Id AND FR.materiaPrimaId = FI.materiaPrimaId AND FR.cor_produto_id = IOP.CorId AND FR.tamanho_produto_id = IOP.TamanhoId AND FR.fichatecnicaitemid = FI.id) ");
            //SQL.AppendLine("INNER JOIN ordemproducao OP ON OP.id = IOP.OrdemProducaoId ");
            //SQL.AppendLine("INNER JOIN produtos PM ON FI.materiaprimaid = PM.Id ");
            //SQL.AppendLine("INNER JOIN cores C ON C.id = FR.cor_materiaprima_id ");
            //SQL.AppendLine("INNER JOIN tamanhos T ON T.id = FR.tamanho_materiaprima_id ");
            //SQL.AppendLine("LEFT JOIN estoque E ON (E.CorId = FR.cor_materiaprima_id AND E.TamanhoId = FR.tamanho_materiaprima_id AND E.ProdutoId = FR.materiaPrimaId)");
            //SQL.AppendLine("WHERE   OP.Semana >= " + semanas.LastOrDefault() + " AND IOP.Status = 0");
            //SQL.AppendLine("GROUP BY  FI.materiaPrimaId, FR.cor_materiaprima_id, FR.tamanho_materiaprima_id ORDER BY PM.Referencia");
            //var cn = new DapperConnection<CompraMaterialSemana>();
            return cn.ExecuteStringSqlToList(new CompraMaterialSemana(), SQL.ToString());
             
        }

        public IEnumerable<CompraMaterialSemana> GetListCompraMaterialSemana1(List<int> semanas)
        {

            var SQL = new StringBuilder();

            SQL.AppendLine("SELECT SUM(Consumo) as Consumo, Referencia, Material, SUM(Compra) as compra, Cor, Tamanho FROM ");

            SQL.AppendLine("((SELECT (FI.quantidade*IOP.Quantidade) as Consumo,  PM.Referencia as Referencia, PM.Descricao as Material, ");
            SQL.AppendLine("0 as Compra, C.Abreviatura as Cor, T.Abreviatura as Tamanho ");

            SQL.AppendLine("FROM itensordemproducao IOP  ");
            SQL.AppendLine("INNER JOIN fichatecnicadomaterial F ON F.ProdutoId = IOP.ProdutoId  ");
            SQL.AppendLine("INNER JOIN fichatecnicadomaterialitem FI ON FI.fichatecnicaid = F.Id   ");
            SQL.AppendLine("INNER JOIN fichatecnicadomaterialrelacao FR ON   ");
            SQL.AppendLine("(FR.fichatecnicaid = F.Id AND FR.materiaPrimaId = FI.materiaPrimaId AND FR.cor_produto_id = IOP.CorId AND FR.tamanho_produto_id = IOP.TamanhoId AND FR.fichatecnicaitemid = FI.id) ");
            SQL.AppendLine("INNER JOIN ordemproducao OP ON OP.id = IOP.OrdemProducaoId ");
            SQL.AppendLine("INNER JOIN produtos PM ON FI.materiaprimaid = PM.Id ");
            SQL.AppendLine("INNER JOIN cores C ON C.id = FR.cor_materiaprima_id ");
            SQL.AppendLine("INNER JOIN tamanhos T ON T.id = FR.tamanho_materiaprima_id ");
            SQL.AppendLine("LEFT JOIN estoque E ON (E.CorId = FR.cor_materiaprima_id AND E.TamanhoId = FR.tamanho_materiaprima_id AND E.ProdutoId = FR.materiaPrimaId)");
            SQL.AppendLine("WHERE   OP.Semana <= " + semanas.LastOrDefault() + " AND IOP.Status = 0 AND OP.Status <> 6 ");
            SQL.AppendLine("GROUP BY  FI.materiaPrimaId, IOP.id, FR.cor_materiaprima_id, FR.tamanho_materiaprima_id ORDER BY PM.Referencia, IOP.id)");

            SQL.AppendLine("UNION ALL ");


            SQL.AppendLine("(SELECT 0 as Consumo, P.Referencia as Referencia, P.Descricao as Material, ");
            SQL.AppendLine("sum(I.Qtd - I.QtdAtendida) as Compra, C.Abreviatura as Cor, T.Abreviatura as Tamanho ");

            SQL.AppendLine("FROM itenspedidoCompra I  ");
            SQL.AppendLine("INNER JOIN pedidoCompra PC ON PC.Id = I.PedidoCompraId  ");
            SQL.AppendLine("INNER JOIN produtos P ON P.Id = I.ProdutoId	");
            SQL.AppendLine("LEFT JOIN tamanhos T ON T.Id = I.TamanhoId ");
            SQL.AppendLine("LEFT JOIN cores C ON C.Id = I.CorId  ");
            SQL.AppendLine("WHERE I.Qtd - I.QtdAtendida > 0 AND  PC.Status <> 4  ");
            SQL.AppendLine("AND PC.semana <= " + semanas.LastOrDefault() + " GROUP BY p.id, c.id, t.id)) as co  ");
            SQL.AppendLine("GROUP BY Referencia, Cor, Tamanho; ");

            var cn = new DapperConnection<CompraMaterialSemana>();


            return cn.ExecuteStringSqlToList(new CompraMaterialSemana(), SQL.ToString());

        }

        public IEnumerable<CompraMaterialSemana> GetListCompraMaterialUltimaSemana(List<int> semanas)
        {

            var SQL = new StringBuilder();
            //SQL.AppendLine("SELECT (SUM(FI.quantidade*IOP.Quantidade)) as Consumo,  PM.Referencia as Referencia, PM.Descricao as Material, ");
            //SQL.AppendLine("(IPC.Qtd) as Compra, C.Abreviatura as Cor, T.Abreviatura as Tamanho");
            //SQL.AppendLine("FROM 	produtos P");
            //SQL.AppendLine("INNER JOIN itensordemproducao IOP ON IOP.ProdutoId = P.Id AND IOP.Status = 1");
            //SQL.AppendLine("INNER JOIN fichatecnicadomaterial F ON F.ProdutoId = P.Id ");
            //SQL.AppendLine("INNER JOIN fichatecnicadomaterialitem FI ON FI.fichatecnicaid = F.Id ");
            //SQL.AppendLine("INNER JOIN fichatecnicadomaterialrelacao FR ON ");
            //SQL.AppendLine("(FR.fichatecnicaid = F.Id AND FR.materiaPrimaId = FI.materiaPrimaId AND FR.cor_produto_id = IOP.CorId AND FR.tamanho_produto_id = IOP.TamanhoId) ");
            //SQL.AppendLine("INNER JOIN ordemproducao OP ON OP.id = IOP.OrdemProducaoId ");
            //SQL.AppendLine("INNER JOIN produtos PM ON FI.materiaprimaid = PM.Id ");
            //SQL.AppendLine("INNER JOIN cores C ON C.id = FR.cor_materiaprima_id ");
            //SQL.AppendLine("INNER JOIN tamanhos T ON T.id = FR.tamanho_materiaprima_id ");
            //SQL.AppendLine("LEFT JOIN itenspedidocompra IPC ON (IPC.ProdutoId = FR.materiaPrimaId AND IPC.CorId = FR.cor_materiaprima_id AND IPC.TamanhoId = FR.tamanho_materiaprima_id");
            //SQL.AppendLine("AND (select ip.id from itenspedidocompra ip inner join pedidocompra pci on ip.PedidoCompraId = pci.Id where ip.ProdutoId = FR.materiaPrimaId AND ip.CorId = FR.cor_materiaprima_id AND ip.TamanhoId = FR.tamanho_materiaprima_id AND pci.Semana >= " + semanas.LastOrDefault() + "))");
            //SQL.AppendLine("LEFT JOIN ordemproducaomateriais OPM ON (FR.cor_materiaprima_id = OPM.CorId AND FR.tamanho_materiaprima_id = OPM.TamanhoId AND OPM.materiaPrimaId = FR.materiaPrimaId)");
            //SQL.AppendLine("LEFT JOIN estoque E ON (E.CorId = FR.cor_materiaprima_id AND E.TamanhoId = FR.tamanho_materiaprima_id AND E.ProdutoId = FR.materiaPrimaId)");
            //SQL.AppendLine("WHERE   OP.Semana >= " + semanas.LastOrDefault());
            //SQL.Append(" IN( ");
            //SQL.Append(item.ProdutoId + " AND IOP.CorId = " + item.CorId + "  AND IOP.TamanhoId = " + item.TamanhoId);
            //SQL.Append(" AND IOP.OrdemProducaoId = " + ordemId);
            //SQL.AppendLine(" GROUP BY  FI.materiaPrimaId, FR.cor_materiaprima_id, FR.tamanho_materiaprima_id ");
            //SQL.AppendLine("ORDER BY P.Referencia");
            //var cn = new DapperConnection<CompraMaterialSemana>();
            //var compras = cn.ExecuteStringSqlToList(new CompraMaterialSemana(), SQL.ToString());
            //if (compras.Count() > 0)
            //{
            //    return compras;
            //}
            //else
            //{
            //    SQL = new StringBuilder();
            //    SQL.AppendLine("SELECT PM.Referencia as Referencia, PM.Descricao as Material, ");
            //    SQL.AppendLine("(IPC.Qtd) as Compra, C.Abreviatura as Cor, T.Abreviatura as Tamanho");
            //    SQL.AppendLine("FROM 	produtos P");
            //    SQL.AppendLine("INNER JOIN itensordemproducao IOP ON IOP.ProdutoId = P.Id ");
            //    SQL.AppendLine("INNER JOIN fichatecnicadomaterial F ON F.ProdutoId = P.Id ");
            //    SQL.AppendLine("INNER JOIN fichatecnicadomaterialitem FI ON FI.fichatecnicaid = F.Id ");
            //    SQL.AppendLine("INNER JOIN fichatecnicadomaterialrelacao FR ON ");
            //    SQL.AppendLine("(FR.fichatecnicaid = F.Id AND FR.materiaPrimaId = FI.materiaPrimaId AND FR.cor_produto_id = IOP.CorId AND FR.tamanho_produto_id = IOP.TamanhoId) ");
            //    SQL.AppendLine("INNER JOIN ordemproducao OP ON OP.id = IOP.OrdemProducaoId ");
            //    SQL.AppendLine("INNER JOIN produtos PM ON FI.materiaprimaid = PM.Id ");
            //    SQL.AppendLine("INNER JOIN cores C ON C.id = FR.cor_materiaprima_id ");
            //    SQL.AppendLine("INNER JOIN tamanhos T ON T.id = FR.tamanho_materiaprima_id ");
            //    SQL.AppendLine("LEFT JOIN itenspedidocompra IPC ON (IPC.ProdutoId = FR.materiaPrimaId AND IPC.CorId = FR.cor_materiaprima_id AND IPC.TamanhoId = FR.tamanho_materiaprima_id");
            //    SQL.AppendLine("AND (select ip.id from itenspedidocompra ip inner join pedidocompra pci on ip.PedidoCompraId = pci.Id where ip.ProdutoId = FR.materiaPrimaId AND ip.CorId = FR.cor_materiaprima_id AND ip.TamanhoId = FR.tamanho_materiaprima_id AND pci.Semana >= " + semanas.LastOrDefault() + "))");
            //    SQL.AppendLine("LEFT JOIN ordemproducaomateriais OPM ON (FR.cor_materiaprima_id = OPM.CorId AND FR.tamanho_materiaprima_id = OPM.TamanhoId AND OPM.materiaPrimaId = FR.materiaPrimaId)");
            //    SQL.AppendLine("LEFT JOIN estoque E ON (E.CorId = FR.cor_materiaprima_id AND E.TamanhoId = FR.tamanho_materiaprima_id AND E.ProdutoId = FR.materiaPrimaId)");
            //    SQL.AppendLine("WHERE   IPC.QTD > 0");
            //    SQL.Append(" IN( ");
            //    SQL.Append(item.ProdutoId + " AND IOP.CorId = " + item.CorId + "  AND IOP.TamanhoId = " + item.TamanhoId);
            //    SQL.Append(" AND IOP.OrdemProducaoId = " + ordemId);
            //    SQL.AppendLine(" GROUP BY  FI.materiaPrimaId, FR.cor_materiaprima_id, FR.tamanho_materiaprima_id ");
            //    SQL.AppendLine("ORDER BY P.Referencia");

            //    return cn.ExecuteStringSqlToList(new CompraMaterialSemana(), SQL.ToString());
            //}

            SQL.AppendLine("SELECT (SUM(FI.quantidade*IOP.Quantidade)) as Consumo,  PM.Referencia as Referencia, PM.Descricao as Material, ");
            SQL.AppendLine("C.Abreviatura as Cor, T.Abreviatura as Tamanho, ");

            SQL.AppendLine("(SELECT sum(I.Qtd - I.QtdAtendida) AS Qtd");
            SQL.AppendLine("FROM 	itenspedidoCompra I");
            SQL.AppendLine("INNER JOIN pedidoCompra PC ON PC.Id = I.PedidoCompraId");
            SQL.AppendLine("INNER JOIN produtos P1 ON P1.Id = I.ProdutoId	");
            SQL.AppendLine("LEFT JOIN tamanhos T1 ON T1.Id = I.TamanhoId 	");
            SQL.AppendLine("LEFT JOIN cores C1 ON C1.Id = I.CorId 	");
            SQL.AppendLine("WHERE I.Qtd - I.QtdAtendida > 0 AND  PC.Status <> 4 ");
            SQL.AppendLine("AND p1.id = FI.materiaprimaid AND c1.id = FR.cor_materiaprima_id  AND t1.id = FR.tamanho_materiaprima_id	");
            SQL.AppendLine("AND PC.semana >=" + semanas.LastOrDefault() + " GROUP BY p1.id, c1.id, t1.id) as Compra	");

            SQL.AppendLine("FROM itensordemproducao IOP  ");
            SQL.AppendLine("INNER JOIN fichatecnicadomaterial F ON F.ProdutoId = IOP.ProdutoId  ");
            SQL.AppendLine("INNER JOIN fichatecnicadomaterialitem FI ON FI.fichatecnicaid = F.Id   ");
            SQL.AppendLine("INNER JOIN fichatecnicadomaterialrelacao FR ON   ");
            SQL.AppendLine("(FR.fichatecnicaid = F.Id AND FR.materiaPrimaId = FI.materiaPrimaId AND FR.cor_produto_id = IOP.CorId AND FR.tamanho_produto_id = IOP.TamanhoId AND FR.fichatecnicaitemid = FI.id) ");
            SQL.AppendLine("INNER JOIN ordemproducao OP ON OP.id = IOP.OrdemProducaoId ");
            SQL.AppendLine("INNER JOIN produtos PM ON FI.materiaprimaid = PM.Id ");
            SQL.AppendLine("INNER JOIN cores C ON C.id = FR.cor_materiaprima_id ");
            SQL.AppendLine("INNER JOIN tamanhos T ON T.id = FR.tamanho_materiaprima_id ");
            SQL.AppendLine("LEFT JOIN estoque E ON (E.CorId = FR.cor_materiaprima_id AND E.TamanhoId = FR.tamanho_materiaprima_id AND E.ProdutoId = FR.materiaPrimaId)");
            SQL.AppendLine("WHERE   OP.Semana >= " + semanas.LastOrDefault() + " AND IOP.Status = 0");
            SQL.AppendLine("GROUP BY  FI.materiaPrimaId, FR.cor_materiaprima_id, FR.tamanho_materiaprima_id ORDER BY PM.Referencia");
            var cn = new DapperConnection<CompraMaterialSemana>();
            return cn.ExecuteStringSqlToList(new CompraMaterialSemana(), SQL.ToString());
        }

        public IEnumerable<CustoConsumo> GetCustoConsumo(FiltroCustoConsumo filtro)
        {
            var SQL = new StringBuilder();
           
            //SQL.AppendLine("SELECT PM.Referencia as Referencia, PM.Descricao as Material, ");
            //SQL.AppendLine("(SUM(FI.quantidade*IOP.Quantidade)) as Consumo, C.Abreviatura as Cor, T.Abreviatura as Tamanho, U.Abreviatura as Unidade,");
     
            //if(filtro.MaiorPreco)
            //    SQL.AppendLine("(SELECT MAX(PrecoFornecedor) FROM produtofornecedorprecos PFP WHERE PFP.idProduto = PM.id) as Preco");
            //else
            //    SQL.AppendLine("(SELECT AVG(PrecoFornecedor) FROM produtofornecedorprecos PFP WHERE PFP.idProduto = PM.id) as Preco");

            //SQL.AppendLine("FROM 	itensordemproducao IOP");
            //SQL.AppendLine("INNER JOIN ordemproducao OP ON IOP.OrdemProducaoId = OP.Id ");
            //SQL.AppendLine("INNER JOIN fichatecnicadomaterial F ON F.ProdutoId = IOP.ProdutoId  ");
            //SQL.AppendLine("INNER JOIN fichatecnicadomaterialitem FI ON FI.fichatecnicaid = F.Id ");
            //SQL.AppendLine("INNER JOIN fichatecnicadomaterialrelacao FR ON ");
            //SQL.AppendLine("(FR.fichatecnicaid = F.Id AND FR.materiaPrimaId = FI.materiaPrimaId AND FR.cor_produto_id = IOP.CorId AND FR.tamanho_produto_id = IOP.TamanhoId) ");
            //SQL.AppendLine("INNER JOIN produtos PM ON FR.materiaprimaid = PM.Id ");
            //SQL.AppendLine("INNER JOIN cores C ON C.id = FR.cor_materiaprima_id ");
            //SQL.AppendLine("INNER JOIN tamanhos T ON T.id = FR.tamanho_materiaprima_id  ");
            //SQL.AppendLine("LEFT JOIN unidademedidas U ON U.id = PM.IdUniMedida ");
            //SQL.AppendLine("LEFT JOIN ordemproducaomateriais OPM ON (OPM.OrdemProducaoId = IOP.OrdemProducaoId AND FR.cor_materiaprima_id = OPM.CorId AND FR.tamanho_materiaprima_id = OPM.TamanhoId AND OPM.materiaPrimaId = FR.materiaPrimaId)");
            //SQL.AppendLine("WHERE ");
            SQL.AppendLine("SELECT  Referencia,  Material, ");
            SQL.AppendLine("(SUM(consumo)) as Consumo, Cor,  Tamanho,  Unidade, Preco FROM (");

            SQL.AppendLine("( SELECT PM.Referencia as Referencia, PM.Descricao as Material, PM.Id as MaterialId, ");
            SQL.AppendLine("(SUM(FI.quantidade*IOP.Quantidade)) as Consumo, C.Abreviatura as Cor, T.Abreviatura as Tamanho, U.Abreviatura as Unidade,");

            if (filtro.MaiorPreco)
                SQL.AppendLine("(SELECT MAX(NULLIF(PrecoFornecedor,0)) FROM produtofornecedorprecos PFP WHERE PFP.idProduto = PM.id) as Preco");
            else
                SQL.AppendLine("(SELECT AVG(NULLIF(PrecoFornecedor,0)) FROM produtofornecedorprecos PFP WHERE PFP.idProduto = PM.id) as Preco");

            SQL.AppendLine("FROM 	itensordemproducao IOP");
            SQL.AppendLine("INNER JOIN ordemproducao OP ON IOP.OrdemProducaoId = OP.Id ");
            SQL.AppendLine("INNER JOIN fichatecnicadomaterial F ON F.ProdutoId = IOP.ProdutoId  ");
            SQL.AppendLine("INNER JOIN fichatecnicadomaterialitem FI ON FI.fichatecnicaid = F.Id ");
            SQL.AppendLine("INNER JOIN fichatecnicadomaterialrelacao FR ON ");
            SQL.AppendLine("(FR.fichatecnicaid = F.Id AND FR.materiaPrimaId = FI.materiaPrimaId AND FR.cor_produto_id = IOP.CorId AND FR.tamanho_produto_id = IOP.TamanhoId AND FR.fichatecnicaitemId = FI.Id) ");
            SQL.AppendLine("INNER JOIN produtos PM ON FR.materiaprimaid = PM.Id ");
            SQL.AppendLine("INNER JOIN cores C ON C.id = FR.cor_materiaprima_id ");
            SQL.AppendLine("INNER JOIN tamanhos T ON T.id = FR.tamanho_materiaprima_id  ");
            SQL.AppendLine("LEFT JOIN unidademedidas U ON U.id = PM.IdUniMedida ");
            SQL.AppendLine("WHERE IOP.Status = 0 ");
            SQL.AppendLine(" AND FR.cor_materiaprima_id <> 999 AND FR.tamanho_materiaprima_id <> 999 "); //NÃO APARECE GRADE SEM USO

            switch (filtro.Exibir)
            {
                case "aberto":
                    SQL.Append(" AND OP.status = 1");
                    break;
                case "liberado":
                    SQL.Append(" AND OP.status in (8,9,10)");
                    break;
                case "abertoliberado":
                    SQL.Append(" AND OP.status in (1,8,9,10) ");
                    break;
                case "finalizado":
                    SQL.Append(" AND OP.status in (5,6)");
                    break;
                case "todas":
                    SQL.Append(" AND OP.status > 0");
                    break;
                default:
                    SQL.Append(" AND OP.status > 0 ");
                    break;

            }

            if (filtro.Produtos != null && filtro.Produtos.Count() > 0)
                SQL.AppendLine(" AND FR.materiaPrimaId in (" + string.Join(",", filtro.Produtos.ToArray()) + ")");

            if (filtro.Ordem != null && filtro.Ordem.Count() > 0)
                SQL.AppendLine(" AND IOP.OrdemProducaoId in (" + string.Join(",", filtro.Ordem.ToArray()) + ")");

            if (filtro.Cor != null && filtro.Cor.Count() > 0)
                SQL.AppendLine(" AND FR.cor_materiaprima_id in (" + string.Join(",", filtro.Cor.ToArray()) + ")");

            if (filtro.Tamanho != null && filtro.Tamanho.Count() > 0)
                SQL.AppendLine(" AND FR.tamanho_materiaprima_id in (" + string.Join(",", filtro.Tamanho.ToArray()) + ")");

            if (filtro.DaEmissao != "" || filtro.AteEmissao != "")
                SQL.AppendLine("        AND DATE(OP.DataEmissao) BETWEEN  '" + filtro.DaEmissao + "' AND '" + filtro.AteEmissao + "' ");

            if (filtro.Agrupar)
                SQL.AppendLine(" GROUP BY FR.materiaPrimaId ");
            else
                SQL.AppendLine(" GROUP BY FR.materiaPrimaId, FR.cor_materiaprima_id, FR.tamanho_materiaprima_id ");
            SQL.Append(" )");

            SQL.AppendLine("UNION ALL");


            SQL.AppendLine("( SELECT PM.Referencia as Referencia, PM.Descricao as Material, PM.Id as MaterialId, ");
            SQL.AppendLine("(SUM(opm.quantidadenecessaria)) as Consumo, C.Abreviatura as Cor, T.Abreviatura as Tamanho, U.Abreviatura as Unidade,");

            if (filtro.MaiorPreco)
                SQL.AppendLine("(SELECT MAX(PrecoFornecedor) FROM produtofornecedorprecos PFP WHERE PFP.idProduto = PM.id) as Preco");
            else
                SQL.AppendLine("(SELECT AVG(PrecoFornecedor) FROM produtofornecedorprecos PFP WHERE PFP.idProduto = PM.id) as Preco");

            SQL.AppendLine("FROM 	itensordemproducao IOP");
            SQL.AppendLine("INNER JOIN ordemproducaomateriais OPM ON (IOP.Id = opm.ItemOrdemProducaoId)");
            SQL.AppendLine("INNER JOIN ordemproducao OP ON IOP.OrdemProducaoId = OP.Id ");
            SQL.AppendLine("INNER JOIN produtos PM ON opm.materiaprimaid = PM.Id ");
            SQL.AppendLine("INNER JOIN cores C ON C.id = opm.corid ");
            SQL.AppendLine("INNER JOIN tamanhos T ON T.id = opm.tamanhoid  ");
            SQL.AppendLine("LEFT JOIN unidademedidas U ON U.id = PM.IdUniMedida ");

            SQL.AppendLine("WHERE IOP.Status > 0 ");

            switch (filtro.Exibir)
            {
                case "aberto":
                    SQL.Append(" AND OP.status = 1");
                    break;
                case "liberado":
                    SQL.Append(" AND OP.status in (8,9,10)");
                    break;
                case "abertoliberado":
                    SQL.Append(" AND OP.status in (1,8,9,10) ");
                    break;
                case "finalizado":
                    SQL.Append(" AND OP.status in (5,6)");
                    break;
                case "todas":
                    SQL.Append(" AND OP.status > 0");
                    break;
                default:
                    SQL.Append(" AND OP.status > 0 ");
                    break;

            }

            if (filtro.Produtos != null && filtro.Produtos.Count() > 0)
                SQL.AppendLine(" AND opm.materiaprimaid in (" + string.Join(",", filtro.Produtos.ToArray()) + ")");

            if (filtro.Ordem != null && filtro.Ordem.Count() > 0)
                SQL.AppendLine(" AND IOP.OrdemProducaoId in (" + string.Join(",", filtro.Ordem.ToArray()) + ")");

            if (filtro.Cor != null && filtro.Cor.Count() > 0)
                SQL.AppendLine(" AND opm.corid in (" + string.Join(",", filtro.Cor.ToArray()) + ")");

            if (filtro.Tamanho != null && filtro.Tamanho.Count() > 0)
                SQL.AppendLine(" AND opm.tamanhoid in (" + string.Join(",", filtro.Tamanho.ToArray()) + ")");

            if (filtro.DaEmissao != "" || filtro.AteEmissao != "")
                SQL.AppendLine("        AND DATE(OP.DataEmissao) BETWEEN  '" + filtro.DaEmissao + "' AND '" + filtro.AteEmissao + "' ");
            
            if(filtro.Agrupar)
                SQL.AppendLine(" GROUP BY  opm.materiaprimaid ");
            else
                SQL.AppendLine(" GROUP BY opm.materiaprimaid, opm.corid, opm.tamanhoid ");

            SQL.Append(" )");
            SQL.Append(" ) as material");

            if (filtro.Agrupar)
                SQL.AppendLine(" GROUP BY  MaterialId ");
            else
                SQL.AppendLine(" GROUP BY MaterialId, Cor, Tamanho ");

            SQL.AppendLine(" ORDER BY Referencia, Cor, Tamanho");
            var cn = new DapperConnection<CustoConsumo>();
            return cn.ExecuteStringSqlToList(new CustoConsumo(), SQL.ToString());
        }

        public IEnumerable<ConsultaRelListaMateriaisView> GetListaMateriaisBaseadoOP(FiltroRelListaMateriais filtro)
        {
            var SQL = new StringBuilder();
            
            SQL.AppendLine("SELECT idGrupoMaterial, grupoMaterial,materiaPrimaId, corId, tamanhoId, referencia, descricao, corAbreviatura, tamAbreviatura, uniAbreviatura, SUM(quantidade) AS quantidade ");
            SQL.AppendLine("FROM (");
            SQL.AppendLine("	(SELECT grupoprodutos.Id as idGrupoMaterial, grupoprodutos.descricao as grupoMaterial,m.Id as materiaPrimaId, c.Id as corId, t.Id as tamanhoId, m.referencia, m.descricao, c.abreviatura as corAbreviatura, ");
            SQL.AppendLine("		t.abreviatura as tamAbreviatura, u.abreviatura as uniAbreviatura,  SUM(opm.quantidadenecessaria) AS quantidade");
            SQL.AppendLine("     FROM ordemproducao op ");
            SQL.AppendLine("	INNER JOIN itensordemproducao iop ON op.Id = iop.OrdemProducaoId ");
            SQL.AppendLine("	INNER JOIN ordemproducaomateriais opm ON op.Id = opm.OrdemProducaoId AND opm.itemordemproducaoid = iop.id ");
            SQL.AppendLine("	INNER JOIN produtos m ON opm.MateriaPrimaId = m.Id	");
            SQL.AppendLine("	INNER JOIN grupoprodutos ON grupoprodutos.Id = m.IdGrupo	");
            SQL.AppendLine("	INNER JOIN cores c ON opm.CorId = c.Id ");
            SQL.AppendLine("	INNER JOIN tamanhos t ON opm.TamanhoId = t.Id  ");
            SQL.AppendLine("	INNER JOIN produtodetalhes pd ON pd.IdProduto = m.Id AND c.Id = pd.IdCor AND t.Id = pd.IdTamanho AND pd.Inutilizado = 0");
            SQL.AppendLine("	LEFT JOIN unidademedidas u ON u.Id = m.IdUniMedida ");
            SQL.AppendLine("    WHERE IOP.Status <> 0 AND c.Id <> 999 AND t.Id <> 999 ");

            if (filtro.idsMateriais != null && filtro.idsMateriais.Count() > 0)
                SQL.AppendLine(" AND m.Id in (" + string.Join(",", filtro.idsMateriais.ToArray()) + ")");

            if (filtro.idsCor != null && filtro.idsCor.Count() > 0)
                SQL.AppendLine(" AND c.Id in (" + string.Join(",", filtro.idsCor.ToArray()) + ")");

            if (filtro.idsTamanho != null && filtro.idsTamanho.Count() > 0)
                SQL.AppendLine(" AND t.Id in (" + string.Join(",", filtro.idsTamanho.ToArray()) + ")");

            if (filtro.idsOrdem != null && filtro.idsOrdem.Count() > 0)
                SQL.AppendLine(" AND op.Id in (" + string.Join(",", filtro.idsOrdem.ToArray()) + ")");

            if (filtro.Grupo != null && filtro.Grupo.Count() > 0)
                SQL.AppendLine(" AND m.IdGrupo in (" + string.Join(",", filtro.Grupo.ToArray()) + ")");

            switch (filtro.StatusOrdem)
            {
                case 1:
                    SQL.Append(" AND OP.status = 1");
                    break;
                case 2:
                    SQL.Append(" AND OP.status <> 1 AND OP.status <> 6 ");
                    break;
                case 3:
                    SQL.Append(" AND OP.status <> 6");
                    break;
                case 4:
                    SQL.Append(" AND OP.status in (1,8,9,10)");
                    break;                
                default:
                    SQL.Append(" AND OP.status > 0 ");
                    break;

            }

            SQL.AppendLine("    GROUP BY m.Id, c.Id, t.Id) ");

            SQL.AppendLine(" UNION ALL ");

            SQL.AppendLine("	(SELECT grupoprodutos.Id as idGrupoMaterial, grupoprodutos.descricao as grupoMaterial,m.Id as materiaPrimaId, c.Id as corId, t.Id as tamanhoId, m.referencia, m.descricao, c.abreviatura as corAbreviatura, ");
            SQL.AppendLine("		t.abreviatura as tamAbreviatura, u.abreviatura as uniAbreviatura, SUM(iop.quantidade * fi.quantidade) AS quantidade");
            SQL.AppendLine("     FROM ordemproducao op ");
            SQL.AppendLine("    INNER JOIN itensordemproducao iop ON op.Id = iop.OrdemProducaoId ");
            SQL.AppendLine("    INNER JOIN fichatecnicadomaterial ft ON ft.ProdutoId = iop.produtoid ");
            SQL.AppendLine("    INNER JOIN fichatecnicadomaterialitem fi ON ft.id = fi.FichaTecnicaId");
            SQL.AppendLine("    INNER JOIN fichatecnicadomaterialrelacao FR ON ");
            SQL.AppendLine("    (FR.fichatecnicaid = ft.Id AND FR.materiaPrimaId = FI.materiaPrimaId AND FR.cor_produto_id = IOP.CorId AND FR.tamanho_produto_id = IOP.TamanhoId AND FR.fichatecnicaitemId = FI.Id) ");
            SQL.AppendLine("	INNER JOIN produtos m ON fr.MateriaPrimaId = m.Id	");
            SQL.AppendLine("	INNER JOIN grupoprodutos ON grupoprodutos.Id = m.IdGrupo	");
            SQL.AppendLine("	INNER JOIN cores c ON fr.cor_materiaprima_Id = c.Id ");
            SQL.AppendLine("	INNER JOIN tamanhos t ON fr.tamanho_materiaprima_Id = t.Id  ");
            SQL.AppendLine("	INNER JOIN produtodetalhes pd ON pd.IdProduto = m.Id AND c.Id = pd.IdCor AND t.Id = pd.IdTamanho AND pd.Inutilizado = 0");
            SQL.AppendLine("	LEFT JOIN unidademedidas u ON u.Id = m.IdUniMedida ");
            SQL.AppendLine("    WHERE IOP.Status = 0 AND c.Id <> 999 AND t.Id <> 999 ");

            if (filtro.idsMateriais != null && filtro.idsMateriais.Count() > 0)
                SQL.AppendLine(" AND m.Id in (" + string.Join(",", filtro.idsMateriais.ToArray()) + ")");

            if (filtro.idsCor != null && filtro.idsCor.Count() > 0)
                SQL.AppendLine(" AND c.Id in (" + string.Join(",", filtro.idsCor.ToArray()) + ")");

            if (filtro.idsTamanho != null && filtro.idsTamanho.Count() > 0)
                SQL.AppendLine(" AND t.Id in (" + string.Join(",", filtro.idsTamanho.ToArray()) + ")");

            if (filtro.idsOrdem != null && filtro.idsOrdem.Count() > 0)
                SQL.AppendLine(" AND op.Id in (" + string.Join(",", filtro.idsOrdem.ToArray()) + ")");

            if (filtro.Grupo != null && filtro.Grupo.Count() > 0)
                SQL.AppendLine(" AND m.IdGrupo in (" + string.Join(",", filtro.Grupo.ToArray()) + ")");

            switch (filtro.StatusOrdem)
            {
                case 1:
                    SQL.Append(" AND OP.status = 1");
                    break;
                case 2:
                    SQL.Append(" AND OP.status <> 1 AND OP.status <> 6 ");
                    break;
                case 3:
                    SQL.Append(" AND OP.status <> 6");
                    break;
                case 4:
                    SQL.Append(" AND OP.status in (1,8,9,10)");
                    break;
                default:
                    SQL.Append(" AND OP.status > 0 ");
                    break;

            }

            SQL.AppendLine("    GROUP BY m.Id, c.Id, t.Id) ");
            
            SQL.AppendLine(" ) as material");
            SQL.AppendLine(" GROUP BY materiaPrimaId, corId, tamanhoId ");
            SQL.AppendLine(" ORDER BY descricao, corAbreviatura, tamanhoId");

            var cn = new DapperConnection<ConsultaRelListaMateriaisView>();
            return cn.ExecuteStringSqlToList(new ConsultaRelListaMateriaisView(), SQL.ToString());
        }

        public IEnumerable<ConsultaRelListaMateriaisView> GetTodosMateriaisBaseadoOP(FiltroRelListaMateriais filtro)
        {
            var SQL = new StringBuilder();

            SQL.AppendLine("SELECT m.Id as materiaPrimaId, ifnull(c.Id,0) as corId, ifnull(t.Id,0) as tamanhoId, m.referencia, m.descricao, c.abreviatura as corAbreviatura, ");
            SQL.AppendLine(" t.abreviatura as tamAbreviatura, u.abreviatura as uniAbreviatura");
            SQL.AppendLine("     FROM produtos m ");
            SQL.AppendLine("	 LEFT JOIN produtodetalhes pd ON pd.IdProduto = m.Id AND pd.Inutilizado = 0");
            SQL.AppendLine("	 LEFT JOIN cores c ON pd.IdCor = c.Id ");
            SQL.AppendLine("	 LEFT JOIN tamanhos t ON pd.IdTamanho = t.Id  ");
            SQL.AppendLine("	 LEFT JOIN unidademedidas u ON u.Id = m.IdUniMedida ");                       
            SQL.AppendLine("     WHERE (m.TipoItem = 1 OR m.TipoItem = 2) AND m.Ativo = 1 AND c.Id <> 999 AND t.Id <> 999 ");

            if (filtro.idsMateriais != null && filtro.idsMateriais.Count() > 0)
                SQL.AppendLine(" AND m.Id in (" + string.Join(",", filtro.idsMateriais.ToArray()) + ")");

            if (filtro.idsCor != null && filtro.idsCor.Count() > 0)
                SQL.AppendLine(" AND c.Id in (" + string.Join(",", filtro.idsCor.ToArray()) + ")");

            if (filtro.idsTamanho != null && filtro.idsTamanho.Count() > 0)
                SQL.AppendLine(" AND t.Id in (" + string.Join(",", filtro.idsTamanho.ToArray()) + ")");

            SQL.AppendLine(" GROUP BY materiaPrimaId, corId, tamanhoId ");            
            SQL.AppendLine(" ORDER BY descricao, corAbreviatura, tamanhoId");

            var cn = new DapperConnection<ConsultaRelListaMateriaisView>();
            return cn.ExecuteStringSqlToList(new ConsultaRelListaMateriaisView(), SQL.ToString());
        }

        public IEnumerable<ConsultaRelListaMateriaisView> GetValoresOP(FiltroRelListaMateriais filtro)
        {
            var SQL = new StringBuilder();

            SQL.AppendLine("SELECT materiaPrimaId, corId, tamanhoId, SUM(qtdopliberada) AS qtdopliberada,  SUM(qtdopnaoliberada) AS qtdopnaoliberada, SUM(estoquebaixado) AS estoquebaixado,  SUM(estoqueempenhado) AS estoqueempenhado  ");
            SQL.AppendLine("FROM (");
            SQL.AppendLine("	(SELECT m.Id as materiaPrimaId, c.Id as corId, t.Id as tamanhoId, m.descricao, c.abreviatura as corAbreviatura,");
            SQL.AppendLine("		IF(SUM(opm.quantidadenecessaria - opm.quantidadebaixada - opm.quantidadeempenhada) < 0, 0 , SUM(opm.quantidadenecessaria - opm.quantidadebaixada - opm.quantidadeempenhada)) as qtdopliberada, ");
            SQL.AppendLine("         0  as qtdopnaoliberada, SUM(opm.quantidadebaixada) as estoquebaixado, SUM(opm.quantidadeempenhada) as estoqueempenhado");
            SQL.AppendLine("     FROM ordemproducao op ");
            SQL.AppendLine("	INNER JOIN itensordemproducao iop ON op.Id = iop.OrdemProducaoId ");
            SQL.AppendLine("	INNER JOIN ordemproducaomateriais opm ON op.Id = opm.OrdemProducaoId AND opm.itemordemproducaoid = iop.id ");
            SQL.AppendLine("	INNER JOIN produtos m ON opm.MateriaPrimaId = m.Id	");
            SQL.AppendLine("	INNER JOIN cores c ON opm.CorId = c.Id ");
            SQL.AppendLine("	INNER JOIN tamanhos t ON opm.TamanhoId = t.Id  ");
            SQL.AppendLine("    WHERE IOP.Status <> 0 AND c.Id <> 999 AND t.Id <> 999 ");

            if (filtro.idsMateriais != null && filtro.idsMateriais.Count() > 0)
                SQL.AppendLine(" AND m.Id in (" + string.Join(",", filtro.idsMateriais.ToArray()) + ")");

            if (filtro.idsCor != null && filtro.idsCor.Count() > 0)
                SQL.AppendLine(" AND c.Id in (" + string.Join(",", filtro.idsCor.ToArray()) + ")");

            if (filtro.idsTamanho != null && filtro.idsTamanho.Count() > 0)
                SQL.AppendLine(" AND t.Id in (" + string.Join(",", filtro.idsTamanho.ToArray()) + ")");

            SQL.AppendLine(" AND OP.status <> 6");

            SQL.AppendLine("    GROUP BY m.Id, c.Id, t.Id) ");

            SQL.AppendLine(" UNION ALL ");

            SQL.AppendLine("	(SELECT m.Id as materiaPrimaId, c.Id as corId, t.Id as tamanhoId, m.descricao, c.abreviatura as corAbreviatura,");
            SQL.AppendLine("        0 as qtdopliberada, SUM(iop.quantidade * fi.quantidade)  as qtdopnaoliberada, 0 as estoquebaixado, 0 as estoqueempenhado");
            SQL.AppendLine("     FROM ordemproducao op ");
            SQL.AppendLine("    INNER JOIN itensordemproducao iop ON op.Id = iop.OrdemProducaoId ");
            SQL.AppendLine("    INNER JOIN fichatecnicadomaterial ft ON ft.ProdutoId = iop.produtoid ");
            SQL.AppendLine("    INNER JOIN fichatecnicadomaterialitem fi ON ft.id = fi.FichaTecnicaId");
            SQL.AppendLine("    INNER JOIN fichatecnicadomaterialrelacao FR ON ");
            SQL.AppendLine("    (FR.fichatecnicaid = ft.Id AND FR.materiaPrimaId = FI.materiaPrimaId AND FR.cor_produto_id = IOP.CorId AND FR.tamanho_produto_id = IOP.TamanhoId AND FR.fichatecnicaitemId = FI.Id) ");
            SQL.AppendLine("	INNER JOIN produtos m ON fr.MateriaPrimaId = m.Id	");
            SQL.AppendLine("	INNER JOIN cores c ON fr.cor_materiaprima_Id = c.Id ");
            SQL.AppendLine("	INNER JOIN tamanhos t ON fr.tamanho_materiaprima_Id = t.Id  ");
            SQL.AppendLine("    WHERE IOP.Status = 0 AND c.Id <> 999 AND t.Id <> 999 ");

            if (filtro.idsMateriais != null && filtro.idsMateriais.Count() > 0)
                SQL.AppendLine(" AND m.Id in (" + string.Join(",", filtro.idsMateriais.ToArray()) + ")");

            if (filtro.idsCor != null && filtro.idsCor.Count() > 0)
                SQL.AppendLine(" AND c.Id in (" + string.Join(",", filtro.idsCor.ToArray()) + ")");

            if (filtro.idsTamanho != null && filtro.idsTamanho.Count() > 0)
                SQL.AppendLine(" AND t.Id in (" + string.Join(",", filtro.idsTamanho.ToArray()) + ")");            

            SQL.AppendLine(" AND OP.status <> 6");             

            SQL.AppendLine("    GROUP BY m.Id, c.Id, t.Id) ");

            SQL.AppendLine(" ) as material");
            SQL.AppendLine(" GROUP BY materiaPrimaId, corId, tamanhoId ");
            SQL.AppendLine(" ORDER BY descricao, corAbreviatura, tamanhoId");

            var cn = new DapperConnection<ConsultaRelListaMateriaisView>();
            return cn.ExecuteStringSqlToList(new ConsultaRelListaMateriaisView(), SQL.ToString());
        }

        public void AtualizaDadosOrdemFaturada(bool Saida,int idOrdem, int IdMaterial, int IdCor, int IdTamanho, decimal QtdBaixada, decimal QtdAcimaDaOp, int idAlmoxarifado)
        {
            string SQL = String.Empty;
            decimal QuantidadeMovimentar = 0;
            decimal QuantidadeBaixadaFinal = 0;

            var cn = new DapperConnection<OrdemProducaoMaterial>();

            var mt = new OrdemProducaoMaterial();
            SQL = "SELECT * from ordemproducaomateriais  WHERE ordemproducaoid = " + idOrdem +
                    " AND materiaprimaid = " + IdMaterial + " AND corid = " + IdCor + " AND tamanhoid = " + IdTamanho + " AND armazemid = " + idAlmoxarifado;
            var dados = cn.ExecuteStringSqlToList(mt, SQL);

            QuantidadeBaixadaFinal = QtdBaixada - QtdAcimaDaOp;


            if (Saida)
            {

                foreach (var item in dados)
                {
                    QuantidadeMovimentar =  item.QuantidadeEmpenhada + item.EmpenhoProducao;
                    SQL = "UPDATE ordemproducaomateriais set quantidadebaixada = " + QuantidadeMovimentar.ToString().Replace(",", ".") + " ,quantidadeempenhada = 0,EmpenhoProducao = 0 " + " WHERE ordemproducaoid = " + idOrdem +
                  " AND materiaprimaid = " + IdMaterial + " AND corid = " + IdCor + " AND tamanhoid = " + IdTamanho + " AND ordemproducaomateriais.id = " + item.Id + " AND armazemid = " + idAlmoxarifado;
                    _cn.ExecuteNonQuery(SQL);
                }

                try
                {
                    /*
                    SQL = "UPDATE ordemproducaomateriais set quantidadebaixada = " + QuantidadeMovimentar.ToString().Replace(",", ".") + " ,quantidadeempenhada = 0,EmpenhoProducao = 0 " + " WHERE ordemproducaoid = " + idOrdem +
                    " AND materiaprimaid = " + IdMaterial + " AND corid = " + IdCor + " AND tamanhoid = " + IdTamanho;
                    _cn.ExecuteNonQuery(SQL);
                    */

                    if(QtdAcimaDaOp > 0)
                    {
                        var mt2 = new OrdemProducaoMaterial();
                        SQL = "SELECT * from ordemproducaomateriais  WHERE ordemproducaoid = " + idOrdem +
                                " AND materiaprimaid = " + IdMaterial + " AND corid = " + IdCor + " AND tamanhoid = " + IdTamanho + " AND armazemid = " + idAlmoxarifado + " limit 1";
                        var dados2 = cn.ExecuteStringSqlToList(mt2, SQL);

                        foreach (var item2 in dados2)
                        {
                            SQL = "UPDATE ordemproducaomateriais set  quantidadebaixada = quantidadebaixada + " + QtdAcimaDaOp.ToString().Replace(",", ".") + " WHERE ordemproducaoid = " + idOrdem +
                        " AND materiaprimaid = " + IdMaterial + " AND corid = " + IdCor + " AND tamanhoid = " + IdTamanho + " AND ordemproducaomateriais.id = " + item2.Id + " AND armazemid = " + idAlmoxarifado; ;
                            _cn.ExecuteNonQuery(SQL);
                        }
                    }
                   

                }
                catch (Exception ex)
                {
                    throw ex;
                }

                try
                {
                    SQL = "UPDATE estoque set Empenhado = Empenhado - " + QuantidadeBaixadaFinal.ToString().Replace(",", ".") + " WHERE ProdutoId = " + IdMaterial + " AND corid = " + IdCor + " AND tamanhoid = " + IdTamanho + " AND AlmoxarifadoId = " + idAlmoxarifado;
                    _cn.ExecuteNonQuery(SQL);

                    SQL = "UPDATE estoque set Saldo = Saldo - " + QtdAcimaDaOp.ToString().Replace(",", ".") + " WHERE ProdutoId = " + IdMaterial + " AND corid = " + IdCor + " AND tamanhoid = " + IdTamanho + " AND AlmoxarifadoId = " + idAlmoxarifado;
                    _cn.ExecuteNonQuery(SQL);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                
                foreach (var item in dados)
                {

                    SQL = "UPDATE ordemproducaomateriais set  quantidadeempenhada = quantidadebaixada, quantidadebaixada = 0  WHERE ordemproducaoid = " + idOrdem +
                          " AND materiaprimaid = " + IdMaterial + " AND corid = " + IdCor + " AND tamanhoid = " + IdTamanho + " AND ordemproducaomateriais.id = " + item.Id + " AND armazemid = " + idAlmoxarifado;
                    _cn.ExecuteNonQuery(SQL);
                    
                }



                try
                {
                    decimal qtdEstorno = QtdBaixada;
                    SQL = "UPDATE estoque set Empenhado = Empenhado + " + qtdEstorno.ToString().Replace(",", ".") + " WHERE ProdutoId = " + IdMaterial + " AND corid = " + IdCor + " AND tamanhoid = " + IdTamanho + " AND AlmoxarifadoId = " + idAlmoxarifado;
                    _cn.ExecuteNonQuery(SQL);
               
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
           





        }



        public IEnumerable<OrdemProducaoMaterialView> GetListByItemLiberacaoTotal(List<int> idsIOP, int ordemId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT PM.Referencia as MaterialReferencia, PM.Descricao as MaterialDescricao, FI.materiaPrimaId, FI.materiaPrimaId as MateriaPrimaOriginalId, (SUM(FI.quantidade*IOP.Quantidade)) as QuantidadeNecessaria,");
            SQL.AppendLine("C.Abreviatura as CorDescricao, T.Abreviatura as TamanhoDescricao, IOP.id as ItemOrdemProducaoId, ");
            SQL.AppendLine("C.ID as CorId, C.ID as CorOriginalId, T.Id as TamanhoId, T.Id as TamanhoOriginalId, AF.abreviatura as ArmazemDescricao, AF.id as ArmazemId, OP.id as OrdemProducaoId, UMD.abreviatura as UM, OP.Referencia as OrdemProducaoReferencia,");
            SQL.AppendLine("FR.cor_produto_id as CorProdutoId, FR.tamanho_produto_id as TamanhoProdutoId, FI.DestinoId, FI.sequencia ");   
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
            SQL.AppendLine("WHERE  ");
            SQL.Append(" IOP.Id IN ( " + string.Join(",", idsIOP) + " ) ");
            SQL.Append(" AND IOP.OrdemProducaoId = " + ordemId);            
            SQL.AppendLine(" GROUP BY  FI.materiaPrimaId, FR.cor_materiaprima_id, FR.tamanho_materiaprima_id ");

            SQL.AppendLine(" ORDER BY PM.Referencia, C.Abreviatura, T.id");
            var cn = new DapperConnection<OrdemProducaoMaterialView>();
            var MatNecess = cn.ExecuteStringSqlToList(new OrdemProducaoMaterialView(), SQL.ToString()).ToList();



            SQL = new StringBuilder();
            SQL.AppendLine("SELECT ordemproducaomateriais.Id, materiaprimaid, corid, tamanhoid, ");
            SQL.AppendLine("produtos.referencia, produtos.descricao,cores.Abreviatura,tamanhos.Abreviatura,unidademedidas.abreviatura,");
            SQL.AppendLine("ordemproducaomateriais.quantidadeempenhada,ordemproducaomateriais.quantidadebaixada");
            SQL.AppendLine("FROM ordemproducaomateriais");
            SQL.AppendLine("INNER JOIN produtos ON produtos.id = ordemproducaomateriais.materiaprimaid");
            SQL.AppendLine("INNER JOIN cores ON cores.id = ordemproducaomateriais.corid");
            SQL.AppendLine("INNER JOIN tamanhos ON tamanhos.Id = ordemproducaomateriais.tamanhoid");
            SQL.AppendLine("INNER JOIN unidademedidas ON unidademedidas.id = produtos.IdUniMedida");
            SQL.AppendLine("WHERE ordemproducaomateriais.ordemproducaoid = " + ordemId);
            SQL.AppendLine("ORDER BY produtos.Descricao,cores.Descricao,tamanhos.id");

            var cn2 = new DapperConnection<OrdemProducaoMaterialView>();
            var materialOrdem = new OrdemProducaoMaterialView();
            var MatInc = cn2.ExecuteStringSqlToList(new OrdemProducaoMaterialView(), SQL.ToString());


            foreach (var item in MatInc)
            {
                var teste = MatNecess.Where(m => m.MateriaPrimaId == item.MateriaPrimaId && m.TamanhoId == item.TamanhoId && m.CorId == item.CorId);

                var record = (from material in MatNecess where material.MateriaPrimaId == item.MateriaPrimaId && 
                              material.TamanhoId == item.TamanhoId && material.CorId == item.CorId select material).SingleOrDefault();

                if (record != null)
                {
                    record.Id = item.Id;
                    record.QuantidadeEmpenhada = item.QuantidadeEmpenhada;
                    record.QuantidadeBaixada = item.QuantidadeBaixada;
                }

            }
            MatNecess.Where(m => m.Id > 0);

            return MatNecess;
            
        }

        public void DeleteMateriaisLiberaTotal(int IdOp)
        {
            string SQL = String.Empty;

            SQL = "delete from ordemproducaomateriais where ordemproducaomateriais.ordemproducaoid =" + IdOp;
            _cn.ExecuteNonQuery(SQL);
        }

        public bool PossuiQuebraManual(int IdItem)
        {
            bool QbManual = false;
            string SQL = String.Empty;

            SQL = " select * from itensordemproducao " +
                  " INNER JOIN fichatecnicadomaterial ON fichatecnicadomaterial.ProdutoId = itensordemproducao.ProdutoId " +
                  " WHERE itensordemproducao.id = " + IdItem;

            var cnQB = new DapperConnection<FichaTecnicaDoMaterial>();
            var DadosFicha = cnQB.ExecuteStringSqlToList(new FichaTecnicaDoMaterial(), SQL).ToList();

            if(DadosFicha != null && DadosFicha.Count > 0)
            {
                foreach (var item in DadosFicha)
                {
                    if(!String.IsNullOrEmpty(item.QuebraManual))
                    {
                        QbManual = true;
                    }
                }
            }
           

            return QbManual;

        }
    }
}

/*QUERY ORIGINAL DA GetByOrdenEItem 28/10
 
            SQL.AppendLine("SELECT MaterialReferencia, MaterialDescricao, materiaPrimaId, MateriaPrimaOriginalId, (SUM(QuantidadeNecessaria)) as QuantidadeNecessaria,");
            SQL.AppendLine("sum(quantidadeempenhada) as quantidadeempenhada, sum(quantidadebaixada) as quantidadebaixada,");
            SQL.AppendLine("CorDescricao, TamanhoDescricao, ");
            SQL.AppendLine("CorId, TamanhoId,  ArmazemDescricao,  ArmazemId,  OrdemProducaoId,  UM,  OrdemProducaoReferencia,");
            SQL.AppendLine(" possuiquebra, QuebraManual, Destino, sequencia FROM ( ");

            SQL.AppendLine("(SELECT PM.Referencia as MaterialReferencia, PM.Descricao as MaterialDescricao, FI.materiaPrimaId, FI.materiaPrimaId as MateriaPrimaOriginalId, (SUM(FI.quantidade*IOP.Quantidade)) as QuantidadeNecessaria,");
            SQL.AppendLine("0 as quantidadeempenhada, 0 as quantidadebaixada,");
            SQL.AppendLine("C.Abreviatura as CorDescricao, T.Abreviatura as TamanhoDescricao, ");
            SQL.AppendLine("C.ID as CorId, T.Id as TamanhoId, AF.abreviatura as ArmazemDescricao, AF.id as ArmazemId, OP.id as OrdemProducaoId, UMD.abreviatura as UM, OP.Referencia as OrdemProducaoReferencia,");
            SQL.AppendLine("F.possuiquebra, F.QuebraManual, d.descricao AS Destino, FI.sequencia");
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
            SQL.AppendLine("LEFT JOIN destinos d ON d.Id = FI.DestinoId ");
            SQL.AppendLine("WHERE   OP.Id = ");
            SQL.Append(ordemId);
            SQL.Append(" AND IOP.id = " + itemId);
            SQL.Append(" AND ( IOP.Status = 0)");
            SQL.Append(" AND ( FR.cor_materiaprima_id <> 999 AND FR.tamanho_materiaprima_id <> 999 )");
            SQL.AppendLine(" GROUP BY  FI.materiaPrimaId, FR.cor_materiaprima_id, FR.tamanho_materiaprima_id ) ");
            //SQL.AppendLine("ORDER BY FI.sequencia");

            SQL.AppendLine("UNION ALL");

            SQL.AppendLine(" ( SELECT p.Referencia as MaterialReferencia, ");
            SQL.AppendLine("p.Descricao as MaterialDescricao, "); //F.possuiquebra, FI.DestinoId AS Destino, FI.sequencia,
            SQL.AppendLine("opm.materiaprimaid, opm.materiaprimaoriginalid, sum(opm.quantidadenecessaria) as quantidadenecessaria, ");
            SQL.AppendLine("sum(opm.quantidadeempenhada) as quantidadeempenhada, sum(opm.quantidadebaixada) as quantidadebaixada,");
            SQL.AppendLine("c.abreviatura as CorDescricao,");
            SQL.AppendLine("t.abreviatura as TamanhoDescricao,");
            SQL.AppendLine("opm.corid, opm.tamanhoid, a.abreviatura as ArmazemDescricao,  opm.armazemid,O.id as OrdemProducaoId,");
            SQL.AppendLine("u.abreviatura as UM,");
            SQL.AppendLine("o.Referencia as OrdemProducaoReferencia, F.possuiquebra, F.QuebraManual, d.descricao AS Destino, opm.sequencia");
            SQL.AppendLine("FROM 	ordemproducaomateriais opm");
            SQL.AppendLine("INNER JOIN ordemproducao o ON o.Id = opm.OrdemProducaoId ");
            SQL.AppendLine("INNER JOIN itensordemproducao IOP ON IOP.Id = opm.ItemOrdemProducaoId ");

            //SQL.AppendLine("INNER JOIN fichatecnicadomaterialrelacao FR ON ");
            //SQL.AppendLine("(FR.produtoId = IOP.ProdutoId   AND FR.cor_produto_Id = IOP.CorId AND FR.Tamanho_produto_Id = IOP.Tamanhoid");
            //SQL.AppendLine("AND FR.materiaPrimaId = OPM.materiaprimaoriginalid AND FR.cor_materiaprima_Id = OPM.cororiginalid AND FR.Tamanho_materiaprima_Id = OPM.tamanhooriginalid) ");
            SQL.AppendLine("INNER JOIN fichatecnicadomaterial F ON (F.produtoid = IOP.ProdutoId)");                        
            //SQL.AppendLine("INNER JOIN fichatecnicadomaterialitem FI ON (FI.fichatecnicaid = F.Id AND FI.materiaPrimaId = OPM.materiaprimaoriginalid) ");

            SQL.AppendLine("INNER JOIN produtos p ON p.Id = opm.materiaprimaid ");
            SQL.AppendLine("INNER JOIN cores c ON c.Id = opm.corid ");
            SQL.AppendLine("INNER JOIN tamanhos t ON t.Id = opm.tamanhoid ");
            SQL.AppendLine("LEFT JOIN almoxarifados a ON a.Id = opm.armazemid ");
            SQL.AppendLine("LEFT JOIN unidademedidas u ON u.Id = p.IdUniMedida ");
            SQL.AppendLine("LEFT JOIN destinos d ON d.Id = opm.DestinoId ");
            SQL.AppendLine("WHERE	opm.OrdemProducaoId = ");
            SQL.Append(ordemId);
            SQL.AppendLine(" AND	opm.ItemOrdemProducaoId = ");
            SQL.Append(itemId);
            SQL.Append(" AND IOP.Status > 0 ");
            SQL.AppendLine(" GROUP BY  opm.materiaPrimaId, opm.corid, opm.tamanhoid )) as opmaterial ");
            SQL.AppendLine(" GROUP BY  materiaPrimaId, CorId, Tamanhoid");
            SQL.AppendLine(" ORDER BY sequencia, MaterialReferencia, CorDescricao, TamanhoDescricao");

 * */
