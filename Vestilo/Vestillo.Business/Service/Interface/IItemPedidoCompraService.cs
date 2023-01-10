using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service
{
    public interface IItemPedidoCompraService : IService<ItemPedidoCompra, ItemPedidoCompraRepository, ItemPedidoCompraController>
    {
        IEnumerable<ItemPedidoCompraView> GetByPedido(int pedidoId);
    }
}
