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
    public class TempoFuncionarioService: GenericService<TempoFuncionario, TempoFuncionarioRepository, TempoFuncionarioController>
    {
        public TempoFuncionarioService()
        {
            base.RequestUri = "TempoFuncionario";
        }

        public new ITempoFuncionarioService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new TempoFuncionarioServiceWeb(this.RequestUri);
            }
            else
            {
                return new TempoFuncionarioServiceApp();
            }
        }   
    }
}
