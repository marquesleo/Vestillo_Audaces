using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models.Views;

namespace Vestillo.Business.Service.Web
{
    public class ItemOrdemProducaoServiceWeb: GenericServiceWeb<ItemOrdemProducao, ItemOrdemProducaoRepository, ItemOrdemProducaoController>, IItemOrdemProducaoService
    {
        public ItemOrdemProducaoServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<ItemOrdemProducaoView> GetByPedidoVenda(int pedidoId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ItemOrdemProducaoView> GetByProduto(int produtoId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ItemOrdemProducaoView> GetByPedido(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ItemOrdemProducaoView> GetByOrdem(int id)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<OrdemProducaoStatusRel> GetByFiltro(Models.Views.FiltroOrdemProducao filtro)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<OrdemProducaoMaterialView> GetByOrdensParaPedidoCompra(List<int> idsOdens)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<OrdemCompra> GetByOrdensParaCompra(List<int> idsOdens, List<int> idsGrupos)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<OrdemCompra> GetByOrdensParaCompraComEstoque(List<int> idsOdens, List<int> idsGrupos)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<OrdemProducaoStatusRel> GetByFiltroOrdem(FiltroOrdemProducao filtro)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<OrdemProducaoStatusRel> GetBySetorFiltro(FiltroOrdemProducao filtro)
        {
            throw new NotImplementedException();
        }

        public void LimparVinculoPedidoOrdem(int idItemPedido)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GestaoOrdemCompra> GetByGestaoOrdemCompra(List<int> idsOdens, List<int> idsMateriaPrima, DateTime DaInclusao, DateTime AteInclusao)
        {
            throw new NotImplementedException();
        }
    }
}
