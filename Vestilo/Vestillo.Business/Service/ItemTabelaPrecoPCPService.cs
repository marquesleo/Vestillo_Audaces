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
    public class ItemTabelaPrecoPCPService: GenericService<ItemTabelaPrecoPCP, ItemTabelaPrecoPCPRepository, ItemTabelaPrecoPCPController>
    {
        public ItemTabelaPrecoPCPService()
        {
            base.RequestUri = "ItemTabelaPrecoPCP";
        }

        public new IItemTabelaPrecoPCPService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new ItemTabelaPrecoPCPServiceWeb(this.RequestUri);
            }
            else
            {
                return new ItemTabelaPrecoPCPServiceAPP();
            }
        }   
    }
}
