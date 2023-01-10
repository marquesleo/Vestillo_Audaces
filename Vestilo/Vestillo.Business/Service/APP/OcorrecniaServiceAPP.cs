using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Service.APP
{
    public class OcorrecniaServiceAPP : GenericServiceAPP<Ocorrencia, OcorrenciaRepository, OcorrenciaController>, IOcorrenciaService
    {
        public OcorrecniaServiceAPP()
            : base(new OcorrenciaController())
        {
        }

    }
}
