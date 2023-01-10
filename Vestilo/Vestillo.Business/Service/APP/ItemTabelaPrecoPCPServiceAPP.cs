using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service.APP
{
    public class ItemTabelaPrecoPCPServiceAPP : GenericServiceAPP<ItemTabelaPrecoPCP, ItemTabelaPrecoPCPRepository, ItemTabelaPrecoPCPController>, IItemTabelaPrecoPCPService
    {

        public ItemTabelaPrecoPCPServiceAPP(): base (new ItemTabelaPrecoPCPController())
        {
        }

        public IEnumerable<ItemTabelaPrecoPCP> GetItensTabelaPreco(int tabelaPrecoId)
        {
            return controller.GetItensTabelaPreco(tabelaPrecoId);
        }

        public ItemTabelaPrecoPCP GetItemTabelaPreco(int tabelaPrecoId, int produtoId)
        {
            return controller.GetItemTabelaPreco(tabelaPrecoId, produtoId);
        }

        public IEnumerable<ItemTabelaPrecoPCP> GetAllByProduto(int produtoId)
        {
            return controller.GetAllByProduto(produtoId);
        }
    }
}
