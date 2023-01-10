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
    public class FormulariosService : GenericService<Formularios, FormulariosRepository, FormulariosController>
    {
        public FormulariosService()
        {
            base.RequestUri = "formularios";
        }

        public new IFormulariosService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new FormulariosServiceWeb(this.RequestUri);
            }
            else
            {
                return new FormulariosServiceAPP();
            }
        }
    }
}
