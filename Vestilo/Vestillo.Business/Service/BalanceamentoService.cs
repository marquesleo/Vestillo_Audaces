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
    public class BalanceamentoService: GenericService<Balanceamento, BalanceamentoRepository, BalanceamentoController>
    {
        public BalanceamentoService()
        {
            base.RequestUri = "Balanceamento";
        }

        public new IBalanceamentoService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new BalanceamentoServiceWeb(this.RequestUri);
            }
            else
            {
                return new BalanceamentoServiceAPP();
            }
        }  
    }
}
