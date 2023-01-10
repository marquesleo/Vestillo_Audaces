using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class ItemTabelaPrecoController: GenericController<ItemTabelaPreco, ItemTabelaPrecoRepository>
    {
        public IEnumerable<ItemTabelaPrecoView> GetItensTabelaPrecoView(int tabelaPrecoId)
        {
            using (var repository = new ItemTabelaPrecoRepository())
            {
                return repository.GetItensTabelaPrecoView(tabelaPrecoId);
            }
        }

        public ItemTabelaPrecoView GetItemTabelaPrecoView(int tabelaPrecoId, int produtoId)
        {
            using (var repository = new ItemTabelaPrecoRepository())
            {
                return repository.GetItemTabelaPrecoView(tabelaPrecoId, produtoId);
            }
        }

        public void DeleteByTabelaPreco(int tabelaPrecoId)
        {
            using (var repository = new ItemTabelaPrecoRepository())
            {
                repository.DeleteByTabelaPreco(tabelaPrecoId);
            }
        }

        public void DeleteByTabelaPrecoEProduto(int tabelaPrecoId, int produtoId)
        {
            using (var repository = new ItemTabelaPrecoRepository())
            {
                repository.DeleteByTabelaPrecoEProduto(tabelaPrecoId, produtoId);
            }
        }

        public IEnumerable<ItemTabelaPreco> GetListByProduto(int produtoId)
        {
            using (var repository = new ItemTabelaPrecoRepository())
            {
                return repository.GetListByProduto(produtoId);
            }
        }

        public IEnumerable<ItemTabelaPrecoView> GetItensTabelaPrecoComGradeView(int tabelaPrecoId, int catalogoId)
        {
            using (var repository = new ItemTabelaPrecoRepository())
            {
                return repository.GetItensTabelaPrecoComGradeView(tabelaPrecoId, catalogoId);
            }
        }

        public IEnumerable<ItemTabelaPrecoView> GetItensTabelaPrecoComGradeView(int tabelaPrecoId, List<int> catalogoIds, List<int> almoxarifadoIds)
        {
            using (var repository = new ItemTabelaPrecoRepository())
            {
                var resp = repository.GetItensTabelaPrecoComGradeView(tabelaPrecoId, catalogoIds, almoxarifadoIds).ToList();
                resp.RemoveAll(p => p.Inutilizado == 1);
                return resp;
            }
        }

        public IEnumerable<ItemTabelaPrecoView> GetItensTabelaPrecoComGradeEstoque(int tabelaPrecoId, List<int> catalogoIds, List<int> tamanhoIds, List<int> almoxarifadoIds)
        {
            using (var repository = new ItemTabelaPrecoRepository())
            {
                return repository.GetItensTabelaPrecoComGradeEstoque(tabelaPrecoId, catalogoIds, tamanhoIds, almoxarifadoIds);
            }
        }

        public IEnumerable<ItemTabelaPrecoView> GetItensTabelaPrecoComGradeOrdem(int tabelaPrecoId, List<int> catalogoIds, List<int> tamanhoIds, List<int> almoxarifadoIds)
        {
            using (var repository = new ItemTabelaPrecoRepository())
            {
                return repository.GetItensTabelaPrecoComGradeOrdem(tabelaPrecoId, catalogoIds, tamanhoIds, almoxarifadoIds);
            }
        }

        public IEnumerable<TamanhoView> GetItensTabelaPrecoTamanhos(int itemId, int corId, List<int> almoxarifadoIds)
        {
            using (var repository = new ItemTabelaPrecoRepository())
            {
                return repository.GetItensTabelaPrecoTamanhos(itemId, corId, almoxarifadoIds);
            }
        }

        public IEnumerable<TamanhoView> GetItensTabelaPrecoTamanhosOrdem(int itemId, int corId, List<int> almoxarifadoIds)
        {
            using (var repository = new ItemTabelaPrecoRepository())
            {
                return repository.GetItensTabelaPrecoTamanhosOrdem(itemId, corId, almoxarifadoIds);
            }
        }
    }
}
