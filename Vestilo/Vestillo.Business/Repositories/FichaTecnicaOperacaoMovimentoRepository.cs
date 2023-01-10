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
    public class FichaTecnicaOperacaoMovimentoRepository : GenericRepository<FichaTecnicaOperacaoMovimento>
    {
        public FichaTecnicaOperacaoMovimentoRepository()
            : base(new DapperConnection<FichaTecnicaOperacaoMovimento>())
        {
        
        }

        public IEnumerable<FichaTecnicaOperacaoMovimento> GetByFichaTecnica(int fichaTecnicaId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT M.* ");
            sql.AppendLine("FROM FichaTecnicaOperacaoMovimento M");
            sql.AppendLine("    INNER JOIN FichaTecnicaOperacao F ON M.FichaTecnicaOperacaoId = F.Id");
            sql.AppendLine("WHERE F.FichaTecnicaId = " + fichaTecnicaId.ToString());
            
            return _cn.ExecuteStringSqlToList(new FichaTecnicaOperacaoMovimento(), sql.ToString());
        }

        public FichaTecnicaOperacaoMovimento GetByFichaTecnicaOperacao(int fichaTecnicaoperacaoId, int movimentoId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT M.* ");
            sql.AppendLine("FROM FichaTecnicaOperacaoMovimento M");
            sql.AppendLine("WHERE M.FichaTecnicaOperacaoId = " + fichaTecnicaoperacaoId.ToString());
            sql.AppendLine(" AND M.MovimentoDaOperacaoId = " + movimentoId.ToString());

            FichaTecnicaOperacaoMovimento result = new FichaTecnicaOperacaoMovimento();
            _cn.ExecuteToModel(ref result, sql.ToString());

            return result;
        }

        public void DeleteByOperacao(int fichaTecnicaOperacaoId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("DELETE ");
            sql.AppendLine("FROM FichaTecnicaOperacaoMovimento ");
            sql.AppendLine("WHERE FichaTecnicaOperacaoId = " + fichaTecnicaOperacaoId.ToString());

            _cn.ExecuteNonQuery(sql.ToString());
        }

        public void DeleteByFichaTecnica(int fichaTecnicaId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("DELETE ");
            sql.AppendLine("FROM FichaTecnicaOperacaoMovimento ");
            sql.AppendLine("WHERE FichaTecnicaOperacaoId IN (SELECT Id FROM FichaTecnicaOperacao WHERE FichaTecnicaId = " + fichaTecnicaId.ToString() + ")");

            _cn.ExecuteNonQuery(sql.ToString());
        }
    }
}
