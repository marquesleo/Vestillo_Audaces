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
    public class GrupoOperacoesService: GenericService<GrupoOperacoes, GrupoOperacoesRepository, GrupoOperacoesController>
    {
        public GrupoOperacoesService()
        {
            base.RequestUri = "GrupoOperacoes";
        }

        public new IGrupoOperacoesService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new GrupoOperacoesServiceWeb(this.RequestUri);
            }
            else
            {
                return new GrupoOperacoesServiceAPP();
            }
        }   
    }
}
