using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service
{
    public interface IItemPedidoVendaService : IService<ItemPedidoVenda, ItemPedidoVendaRepository, ItemPedidoVendaController>
    {
        IEnumerable<ItemPedidoVendaView> GetAllView();
        IEnumerable<ConsultaPedidoVendaRelatorio> GetPedidoParaRelatorio(FiltroPedidoVendaRelatorio filtro);
        IEnumerable<ItemPedidoVendaView> GetByPedido(int pedidoId);
        IEnumerable<ItemPedidoVendaView> GetByPedidoAgrupadoView(int pedidoId, bool AgruparTamanho);
        IEnumerable<ItemPedidoVendaView> GetItensConferenciaByBusca(int almoxarifadoId, List<int> produtosIds);
    }
}
