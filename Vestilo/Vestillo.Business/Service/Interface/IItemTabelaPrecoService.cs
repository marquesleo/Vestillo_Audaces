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
    public interface IItemTabelaPrecoService: IService<ItemTabelaPreco, ItemTabelaPrecoRepository, ItemTabelaPrecoController>
    {
        IEnumerable<ItemTabelaPrecoView> GetItensTabelaPrecoView(int tabelaPrecoId);
        ItemTabelaPrecoView GetItemTabelaPrecoView(int tabelaPrecoId, int produtoId);
        IEnumerable<ItemTabelaPrecoView> GetItensTabelaPrecoComGradeView(int tabelaPrecoId, int catalogoId);
        IEnumerable<ItemTabelaPrecoView> GetItensTabelaPrecoComGradeView(int tabelaPrecoId, List<int> catalogoIds, List<int> almoxarifadoIds);
        IEnumerable<ItemTabelaPrecoView> GetItensTabelaPrecoComGradeEstoque(int tabelaPrecoId, List<int> catalogoIds, List<int>tamanhoIds, List<int> almoxarifadoIds);
        IEnumerable<ItemTabelaPrecoView> GetItensTabelaPrecoComGradeOrdem(int tabelaPrecoId, List<int> catalogoIds, List<int> tamanhoIds, List<int> almoxarifadoIds);
        IEnumerable<TamanhoView> GetItensTabelaPrecoTamanhos(int itemId, int corId, List<int> almoxarifadoIds);
        IEnumerable<TamanhoView> GetItensTabelaPrecoTamanhosOrdem(int itemId, int corId, List<int> almoxarifadoIds);
    }
}
