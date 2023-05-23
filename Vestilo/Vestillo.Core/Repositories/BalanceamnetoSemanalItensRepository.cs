
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Core.Models;
using Vestillo.Core.Connection;

namespace Vestillo.Core.Repositories
{
    public class BalanceamnetoSemanalItensRepository : GenericRepository<BalanceamentoSemanalItens>
    {
        public IEnumerable<BalanceamentoSemanalItensView> ListBySetores(int BalanceamentoId)
        {
            string sql = " SELECT  *,setores.Abreviatura as Setor FROM BalanceamentoSemanalItens " +
                         " INNER JOIN setores ON setores.Id = BalanceamentoSemanalItens.SetorId " +
                         " WHERE BalanceamentoSemanalItens.BalanceamentoId = " + BalanceamentoId;            

            return VestilloConnection.ExecSQLToListWithNewConnection<BalanceamentoSemanalItensView>(sql);
        }

        public void DeleteItensBalanco(int BalanceamentoId)
        {
            string SQL = "DELETE FROM BalanceamentoSemanalItens WHERE BalanceamentoSemanalItens.BalanceamentoId = " + BalanceamentoId;
            VestilloConnection.ExecNonQuery(SQL);

        }


    }
}
