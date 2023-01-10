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
    public class PlanoContasService: GenericService<PlanoContas, PlanoContasRepository, PlanoContasController>
    {
        public PlanoContasService()
        {
            base.RequestUri = "PlanoContas";
        }

        public new IPlanoContasService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new PlanoContasServiceWeb(this.RequestUri);
            }
            else
            {
                return new PlanoContasServiceAPP();
            }
        }  
    }
}
