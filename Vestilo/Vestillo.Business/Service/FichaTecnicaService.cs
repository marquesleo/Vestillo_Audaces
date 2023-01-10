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
    public class FichaTecnicaService: GenericService<FichaTecnica, FichaTecnicaRepository, FichaTecnicaController>
    {
        public FichaTecnicaService()
        {
            base.RequestUri = "FichaTecnica";
        }

        public new IFichaTecnicaService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new FichaTecnicaServiceWeb(this.RequestUri);
            }
            else
            {
                return new FichaTecnicaServiceAPP();
            }
        }   

    }
}
