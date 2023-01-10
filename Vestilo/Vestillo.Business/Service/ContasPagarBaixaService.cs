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
    public class ContasPagarBaixaService : GenericService<ContasPagarBaixa, ContasPagarBaixaRepository, ContasPagarBaixaController>
    {
        public ContasPagarBaixaService()
        {
            base.RequestUri = "ContasPagarBaixa";
        }

        public new IContasPagarBaixaService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new ContasPagarBaixaServiceWeb(this.RequestUri);
            }
            else
            {
                return new ContasPagarBaixaServiceAPP();
            }
        }
    }
}
