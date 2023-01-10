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
    public class MovimentosDaOperacaoService : GenericService<MovimentosDaOperacao, MovimentosDaOperacaoRepository, MovimentosDaOperacaoController>
    {
        public MovimentosDaOperacaoService()
        {
            base.RequestUri = "MovimentosDaOperacao";
        }

        public new IMovimentosDaOperacaoService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new MovimentosDaOperacaoServiceWeb(this.RequestUri);
            }
            else
            {
                return new MovimentosDaOperacaoServiceAPP();
            }
        }   

    }
}

