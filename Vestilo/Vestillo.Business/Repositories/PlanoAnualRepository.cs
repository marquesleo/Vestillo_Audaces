using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class PlanoAnualRepository: GenericRepository<PlanoAnual>
    {
        public PlanoAnualRepository()
            : base(new DapperConnection<PlanoAnual>())
        {
        }

        public List<PlanoAnualDetalhesView> GetPlanoAnualDetalhesTotal(int codigo)
        {
            var cn = new DapperConnection<PlanoAnualDetalhesView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	pd.Mes,");
            SQL.AppendLine("SUM(pd.Costureira) as Costureira,");
            SQL.AppendLine("AVG(pd.DiasUteis) DiasUteis,");
            SQL.AppendLine("AVG(pd.Jornada) as Jornada,");
            SQL.AppendLine("AVG(pd.Presenca) as Presenca,");
            SQL.AppendLine("AVG(pd.Aproveitamento) as Aproveitamento,");
            SQL.AppendLine("AVG(pd.Eficiencia) as Eficiencia,");
            SQL.AppendLine("AVG(pd.TempoMedio) as TempoMedio");
            SQL.AppendLine("FROM 	PlanoAnualDetalhes pd");
         
            SQL.AppendLine("WHERE ");
            SQL.AppendLine(" pd.PlanoId = " + codigo);
            SQL.AppendLine("GROUP BY pd.Mes");

            return cn.ExecuteStringSqlToList(new PlanoAnualDetalhesView(), SQL.ToString()).ToList();
        }

        public List<PlanoAnualDetalhesView> GetPlanoAnualDetalhes(int codigo)
        {
            var cn = new DapperConnection<PlanoAnualDetalhesView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	pd.*");
            SQL.AppendLine("FROM 	PlanoAnualDetalhes pd");

            SQL.AppendLine("WHERE ");
            SQL.AppendLine(" pd.PlanoId = " + codigo);

            return cn.ExecuteStringSqlToList(new PlanoAnualDetalhesView(), SQL.ToString()).ToList();
        }

        public List<GrupProduto> GetGrupos(int codigo)
        {
            var cn = new DapperConnection<GrupProduto>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	gp.*");
            SQL.AppendLine("FROM 	PlanoAnualDetalhes pd");
            SQL.AppendLine("INNER JOIN 	GrupoProdutos gp ON gp.id = pd.GrupoId");
            SQL.AppendLine("WHERE ");
            SQL.AppendLine(" pd.PlanoId = " + codigo);
            SQL.AppendLine("GROUP BY pd.GrupoId");

            return cn.ExecuteStringSqlToList(new GrupProduto(), SQL.ToString()).ToList();
        }
    }
}
