using Vestillo.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class PessoasPremioRepository : GenericRepository<PessoasPremio>
    {
        public PessoasPremioRepository()
            : base(new DapperConnection<PessoasPremio>())
        {
        }

        public IEnumerable<PessoasPremio> GetByPremio(int premioId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT *");
            SQL.AppendLine("FROM pessoaspremio");
            SQL.AppendLine("WHERE premioid = ");
            SQL.Append(premioId);

            var cn = new DapperConnection<PessoasPremio>();
            return cn.ExecuteStringSqlToList(new PessoasPremio(), SQL.ToString());
        }

        public IEnumerable<PessoasPremio> GetByPessoaId(int pessoaId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT *");
            SQL.AppendLine("FROM pessoaspremio");
            SQL.AppendLine("WHERE pessoaid = ");
            SQL.Append(pessoaId);

            var cn = new DapperConnection<PessoasPremio>();
            return cn.ExecuteStringSqlToList(new PessoasPremio(), SQL.ToString());
        }

        public IEnumerable<PessoasPremioView> GetByPremioView(int premioId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT PP.*, F.Nome AS PessoaNome, C.Descricao AS PessoaCargo ");
            SQL.AppendLine("FROM pessoaspremio AS PP");
            SQL.AppendLine("   INNER JOIN funcionarios AS F ON F.Id = PP.PessoaId");
            SQL.AppendLine("   LEFT JOIN Cargos AS C ON C.Id = F.CargoId");
            SQL.AppendLine("WHERE premioid = ");
            SQL.Append(premioId);

            var cn = new DapperConnection<PessoasPremioView>();
            return cn.ExecuteStringSqlToList(new PessoasPremioView(), SQL.ToString());
        }
    }
}
