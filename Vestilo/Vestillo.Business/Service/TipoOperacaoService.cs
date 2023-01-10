
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
    public class TipoOperacaoService: GenericService<TipoOperacao, TipoOperacaoRepository, TipoOperacaoController>
    {
        public TipoOperacaoService()
        {
            base.RequestUri = "TipoOperacao";
        }

        public new ITipoOperacaoService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new TipoOperacaoServiceWeb(this.RequestUri);
            }
            else
            {
                return new TipoOperacaoServiceAPP();
            }
        }   
    }
}
