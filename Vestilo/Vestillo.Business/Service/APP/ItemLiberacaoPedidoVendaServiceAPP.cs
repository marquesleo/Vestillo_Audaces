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
    public class ItemLiberacaoPedidoVendaServiceAPP : GenericServiceAPP<ItemLiberacaoPedidoVenda, ItemLiberacaoPedidoVendaRepository, ItemLiberacaoPedidoVendaController>, IItemLiberacaoPedidoVendaService
    {

        public ItemLiberacaoPedidoVendaServiceAPP()
            : base(new ItemLiberacaoPedidoVendaController())
        {
        }

        public void Save(ref List<ItemLiberacaoPedidoVenda> itensLiberacao)
        {
            controller.Save(ref itensLiberacao);
        }

        public List<ItemLiberacaoPedidoVenda> GetByPedidoVenda(int pedidoVendaId)
        {
            return controller.GetByPedidoVenda(pedidoVendaId);
        }

        public IEnumerable<ItemLiberacaoPedidoVendaView> GetListByItensLiberacaoView(int IdPedido)
        {

            return controller.GetListByItensLiberacaoView(IdPedido);
            
        }

        public IEnumerable<ItemLiberacaoPedidoVendaView> GetItensLiberacaoParcialSemEstoque()
        {

            return controller.GetItensLiberacaoParcialSemEstoque();

        }

        public IEnumerable<LiberacaoPedidoVenda> GetLiberacaoPedidoVenda(int IdPedido)
        {
            return controller.GetLiberacaoPedidoVenda(IdPedido);
        }


        public IEnumerable<ItemLiberacaoPedidoVenda> GetByItemPedidoVenda(int itemPedidoVendaId)
        {
            return controller.GetByItemPedidoVenda(itemPedidoVendaId);
        }


        public IEnumerable<ItemLiberacaoPedidoVendaView> GetListByItensLiberacaoParaConferenciaView(List<int> IdPedido)
        {

            return controller.GetListByItensLiberacaoParaConferenciaView(IdPedido);
        }

        public IEnumerable<ItemLiberacaoPedidoVendaView> GetListByConferencia(int conferenciaId)
        {
            return controller.GetListByConferencia(conferenciaId);
        }

        public IEnumerable<ItemLiberacaoPedidoVendaView> GetItensLiberacaoParcialSemEstoque(bool fichaTecnicaCompleta)
        {

            return controller.GetItensLiberacaoParcialSemEstoque(fichaTecnicaCompleta);

        }

        public IEnumerable<ItemLiberacaoPedidoVendaView> GetItensLiberacaoParaConferenciaSemEmpenho(List<int> IdPedido, bool visualizar)
        {
            return controller.GetItensLiberacaoParaConferenciaSemEmpenho(IdPedido, visualizar);
        }
    }
}



