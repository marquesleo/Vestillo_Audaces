using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Service.APP
{
    public class PremioServiceAPP : GenericServiceAPP<Premio, PremioRepository, PremioController>, IPremioService
    {
        public PremioServiceAPP()
            : base(new PremioController())
        {
        }

        public Premio GetByIdView(int id)
        {
            PremioController controller = new PremioController();
            return controller.GetByIdView(id);
        }


        public IEnumerable<Premio> GetByDescricao(string descricao)
        {
            PremioController controller = new PremioController();
            return controller.GetByDescricao(descricao);
        }


        public IEnumerable<Premio> GetByReferencia(string referencia)
        {
            PremioController controller = new PremioController();
            return controller.GetByReferencia(referencia);
        }
    }
}
