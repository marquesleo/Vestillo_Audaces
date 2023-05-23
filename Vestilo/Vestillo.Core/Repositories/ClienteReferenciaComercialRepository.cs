using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Core.Models;
using Vestillo.Core.Connection;

namespace Vestillo.Core.Repositories
{
    public class ClienteReferenciaComercialRepository : GenericRepository<ClienteReferenciaComercial>
    {
        public IEnumerable<ClienteReferenciaComercial> ListByCliente(int clienteId)
        {
            return VestilloConnection.ExecSQLToList<ClienteReferenciaComercial>("SELECT * FROM ClienteReferenciasComerciais WHERE ClienteId = " + clienteId.ToString());
        }

        public void DeleteByCliente(int clienteId)
        {
            VestilloConnection.ExecNonQuery("DELETE FROM ClienteReferenciasComerciais WHERE ClienteId = " + clienteId.ToString());
        }
    }
}
