using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo;
using Vestillo.Business.Models;
using Vestillo.Connection;
using Vestillo.Business.Models;

namespace Vestillo.Business.Repositories
{
    public class FichaTecnicaOperacaoRepository : GenericRepository<FichaTecnicaOperacao>
    {
        public FichaTecnicaOperacaoRepository()
            : base(new DapperConnection<FichaTecnicaOperacao>())
        {
            
        }

        public IEnumerable<FichaTecnicaOperacao> GetByFichaTecnica(int fichaTecnicaId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT  * ");
            sql.AppendLine("FROM    FichaTecnicaOperacao");
            sql.AppendLine("WHERE   FichaTecnicaId = " + fichaTecnicaId.ToString());
            sql.AppendLine(" ORDER BY Numero");

            return _cn.ExecuteStringSqlToList(new FichaTecnicaOperacao(), sql.ToString());
        }

        public IEnumerable<FichaTecnicaOperacao> GetByProduto(int produtoId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT  FO.* ");
            sql.AppendLine("FROM    FichaTecnicaOperacao FO");
            sql.AppendLine("    INNER JOIN FichaTecnica F ON F.Id = FO.FichaTecnicaId");
            sql.AppendLine("WHERE   F.ProdutoId = " + produtoId.ToString());
            sql.AppendLine(" ORDER BY FO.BalanceamentoId  ");

            return _cn.ExecuteStringSqlToList(new FichaTecnicaOperacao(), sql.ToString());
        }

        public IEnumerable<FichaTecnicaOperacaoView> GetByFichaTecnicaView(int fichaTecnicaId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT  O.*, OP.Referencia AS Maquina ");
            sql.AppendLine("FROM    FichaTecnicaOperacao O");
            sql.AppendLine("    INNER JOIN operacaopadrao OP ON OP.Id = O.OperacaoPadraoId");
            sql.AppendLine("WHERE   FichaTecnicaId = " + fichaTecnicaId.ToString());
            sql.AppendLine("ORDER BY Numero");

            var cn = new DapperConnection<FichaTecnicaOperacaoView>();
            return cn.ExecuteStringSqlToList(new FichaTecnicaOperacaoView(), sql.ToString());
        }

        public IEnumerable<FichaTecnicaOperacao> GetByOperacao(int operacaoId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT  * ");
            sql.AppendLine("FROM    FichaTecnicaOperacao");
            sql.AppendLine("WHERE   OperacaoPadraoId = " + operacaoId);
            sql.AppendLine("ORDER BY Numero");

            return _cn.ExecuteStringSqlToList(new FichaTecnicaOperacao(), sql.ToString());
        }

        public IEnumerable<FichaTecnicaOperacao> GetByMovimentosDaOperacao(int movimentoId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT  f.* ");
            sql.AppendLine("FROM    FichaTecnicaOperacao f");
            sql.AppendLine(" INNER JOIN fichatecnicaoperacaomovimento fm ON f.id = fm.fichatecnicaoperacaoid");
            sql.AppendLine("WHERE   fm.movimentosdaoperacaoid = " + movimentoId);
            sql.AppendLine("ORDER BY f.FichaTecnicaId, f.Numero");

            return _cn.ExecuteStringSqlToList(new FichaTecnicaOperacao(), sql.ToString());
        }

        public void DeleteByFichaTecnica(int fichaTecnicaId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("DELETE");
            sql.AppendLine("FROM    FichaTecnicaOperacao");
            sql.AppendLine("WHERE   FichaTecnicaId = " + fichaTecnicaId.ToString());
            _cn.ExecuteNonQuery(sql.ToString());

        }
    }
}
