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
    public class AlmoxarifadoService : GenericService<Almoxarifado, AlmoxarifadoRepository, AlmoxarifadoController>
    {
        public AlmoxarifadoService()
        {
            base.RequestUri = "Almoxarifado";
        }

        public new IAlmoxarifadoService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new AlmoxarifadoServiceWeb(this.RequestUri);
            }
            else
            {
                return new AlmoxarifadoServiceAPP();
            }
        }   
    }
}