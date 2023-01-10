using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class TipoMaquinaController : GenericController<TipoMaquina, TipoMaquinaRepository>
    {
        public IEnumerable<TipoMaquina> GetByAtivos(int AtivoInativo)
        {
            using (TipoMaquinaRepository repository = new TipoMaquinaRepository())
            {
                return repository.GetByAtivos(AtivoInativo);
            }
        }

    }
}
