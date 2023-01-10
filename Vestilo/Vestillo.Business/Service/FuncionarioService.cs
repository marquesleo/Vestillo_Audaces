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
    public class FuncionarioService: GenericService<Funcionario, FuncionarioRepository, FuncionarioController>
    {
        public FuncionarioService()
        {
            base.RequestUri = "Funcionario";
        }

        public new IFuncionarioService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new FuncionarioServiceWeb(this.RequestUri);
            }
            else
            {
                return new FuncionarioServiceAPP();
            }
        }   

    }
}
