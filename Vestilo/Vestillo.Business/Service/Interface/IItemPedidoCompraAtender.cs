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
    public interface IItemPedidoCompraAtender : IService<ItemPedidoCompraAtender , ItemPedidoCompraAtenderRepository, ItemPedidoCompraAtenderController>
    {
        IEnumerable<ItemPedidoCompraAtenderView> GetByItensPorFornecedor(int fornecedorId, int JaSelecionado);
        ItemPedidoCompraAtender GetByPedidoDeCompraDoItem(int IdDoItemNoPedido);
        IEnumerable<ItemPedidoCompraAtender> GetByPedidoCompra(int pedidoCompraId);        
        
    }
}