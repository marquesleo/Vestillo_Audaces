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
    public class PlanoContasNaturezasFinanceirasService: GenericService<PlanoContasNaturezasFinanceiras, PlanoContasNaturezasFinanceirasRepository, PlanoContasNaturezasFinanceirasController>
    {
        public PlanoContasNaturezasFinanceirasService()
        {
            base.RequestUri = "PlanoContasNaturezasFinanceiras";
        }

        public new IPlanoContasNaturezasFinanceirasService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new PlanoContasNaturezasFinanceirasServiceWeb(this.RequestUri);
            }
            else
            {
                return new PlanoContasNaturezasFinanceirasServiceAPP();
            }
        }  
    }
}
