using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.APP;
using Vestillo.Business.Service.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Service
{
    public class ItemLiberacaoOrdemProducaoService: GenericService<ItemLiberacaoOrdemProducao, ItemLiberacaoOrdemProducaoRepository, ItemLiberacaoOrdemProducaoController>
    {
        public ItemLiberacaoOrdemProducaoService()
        {
            base.RequestUri = "ItemLiberacaoOrdemProducao";
        }

        public new IItemLiberacaoOrdemProducaoService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new ItemLiberacaoOrdemProducaoServiceWeb(this.RequestUri);
            }
            else
            {
                return new ItemLiberacaoOrdemProducaoServiceAPP();
            }
        }
    }
}
