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
    public class ItemTabelaPrecoPCPServiceWeb: GenericServiceWeb<ItemTabelaPrecoPCP, ItemTabelaPrecoPCPRepository, ItemTabelaPrecoPCPController>, IItemTabelaPrecoPCPService
    {

        public ItemTabelaPrecoPCPServiceWeb(string requestUri) : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<ItemTabelaPrecoPCP> GetItensTabelaPreco(int tabelaPrecoId)
        {
            //var c = new ConnectionWebAPI<ItemTabelaPrecoPCP>(VestilloSession.UrlWebAPI);
            //return c.Get(this.RequestUri, "tabelaPrecoId=" + tabelaPrecoId.ToString());
            return null;
        }

        public ItemTabelaPrecoPCP GetItemTabelaPreco(int tabelaPrecoId, int produtoId)
        {
            //var c = new ConnectionWebAPI<ItemTabelaPrecoView>(VestilloSession.UrlWebAPI);
            //return c.Get(this.RequestUri, "tabelaPrecoId=" + tabelaPrecoId.ToString());
            return null;
        }

        public IEnumerable<ItemTabelaPrecoPCP> GetAllByProduto(int produtoId)
        {
            return null;
        }
    }
}
