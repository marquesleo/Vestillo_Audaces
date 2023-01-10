using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Controllers;
using Vestillo.Business.Service.Web;
using Vestillo.Business.Service.APP;

namespace Vestillo.Business.Service
{
    public class TabelaPrecoService : GenericService<TabelaPreco, TabelaPrecoRepository, TabelaPrecoController>
    {
        public TabelaPrecoService()
        {
            base.RequestUri = "TabelaPreco";
        }

        public new ITabelaPrecoService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new TabelaPrecoServiceWeb(this.RequestUri);
            }
            else
            {
                return new TabelaPrecoServiceAPP();
            }
        }   
    }
}