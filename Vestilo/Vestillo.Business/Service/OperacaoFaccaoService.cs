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

namespace Vestillo.Business.Service
{
    public class OperacaoFaccaoService: GenericService<OperacaoFaccao, OperacaoFaccaoRepository, OperacaoFaccaoController>
    {
        public OperacaoFaccaoService()
        {
            base.RequestUri = "OperacaoFaccao";
        }

        public new IOperacaoFaccaoService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new OperacaoFaccaoServiceWeb(this.RequestUri);
            }
            else
            {
                return new OperacaoFaccaoServiceAPP();
            }
        }   
    }
}
