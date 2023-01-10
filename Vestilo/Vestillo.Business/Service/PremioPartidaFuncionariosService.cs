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
    public class PremioPartidaFuncionariosService : GenericService<PremioPartidaFuncionarios, PremioPartidaFuncionariosRepository, PremioPartidaFuncionariosController>
    {
        public PremioPartidaFuncionariosService()
        {
            base.RequestUri = "PremioPartidaFuncionarios";
        }

        public new IPremioPartidaFuncionariosService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new PremioPartidaFuncionariosServiceWeb(this.RequestUri);
            }
            else
            {
                return new PremioPartidaFuncionariosServiceAPP();
            }
        }
    }
}
