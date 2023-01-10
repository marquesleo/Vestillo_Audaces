using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repository;
using Vestillo.Business.Service.APP;
using Vestillo.Business.Service.Web;

namespace Vestillo.Business.Service
{
    public class ParcelaPadraoClienteService : GenericService<ParcelaPadraoCliente, ParcelaPadraoClienteRepository, ParcelaPadraoClienteController>
    {
        public ParcelaPadraoClienteService()
        {
            base.RequestUri = "ParcelaPadraoCliente";
        }

        public new IParcelaPadraoClienteService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new ParcelaPadraoClienteServiceWEB(this.RequestUri);
            }
            else
            {
                return new ParcelaPadraoClienteServiceAPP();
            }
        }
    }
}
