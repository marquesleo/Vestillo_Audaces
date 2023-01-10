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
    public class TabelaPrecoPCPService : GenericService<TabelaPrecoPCP, TabelaPrecoPCPRepository, TabelaPrecoPCPController>
    {
        public TabelaPrecoPCPService()
        {
            base.RequestUri = "TabelaPreco";
        }

        public new ITabelaPrecoPCPService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new TabelaPrecoPCPServiceWeb(this.RequestUri);
            }
            else
            {
                return new TabelaPrecoPCPServiceAPP();
            }
        }   
    }
}