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
    public class BoletosGeradosService : GenericService<BoletosGerados, BoletosGeradosRepository, BoletosGeradosController>
    {
        public BoletosGeradosService()
        {
            base.RequestUri = "BoletosGerados";
        }

        public new IBoletosGeradosService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new BoletosGeradosServiceWeb(this.RequestUri);
            }
            else
            {
                return new BoletosGeradosServiceAPP();
            }
        }

    }
}
