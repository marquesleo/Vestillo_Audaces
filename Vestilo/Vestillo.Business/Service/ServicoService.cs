using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.Interface;
using Vestillo.Business.Service.Web;
using Vestillo.Business.Service.APP;

namespace Vestillo.Business.Service
{
    public class ServicoService : GenericService<Servico, ServicoRepository, ServicoController>
    {
        public ServicoService()
        {
            base.RequestUri = "Servico";
        }

        public new IServicoService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new ServicoServiceWeb(this.RequestUri);
            }
            else
            {
                return new ServicoServiceAPP();
            }
        }  
    }
}
