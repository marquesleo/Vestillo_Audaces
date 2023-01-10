using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Controllers;
using Vestillo.Business.Repositories;
using Vestillo.Lib;

namespace Vestillo.Business.Service.Web
{
    public class ItemLiberacaoPedidoVendaServiceWeb : GenericServiceWeb<ItemLiberacaoPedidoVenda,ItemLiberacaoPedidoVendaRepository, ItemLiberacaoPedidoVendaController>, IItemLiberacaoPedidoVendaService
    {

        public ItemLiberacaoPedidoVendaServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public void Save(ref List<ItemLiberacaoPedidoVenda> itensLiberacao)
        {
            throw new NotImplementedException();
        }

        public List<ItemLiberacaoPedidoVenda> GetByPedidoVenda(int pedidoVendaId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ItemLiberacaoPedidoVendaView> GetListByItensLiberacaoView(int IdPedido)
        {

            throw new NotImplementedException();

        }

        public IEnumerable<ItemLiberacaoPedidoVendaView> GetItensLiberacaoParcialSemEstoque()
        {

            throw new NotImplementedException();

        }

        public IEnumerable<LiberacaoPedidoVenda> GetLiberacaoPedidoVenda(int IdPedido)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<ItemLiberacaoPedidoVenda> GetByItemPedidoVenda(int itemPedidoVendaId)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<ItemLiberacaoPedidoVendaView> GetListByItensLiberacaoParaConferenciaView(List<int> IdPedido)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ItemLiberacaoPedidoVendaView> GetListByConferencia(int conferenciaId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ItemLiberacaoPedidoVendaView> GetItensLiberacaoParcialSemEstoque(bool fichaTecnicaCompleta)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ItemLiberacaoPedidoVendaView> GetItensLiberacaoParaConferenciaSemEmpenho(List<int> IdPedido, bool visualizar)
        {
            throw new NotImplementedException();
        }
    }
}


