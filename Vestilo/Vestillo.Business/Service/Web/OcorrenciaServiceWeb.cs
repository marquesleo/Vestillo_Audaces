using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Service.Web
{
    public class OcorrenciaServiceWeb: GenericServiceWeb<Ocorrencia, OcorrenciaRepository, OcorrenciaController>, IOcorrenciaService
    {

        public OcorrenciaServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }
    }
}
