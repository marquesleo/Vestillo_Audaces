using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class MunicipioIbgeController : GenericController<MunicipioIbge, MunicipioIbgeRepository>
    {

        public IEnumerable<MunicipioIbge> GetAllWhere(String where)
        {
            using (MunicipioIbgeRepository repository = new MunicipioIbgeRepository())
            {
                return repository.GetAllWhere(where);
            }
        }
    }
}
