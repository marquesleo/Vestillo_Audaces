using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.APP;
using Vestillo.Business.Service.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Service
{
    public class GrupoPacoteService : GenericService<GrupoPacote, GrupoPacoteRepository, GrupoPacoteController>
    {
        public GrupoPacoteService()
        {
            base.RequestUri = "GrupoPacote";
        }

        public new IGrupoPacoteService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new GrupoPacoteServiceWeb(this.RequestUri);
            }
            else
            {
                return new GrupoPacoteServiceAPP();
            }
        }   
    }
}
