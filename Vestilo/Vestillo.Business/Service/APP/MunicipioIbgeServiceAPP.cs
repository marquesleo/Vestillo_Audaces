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
    class MunicipioIbgeServiceAPP: GenericServiceAPP<MunicipioIbge, MunicipioIbgeRepository, MunicipioIbgeController>, IMunicipioService
    {
        public MunicipioIbgeServiceAPP()
            : base(new MunicipioIbgeController())
        {
        }

        public IEnumerable<MunicipioIbge> GetAllWhere(string where)
        {
            MunicipioIbgeController controller = new MunicipioIbgeController();
            return controller.GetAllWhere(where);
        }
    }
}
