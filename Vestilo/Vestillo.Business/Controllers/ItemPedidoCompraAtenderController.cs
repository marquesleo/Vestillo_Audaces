

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class ItemPedidoCompraAtenderController: GenericController<ItemPedidoCompraAtender, ItemPedidoCompraAtenderRepository>
    {
        public IEnumerable<ItemPedidoCompraAtenderView> GetByItensPorFornecedor(int fornecedorId, int JaSelecionado)
        {
            using (ItemPedidoCompraAtenderRepository repository = new ItemPedidoCompraAtenderRepository())
            {
                return repository.GetByItensPorFornecedor(fornecedorId, JaSelecionado);
            }
        }

        public ItemPedidoCompraAtender GetByPedidoDeCompraDoItem(int IdDoItemNoPedido)
        {
            using (ItemPedidoCompraAtenderRepository repository = new ItemPedidoCompraAtenderRepository())
            {
                return repository.GetByPedidoDeCompraDoItem(IdDoItemNoPedido);
            }
        }

        public IEnumerable<ItemPedidoCompraAtender> GetByPedidoCompra(int pedidoCompraId)
        {
            using (ItemPedidoCompraAtenderRepository repository = new ItemPedidoCompraAtenderRepository())
            {
                return repository.GetByPedidoCompra(pedidoCompraId);
            }
        }
        
    }
}
