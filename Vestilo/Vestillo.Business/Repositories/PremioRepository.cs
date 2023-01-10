using Vestillo.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class PremioRepository : GenericRepository<Premio>
    {
        public PremioRepository()
            : base(new DapperConnection<Premio>())
        {
        }

        public Premio GetByIdView(int id)
        {
            var cn = new DapperConnection<Premio>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	*");
            SQL.AppendLine("FROM 	premios");
            SQL.AppendLine("WHERE Id = ");
            SQL.Append(id);

            Premio ret = new Premio();

            cn.ExecuteToModel(ref ret, SQL.ToString());

            if (ret != null)
            {
                var pessoaPremioRepository = new PessoasPremioRepository();
                ret.Pessoas = pessoaPremioRepository.GetByPremioView(ret.Id).ToList();
                var faixaPremioRepository = new FaixaPremioRepository();
                ret.Faixas = faixaPremioRepository.GetByPremio(ret.Id).ToList();
                ret.Faixas.ForEach(f =>
                {
                    f.ValorTotal = f.ValorMinuto * ret.MinutosUteis * ret.Dias;
                });
                //var controleFaltaRepository = new ControleFaltaRepository();
                //ret.Faltas = controleFaltaRepository.GetByPremio(ret.Id).ToList();
            }

            return ret;
        }

        public IEnumerable<Premio> GetByDescricao(string descricao)
        {
            var cn = new DapperConnection<Premio>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	*");
            SQL.AppendLine("FROM 	premios");
            SQL.AppendLine("WHERE descricao like '%" + descricao + "%'");

            return cn.ExecuteStringSqlToList(new Premio(), SQL.ToString());
        }

        public IEnumerable<Premio> GetByReferencia(string referencia)
        {
            var cn = new DapperConnection<Premio>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	*");
            SQL.AppendLine("FROM 	premios");
            SQL.AppendLine("WHERE referencia like '%" + referencia + "%'");

            return cn.ExecuteStringSqlToList(new Premio(), SQL.ToString());
        }

        public Premio GetByFuncionario(int funcionario)
        {
            var cn = new DapperConnection<Premio>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	P.*");
            SQL.AppendLine("FROM 	premios P");
            SQL.AppendLine("   INNER JOIN pessoaspremio PP ON P.Id = PP.PremioId");
            SQL.AppendLine("WHERE PP.PessoaId = ");
            SQL.Append(funcionario);
            SQL.AppendLine(" ORDER BY P.id ");

            Premio ret = new Premio();

            cn.ExecuteToModel(ref ret, SQL.ToString());

            if (ret != null)
            {
                var pessoaPremioRepository = new PessoasPremioRepository();
                ret.Pessoas = pessoaPremioRepository.GetByPremioView(ret.Id).ToList();
                var faixaPremioRepository = new FaixaPremioRepository();
                ret.Faixas = faixaPremioRepository.GetByPremio(ret.Id).ToList();
                //var controleFaltaRepository = new ControleFaltaRepository();
                //ret.Faltas = controleFaltaRepository.GetByPremio(ret.Id).ToList();
            }

            return ret;
        }
    }
}
