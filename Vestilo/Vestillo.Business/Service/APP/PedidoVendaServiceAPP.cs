using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.Interface;

namespace Vestillo.Business.Service.APP
{
    public class PedidoVendaServiceAPP: GenericServiceAPP<PedidoVenda, PedidoVendaRepository, PedidoVendaController>, IPedidoVendaService
    {
        public PedidoVendaServiceAPP()
            : base(new PedidoVendaController())
        {
        }

        public PedidoVendaView GetByIdView(int id)
        {
            PedidoVendaController controller = new PedidoVendaController();
            return controller.GetByIdView(id);
        }

        public PedidoVendaView GetByIdViewLiberacao(int id)
        {
            PedidoVendaController controller = new PedidoVendaController();
            return controller.GetByIdViewLiberacao(id);
        }

        public PedidoVendaView GetByIdViewAgrupado(int id)
        {
            PedidoVendaController controller = new PedidoVendaController();
            return controller.GetByIdViewAgrupado(id);
        }

        public PedidoVendaView GetByItemIdView(int id)
        {
            PedidoVendaController controller = new PedidoVendaController();
            return controller.GetByItemIdView(id);
        }

        public IEnumerable<PedidoVendaView> GetAllView()
        {
            PedidoVendaController controller = new PedidoVendaController();
            return controller.GetAllView();
        }

        public List<PedidoVendaLiberacaoView> GetPedidoLiberacao(int StatusPedido = 0, int StatusPedido2 = 0)
        {
            PedidoVendaController controller = new PedidoVendaController();
            return controller.GetPedidoLiberacao(StatusPedido, StatusPedido2);
        }

        public IEnumerable<PedidoVendaView> GetListPorReferencia(string referencia,string parametrosDaBusca)
        {
            return controller.GetListPorReferencia(referencia, parametrosDaBusca);
        }

        public IEnumerable<PedidoVendaView> GetListPorCliente(string Cliente, string parametrosDaBusca)
        {
            return controller.GetListPorCliente(Cliente, parametrosDaBusca);
        }

        public void FinalizarPedidoVenda(int pedidoVendaId)
        {
            controller.FinalizarPedidoVenda(pedidoVendaId);
        }


        public List<PedidoVendaLiberacaoView> GetPedidoLiberacaoParaConferencia()
        {
            return controller.GetPedidoLiberacaoParaConferencia();
        }


        public List<PedidoVendaLiberacaoView> GetPedidoLiberacaoParaConferencia(int codigo)
        {
            return controller.GetPedidoLiberacaoParaConferencia(codigo);
        }


        public PedidoVenda GetByIdAtualizacao(int id)
        {
            return controller.GetByIdAtualizacao(id);
        }

        public List<PedidoVendaLiberacaoView> GetPedidoByItem(string referencia)
        {
            return controller.GetPedidoByItem(referencia);
        }

    }
}
