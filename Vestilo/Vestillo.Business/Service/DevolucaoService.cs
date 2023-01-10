
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
    public class DevolucaoService: GenericService<Devolucao, DevolucaoRepository, DevolucaoController>
    {
        public DevolucaoService()
        {
            base.RequestUri = "Devolucao";
        }

        public new IDevolucaoService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new DevolucaoServiceWeb(this.RequestUri);
            }
            else
            {
                return new DevolucaoServiceAPP();
            }
        }   

    }
}
