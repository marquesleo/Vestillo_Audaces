
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
    public class OperacaoPadraoService: GenericService<OperacaoPadrao, OperacaoPadraoRepository, OperacaoPadraoController>
    {
        public OperacaoPadraoService()
        {
            base.RequestUri = "OperacaoPadrao";
        }

        public new IOperacaoPadraoService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new OperacaoPadraoServiceWeb(this.RequestUri);
            }
            else
            {
                return new OperacaoPadraoServiceAPP();
            }
        }   

    }
}
