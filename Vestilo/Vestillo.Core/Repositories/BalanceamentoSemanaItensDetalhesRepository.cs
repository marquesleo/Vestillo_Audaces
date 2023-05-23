
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Core.Models;
using Vestillo.Core.Connection;

namespace Vestillo.Core.Repositories
{
    public class BalanceamentoSemanaItensDetalhesRepository : GenericRepository<BalanceamentoSemanaItensDetalhes>
    {
        public IEnumerable<BalanceamentoSemanaItensDetalhesView> ListBySetoresDetalhes(int BalanceamentoId)
        {
            string sql = " SELECT  *,setores.Abreviatura as Setor FROM BalanceamentoSemanaItensDetalhes " +
                         " INNER JOIN setores ON setores.Id = BalanceamentoSemanaItensDetalhes.SetorId " +
                         " WHERE BalanceamentoSemanaItensDetalhes.BalanceamentoId = " + BalanceamentoId;

            return VestilloConnection.ExecSQLToListWithNewConnection<BalanceamentoSemanaItensDetalhesView>(sql);
        }

        public void DeleteItensDetalhesBalanco(int BalanceamentoId)
        {
            string SQL = "DELETE FROM BalanceamentoSemanaItensDetalhes WHERE BalanceamentoSemanaItensDetalhes.BalanceamentoId = " + BalanceamentoId;
            VestilloConnection.ExecNonQuery(SQL);

        }

    }
}
