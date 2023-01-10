
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
    public class BalanceamentoSemanalService : GenericService<BalanceamentoSemanal, BalanceamentoSemanalRepository, BalanceamentoSemanalController>
    {
        public BalanceamentoSemanalService()
        {
            base.RequestUri = "BalanceamentoSemanal";
        }

        public new IBalanceamentoSemanalService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new BalanceamentoSemanalServiceWeb(this.RequestUri);
            }
            else
            {
                return new BalanceamentoSemanalServiceAPP();
            }
        }

    }
}
