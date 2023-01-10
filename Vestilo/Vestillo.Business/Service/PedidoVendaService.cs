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
    public class PedidoVendaService: GenericService<PedidoVenda, PedidoVendaRepository, PedidoVendaController>
    {
        public PedidoVendaService()
        {
            base.RequestUri = "PedidoVenda";
        }

        public new IPedidoVendaService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new PedidoVendaServiceWeb(this.RequestUri);
            }
            else
            {
                return new PedidoVendaServiceAPP();
            }
        }   
    }
}
