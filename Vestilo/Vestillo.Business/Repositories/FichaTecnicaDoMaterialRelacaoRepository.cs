using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;
using Vestillo.Lib;
using Vestillo.Business.Models;

namespace Vestillo.Business.Repositories
{
    public class FichaTecnicaDoMaterialRelacaoRepository : GenericRepository<FichaTecnicaDoMaterialRelacao>
    {
        public FichaTecnicaDoMaterialRelacaoRepository()
            : base(new DapperConnection<FichaTecnicaDoMaterialRelacao>())
        { }

        public IEnumerable<FichaTecnicaDoMaterialRelacao> GetAllViewByFichaTecnica(int FichaTecnicaId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT * from fichatecnicadomaterialrelacao");
            SQL.AppendLine(" where fichatecnicaId= " + FichaTecnicaId.ToString());


            var cn = new DapperConnection<FichaTecnicaDoMaterialRelacao>();
            return cn.ExecuteStringSqlToList(new FichaTecnicaDoMaterialRelacao(), SQL.ToString());
        }

        public IEnumerable<FichaTecnicaDoMaterialRelacao> GetAllViewByFichaTecnicaItem(int FichaTecnicaItemId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT * from fichatecnicadomaterialrelacao");
            SQL.AppendLine(" where fichatecnicaitemId= " + FichaTecnicaItemId.ToString());


            var cn = new DapperConnection<FichaTecnicaDoMaterialRelacao>();
            return cn.ExecuteStringSqlToList(new FichaTecnicaDoMaterialRelacao(), SQL.ToString());
        }

        public IEnumerable<FichaTecnicaDoMaterialRelacao> GetAllViewByGradeProduto(ProdutoDetalhe grade)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT * from fichatecnicadomaterialrelacao");
            SQL.AppendLine(" where produtoid = " + grade.IdProduto);
            SQL.AppendLine(" AND cor_produto_id= " + grade.Idcor);
            SQL.AppendLine(" AND tamanho_produto_id = " + grade.IdTamanho);

            var cn = new DapperConnection<FichaTecnicaDoMaterialRelacao>();
            return cn.ExecuteStringSqlToList(new FichaTecnicaDoMaterialRelacao(), SQL.ToString());
        }

        public void ExcluirRelacao(int FichaTecnicaId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("delete from fichatecnicadomaterialrelacao");
            SQL.AppendLine(" where fichatecnicaId= " + FichaTecnicaId.ToString());
            var cn = new DapperConnection<FichaTecnicaDoMaterialRelacao>();
            cn.ExecuteNonQuery(SQL.ToString());
        }

        public IEnumerable<FichaTecnicaDoMaterialRelacao> GetAllViewByGrade(ProdutoDetalheView grade)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT * FROM fichatecnicadomaterialrelacao WHERE");
            SQL.AppendLine(" ( ");
            SQL.AppendLine(" materiaprimaid = " + grade.IdProduto);
            SQL.AppendLine(" AND cor_materiaprima_id = " + grade.Idcor);
            SQL.AppendLine(" AND tamanho_materiaprima_id = " + grade.IdTamanho);            
            SQL.AppendLine(" ) OR (");
            SQL.AppendLine(" produtoid = " + grade.IdProduto);
            SQL.AppendLine(" AND cor_produto_id= " + grade.Idcor);
            SQL.AppendLine(" AND tamanho_produto_id = " + grade.IdTamanho);
            SQL.AppendLine(" ) ");           

            var cn = new DapperConnection<FichaTecnicaDoMaterialRelacao>();
            return cn.ExecuteStringSqlToList(new FichaTecnicaDoMaterialRelacao(), SQL.ToString());
        }

        public void UpdateFichaRelacao(FichaTecnicaDoMaterialRelacao ficha, int TipoItem)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("UPDATE fichatecnicadomaterialrelacao SET ");
            if(TipoItem == 0)
            {
                SQL.AppendLine("  tamanho_produto_id = " + ficha.Tamanho_Produto_Id + ", ");
                SQL.AppendLine("  cor_produto_id =  " + ficha.Cor_Produto_Id);
            }
            else
            {
                SQL.AppendLine("  tamanho_materiaprima_id = " + ficha.Tamanho_Materiaprima_Id + ", ");
                SQL.AppendLine("  cor_materiaprima_id =  " + ficha.cor_materiaprima_Id);
            }
            SQL.AppendLine(" WHERE Id = ");
            SQL.Append(ficha.Id);

            _cn.ExecuteNonQuery(SQL.ToString());
        }


    }
}
