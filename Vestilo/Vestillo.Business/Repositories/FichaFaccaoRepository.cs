using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo;
using Vestillo.Business.Models;
using Vestillo.Connection;
using System.Data;
using Vestillo.Lib;
using Vestillo.Business.Models.Views;

namespace Vestillo.Business.Repositories
{
    public class FichaFaccaoRepository : GenericRepository<FichaFaccao>
    {
        public FichaFaccaoRepository() : base(new DapperConnection<FichaFaccao>())
        {
        }

        public IEnumerable<FichaFaccao> VerificaFaccao(List<int> idPacote, List<int> idProduto , List<int> idFaccao)
        {
            var cn = new DapperConnection<FichaFaccao>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	ff.*");  
            SQL.AppendLine("FROM 	pacotes pa");
            SQL.AppendLine("LEFT JOIN fichatecnica ft on ft.produtoid = pa.produtoid");
            SQL.AppendLine("INNER JOIN fichafaccao ff ON ff.idficha = ft.id");
            SQL.AppendLine("LEFT JOIN colaboradores f ON f.id = ff.idfaccao");
            SQL.AppendLine("WHERE " + FiltroEmpresa(" ft.empresaid "));

            if (idPacote != null && idPacote.Count() > 0)
                SQL.AppendLine(" AND pa.id IN (" + string.Join(", ", idPacote) + ")");

            if (idFaccao != null && idFaccao.Count() > 0)
                SQL.AppendLine(" AND ff.idfaccao IN (" + string.Join(", ", idFaccao) + ")");

            if (idProduto != null && idProduto.Count() > 0)
                SQL.AppendLine(" AND pa.produtoId IN (" + string.Join(", ", idProduto) + ")");

            return cn.ExecuteStringSqlToList(new FichaFaccao(), SQL.ToString());
        }

        public IEnumerable<FichaFaccao> GetByIdFicha(int idFicha)
        {
            var cn = new DapperConnection<FichaFaccao>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	ff.*");
            SQL.AppendLine("FROM 	fichafaccao ff");
            SQL.AppendLine("LEFT JOIN fichatecnica ft on ff.idficha = ft.id");
            SQL.AppendLine("WHERE " + FiltroEmpresa(" ft.empresaid "));
            SQL.AppendLine(" AND ff.idFicha = " + idFicha);
            SQL.AppendLine(" GROUP BY ff.idFaccao ");

            return cn.ExecuteStringSqlToList(new FichaFaccao(), SQL.ToString());
        }

        public IEnumerable<FichaFaccaoView> GetByIdFichaView(int idFicha)
        {
            var cn = new DapperConnection<FichaFaccaoView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	ff.*, f.nome, f.razaosocial, f.referencia ");
            SQL.AppendLine("FROM 	fichafaccao ff");
            SQL.AppendLine("INNER JOIN fichatecnica ft on ff.idficha = ft.id");
            SQL.AppendLine("LEFT JOIN colaboradores f ON f.id = ff.idfaccao");
            SQL.AppendLine("WHERE " + FiltroEmpresa(" ft.empresaid "));
            SQL.AppendLine(" AND ff.idFicha = " + idFicha);
            SQL.AppendLine(" GROUP BY ff.idFaccao ");
            SQL.AppendLine(" ORDER BY f.Referencia, ff.valorPeca ");

            return cn.ExecuteStringSqlToList(new FichaFaccaoView(), SQL.ToString());
        }

        public IEnumerable<FichaFaccao> GetByIdFaccao(int idFaccao)
        {
            var cn = new DapperConnection<FichaFaccao>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	ff.*");
            SQL.AppendLine("FROM 	fichafaccao ff");
            SQL.AppendLine("LEFT JOIN fichatecnica ft on ff.idficha = ft.id");
            SQL.AppendLine("WHERE " + FiltroEmpresa(" ft.empresaid "));
            SQL.AppendLine(" AND ff.idFaccao = " + idFaccao);

            return cn.ExecuteStringSqlToList(new FichaFaccao(), SQL.ToString());
        }

        public void DeleteByFichaTecnica(int fichaTecnicaId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("DELETE");
            sql.AppendLine("FROM    fichafaccao");
            sql.AppendLine("WHERE   idFicha = " + fichaTecnicaId);
            _cn.ExecuteNonQuery(sql.ToString());

        }

        public void DeleteByFaccao(int faccaoId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("DELETE");
            sql.AppendLine("FROM    fichafaccao");
            sql.AppendLine("WHERE   idFaccao = " + faccaoId);
            _cn.ExecuteNonQuery(sql.ToString());

        }
    }
}