using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repository;

namespace Vestillo.Business.Service
{
    public interface IParcelaPadraoClienteService : IService<ParcelaPadraoCliente, ParcelaPadraoClienteRepository, ParcelaPadraoClienteController>
    {
        IEnumerable<ParcelaPadraoCliente> GetParcelasPorCliente(int clienteId);
    }
}
