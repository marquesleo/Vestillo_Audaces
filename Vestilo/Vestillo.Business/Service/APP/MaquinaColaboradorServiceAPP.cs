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
    public class MaquinaColaboradorServiceAPP : GenericServiceAPP<MaquinaColaborador, MaquinaColaboradorRepository, MaquinaColaboradorController>, IMaquinaColaboradorService
    {
        public MaquinaColaboradorServiceAPP()
            : base(new MaquinaColaboradorController())
        {
        }

        public MaquinaColaboradorView GetByColaborador(int IdColaborador)
        {
            return controller.GetByColaborador(IdColaborador);
        }

        public IEnumerable<MaquinaColaboradorView> GetListByColaborador(int IdColaborador)
        {
            return controller.GetListByColaborador(IdColaborador);
        }
    }
}
