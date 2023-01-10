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
    public interface IPedidoCompraService : IService<PedidoCompra, PedidoCompraRepository, PedidoCompraController>
    {
        PedidoCompraView GetByIdView(int id);
        IEnumerable<PedidoCompraView> GetAllView();
        IEnumerable<PedidoCompraView> GetAllViewComItens();
        IEnumerable<PedidoCompraView> GetListPorReferencia(string referencia,string parametrosDaBusca);
        IEnumerable<PedidoCompraView> GetListPorFornecedor(string fornecedor, string parametrosDaBusca);
        IEnumerable<ConsultaPedidoCompraRelatorio> GetPedidoParaRelatorio(FiltroPedidoCompra filtro);
        void FinalizarPedidoCompra(int pedidoCompraId);
        List<int> GetSemanas();
        
    }
}
