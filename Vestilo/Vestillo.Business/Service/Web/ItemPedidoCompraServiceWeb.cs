using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Controllers;
using Vestillo.Business.Repositories;
using Vestillo.Lib;

namespace Vestillo.Business.Service.Web
{
    public class ItemPedidoCompraServiceWeb : GenericServiceWeb<ItemPedidoCompra, ItemPedidoCompraRepository, ItemPedidoCompraController>, IItemPedidoCompraService
    {
        public ItemPedidoCompraServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<ItemPedidoCompraView> GetByPedido(int pedidoId)
        {
            throw new NotImplementedException();
        }
    }
}
