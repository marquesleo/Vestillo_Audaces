using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Service
{
    public interface IItemLiberacaoOrdemProducaoService : IService<ItemLiberacaoOrdemProducao, ItemLiberacaoOrdemProducaoRepository, ItemLiberacaoOrdemProducaoController>
    {
        void Save(List<ItemLiberacaoOrdemProducaoView> itensLiberados, List<OrdemProducaoMaterialView> ordemProducaoMateriais, OrdemProducao ordemProducao);
        void Finalizar(List<ItemLiberacaoOrdemProducaoView> itensLiberados, OrdemProducao ordemProducao);
        IEnumerable<ItemLiberacaoOrdemProducaoView> GetByOrdemIdView(int ordemId);
    }
}
