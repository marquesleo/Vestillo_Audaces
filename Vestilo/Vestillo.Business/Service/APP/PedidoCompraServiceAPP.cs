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
    public class PedidoCompraServiceAPP: GenericServiceAPP<PedidoCompra, PedidoCompraRepository, PedidoCompraController>, IPedidoCompraService
    {
        public PedidoCompraServiceAPP()
            : base(new PedidoCompraController())
        {
        }

        public PedidoCompraView GetByIdView(int id)
        {
            PedidoCompraController controller = new PedidoCompraController();
            return controller.GetByIdView(id);
        }

        public IEnumerable<PedidoCompraView> GetAllView()
        {
            PedidoCompraController controller = new PedidoCompraController();
            return controller.GetAllView();
        }

        public IEnumerable<PedidoCompraView> GetListPorReferencia(string referencia,string parametrosDaBusca)
        {
            return controller.GetListPorReferencia(referencia, parametrosDaBusca);
        }

        public IEnumerable<PedidoCompraView> GetListPorFornecedor(string fornecedor, string parametrosDaBusca)
        {
            return controller.GetListPorFornecedor(fornecedor, parametrosDaBusca);
        }

        public void FinalizarPedidoCompra(int pedidoCompraId)
        {
            controller.FinalizarPedidoCompra(pedidoCompraId);
        }

        public List<int> GetSemanas()
        {
            return controller.GetSemanas();
        }


        public IEnumerable<PedidoCompraView> GetAllViewComItens()
        {
            return controller.GetAllViewComItens();
        }


        public IEnumerable<ConsultaPedidoCompraRelatorio> GetPedidoParaRelatorio(FiltroPedidoCompra filtro)
        {
            return controller.GetPedidoParaRelatorio(filtro);
        }
    }
}
