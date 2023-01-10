
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
    public class TransferenciaService : GenericService<Transferencia, TransferenciaRepository, TransferenciaController>
    {
        public TransferenciaService()
        {
            base.RequestUri = "Transferencia";
        }

        public new ITransferenciaService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new TransferenciaServiceWeb(this.RequestUri);
            }
            else
            {
                return new TransferenciaServiceAPP();
            }
        }

    }
}
