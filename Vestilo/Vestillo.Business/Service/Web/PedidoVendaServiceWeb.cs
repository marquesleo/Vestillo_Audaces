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
using Vestillo.Lib;

namespace Vestillo.Business.Service.Web
{
    public class PedidoVendaServiceWeb: GenericServiceWeb<PedidoVenda, PedidoVendaRepository, PedidoVendaController>, IPedidoVendaService
    {
        public PedidoVendaServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public PedidoVendaView GetByIdView(int id)
        {
            throw new NotImplementedException();
        }

        public List<PedidoVendaLiberacaoView> GetPedidoLiberacao(int StatusPedido = 0,int StatusPedido2 = 0)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PedidoVendaView> GetAllView()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PedidoVendaView> GetListPorReferencia(string referencia,string parametrosDaBusca)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PedidoVendaView> GetListPorCliente(string Cliente, string parametrosDaBusca)
        {
            throw new NotImplementedException();
        }

        public void FinalizarPedidoVenda(int pedidoVendaId)
        {
            throw new NotImplementedException();
        }


        public List<PedidoVendaLiberacaoView> GetPedidoLiberacaoParaConferencia()
        {
            throw new NotImplementedException();
        }


        public PedidoVendaView GetByItemIdView(int id)
        {
            throw new NotImplementedException();
        }


        public PedidoVendaView GetByIdViewAgrupado(int id)
        {
            throw new NotImplementedException();
        }


        public PedidoVendaView GetByIdViewLiberacao(int id)
        {
            throw new NotImplementedException();
        }


        public List<PedidoVendaLiberacaoView> GetPedidoLiberacaoParaConferencia(int codigo)
        {
            throw new NotImplementedException();
        }


        public PedidoVenda GetByIdAtualizacao(int id)
        {
            throw new NotImplementedException();
        }

        public List<PedidoVendaLiberacaoView> GetPedidoByItem(string referencia)
        {
            throw new NotImplementedException();
        }
    }
}
