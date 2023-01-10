using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class ItemPedidoVendaController: GenericController<ItemPedidoVenda, ItemPedidoVendaRepository>
    {
        public IEnumerable<ItemPedidoVendaView> GetAllView()
        {
            using (var repository = new ItemPedidoVendaRepository())
            {
                return repository.GetAllView();
            }
        }

        public IEnumerable<ConsultaPedidoVendaRelatorio> GetPedidoParaRelatorio(FiltroPedidoVendaRelatorio filtro)
        {
            using (var repository = new ItemPedidoVendaRepository())
            {
                var resp = repository.GetPedidoParaRelatorio(filtro).ToList();
                //switch (filtro.Ordernar)
                //{
                //    case "Pedido":
                //        resp.OrderBy(r => r.PedidoId);
                //        break;
                //    case "Emissao":
                //        resp.OrderBy(r => r.DataEmissao);
                //        break;
                //    case "Cliente":
                //        resp.OrderBy(r => r.NomeCliente);
                //        break;
                //}

                return  resp;// repository.GetPedidoParaRelatorio(filtro);
            }
        }

        public IEnumerable<ItemPedidoVendaView> GetByPedido(int pedidoId)
        {
            return _repository.GetByPedido(pedidoId);
        }

        public IEnumerable<ItemPedidoVendaView> GetByPedidoAgrupadoView(int pedidoId, bool AgruparTamanho)
        {
            return _repository.GetByPedidoAgrupadoView(pedidoId, AgruparTamanho);
        }

        public IEnumerable<ItemPedidoVendaView> GetItensConferenciaByBusca(int almoxarifadoId, List<int> produtosIds)
        {
            return _repository.GetItensConferenciaByBusca(almoxarifadoId, produtosIds);
        }
    }
}
