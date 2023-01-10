
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.APP;
using Vestillo.Business.Service.Interface;
using Vestillo.Business.Service.Web;

namespace Vestillo.Business.Service
{
    public class NotaEntradaFaccaoItensService : GenericService<NotaEntradaFaccaoItens, NotaEntradaFaccaoItensRepository, NotaEntradaItensFaccaoController>
    {
        public NotaEntradaFaccaoItensService()
        {
            base.RequestUri = "notaentradafaccaoitens";
        }

        public new INotaEntradaFaccaoItensService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new NotaEntradaFaccaoItensServiceWeb(this.RequestUri);
            }
            else
            {
                return new NotaEntradaFaccaoItensServiceAPP();
            }
        }
    }
}
