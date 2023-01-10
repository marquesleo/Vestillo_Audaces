using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.APP;
using Vestillo.Business.Service.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Service
{
    public class OcorrenciaService : GenericService<Ocorrencia, OcorrenciaRepository, OcorrenciaController>
    {
        public OcorrenciaService()
        {
            base.RequestUri = "Ocorrencia";
        }

        public new IOcorrenciaService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new OcorrenciaServiceWeb(this.RequestUri);
            }
            else
            {
                return new OcorrecniaServiceAPP();
            }
        }   
    }
}
