
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class FormulariosController: GenericController<Formularios, FormulariosRepository>
    {

        public IEnumerable<Formularios> GetBuscarPeloTipo(int Tipo)
        {
            using (FormulariosRepository repository = new FormulariosRepository())
            {
                return repository.GetBuscarPeloTipo(Tipo);
            }
        }

    }   

    
}
