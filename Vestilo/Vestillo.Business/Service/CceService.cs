
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
    public class CceService: GenericService<Cce, CceRepository, CceController>
    {
        public CceService()
        {
            base.RequestUri = "Cce";
        }

        public new ICceService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new CceServiceWeb(this.RequestUri);
            }
            else
            {
                return new CceServiceAPP();
            }
        }  
    }
}
