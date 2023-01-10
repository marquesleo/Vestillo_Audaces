using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Controllers;
using Vestillo.Business.Service.APP;
using Vestillo.Business.Service.Web;

namespace Vestillo.Business.Service
{
    public class ItemPedidoCompraService: GenericService<ItemPedidoCompra, ItemPedidoCompraRepository, ItemPedidoCompraController>
    {
        public ItemPedidoCompraService()
        {
            base.RequestUri = "ItemPedidoCompra";
        }

        public new IItemPedidoCompraService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new ItemPedidoCompraServiceWeb(this.RequestUri);
            }
            else
            {
                return new ItemPedidoCompraServiceAPP();
            }
        }   
    }
}
