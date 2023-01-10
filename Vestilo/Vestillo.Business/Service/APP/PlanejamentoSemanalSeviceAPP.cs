
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.Interface;

namespace Vestillo.Business.Service.APP
{
    public class PlanejamentoSemanalSeviceAPP : GenericServiceAPP<PlanejamentoSemanal, PlanejamentoSemanalRepository, PlanejamentoSemanalController>, IPlanejamentoSemanalService
    {
        public PlanejamentoSemanalSeviceAPP() : base(new PlanejamentoSemanalController())
        {

        }

        public IEnumerable<PlanejamentoSemanal> GetByRefView(string referencia)
        {
            PlanejamentoSemanalController controller = new PlanejamentoSemanalController();
            return controller.GetByRefView(referencia);
        }
    }
}



