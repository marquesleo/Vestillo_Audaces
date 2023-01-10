
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
    public class GruposProducaoService: GenericService<GruposProducao,GruposProducaoRepository, GruposProducaoController>
    {
        public GruposProducaoService()
        {
            base.RequestUri = "GruposProducao";
        }

        public new IGruposProducaoService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new GruposProducaoServiceWeb(this.RequestUri);
            }
            else
            {
                return new GruposProducaoServiceAPP();
            }
        }   

    }
}
