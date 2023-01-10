using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.Interface;
using Vestillo.Business.Models.Views;
namespace Vestillo.Business.Service.APP
{
    public class BalancoEstoqueServiceAPP : GenericServiceAPP<BalancoEstoque, BalancoEstoqueRepository, BalancoEstoqueController>, IBalancoEstoqueService
    {
        public BalancoEstoqueServiceAPP()
            : base(new BalancoEstoqueController())
        {
        }

        public IEnumerable<BalancoEstoqueView> GetBalancoEstoque()
        {
            BalancoEstoqueController controller = new BalancoEstoqueController();
            return controller.GetBalancoEstoque();
        }

        public BalancoEstoqueView GetByIdView(int idBalanco)
        {
            BalancoEstoqueController controller = new BalancoEstoqueController();
            return controller.GetByIdView(idBalanco);
        }

        public IEnumerable<BalancoEstoqueItensView> GetBuscaProduto(int almoxarifadoId, int tipoBusca, List<int> id)
        {
            BalancoEstoqueController controller = new BalancoEstoqueController();
            return controller.GetBuscaProduto(almoxarifadoId, tipoBusca, id);
        }

        public void FinalizarBalancoEstoque(int balancoId)
        {
            BalancoEstoqueController controller = new BalancoEstoqueController();
            controller.FinalizarBalancoEstoque(balancoId);
        }

        public IEnumerable<BalancoEstoqueView> GetByAlmoxarifado(int idAlmoxarifado)
        {
            BalancoEstoqueController controller = new BalancoEstoqueController();
            return controller.GetByAlmoxarifado(idAlmoxarifado);
        }

        public IEnumerable<BalancoEstoqueItensView> GetBuscaByProduto(string busca, bool buscarPorId, int almoxarifadoId)
        {
            BalancoEstoqueController controller = new BalancoEstoqueController();
            return controller.GetBuscaByProduto(busca, buscarPorId, almoxarifadoId);
        }

        public void UpdateGridPedido()
        {
            BalancoEstoqueController controller = new BalancoEstoqueController();
            controller.UpdateGridPedido();
        }
    }
}
