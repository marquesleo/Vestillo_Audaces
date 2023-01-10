
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
    public class ItemPedidoCompraAtenderServiceAPP : GenericServiceAPP<ItemPedidoCompraAtender, ItemPedidoCompraAtenderRepository, ItemPedidoCompraAtenderController>, IItemPedidoCompraAtender
    {
        public ItemPedidoCompraAtenderServiceAPP()  : base(new ItemPedidoCompraAtenderController())
        {
        }

        public IEnumerable<ItemPedidoCompraAtenderView> GetByItensPorFornecedor(int fornecedorId, int JaSelecionado)
        {
            return controller.GetByItensPorFornecedor(fornecedorId, JaSelecionado);
        }

         public ItemPedidoCompraAtender GetByPedidoDeCompraDoItem(int IdDoItemNoPedido)
        {
            return controller.GetByPedidoDeCompraDoItem(IdDoItemNoPedido);
        }

         public IEnumerable<ItemPedidoCompraAtender> GetByPedidoCompra(int pedidoCompraId)
        {
            return controller.GetByPedidoCompra(pedidoCompraId);
        }
    }
}
