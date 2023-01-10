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
    public class PedidoCompraServiceWeb: GenericServiceWeb<PedidoCompra, PedidoCompraRepository, PedidoCompraController>, IPedidoCompraService
    {
        public PedidoCompraServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public PedidoCompraView GetByIdView(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PedidoCompraView> GetAllView()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PedidoCompraView> GetListPorReferencia(string referencia,string parametrosDaBusca)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PedidoCompraView> GetListPorFornecedor(string fornecedor, string parametrosDaBusca)
        {
            throw new NotImplementedException();
        }

        public void FinalizarPedidoCompra(int pedidoCompraId)
        {
            throw new NotImplementedException();
        }

        public List<int> GetSemanas()
        {
            throw new NotImplementedException();
        }


        public IEnumerable<PedidoCompraView> GetAllViewComItens()
        {
            throw new NotImplementedException();
        }


        public IEnumerable<ConsultaPedidoCompraRelatorio> GetPedidoParaRelatorio(FiltroPedidoCompra filtro)
        {
            throw new NotImplementedException();
        }
    }
}
