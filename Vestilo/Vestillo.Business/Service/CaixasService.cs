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
    public class CaixasService : GenericService<Caixas, CaixasRepository, CaixasController>
    {
        public CaixasService()
        {
            base.RequestUri = "Caixas";
        }

        public new ICaixasService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new CaixasServiceWeb(this.RequestUri);
            }
            else
            {
                return new CaixasServiceAPP();
            }
        }   
    }
}

