using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.Web;
using Vestillo.Business.Service.APP;

namespace Vestillo.Business.Service
{
    public class MaquinaColaboradorService : GenericService<MaquinaColaborador, MaquinaColaboradorRepository, MaquinaColaboradorController>
    {
        public MaquinaColaboradorService()
        {
            base.RequestUri = "MaquinaColaborador";
        }

        public new IMaquinaColaboradorService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new MaquinaColaboradorServiceWeb(this.RequestUri);
            }
            else
            {
                return new MaquinaColaboradorServiceAPP();
            }
        }  
    }
}
