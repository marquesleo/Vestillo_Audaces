using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Core.Models;
using Vestillo.Core.Connection;

namespace Vestillo.Core.Repositories
{
    public class VendedorMetaRepository : GenericRepository<VendedorMeta>
    {
        public IEnumerable<VendedorMetaView> ListByVendedor(int vendedorId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT  *");
            sql.AppendLine("FROM    MetaVendedores");
            sql.AppendLine("WHERE   VendedorId = " + vendedorId.ToString());

            return VestilloConnection.ExecSQLToList<VendedorMetaView>(sql.ToString());
        }

        public void DeleteByVendedor(int vendedorId)
        {
            VestilloConnection.ExecNonQuery("DELETE FROM MetaVendedores WHERE VendedorId = " + vendedorId.ToString());
        }
    }
}
