using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Controllers;
using Vestillo.Business.Service.APP;
using Vestillo.Business.Service.Web;

namespace Vestillo.Business.Service
{
    public class ItemTabelaPrecoService: GenericService<ItemTabelaPreco, ItemTabelaPrecoRepository, ItemTabelaPrecoController>
    {
        public ItemTabelaPrecoService()
        {
            base.RequestUri = "ItemTabelaPreco";
        }

        public new IItemTabelaPrecoService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new ItemTabelaPrecoServiceWeb(this.RequestUri);
            }
            else
            {
                return new ItemTabelaPrecoServiceAPP();
            }
        }   
    }
}
