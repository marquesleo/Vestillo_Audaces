
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
    public class ContadorNossoNumeroServiceAPP : GenericServiceAPP<ContadorNossoNumero, ContadorNossoNumeroRepository, ContadorNossoNumeroController>, IContadorNossoNumeroService
    {

        public ContadorNossoNumeroServiceAPP() : base(new ContadorNossoNumeroController())
        {
        }

        public string GetProximo(int IdBanco)
        {
            return controller.GetProximo(IdBanco);
        }

        public ContadorNossoNumero GetByBanco(int IdBanco)
        {
            return controller.GetByBanco(IdBanco);
        }
    }
}
