using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.APP;
using Vestillo.Business.Service.Web;

namespace Vestillo.Business.Service
{
    public class TipoMaquinaService: GenericService<TipoMaquina, TipoMaquinaRepository, TipoMaquinaController>
    {
        public TipoMaquinaService()
        {
            base.RequestUri = "TipoMaquina";
        }

        public new ITipoMaquinaService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new TipoMaquinaServiceWeb(this.RequestUri);
            }
            else
            {
                return new TipoMaquinaServiceAPP();
            }
        }   
    }
}
