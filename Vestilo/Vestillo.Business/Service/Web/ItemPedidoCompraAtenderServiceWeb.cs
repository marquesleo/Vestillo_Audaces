
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
    public class ItemPedidoCompraAtenderServiceWeb : GenericServiceWeb<ItemPedidoCompraAtender, ItemPedidoCompraAtenderRepository, ItemPedidoCompraAtenderController>, IItemPedidoCompraAtender
    {

        public ItemPedidoCompraAtenderServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<ItemPedidoCompraAtenderView> GetByItensPorFornecedor(int fornecedorId, int JaSelecionado)
        {
            throw new NotImplementedException();
        }

        public ItemPedidoCompraAtender GetByPedidoDeCompraDoItem(int IdDoItemNoPedido)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ItemPedidoCompraAtender> GetByPedidoCompra(int pedidoCompraId)
        {
            throw new NotImplementedException();
        }
    }
}


