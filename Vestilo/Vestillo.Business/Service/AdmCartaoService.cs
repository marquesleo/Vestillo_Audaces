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
    public class AdmCartaoService : GenericService<AdmCartao, AdmCartaoRepository, AdmCartaoController>
    {
        public AdmCartaoService()
        {
            base.RequestUri = "AdmCartao";
        }

        public new IAdmCartaoService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new AdmCartaoServiceWeb(this.RequestUri);
            }
            else
            {
                return new AdmCartaoServiceAPP();
            }
        }   
    }
}