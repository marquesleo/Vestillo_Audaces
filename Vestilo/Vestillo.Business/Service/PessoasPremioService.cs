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
    public class PessoasPremioService: GenericService<Premio, PremioRepository, PremioController>
    {
        public PessoasPremioService()
        {
            base.RequestUri = "PessoasPremio";
        }

        public new IPessoasPremioService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new PessoasPremioServiceWeb(this.RequestUri);
            }
            else
            {
                return new PessoasPremioServiceAPP();
            }
        }   
    }
}
