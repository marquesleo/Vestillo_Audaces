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
    public class ContasReceberBaixaService : GenericService<ContasReceberBaixa, ContasReceberBaixaRepository, ContasReceberBaixaController>
    {
        public ContasReceberBaixaService()
        {
            base.RequestUri = "ContasReceberBaixa";
        }

        public new IContasReceberBaixaService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new ContasReceberBaixaServiceWeb(this.RequestUri);
            }
            else
            {
                return new ContasReceberBaixaServiceAPP();
            }
        }
    }
}
