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
    public class PedidoVendaConferenciaItemService: GenericService<PedidoVendaConferenciaItem, PedidoVendaConferenciaItemRepository, PedidoVendaConferenciaItemController>
    {
        public PedidoVendaConferenciaItemService()
        {
            base.RequestUri = "PedidoVendaConferenciaItem";
        }

        public new IPedidoVendaConferenciaItemService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new PedidoVendaConferenciaItemServiceWeb(this.RequestUri);
            }
            else
            {
                return new PedidoVendaConferenciaItemServiceAPP();
            }
        }   

    }
}
