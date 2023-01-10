using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service.Web
{
    public class ItemPedidoVendaServiceWeb : GenericServiceWeb<ItemPedidoVenda, ItemPedidoVendaRepository, ItemPedidoVendaController>, IItemPedidoVendaService
    {

        public ItemPedidoVendaServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<ItemPedidoVendaView> GetAllView()
        {
            throw new NotImplementedException();
        }


        public IEnumerable<ConsultaPedidoVendaRelatorio> GetPedidoParaRelatorio(Models.Views.FiltroPedidoVendaRelatorio filtro)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<ItemPedidoVendaView> GetByPedido(int pedidoId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ItemPedidoVendaView> GetByPedidoAgrupadoView(int pedidoId,bool AgrupaTamanho)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ItemPedidoVendaView> GetItensConferenciaByBusca(int almoxarifadoId, List<int> produtosIds)
        {
            throw new NotImplementedException();
        }
    }
}
