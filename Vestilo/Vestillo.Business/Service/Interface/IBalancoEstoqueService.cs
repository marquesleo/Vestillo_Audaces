using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Models.Views;

namespace Vestillo.Business.Service
{
    public interface IBalancoEstoqueService : IService<BalancoEstoque, BalancoEstoqueRepository, BalancoEstoqueController> 
    {
        IEnumerable<BalancoEstoqueView> GetBalancoEstoque();
        BalancoEstoqueView GetByIdView(int idBalanco);
        IEnumerable<BalancoEstoqueItensView> GetBuscaProduto(int almoxarifadoId, int tipoBusca, List<int> id);

        IEnumerable<BalancoEstoqueItensView> GetBuscaByProduto(string busca, bool buscarPorId, int almoxarifadoId);

        void FinalizarBalancoEstoque(int balancoId);

        IEnumerable<BalancoEstoqueView> GetByAlmoxarifado(int idAlmoxarifado);

        void UpdateGridPedido();
    }
}
