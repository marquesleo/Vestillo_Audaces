using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class ItemPedidoCompraController: GenericController<ItemPedidoCompra, ItemPedidoCompraRepository>
    {
        public IEnumerable<ItemPedidoCompraAtenderView> GetByItensPorFornecedor(int fornecedorId, int JaSelecionado)
        {
            using (ItemPedidoCompraAtenderRepository repository = new ItemPedidoCompraAtenderRepository())
            {
                return repository.GetByItensPorFornecedor(fornecedorId, JaSelecionado);
            }
        }
        public IEnumerable<ItemPedidoCompraView> GetByPedido(int pedidoId)
        {
            using (ItemPedidoCompraRepository repository = new ItemPedidoCompraRepository())
            {
                return repository.GetByPedido(pedidoId);
            }
        }



    }
}
