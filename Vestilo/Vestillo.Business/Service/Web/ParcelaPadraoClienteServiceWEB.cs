using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repository;

namespace Vestillo.Business.Service.Web
{
    public class ParcelaPadraoClienteServiceWEB : GenericServiceWeb<ParcelaPadraoCliente, ParcelaPadraoClienteRepository, ParcelaPadraoClienteController>, IParcelaPadraoClienteService
    {
        public ParcelaPadraoClienteServiceWEB(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<ParcelaPadraoCliente> GetParcelasPorCliente(int clienteId)
        {
            throw new NotImplementedException();
        }
    }
}
