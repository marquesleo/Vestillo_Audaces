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
    public class PedidoVendaConferenciaService: GenericService<PedidoVendaConferencia, PedidoVendaConferenciaRepository, PedidoVendaConferenciaController>
    {
        public PedidoVendaConferenciaService()
        {
            base.RequestUri = "PedidoVendaConferencia";
        }

        public new IPedidoVendaConferenciaService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new PedidoVendaConferenciaServiceWeb(this.RequestUri);
            }
            else
            {
                return new PedidoVendaConferenciaServiceAPP();
            }
        }   

    }
}
