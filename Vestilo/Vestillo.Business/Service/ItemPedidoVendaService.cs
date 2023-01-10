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
    public class ItemPedidoVendaService: GenericService<ItemPedidoVenda, ItemPedidoVendaRepository, ItemPedidoVendaController>
    {
        public ItemPedidoVendaService()
        {
            base.RequestUri = "ItemPedidoVenda";
        }

        public new IItemPedidoVendaService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new ItemPedidoVendaServiceWeb(this.RequestUri);
            }
            else
            {
                return new ItemPedidoVendaServiceAPP();
            }
        }   
    }
}
