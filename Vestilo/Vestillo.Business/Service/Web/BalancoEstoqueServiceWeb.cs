using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Controllers;
using Vestillo.Business.Repositories;
using Vestillo.Lib;

namespace Vestillo.Business.Service.Web
{
    public class BalancoEstoqueServiceWeb : GenericServiceWeb<BalancoEstoque, BalancoEstoqueRepository, BalancoEstoqueController>, IBalancoEstoqueService
    {
        public BalancoEstoqueServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }
        
        public IEnumerable<BalancoEstoqueView> GetBalancoEstoque()
        {
            throw new NotImplementedException();
        }

        public BalancoEstoqueView GetByIdView(int idBalanco)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BalancoEstoqueItensView> GetBuscaProduto(int almoxarifadoId, int tipoBusca, List<int> id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BalancoEstoqueItensView> GetBuscaByProduto(string busca, bool buscarPorId, int almoxarifadoId)
        {
            throw new NotImplementedException();
        }

        public void FinalizarBalancoEstoque(int balancoId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BalancoEstoqueView> GetByAlmoxarifado(int idAlmoxarifado)
        {
            throw new NotImplementedException();
        }

        public void UpdateGridPedido()
        {
            throw new NotImplementedException();
        }

    }
}
