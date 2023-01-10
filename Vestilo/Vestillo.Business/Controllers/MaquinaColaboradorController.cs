using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class MaquinaColaboradorController : GenericController<MaquinaColaborador, MaquinaColaboradorRepository>
    {
        public MaquinaColaboradorView GetByColaborador(int IdColaborador)
        {
            using (MaquinaColaboradorRepository repository = new MaquinaColaboradorRepository())
            {
                return repository.GetByColaborador(IdColaborador);
            }
        }

        public IEnumerable<MaquinaColaboradorView> GetListByColaborador(int IdColaborador)
        {
            using (MaquinaColaboradorRepository repository = new MaquinaColaboradorRepository())
            {
                return repository.GetListByColaborador(IdColaborador);
            }
        }
    }
}
