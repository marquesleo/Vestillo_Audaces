using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repository;

namespace Vestillo.Business.Controllers
{
    public class ParcelaPadraoClienteController : GenericController<ParcelaPadraoCliente, ParcelaPadraoClienteRepository>
    {
        public IEnumerable<ParcelaPadraoCliente> GetParcelasPorCliente(int clienteId)
        {
            using (var repository = new ParcelaPadraoClienteRepository())
            {
                return repository.GetParcelasPorCliente(clienteId);
            }
        }
    }
}
