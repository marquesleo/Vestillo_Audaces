
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
    public class FormulariosServiceAPP : GenericServiceAPP<Formularios, FormulariosRepository, FormulariosController>, IFormulariosService
    {

        public FormulariosServiceAPP() : base(new FormulariosController())
        {
        }

        public IEnumerable<Formularios> GetBuscarPeloTipo(int Tipo)
        {
            return controller.GetBuscarPeloTipo(Tipo);
        }

    }
}



