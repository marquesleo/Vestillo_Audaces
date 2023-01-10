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
    public class FichaTecnicaOperacaoMovimentoService : GenericService<FichaTecnicaOperacaoMovimento, FichaTecnicaOperacaoMovimentoRepository, FichaTecnicaOperacaoMovimentoController>
    {
        public FichaTecnicaOperacaoMovimentoService()
        {
            base.RequestUri = "FichaTecnicaOperacaoMovimento";
        }

        public new IFichaTecnicaOperacaoMovimentoService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new FichaTecnicaOperacaoMovimentoServiceWeb(this.RequestUri);
            }
            else
            {
                return new FichaTecnicaOperacaoMovimentoServiceAPP();
            }
        }
    }
}
