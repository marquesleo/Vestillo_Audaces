using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Controllers;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service
{
    public interface IItemTabelaPrecoPCPService: IService<ItemTabelaPrecoPCP, ItemTabelaPrecoPCPRepository, ItemTabelaPrecoPCPController>
    {
        IEnumerable<ItemTabelaPrecoPCP> GetItensTabelaPreco(int tabelaPrecoId);
        ItemTabelaPrecoPCP GetItemTabelaPreco(int tabelaPrecoId, int produtoId);
        IEnumerable<ItemTabelaPrecoPCP> GetAllByProduto(int produtoId);
        
    }
}
