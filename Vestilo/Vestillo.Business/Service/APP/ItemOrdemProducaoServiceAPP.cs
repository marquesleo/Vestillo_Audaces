using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Service.APP
{
    class ItemOrdemProducaoServiceAPP: GenericServiceAPP<ItemOrdemProducao, ItemOrdemProducaoRepository, ItemOrdemProducaoController>, IItemOrdemProducaoService
    {
        public ItemOrdemProducaoServiceAPP()
            : base(new ItemOrdemProducaoController())
        {

        }

        public IEnumerable<ItemOrdemProducaoView> GetByPedidoVenda(int pedidoId)
        {
            ItemOrdemProducaoController controller = new ItemOrdemProducaoController();
            return controller.GetByPedidoVenda(pedidoId);
        }

        public IEnumerable<ItemOrdemProducaoView> GetByPedido(int id)
        {
            ItemOrdemProducaoController controller = new ItemOrdemProducaoController();
            return controller.GetByPedido(id);
        }

        public IEnumerable<ItemOrdemProducaoView> GetByOrdem(int id)
        {
            ItemOrdemProducaoController controller = new ItemOrdemProducaoController();
            return controller.GetByOrdem(id);
        }

        public IEnumerable<ItemOrdemProducaoView> GetByProduto(int produtoId)
        {
            ItemOrdemProducaoController controller = new ItemOrdemProducaoController();
            return controller.GetByProduto(produtoId);
        }


        public IEnumerable<OrdemProducaoStatusRel> GetByFiltro(FiltroOrdemProducao filtro)
        {
            ItemOrdemProducaoController controller = new ItemOrdemProducaoController();
            return controller.GetByProduto(filtro);
        }


        public IEnumerable<OrdemProducaoMaterialView> GetByOrdensParaPedidoCompra(List<int> idsOdens)
        {
            ItemOrdemProducaoController controller = new ItemOrdemProducaoController();
            return controller.GetByOrdensParaPedidoCompra(idsOdens);
        }


        public IEnumerable<OrdemCompra> GetByOrdensParaCompra(List<int> idsOdens, List<int> idsGrupos)
        {
            ItemOrdemProducaoController controller = new ItemOrdemProducaoController();
            return controller.GetByOrdensParaCompra(idsOdens, idsGrupos);
        }


        public IEnumerable<OrdemCompra> GetByOrdensParaCompraComEstoque(List<int> idsOdens, List<int> idsGrupos)
        {
            ItemOrdemProducaoController controller = new ItemOrdemProducaoController();
            return controller.GetByOrdensParaCompraComEstoque(idsOdens, idsGrupos);
        }


        public IEnumerable<OrdemProducaoStatusRel> GetByFiltroOrdem(FiltroOrdemProducao filtro)
        {
            ItemOrdemProducaoController controller = new ItemOrdemProducaoController();
            return controller.GetByFiltroOrdem(filtro);
        }


        public IEnumerable<OrdemProducaoStatusRel> GetBySetorFiltro(FiltroOrdemProducao filtro)
        {
            ItemOrdemProducaoController controller = new ItemOrdemProducaoController();
            return controller.GetBySetorFiltro(filtro);
        }

        public void LimparVinculoPedidoOrdem(int idItemPedido)
        {
            ItemOrdemProducaoController controller = new ItemOrdemProducaoController();
            controller.LimparVinculoPedidoOrdem(idItemPedido);
        }

        public IEnumerable<GestaoOrdemCompra> GetByGestaoOrdemCompra(List<int> idsOdens, List<int> idsMateriaPrima, DateTime DaInclusao, DateTime AteInclusao)
        {
            ItemOrdemProducaoController controller = new ItemOrdemProducaoController();
            return controller.GetByGestaoOrdemCompra(idsOdens, idsMateriaPrima, DaInclusao, AteInclusao);
        }
    }
}
