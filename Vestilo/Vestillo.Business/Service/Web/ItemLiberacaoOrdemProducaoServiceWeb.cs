using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Service.Web
{
    public class ItemLiberacaoOrdemProducaoServiceWeb: GenericServiceWeb<ItemLiberacaoOrdemProducao, ItemLiberacaoOrdemProducaoRepository, ItemLiberacaoOrdemProducaoController>, IItemLiberacaoOrdemProducaoService
    {
        public ItemLiberacaoOrdemProducaoServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public void Save(List<ItemLiberacaoOrdemProducaoView> itensLiberados, List<OrdemProducaoMaterialView> ordemProducaoMateriais, OrdemProducao ordemProducao)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<ItemLiberacaoOrdemProducaoView> GetByOrdemIdView(int ordemId)
        {
            throw new NotImplementedException();
        }

        public void Finalizar(List<ItemLiberacaoOrdemProducaoView> itensLiberados, OrdemProducao ordemProducao)
        {
            throw new NotImplementedException();
        }
    }
}
