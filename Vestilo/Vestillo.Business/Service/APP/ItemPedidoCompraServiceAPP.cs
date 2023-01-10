using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service.APP
{
    public class ItemPedidoCompraServiceAPP : GenericServiceAPP<ItemPedidoCompra, ItemPedidoCompraRepository, ItemPedidoCompraController>, IItemPedidoCompraService
    {
        public ItemPedidoCompraServiceAPP() : base(new ItemPedidoCompraController())
        {
        }

        public IEnumerable<ItemPedidoCompraView> GetByPedido(int pedidoId)
        {
            return controller.GetByPedido(pedidoId);
        }
    }
}
