using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.APP;
using Vestillo.Business.Service.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Service
{
    public class LiberacaoPedidoVendaService : GenericService<LiberacaoPedidoVenda, LiberacaoPedidoVendaRepository, LiberacaoPedidoVendaController>
    {
        public LiberacaoPedidoVendaService()
        {
            base.RequestUri = "LiberacaoPedidoVenda";
        }

        public new ILiberacaoPedidoVendaService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new LiberacaoPedidoVendaServiceWeb(this.RequestUri);
            }
            else
            {
                return new LiberacaoPedidoVendaServiceAPP();
            }
        }   
    }
}
