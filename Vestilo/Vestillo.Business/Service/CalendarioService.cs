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
    public class CalendarioService: GenericService<Calendario, CalendarioRepository, CalendarioController>
    {
        public CalendarioService()
        {
            base.RequestUri = "Calendario";
        }

        public new ICalendarioService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new CalendarioServiceWeb(this.RequestUri);
            }
            else
            {
                return new CalendarioServiceAPP();
            }
        }   

    }
}
