

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
    public class ControlaEmailCobrancaService : GenericService<ControlaEmailCobranca, ControlaEmailCobrancaRepository, ControlaEmailCobrancaController>
    {
        public ControlaEmailCobrancaService()
        {
            base.RequestUri = "ControlaEmailCobranca";
        }

        public new IControlaEmailCobrancaService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new ControlaEmailCobrancaServiceWeb(this.RequestUri);
            }
            else
            {
                return new ControlaEmailCobrancaServiceAPP();
            }
        }
    }
}
