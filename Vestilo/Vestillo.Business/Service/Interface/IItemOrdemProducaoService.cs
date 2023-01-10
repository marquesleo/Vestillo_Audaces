using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Service
{
    public interface IItemOrdemProducaoService : IService<ItemOrdemProducao, ItemOrdemProducaoRepository, ItemOrdemProducaoController>
    {
        IEnumerable<ItemOrdemProducaoView> GetByOrdem(int id);
        IEnumerable<ItemOrdemProducaoView> GetByPedidoVenda(int pedidoId);
        IEnumerable<ItemOrdemProducaoView> GetByPedido(int id);
        IEnumerable<ItemOrdemProducaoView> GetByProduto(int produtoId);
        IEnumerable<OrdemProducaoStatusRel> GetByFiltro(FiltroOrdemProducao filtro);
        IEnumerable<OrdemProducaoStatusRel> GetBySetorFiltro(FiltroOrdemProducao filtro);
        IEnumerable<OrdemProducaoMaterialView> GetByOrdensParaPedidoCompra(List<int> idsOdens);
        IEnumerable<OrdemCompra> GetByOrdensParaCompra(List<int> idsOdens, List<int> idsGrupos);
        IEnumerable<OrdemCompra> GetByOrdensParaCompraComEstoque(List<int> idsOdens, List<int> idsGrupos);
        IEnumerable<OrdemProducaoStatusRel> GetByFiltroOrdem(FiltroOrdemProducao filtro);
        void LimparVinculoPedidoOrdem(int idItemPedido);
        IEnumerable<GestaoOrdemCompra> GetByGestaoOrdemCompra(List<int> idsOdens, List<int> idsMateriaPrima, DateTime DaInclusao, DateTime AteInclusao);
    }
}
