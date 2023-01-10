using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service
{
    public class MesDiasService: GenericService<MesDias, MesDiasRepository, MesDiasController>
    {
        public MesDiasService()
        {
            base.RequestUri = "MesDias";
        }

        public List<MesDias> GetByPremio(int premio)
        {
            MesDiasController controller = new MesDiasController();
            return controller.GetByPremio(premio);
        }
    }
}
