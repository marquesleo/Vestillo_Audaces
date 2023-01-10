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
    public class ItemOrdemProducaoService : GenericService<ItemOrdemProducao, ItemOrdemProducaoRepository, ItemOrdemProducaoController>
    {
        public ItemOrdemProducaoService()
        {
            base.RequestUri = "ItemOrdemProducao";
        }

        public new IItemOrdemProducaoService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new ItemOrdemProducaoServiceWeb(this.RequestUri);
            }
            else
            {
                return new ItemOrdemProducaoServiceAPP();
            }
        }
    }
}
