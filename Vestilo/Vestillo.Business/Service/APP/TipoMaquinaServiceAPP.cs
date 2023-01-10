
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
    public class TipoMaquinaServiceAPP : GenericServiceAPP<TipoMaquina, TipoMaquinaRepository, TipoMaquinaController>, ITipoMaquinaService
    {

        public TipoMaquinaServiceAPP() : base(new TipoMaquinaController())
        {
        }

        public IEnumerable<TipoMaquina> GetByAtivos(int AtivoInativo)
        {
            return controller.GetByAtivos(AtivoInativo);
        }
    }
}



