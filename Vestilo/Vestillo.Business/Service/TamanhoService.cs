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
    public class TamanhoService: GenericService<Tamanho, TamanhoRepository, TamanhoController>
    {
        public TamanhoService()
        {
            base.RequestUri = "Tamanho";
        }

        public new ITamanhoService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new TamanhoServiceWeb(this.RequestUri);
            }
            else
            {
                return new TamanhoServiceAPP();
            }
        }   

    }
}
