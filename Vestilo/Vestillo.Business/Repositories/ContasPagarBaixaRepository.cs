using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;
using Vestillo.Lib;

namespace Vestillo.Business.Repositories
{
    public class ContasPagarBaixaRepository: GenericRepository<ContasPagarBaixa>
    {
        public ContasPagarBaixaRepository()
            : base(new DapperConnection<ContasPagarBaixa>())
        {
            
        }

        public List<ContasPagarBaixa> GetByContasPagar(int ContasPagarId)
        {
            return _cn.ExecuteToList(new ContasPagarBaixa(), "ContasPagarId = " + ContasPagarId.ToString()).ToList();
        }
        
    }
}
