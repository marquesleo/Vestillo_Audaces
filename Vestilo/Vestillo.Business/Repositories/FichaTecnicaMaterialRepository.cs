using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;
using Vestillo.Lib;
using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;

namespace Vestillo.Business.Repositories
{
    public class FichaTecnicaDoMaterialRepository : GenericRepository<FichaTecnicaDoMaterial>
    {
        public FichaTecnicaDoMaterialRepository()
            : base(new DapperConnection<FichaTecnicaDoMaterial>())
        { }

        public IEnumerable<FichaTecnicaDoMaterialView> GetAllView()
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT F.*,");
            SQL.AppendLine("    o.Observacao AS Observacao, ");
            SQL.AppendLine("    P.Referencia AS ProdutoReferencia,");
            SQL.AppendLine("    P.Descricao AS ProdutoDescricao, ");
            SQL.AppendLine("    C.descricao AS Colecao, ");
            SQL.AppendLine("    GP.descricao AS GrupoProdutoDescricao, ");
            SQL.AppendLine("    S.descricao AS Segmento, ");
            SQL.AppendLine("    U.Nome AS NomeUsuario ");
            SQL.AppendLine("FROM 	fichatecnicadomaterial F");
            SQL.AppendLine(" INNER JOIN produtos P ON P.Id = F.ProdutoId	");
            SQL.AppendLine(" LEFT JOIN Colecoes AS C ON C.Id = P.Idcolecao	");
            SQL.AppendLine(" LEFT JOIN grupoprodutos AS GP ON GP.id = P.IdGrupo	");
            SQL.AppendLine(" LEFT JOIN segmentos AS S ON S.id = P.Idsegmento	");
            SQL.AppendLine(" LEFT JOIN observacaoproduto AS o ON p.id = o.ProdutoId ");
            SQL.AppendLine(" LEFT JOIN usuarios AS U ON U.id = f.UserId ");
            SQL.AppendLine(" WHERE " + FiltroEmpresa("", "F"));
     
            var cn = new DapperConnection<FichaTecnicaDoMaterialView>();
            return cn.ExecuteStringSqlToList(new FichaTecnicaDoMaterialView(), SQL.ToString());
        }


