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
    public class ParametroServiceAPP: GenericServiceAPP<Parametro, ParametroRepository, ParametroController>, IParametroService
    {
        public ParametroServiceAPP()
            : base(new ParametroController())
        {
        }

        public Parametro GetByChave(string chave)
        {
            ParametroController controller = new ParametroController();
            return controller.GetByChave(chave);
        }

        public IEnumerable<Parametro> GetAllVisaoCliente()
        {

            ParametroController controller = new ParametroController();
            return controller.GetAllVisaoCliente();
        }
    }
}
