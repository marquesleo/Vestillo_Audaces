
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
    public class InstrucoesDosBoletosService : GenericService<InstrucoesDosBoletos, InstrucoesDosBoletosRepository, InstrucoesDosBoletosController>
    {
        public InstrucoesDosBoletosService()
        {
            base.RequestUri = "InstrucoesDosBoletos";
        }

        public new IInstrucoesDosBoletosService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new InstrucoesDosBoletosServiceWeb(this.RequestUri);
            }
            else
            {
                return new InstrucoesDosBoletosServiceAPP();
            }
        }
    }
}
