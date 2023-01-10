using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repository;

namespace Vestillo.Business.Service.APP
{
    public class ParcelaPadraoClienteServiceAPP : GenericServiceAPP<ParcelaPadraoCliente, ParcelaPadraoClienteRepository, ParcelaPadraoClienteController>, IParcelaPadraoClienteService
    {
        public ParcelaPadraoClienteServiceAPP()
            : base(new ParcelaPadraoClienteController())
        {

        }

        public IEnumerable<ParcelaPadraoCliente> GetParcelasPorCliente(int clienteId)
        {
            ParcelaPadraoClienteController controller = new ParcelaPadraoClienteController();
            return controller.GetParcelasPorCliente(clienteId);
        }
    }
}
