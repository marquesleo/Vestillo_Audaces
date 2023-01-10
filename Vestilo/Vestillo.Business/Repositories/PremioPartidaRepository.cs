using Vestillo.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class PremioPartidaRepository : GenericRepository<PremioPartida>
    {
        public PremioPartidaRepository()
            : base(new DapperConnection<PremioPartida>())
        {
        }

        public PremioPartida GetByIdView(int id)
        {
            var cn = new DapperConnection<PremioPartida>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	*");
            SQL.AppendLine("FROM 	premiopartida");
            SQL.AppendLine("WHERE Id = ");
            SQL.Append(id);

            PremioPartida ret = new PremioPartida();

            cn.ExecuteToModel(ref ret, SQL.ToString());

            if (ret != null)
            {
                var funcionariosRepository = new PremioPartidaFuncionariosRepository();
                ret.Funcionarios = funcionariosRepository.GetByPremioView(ret.Id).ToList();
                
            }

            return ret;
        }

        public IEnumerable<PremioPartida> GetByDescricao(string descricao)
        {
            var cn = new DapperConnection<PremioPartida>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	*");
            SQL.AppendLine("FROM 	premiopartida");
            SQL.AppendLine("WHERE descricao like '%" + descricao + "%'");

            return cn.ExecuteStringSqlToList(new PremioPartida(), SQL.ToString());
        }

        public IEnumerable<PremioPartida> GetByReferencia(string referencia)
        {
            var cn = new DapperConnection<PremioPartida>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	*");
            SQL.AppendLine("FROM 	premiopartida");
            SQL.AppendLine("WHERE referencia like '%" + referencia + "%'");

            return cn.ExecuteStringSqlToList(new PremioPartida(), SQL.ToString());
        }

        public PremioPartida GetByFuncionario(int funcionario)
        {
            var cn = new DapperConnection<PremioPartida>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	P.*");
            SQL.AppendLine("FROM 	premiopartida P");
            SQL.AppendLine("   INNER JOIN premiopartidafuncionarios PF ON P.Id = PF.IdPremio");
            SQL.AppendLine("WHERE PF.IdFuncionario = ");
            SQL.Append(funcionario);
            SQL.AppendLine(" ORDER BY P.id ");

            PremioPartida ret = new PremioPartida();

            cn.ExecuteToModel(ref ret, SQL.ToString());

            if (ret != null)
            {
                var pessoaPremioRepository = new PremioPartidaFuncionariosRepository();
                ret.Funcionarios = pessoaPremioRepository.GetByPremioView(ret.Id).ToList();
            }

            return ret;
        }
    }
}
