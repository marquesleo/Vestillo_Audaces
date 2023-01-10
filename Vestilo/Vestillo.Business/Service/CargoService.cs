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
    public class CargoService: GenericService<Cargo, CargoRepository, CargoController>
    {
        public CargoService()
        {
            base.RequestUri = "Cargo";
        }

        public new ICargoService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new CargoServiceWeb(this.RequestUri);
            }
            else
            {
                return new CargoServiceAPP();
            }
        }   

    }
}
