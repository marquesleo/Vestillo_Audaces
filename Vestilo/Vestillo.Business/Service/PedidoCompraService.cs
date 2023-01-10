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
    public class PedidoCompraService: GenericService<PedidoCompra, PedidoCompraRepository, PedidoCompraController>
    {
        public PedidoCompraService()
        {
            base.RequestUri = "PedidoCompra";
        }

        public new IPedidoCompraService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new PedidoCompraServiceWeb(this.RequestUri);
            }
            else
            {
                return new PedidoCompraServiceAPP();
            }
        }   
    }
}
