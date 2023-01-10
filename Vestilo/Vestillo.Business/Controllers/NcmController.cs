using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class NcmController : GenericController<Ncm, NcmRepository>
    {

        public Ncm GetByReferencia(String referencia)
        {
            using (NcmRepository repository = new NcmRepository())
            {
                return repository.GetByReferencia(referencia);
            }
        }
    }
}
