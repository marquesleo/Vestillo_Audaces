using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class FuncionarioDespesaRepository : GenericRepository<FuncionarioDespesa>
    {
        public FuncionarioDespesaRepository() : base(new DapperConnection<FuncionarioDespesa>())
        {
        
        }

        public void DeleteByFuncionario(int funcionarioId)
        {
            string sql = "DELETE FROM FuncionarioDespesa WHERE FuncionarioId = " + funcionarioId.ToString();
            _cn.ExecuteNonQuery(sql);
        }

        public IEnumerable<FuncionarioDespesa> GetByFuncionario(int funcionarioId)
        {
            return _cn.ExecuteToList(new FuncionarioDespesa(), "FuncionarioId = " + funcionarioId.ToString());
        }
    }
}
