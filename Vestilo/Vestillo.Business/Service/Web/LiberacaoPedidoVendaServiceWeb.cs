using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Service.Web
{
    public class LiberacaoPedidoVendaServiceWeb : GenericServiceWeb<LiberacaoPedidoVenda, LiberacaoPedidoVendaRepository, LiberacaoPedidoVendaController>, ILiberacaoPedidoVendaService
    {
        public LiberacaoPedidoVendaServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }


        public List<LiberacaoPedidoVendaView> GetByPedidoIdView(int pedidoId)
        {
            throw new NotImplementedException();
        }
    }
}
