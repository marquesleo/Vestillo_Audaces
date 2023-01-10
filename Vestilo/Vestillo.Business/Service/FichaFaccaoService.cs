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
    public class FichaFaccaoService : GenericService<FichaFaccao, FichaFaccaoRepository, FichaFaccaoController>
    {
        public FichaFaccaoService()
        {
            base.RequestUri = "FichaFaccao";
        }

        public new IFichaFaccaoService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new FichaFaccaoServiceWeb(this.RequestUri);
            }
            else
            {
                return new FichaFaccaoServiceApp();
            }
        }

    }
}
