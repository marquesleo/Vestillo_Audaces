using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class ItemTabelaPrecoPCPController: GenericController<ItemTabelaPrecoPCP, ItemTabelaPrecoPCPRepository>
    {
        public IEnumerable<ItemTabelaPrecoPCP> GetItensTabelaPreco(int tabelaPrecoId)
        {
            using (var repository = new ItemTabelaPrecoPCPRepository())
            {
                return repository.GetItensTabelaPreco(tabelaPrecoId);
            }
        }

        public ItemTabelaPrecoPCP GetItemTabelaPreco(int tabelaPrecoId, int produtoId)
        {
            using (var repository = new ItemTabelaPrecoPCPRepository())
            {
                return repository.GetItemTabelaPreco(tabelaPrecoId, produtoId);
            }
        }

        public void DeleteByTabelaPreco(int tabelaPrecoId)
        {
            using (var repository = new ItemTabelaPrecoPCPRepository())
            {
                repository.DeleteByTabelaPreco(tabelaPrecoId);
            }
        }

        public IEnumerable<ItemTabelaPrecoPCP> GetListByProduto(int produtoId)
        {
            using (var repository = new ItemTabelaPrecoPCPRepository())
            {
                return repository.GetListByProduto(produtoId);
            }
        }

        public IEnumerable<ItemTabelaPrecoPCP> GetAllByProduto(int produtoId)
        {
            using (var repository = new ItemTabelaPrecoPCPRepository())
            {
                return repository.GetAllByProduto(produtoId);
            }
        }
    }
}
