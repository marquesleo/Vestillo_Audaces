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
    public class OperacaoOperadoraService: GenericService<OperacaoOperadora, OperacaoOperadoraRepository, OperacaoOperadoraController>
    {
        public OperacaoOperadoraService()
        {
            base.RequestUri = "OperacaoOperadora";
        }

        public new IOperacaoOperadoraService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new OperacaoOperadoraServiceWeb(this.RequestUri);
            }
            else
            {
                return new OperacaoOperadoraServiceAPP();
            }
        }   
    }
}
