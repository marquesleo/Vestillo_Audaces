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
    public class ContasReceberBaixaRepository: GenericRepository<ContasReceberBaixa>
    {
        public ContasReceberBaixaRepository()
            : base(new DapperConnection<ContasReceberBaixa>())
        {
            
        }

        public List<ContasReceberBaixa> GetByContasReceber(int contasReceberId)
        {
            return _cn.ExecuteToList(new ContasReceberBaixa(), "ContasReceberId = " + contasReceberId.ToString()).ToList();
        }

        public List<ContasReceberBaixa> GetByContasReceberEBordero(int contasReceberId, int borderoCobrancaId)
        {
            return _cn.ExecuteToList(new ContasReceberBaixa(), "ContasReceberId = " + contasReceberId.ToString() + " AND BorderoId =  " + borderoCobrancaId.ToString()).ToList();
        }
    }
}
