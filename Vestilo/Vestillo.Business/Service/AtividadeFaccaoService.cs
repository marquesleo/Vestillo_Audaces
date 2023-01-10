
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
    public class AtividadeFaccaoService : GenericService<AtividadeFaccao, AtividadeFaccaoRepository, AtividadeFaccaoController>
    {
        public AtividadeFaccaoService()
        {
            base.RequestUri = "AtividadeFaccao";
        }

        public new IAtividadeFaccaoService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new AtividadeFaccaoServiceWeb(this.RequestUri);
            }
            else
            {
                return new AtividadeFaccaoServiceAPP();
            }
        }

    }
}
