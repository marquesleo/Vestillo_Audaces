using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service.APP
{
    public class ItemPedidoVendaServiceAPP : GenericServiceAPP<ItemPedidoVenda, ItemPedidoVendaRepository, ItemPedidoVendaController>, IItemPedidoVendaService
    {

        public ItemPedidoVendaServiceAPP()
            : base(new ItemPedidoVendaController())
        {
        }

        public IEnumerable<ItemPedidoVendaView> GetAllView()
        {
            return controller.GetAllView();
        }

        public IEnumerable<ConsultaPedidoVendaRelatorio> GetPedidoParaRelatorio(Models.Views.FiltroPedidoVendaRelatorio filtro)
        {
            return controller.GetPedidoParaRelatorio(filtro);
        }


        public IEnumerable<ItemPedidoVendaView> GetByPedido(int pedidoId)
        {
            return controller.GetByPedido(pedidoId);
        }

        public IEnumerable<ItemPedidoVendaView> GetByPedidoAgrupadoView(int pedidoId, bool AgrupaTamanho)
        {
            return controller.GetByPedidoAgrupadoView(pedidoId, AgrupaTamanho);
        }

        public IEnumerable<ItemPedidoVendaView> GetItensConferenciaByBusca(int almoxarifadoId, List<int> produtosIds)
        {
            return controller.GetItensConferenciaByBusca(almoxarifadoId, produtosIds);
        }

    }
}
