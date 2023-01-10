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
    public class ItemTabelaPrecoServiceAPP : GenericServiceAPP<ItemTabelaPreco, ItemTabelaPrecoRepository, ItemTabelaPrecoController>, IItemTabelaPrecoService
    {

        public ItemTabelaPrecoServiceAPP(): base (new ItemTabelaPrecoController())
        {
        }

        public IEnumerable<ItemTabelaPrecoView> GetItensTabelaPrecoView(int tabelaPrecoId)
        {
            return controller.GetItensTabelaPrecoView(tabelaPrecoId);
        }

        public ItemTabelaPrecoView GetItemTabelaPrecoView(int tabelaPrecoId, int produtoId)
        {
            return controller.GetItemTabelaPrecoView(tabelaPrecoId, produtoId);
        }


        public IEnumerable<ItemTabelaPrecoView> GetItensTabelaPrecoComGradeView(int tabelaPrecoId, int catalogoId)
        {
            return controller.GetItensTabelaPrecoComGradeView(tabelaPrecoId, catalogoId);
        }


        public IEnumerable<TamanhoView> GetItensTabelaPrecoTamanhos(int itemId, int corId, List<int> almoxarifadoIds)
        {
            return controller.GetItensTabelaPrecoTamanhos(itemId, corId, almoxarifadoIds);
        }

        public IEnumerable<TamanhoView> GetItensTabelaPrecoTamanhosOrdem(int itemId, int corId, List<int> almoxarifadoIds)
        {
            return controller.GetItensTabelaPrecoTamanhosOrdem(itemId, corId, almoxarifadoIds);
        }


        public IEnumerable<ItemTabelaPrecoView> GetItensTabelaPrecoComGradeView(int tabelaPrecoId, List<int> catalogoIds, List<int> almoxarifadoIds)
        {
            return controller.GetItensTabelaPrecoComGradeView(tabelaPrecoId, catalogoIds, almoxarifadoIds);
        }

        public IEnumerable<ItemTabelaPrecoView> GetItensTabelaPrecoComGradeEstoque(int tabelaPrecoId, List<int> catalogoIds, List<int> tamanhoIds, List<int> almoxarifadoIds)
        {
            return controller.GetItensTabelaPrecoComGradeEstoque(tabelaPrecoId, catalogoIds, tamanhoIds, almoxarifadoIds);
        }

        public IEnumerable<ItemTabelaPrecoView> GetItensTabelaPrecoComGradeOrdem(int tabelaPrecoId, List<int> catalogoIds, List<int> tamanhoIds, List<int> almoxarifadoIds)
        {
            return controller.GetItensTabelaPrecoComGradeOrdem(tabelaPrecoId, catalogoIds, tamanhoIds, almoxarifadoIds);
        }
    }
}
