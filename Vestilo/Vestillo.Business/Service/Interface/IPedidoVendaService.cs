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
    public interface IPedidoVendaService : IService<PedidoVenda, PedidoVendaRepository, PedidoVendaController>
    {
        IEnumerable<PedidoVendaView> GetAllView();
        PedidoVendaView GetByIdView(int id);
        PedidoVenda GetByIdAtualizacao(int id);
        PedidoVendaView GetByIdViewLiberacao(int id);
        PedidoVendaView GetByIdViewAgrupado(int id);
        PedidoVendaView GetByItemIdView(int id);
        List<PedidoVendaLiberacaoView> GetPedidoLiberacao(int StatusPedido = 0,int StatusPedido2 = 0);
        List<PedidoVendaLiberacaoView> GetPedidoLiberacaoParaConferencia();
        List<PedidoVendaLiberacaoView> GetPedidoLiberacaoParaConferencia(int codigo);
        IEnumerable<PedidoVendaView> GetListPorReferencia(string referencia,string parametrosDaBusca);
        IEnumerable<PedidoVendaView> GetListPorCliente(string Cliente, string parametrosDaBusca);
        void FinalizarPedidoVenda(int pedidoVendaId);
        List<PedidoVendaLiberacaoView> GetPedidoByItem(string referencia);
    }
}
