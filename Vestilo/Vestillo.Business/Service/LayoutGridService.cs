using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Controllers;
using Vestillo.Business.Service.Web;
using Vestillo.Business.Service.APP;

namespace Vestillo.Business.Service
{
    public class LayoutGridService : GenericService<LayoutGrid, LayoutGridRepository, LayoutGridController>
    {
        public LayoutGridService()
        {
            base.RequestUri = "LayoutGrid";
        }

        public new ILayoutGridService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new LayoutGridServiceWeb(this.RequestUri);
            }
            else
            {
                return new LayoutGridServiceAPP();       
            }
        }   
    }
}
