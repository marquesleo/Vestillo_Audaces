using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service;
using Vestillo.Business.Service.APP;
using Vestillo.Business.Service.Web;

namespace Vestillo.Business.Service
{
    public class OrdemProducaoService : GenericService<OrdemProducao, OrdemProducaoRepository, OrdemProducaoController>
    {
        public OrdemProducaoService()
        {
            base.RequestUri = "OrdemProducao";
        }

        public new IOrdemProducaoService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new OrdemProducaoServiceWeb(this.RequestUri);
            }
            else
            {
                return new OrdemProducaoServiceAPP();
            }
        }   
    }
}

