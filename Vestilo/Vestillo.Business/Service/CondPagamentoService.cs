using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Controllers;
using Vestillo.Business.Service.Interface;
using Vestillo.Business.Service.Web;
using Vestillo.Business.Service.APP;

namespace Vestillo.Business.Service
{
    public class CondPagamentoService : GenericService<CondPagamento, CondPagamentoRepository, CondPagamentoController>
    {
        public CondPagamentoService()
        {
            base.RequestUri = "CondPagamento";
        }

        public new ICondPagamentoService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new CondPagamentoServiceWeb(this.RequestUri);
            }
            else
            {
                return new CondPagamentoServiceAPP();
            }
        }  
    }
}
