using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Service.APP
{
    public class ItemLiberacaoOrdemProducaoServiceAPP: GenericServiceAPP<ItemLiberacaoOrdemProducao, ItemLiberacaoOrdemProducaoRepository, ItemLiberacaoOrdemProducaoController>, IItemLiberacaoOrdemProducaoService
    {
        public ItemLiberacaoOrdemProducaoServiceAPP()
            : base(new ItemLiberacaoOrdemProducaoController())
        {

        }

        public void Save(List<ItemLiberacaoOrdemProducaoView> itensLiberados, List<OrdemProducaoMaterialView> ordemProducaoMateriais, OrdemProducao ordemProducao)
        {
            ItemLiberacaoOrdemProducaoController controller = new ItemLiberacaoOrdemProducaoController();
            controller.Save(itensLiberados, ordemProducaoMateriais, ordemProducao);
        }

        public void Finalizar(List<ItemLiberacaoOrdemProducaoView> itensLiberados, OrdemProducao ordemProducao)
        {
            ItemLiberacaoOrdemProducaoController controller = new ItemLiberacaoOrdemProducaoController();
            controller.Finalizar(itensLiberados, ordemProducao);
        }

        public IEnumerable<ItemLiberacaoOrdemProducaoView> GetByOrdemIdView(int ordemId)
        {
            ItemLiberacaoOrdemProducaoController controller = new ItemLiberacaoOrdemProducaoController();
            return controller.GetByOrdemIdView(ordemId);
        }
    }
}
