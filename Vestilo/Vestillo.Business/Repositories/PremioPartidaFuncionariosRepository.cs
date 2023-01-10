using Vestillo.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class PremioPartidaFuncionariosRepository : GenericRepository<PremioPartidaFuncionarios>
    {
        public PremioPartidaFuncionariosRepository()
            : base(new DapperConnection<PremioPartidaFuncionarios>())
        {
        }

        public PremioPartidaFuncionarios GetByIdView(int id)
        {
            var cn = new DapperConnection<PremioPartidaFuncionarios>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	*");
            SQL.AppendLine("FROM 	premiopartidafuncionarios");
            SQL.AppendLine("WHERE Id = ");
            SQL.Append(id);

            PremioPartidaFuncionarios ret = new PremioPartidaFuncionarios();

            cn.ExecuteToModel(ref ret, SQL.ToString());            

            return ret;
        }

        public IEnumerable<PremioPartidaFuncionariosView> GetByPremioView(int premioId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 1 as Selecionado, pf.*, f.nome as Nome, c.Descricao as Cargo");
            SQL.AppendLine("FROM premiopartidafuncionarios pf ");
            SQL.AppendLine("    INNER JOIN funcionarios f on f.id = pf.idfuncionario ");
            SQL.AppendLine("    INNER JOIN cargos c on c.id = f.cargoid ");
            SQL.AppendLine("WHERE  pf.idpremio = ");
            SQL.Append(premioId);

            var cn = new DapperConnection<PremioPartidaFuncionariosView>();
            return cn.ExecuteStringSqlToList(new PremioPartidaFuncionariosView(), SQL.ToString());
        }

        public IEnumerable<PremioPartidaFuncionariosView> GetFuncionariosGrid(int? premioId)
        {           
            var SQL = new StringBuilder();
            SQL.AppendLine(" SELECT f.id as IdFuncionario, f.nome as Nome, c.Descricao as Cargo, ");
            SQL.AppendLine("    IF(p.PremioIndividual IS NOT NULL, p.PremioIndividual, 0) as PremioIndividual, ");
            SQL.AppendLine("    IF(p.PremioAssiduidade IS NOT NULL, p.PremioAssiduidade, 0) as PremioAssiduidade, ");
            SQL.AppendLine("    IF(p.PremioGrupo IS NOT NULL, p.PremioGrupo, 0) as PremioGrupo, ");

            if(premioId == null || premioId == 0)
                SQL.AppendLine("    0 as Selecionado ");
            else
                SQL.AppendLine("    IF(p.id = " + premioId.ToString() + ", 1, 0) as Selecionado ");

            SQL.AppendLine(" FROM funcionarios f ");
            SQL.AppendLine("    INNER JOIN cargos c on c.id = f.cargoid ");
            SQL.AppendLine("    LEFT JOIN premiopartidafuncionarios pf on pf.idfuncionario = f.id ");
            SQL.AppendLine("    LEFT JOIN premiopartida p on pf.idpremio = p.id ");
            SQL.AppendLine(" WHERE  f.ativo = 1 ");
            SQL.AppendLine(" GROUP BY f.id ");
            SQL.AppendLine(" ORDER BY Selecionado DESC, Nome ");

            var cn = new DapperConnection<PremioPartidaFuncionariosView>();
            return cn.ExecuteStringSqlToList(new PremioPartidaFuncionariosView(), SQL.ToString());
        }
    }
}