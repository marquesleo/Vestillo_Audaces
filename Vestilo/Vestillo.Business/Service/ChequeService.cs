using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.Web;
using Vestillo.Business.Service.APP;

namespace Vestillo.Business.Service
{
    public class ChequeService : GenericService<Cheque, ChequeRepository, ChequeController>
    {
        public ChequeService()
        {
            base.RequestUri = "Cheque";
        }

        
        public new IChequeService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new ChequeServiceWeb(this.RequestUri);
            }
            else
            {
                return new ChequeServiceAPP();
            }
        }  
    }
}
