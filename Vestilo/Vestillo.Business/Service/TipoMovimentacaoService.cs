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
    public class TipoMovimentacaoService: GenericService<TipoMovimentacao, TipoMovimentacaoRepository, TipoMovimentacaoController>
    {
        public TipoMovimentacaoService()
        {
            base.RequestUri = "TipoMovimentacao";
        }

        public new ITipoMovimentacaoService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new TipoMovimentacaoServiceWeb(this.RequestUri);
            }
            else
            {
                return new TipoMovimentacaoServiceAPP();
            }
        }   

    }
}
