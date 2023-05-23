using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;
using Vestillo.Core.Models;
using Vestillo.Lib;

namespace Vestillo.Business.Repositories
{
    public class FichaTecnicaDoMaterialItemRepository : GenericRepository<FichaTecnicaDoMaterialItem>
    {
        public FichaTecnicaDoMaterialItemRepository()
            : base(new DapperConnection<FichaTecnicaDoMaterialItem>())
        { }


        public IEnumerable<FichaTecnicaDoMaterialItem> GetAllViewByFichaTecnica(int FichaTecnicaId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT * from fichatecnicadomaterialitem");
            SQL.AppendLine(" where FichaTecnicaId= " + FichaTecnicaId.ToString());

         
            var cn = new DapperConnection<FichaTecnicaDoMaterialItem>();
            return cn.ExecuteStringSqlToList(new FichaTecnicaDoMaterialItem(), SQL.ToString());
        }

        public IEnumerable<FichaTecnicaDoMaterialItem> GetAllViewByProdutoId(int produtoId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT FI.* from fichatecnicadomaterialitem FI");
            SQL.AppendLine("INNER JOIN fichatecnicadomaterial F ON F.Id = FI.fichatecnicaId");
            SQL.AppendLine(" where F.ProdutoId = " + produtoId + " OR MateriaPrimaId = " + produtoId);


            var cn = new DapperConnection<FichaTecnicaDoMaterialItem>();
            return cn.ExecuteStringSqlToList(new FichaTecnicaDoMaterialItem(), SQL.ToString());
        }

        public void ExcluirRelacao(int FichaTecnicaId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("delete from fichatecnicadomaterialitem");
            SQL.AppendLine(" where fichatecnicaId= " + FichaTecnicaId.ToString());
            var cn = new DapperConnection<FichaTecnicaDoMaterialItem>();
            cn.ExecuteNonQuery(SQL.ToString());
        }

        public IEnumerable<FichaTecnicaDoMaterialItem> GetListByProdutos(int[] produtosIds)
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT FI.* FROM fichatecnicadomaterialitem FI");
            sql.AppendLine("INNER JOIN fichatecnicadomaterial F ON F.Id = FI.fichatecnicaId");
            sql.AppendLine("WHERE F.ProdutoId IN (" + string.Join(", ", produtosIds) + ")");

            return _cn.ExecuteStringSqlToList(new FichaTecnicaDoMaterialItem(), sql.ToString());
        }

        public IEnumerable<FichaTecnicaDoMaterialItem> GetListByProduto(int produto)
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT FI.* FROM fichatecnicadomaterialitem FI");
            sql.AppendLine("INNER JOIN fichatecnicadomaterial F ON F.Id = FI.fichatecnicaId");
            sql.AppendLine("WHERE F.ProdutoId = " + produto + "");

            return _cn.ExecuteStringSqlToList(new FichaTecnicaDoMaterialItem(), sql.ToString());
        }

        public IEnumerable<FichaTecnicaDoMaterialItem> GetListByMateriaPrima(int materiaId)
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT FI.* FROM fichatecnicadomaterialitem FI");
            sql.AppendLine("INNER JOIN fichatecnicadomaterial F ON F.Id = FI.fichatecnicaId");
            sql.AppendLine("WHERE FI.MateriaPrimaId = " +  materiaId + " and F.Ativo = 1");

            return _cn.ExecuteStringSqlToList(new FichaTecnicaDoMaterialItem(), sql.ToString());
        }

        public FichaTecnicaDoMaterialItem GetByProdutoMateriaPrima(int materiaId, int corId, int tamanhoId)
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT FI.id, fi.fichatecnicaid, fi.materiaprimaId, avg(fi.preco) as preco FROM fichatecnicadomaterialitem FI");
            sql.AppendLine("INNER JOIN  fichatecnicadomaterialrelacao FR ON FI.FichaTecnicaId = FR.FichaTecnicaId");
            sql.AppendLine("WHERE FI.MateriaPrimaId = " + materiaId + "");
            sql.AppendLine("AND FR.Cor_MateriaPrima_id = " + corId + " AND FR.Tamanho_MateriaPrima_Id = " + tamanhoId);
            sql.AppendLine("GROUP BY materiaprimaid");

            FichaTecnicaDoMaterialItem item = new FichaTecnicaDoMaterialItem();
            _cn.ExecuteToModel(ref item, sql.ToString());
            return item;
        }

        public void TotalMateriais (List<ProdutoFichaView> ProdutoFicha,int IdProduto)
        {
            string SQl = String.Empty;
            decimal Soma = 0;
            int IdFicha = 0;


            try
            {
                SQl = "  SELECT Id from fichatecnicadomaterial where fichatecnicadomaterial.ProdutoId = " + IdProduto;
                var cn = new DapperConnection<FichaTecnicaDoMaterial>();
                var dados = cn.ExecuteStringSqlToList(new FichaTecnicaDoMaterial(), SQl);
                foreach (var item in dados)
                {
                    IdFicha = item.Id;
                }
               

                var cnItem = new DapperConnection<FichaTecnicaDoMaterialItem>();
                foreach (var itemMaterial in ProdutoFicha)
                {
                    
                    
                    SQl = " UPDATE FichaTecnicaDoMaterialItem SET quantidade = " + itemMaterial.quantidade.ToString().Replace(",", ".") + ",custocalculado = " 
                        + itemMaterial.valor.ToString().Replace(",", ".") + ",valor = " + itemMaterial.valor.ToString().Replace(",", ".") + " WHERE fichatecnicaId = " + IdFicha + " AND materiaprimaId = " + itemMaterial.MaterialId + " AND sequencia = " + itemMaterial.Numero;

                    cnItem.ExecuteNonQuery(SQl);

                }


                SQl = " SELECT SUM(valor) as valor from fichatecnicadomaterialitem where fichatecnicadomaterialitem.fichatecnicaId = " + IdFicha;
                
                var dadosTotalizador = cnItem.ExecuteStringSqlToList(new FichaTecnicaDoMaterialItem(), SQl);

                foreach (var item in dadosTotalizador)
                {
                    Soma = item.valor;
                }

                Soma = Soma.ToRound(2);

                SQl = " UPDATE fichatecnicadomaterial SET total = " + Soma.ToString().Replace(",",".") + " WHERE ProdutoId = " + IdProduto;
                var cn2 = new DapperConnection<FichaTecnicaDoMaterial>();
                cn2.ExecuteNonQuery(SQl);
            }
            catch(Exception ex)
            {

            }

    


        }            
            
    }
}
