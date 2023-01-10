using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class ParametroController : GenericController<Parametro, ParametroRepository>
    {
        public Parametro GetByChave(string chave)
        {
            using (ParametroRepository repository = new ParametroRepository())
            {
                return repository.GetByChave(chave);
            }
        }

        public IEnumerable<Parametro> GetAllVisaoCliente()
        {

            using (ParametroRepository repository = new ParametroRepository())
            {
                return repository.GetAllVisaoCliente();
            }
        }


    }
}
