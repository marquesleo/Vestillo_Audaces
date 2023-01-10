using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.APP;
using Vestillo.Business.Service.Interface;
using Vestillo.Business.Service.Web;

namespace Vestillo.Business.Service
{
    public class PlanejamentoSemanalService : GenericService<PlanejamentoSemanal, PlanejamentoSemanalRepository, PlanejamentoSemanalController>
    {
        public PlanejamentoSemanalService()
        {
            base.RequestUri = "Planejamento";
        }

        public new IPlanejamentoSemanalService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new PlanejamentoSemanalServiceWeb(this.RequestUri);
            }
            else
            {
                return new PlanejamentoSemanalSeviceAPP();
            }
        }
    }
}
