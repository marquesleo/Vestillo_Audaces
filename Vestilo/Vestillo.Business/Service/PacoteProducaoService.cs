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
using Vestillo.Business;
using Vestillo.Business.Service;

namespace Vestillo.Business.Service
{
    public class PacoteProducaoService: GenericService<PacoteProducao, PacoteProducaoRepository, PacoteProducaoController>
    {
        public PacoteProducaoService()
        {
            base.RequestUri = "PacoteProducao";
        }

        public new IPacoteProducaoService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new PacoteProducaoServiceWeb(this.RequestUri);
            }
            else
            {
                return new PacoteProducaoServiceAPP();
            }
        }   
    }
}
