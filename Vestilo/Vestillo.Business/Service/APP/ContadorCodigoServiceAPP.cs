using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service.APP
{
    public class ContadorCodigoServiceAPP : GenericServiceAPP<ContadorCodigo, ContadorCodigoRepository, ContadorCodigoController>, IContadorCodigoService
    {

        public ContadorCodigoServiceAPP(): base (new ContadorCodigoController())
        {
        }

        public ContadorCodigo GetByNome(string nome)
        {
            return controller.GetByNome(nome);
        }
    }
}
