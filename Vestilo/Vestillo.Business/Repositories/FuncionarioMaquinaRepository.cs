using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class FuncionarioMaquinaRepository : GenericRepository<FuncionarioMaquina>
    {
        public FuncionarioMaquinaRepository()
            : base(new DapperConnection<FuncionarioMaquina>())
        {
        
        }

        public void DeleteByFuncionario(int funcionarioId)
        {
            string sql = "DELETE FROM FuncionarioMaquina WHERE FuncionarioId = " + funcionarioId.ToString();
            _cn.ExecuteNonQuery(sql);
        }

        public IEnumerable<FuncionarioMaquinaView> GetByFuncionario(int funcionarioId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT 	F.*, M.descricao AS TipoMaquinaDescricao");
            sql.AppendLine("FROM 	FuncionarioMaquina F");
            sql.AppendLine("    INNER JOIN tipomaquinas M ON M.id = F.TipoMaquinaId");
            sql.AppendLine("WHERE F.FuncionarioId = " + funcionarioId.ToString());

            var cn = new DapperConnection<FuncionarioMaquinaView>();
            return cn.ExecuteStringSqlToList(new FuncionarioMaquinaView(), sql.ToString());
        }
    }
}
