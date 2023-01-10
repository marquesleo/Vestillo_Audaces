
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
    public class InstrucoesPorBancosService : GenericService<InstrucoesPorBancos, InstrucoesPorBancosRepository, InstrucoesPorBancosController>
    {
        public InstrucoesPorBancosService()
        {
            base.RequestUri = "InstrucoesPorBancos";
        }

        public new IInstrucoesPorBancosService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new InstrucoesPorBancosServiceWeb(this.RequestUri);
            }
            else
            {
                return new InstrucoesPorBancosServiceAPP();
            }
        }

    }
}
