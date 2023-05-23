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
    public class ParametroService : GenericService<Parametro, ParametroRepository, ParametroController>
    {
        public ParametroService()
        {
            base.RequestUri = "Parametro";
        }

        public new IParametroService GetServiceFactory()
        {
            if (!Vestillo.Connection.ProviderFactory.IsAPI)
            {
                if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
                {
                    return new ParametroServiceWeb(this.RequestUri);
                }
                else
                {
                    return new ParametroServiceAPP();
                }
            }
            else
            {
                return new ParametroServiceAPP();
            }
        }
    }
}