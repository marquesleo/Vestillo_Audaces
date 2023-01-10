using Vestillo.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class ProdutoMaterialRepository : GenericRepository<ProdutoMaterial>
    {
        public ProdutoMaterialRepository()
            : base(new DapperConnection<ProdutoMaterial>())
        {
        }

        public IEnumerable<ProdutoMaterial> GetByMaterial(int material)
        {
            var SQL = new StringBuilder();

            SQL.AppendLine("SELECT * ");
            SQL.AppendLine("FROM 	ProdutoMaterial PM");
            //SQL.AppendLine("INNER JOIN Almoxarifados A ON A.Id = E.AlmoxarifadoId");
            //SQL.AppendLine("INNER JOIN Produtos P ON P.Id = E.ProdutoId");  + FiltroEmpresa("P.IdEmpresa")

            SQL.AppendLine("WHERE ");
            SQL.AppendLine(" PM.IdMaterial = " + material);

            var cn = new DapperConnection<ProdutoMaterial>();
            return cn.ExecuteStringSqlToList(new ProdutoMaterial(), SQL.ToString());
        }

        public IEnumerable<ProdutoMaterial> GetByProduto(int produto)
        {
            var SQL = new StringBuilder();

            SQL.AppendLine("SELECT * ");
            SQL.AppendLine("FROM 	ProdutoMaterial PM");
            //SQL.AppendLine("INNER JOIN Almoxarifados A ON A.Id = E.AlmoxarifadoId");
            //SQL.AppendLine("INNER JOIN Produtos P ON P.Id = E.ProdutoId");  + FiltroEmpresa("P.IdEmpresa")

            SQL.AppendLine("WHERE ");
            SQL.AppendLine(" PM.IdProduto = " + produto);

            var cn = new DapperConnection<ProdutoMaterial>();
            return cn.ExecuteStringSqlToList(new ProdutoMaterial(), SQL.ToString());
        }

        public ProdutoMaterial GetByProdutoMaterial(int produto, int material)
        {
            var SQL = new StringBuilder();

            SQL.AppendLine("SELECT * ");
            SQL.AppendLine("FROM 	ProdutoMaterial PM");
            //SQL.AppendLine("INNER JOIN Almoxarifados A ON A.Id = E.AlmoxarifadoId");
            //SQL.AppendLine("INNER JOIN Produtos P ON P.Id = E.ProdutoId");  + FiltroEmpresa("P.IdEmpresa")

            SQL.AppendLine("WHERE ");
            SQL.AppendLine(" PM.IdMaterial = " + material);
            SQL.AppendLine(" AND PM.IdProduto = " + produto);

            var cn = new DapperConnection<ProdutoMaterial>();
            ProdutoMaterial pm = new ProdutoMaterial();
            cn.ExecuteToModel( ref pm, SQL.ToString());
            return pm;
        }

        public void DeletaBaseadoNoProduto(int produtoId)
        {
            var SQL = String.Empty;
            SQL = "DELETE FROM ProdutoMaterial WHERE ProdutoMaterial.IdProduto = " + produtoId;
            _cn.ExecuteNonQuery(SQL);
        }

        public void DeletaBaseadoNoMaterial(int materialId)
        {
            var SQL = String.Empty;
            SQL = "DELETE FROM ProdutoMaterial WHERE ProdutoMaterial.IdMaterial = " + materialId;
            _cn.ExecuteNonQuery(SQL);
        }
    }
}
