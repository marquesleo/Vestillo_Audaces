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
using Vestillo.Business.Service.Interface;

namespace Vestillo.Business.Service
{
    public class ItemLiberacaoPedidoVendaService: GenericService<ItemLiberacaoPedidoVenda, ItemLiberacaoPedidoVendaRepository, ItemLiberacaoPedidoVendaController>
    {
        public ItemLiberacaoPedidoVendaService()
        {
            base.RequestUri = "ItemLiberacaoPedidoVenda";
        }

        public new IItemLiberacaoPedidoVendaService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new ItemLiberacaoPedidoVendaServiceWeb(this.RequestUri);
            }
            else
            {
                return new ItemLiberacaoPedidoVendaServiceAPP();
            }
        }   

    }
}
