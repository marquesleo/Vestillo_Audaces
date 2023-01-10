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
    public class ItemTabelaPrecoServiceWeb: GenericServiceWeb<ItemTabelaPreco, ItemTabelaPrecoRepository, ItemTabelaPrecoController>, IItemTabelaPrecoService
    {

        public ItemTabelaPrecoServiceWeb(string requestUri) : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<ItemTabelaPrecoView> GetItensTabelaPrecoView(int tabelaPrecoId)
        {
            //var c = new ConnectionWebAPI<ItemTabelaPrecoView>(VestilloSession.UrlWebAPI);
            //return c.Get(this.RequestUri, "tabelaPrecoId=" + tabelaPrecoId.ToString());
            return null;
        }

        public ItemTabelaPrecoView GetItemTabelaPrecoView(int tabelaPrecoId, int produtoId)
        {
            //var c = new ConnectionWebAPI<ItemTabelaPrecoView>(VestilloSession.UrlWebAPI);
            //return c.Get(this.RequestUri, "tabelaPrecoId=" + tabelaPrecoId.ToString());
            return null;
        }


        public IEnumerable<ItemTabelaPrecoView> GetItensTabelaPrecoComGradeView(int tabelaPrecoId, int catalogoId)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<TamanhoView> GetItensTabelaPrecoTamanhos(int itemId, int corId, List<int> almoxarifadoIds)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TamanhoView> GetItensTabelaPrecoTamanhosOrdem(int itemId, int corId, List<int> almoxarifadoIds)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<ItemTabelaPrecoView> GetItensTabelaPrecoComGradeView(int tabelaPrecoId, List<int> catalogoIds, List<int> almoxarifadoIds)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ItemTabelaPrecoView> GetItensTabelaPrecoComGradeEstoque(int tabelaPrecoId, List<int> catalogoIds, List<int> tamanhoIds, List<int> almoxarifadoIds)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ItemTabelaPrecoView> GetItensTabelaPrecoComGradeOrdem(int tabelaPrecoId, List<int> catalogoIds, List<int> tamanhoIds, List<int> almoxarifadoIds)
        {
            throw new NotImplementedException();
        }


    }
}
