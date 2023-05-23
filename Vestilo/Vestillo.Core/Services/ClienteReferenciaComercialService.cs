using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Core.Models;
using Vestillo.Core.Repositories;

namespace Vestillo.Core.Services
{
    public class ClienteReferenciaComercialService : GenericService<ClienteReferenciaComercial, ClienteReferenciaComercialRepository>
    {
        public IEnumerable<ClienteReferenciaComercial> ListByCliente(int clienteId)
        {
            return _repository.ListByCliente(clienteId);
        }

        public void Save(IEnumerable<ClienteReferenciaComercial> referencias, int clienteId)
        {
            bool openTransaction = false;

            try
            {
                openTransaction = _repository.BeginTransaction();

                _repository.DeleteByCliente(clienteId);

                foreach (ClienteReferenciaComercial referencia in referencias)
                {
                    referencia.Id = 0;
                    referencia.ClienteId = clienteId;
                    base.Save(referencia);
                }

                if (openTransaction)
                    _repository.CommitTransaction();

            }
            catch (Exception ex)
            {
                if (openTransaction)
                    _repository.RollbackTransaction();

                throw ex;
            }
        }
    }
}


