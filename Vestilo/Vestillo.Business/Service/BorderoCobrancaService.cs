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
    public class BorderoCobrancaService: GenericService<BorderoCobranca, BorderoCobrancaRepository, BorderoCobrancaController>
    {
        public BorderoCobrancaService()
        {
            base.RequestUri = "BorderoCobranca";
        }

        public new IBorderoCobrancaService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new BorderoCobrancaServiceWeb(this.RequestUri);
            }
            else
            {
                return new BorderoCobrancaServiceAPP();
            }
        }  
    }
}
