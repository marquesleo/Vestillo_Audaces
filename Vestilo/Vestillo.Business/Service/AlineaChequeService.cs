
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.APP;
using Vestillo.Business.Service.Web;

namespace Vestillo.Business.Service
{
    public class AlineaChequeService: GenericService<AlineaCheque, AlineaChequeRepository, AlineaChequeController>
    {
        public AlineaChequeService()
        {
            base.RequestUri = "AlineaCheque";
        }

        public new IAlineaChequeService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new AlineaChequeServiceWeb(this.RequestUri);
            }
            else
            {
                return new AlineaChequeServiceAPP();
            }
        }  
    }
}