        public IEnumerable<FichaTecnicaDoMaterial> GetAllView(List<int> lstProdutos)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT F.* ");
            SQL.AppendLine("FROM 	fichatecnicadomaterial F");
            SQL.AppendLine(" WHERE " + FiltroEmpresa("", "F"));
            SQL.AppendLine(" and F.ProdutoId in (" + string.Join(",", lstProdutos.ToArray()) + ")");
            var cn = new DapperConnection<FichaTecnicaDoMaterial>();
            return cn.ExecuteStringSqlToList(new FichaTecnicaDoMaterial(), SQL.ToString());
        }

        public FichaTecnicaDoMaterial GetByProduto(int produtoId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT F.* ");
            SQL.AppendLine("FROM 	fichatecnicadomaterial F");
            SQL.AppendLine("WHERE " + FiltroEmpresa("", "F"));
            SQL.AppendLine(" and F.ProdutoId = " + produtoId);
            var cn = new DapperConnection<FichaTecnicaDoMaterial>();
            FichaTecnicaDoMaterial ficha = new FichaTecnicaDoMaterial();
            cn.ExecuteToModel(ref ficha, SQL.ToString());
            return ficha;
        }

        public IEnumerable<FichaTecnicaDoMaterial> GetAllViewByFiltro(FiltroCustoProdutoAnalitico filtro)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT F.*,  o.Observacao as Observacao "); //I.imagem as Imagem,
            SQL.AppendLine("FROM 	fichatecnicadomaterial F");
            SQL.AppendLine("    INNER JOIN produtos P ON P.id = F.ProdutoId");
            SQL.AppendLine("    LEFT JOIN fichatecnica FT ON (FT.ProdutoId = p.Id)");
            SQL.AppendLine("    LEFT JOIN observacaoproduto o ON (o.ProdutoId = p.Id)");
            //SQL.AppendLine("LEFT JOIN imagens i ON (i.idProduto = p.Id)");
            SQL.AppendLine("WHERE " + FiltroEmpresa("", "F"));

            if (filtro.Produtos != null && filtro.Produtos.Count > 0)
                SQL.AppendLine(" AND F.ProdutoId in (" + string.Join(",", filtro.Produtos.ToArray()) + ")");

            if (filtro.Grupo != null && filtro.Grupo.Count > 0)
                SQL.AppendLine(" AND P.idGrupo in (" + string.Join(",", filtro.Grupo.ToArray()) + ")");

            if(filtro.Segmento != null && filtro.Segmento.Count > 0)
                SQL.AppendLine(" AND P.idSegmento in (" + string.Join(",", filtro.Segmento.ToArray()) + ")");

            if (filtro.Colecao != null && filtro.Colecao.Count > 0)
                SQL.AppendLine(" AND P.idColecao in (" + string.Join(",", filtro.Colecao.ToArray()) + ")");


            var cn = new DapperConnection<FichaTecnicaDoMaterial>();
            return cn.ExecuteStringSqlToList(new FichaTecnicaDoMaterial(), SQL.ToString());
        }

        public IEnumerable<FichaTecnicaDoMaterial> GetAllViewByFiltro(FiltroCustoProdutoSintetico filtro)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT F.* "); 
            SQL.AppendLine("FROM 	fichatecnicadomaterial F");
            SQL.AppendLine("INNER JOIN produtos P ON P.id = F.ProdutoId");
            SQL.AppendLine("WHERE " + FiltroEmpresa("", "F"));

            if (filtro.Produtos != null && filtro.Produtos.Count > 0)
                SQL.AppendLine(" AND F.ProdutoId in (" + string.Join(",", filtro.Produtos.ToArray()) + ")");

            if (filtro.Grupo != null && filtro.Grupo.Count > 0)
                SQL.AppendLine(" AND P.idGrupo in (" + string.Join(",", filtro.Grupo.ToArray()) + ")");

            if (filtro.Segmento != null && filtro.Segmento.Count > 0)
                SQL.AppendLine(" AND P.idSegmento in (" + string.Join(",", filtro.Segmento.ToArray()) + ")");

            if (filtro.Colecao != null && filtro.Colecao.Count > 0)
                SQL.AppendLine(" AND P.idColecao in (" + string.Join(",", filtro.Colecao.ToArray()) + ")");

            if(filtro.DoAno != "" && filtro.DoAno != null)
                SQL.AppendLine(" AND (P.ano > " + filtro.DoAno + " OR P.ano = " + filtro.DoAno + ") ");

            if (filtro.AteAno != "" && filtro.AteAno != null)
                SQL.AppendLine("AND (P.ano < " + filtro.AteAno + " OR P.ano = " + filtro.AteAno + ") ");

            if (filtro.OrdenarPor == 1)
            {
                SQL.AppendLine("ORDER BY P.Descricao");
            }
            else
            {
                SQL.AppendLine("ORDER BY P.Referencia");
            }

            var cn = new DapperConnection<FichaTecnicaDoMaterial>();
            return cn.ExecuteStringSqlToList(new FichaTecnicaDoMaterial(), SQL.ToString());
        }

        public IEnumerable<FichaTecnicaDoMaterialView> GetAllViewByFiltro(FiltroFichaTecnica filtro)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT F.* ");
            SQL.AppendLine("FROM 	fichatecnicadomaterial F");
            SQL.AppendLine("INNER JOIN fichatecnicadomaterial P ON P.id = F.ProdutoId");
            SQL.AppendLine("INNER JOIN produtos P ON P.id = F.ProdutoId");
            SQL.AppendLine("WHERE " + FiltroEmpresa("", "F"));

            //SQL.AppendLine(" where F.ProdutoId in (" + string.Join(",", filtro.Produtos.ToArray()) + ")");

            if (filtro.Produtos != null && filtro.Produtos.Count > 0)
                SQL.AppendLine(" AND P.id in (" + string.Join(",", filtro.Produtos.ToArray()) + ")");

            if (filtro.Catalogo != null && filtro.Catalogo.Count > 0)
                SQL.AppendLine(" AND P.idCatalogo in (" + string.Join(",", filtro.Catalogo.ToArray()) + ")");

            if (filtro.Grupo != null && filtro.Grupo.Count > 0)
                SQL.AppendLine(" AND P.idGrupo in (" + string.Join(",", filtro.Grupo.ToArray()) + ")");

            if (filtro.Segmento != null && filtro.Segmento.Count > 0)
                SQL.AppendLine(" AND P.idSegmento in (" + string.Join(",", filtro.Segmento.ToArray()) + ")");

            if (filtro.Colecao != null && filtro.Colecao.Count > 0)
                SQL.AppendLine(" AND P.idColecao in (" + string.Join(",", filtro.Colecao.ToArray()) + ")");


            var cn = new DapperConnection<FichaTecnicaDoMaterialView>();
            return cn.ExecuteStringSqlToList(new FichaTecnicaDoMaterialView(), SQL.ToString());
        }

    }
}
