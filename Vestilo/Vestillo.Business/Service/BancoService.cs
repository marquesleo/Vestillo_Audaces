using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.APP;
using Vestillo.Business.Service.Interface;
using Vestillo.Business.Service.Web;

namespace Vestillo.Business.Service
{
    public class BancoService : GenericService<Banco, BancoRepository, BancoController>
    {
        public BancoService()
        {
            base.RequestUri = "Banco";
        }

        public new IBancoService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new BancoServiceWeb(this.RequestUri);
            }
            else
            {
                return new BancoServiceAPP();
            }
        }  
    }
}
