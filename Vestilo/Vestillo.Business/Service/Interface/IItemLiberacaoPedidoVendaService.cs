using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Controllers;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service
{
    public interface IItemLiberacaoPedidoVendaService : IService<ItemLiberacaoPedidoVenda, ItemLiberacaoPedidoVendaRepository, ItemLiberacaoPedidoVendaController>
    {
        void Save(ref List<ItemLiberacaoPedidoVenda> itensLiberacao);
        List<ItemLiberacaoPedidoVenda> GetByPedidoVenda(int pedidoVendaId);
        IEnumerable<ItemLiberacaoPedidoVendaView> GetListByItensLiberacaoView(int IdPedido);
        IEnumerable<ItemLiberacaoPedidoVendaView> GetListByItensLiberacaoParaConferenciaView(List<int> IdPedido);
        IEnumerable<ItemLiberacaoPedidoVendaView> GetItensLiberacaoParcialSemEstoque();
        IEnumerable<ItemLiberacaoPedidoVenda> GetByItemPedidoVenda(int itemPedidoVendaId);
        IEnumerable<ItemLiberacaoPedidoVendaView> GetListByConferencia(int conferenciaId);
        IEnumerable<ItemLiberacaoPedidoVendaView> GetItensLiberacaoParcialSemEstoque(bool fichaTecnicaCompleta);
        IEnumerable<ItemLiberacaoPedidoVendaView> GetItensLiberacaoParaConferenciaSemEmpenho(List<int> IdPedido, bool visualizar);
    }
}



