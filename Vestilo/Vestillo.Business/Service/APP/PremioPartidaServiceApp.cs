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
    public class PremioPartidaServiceAPP : GenericServiceAPP<PremioPartida, PremioPartidaRepository, PremioPartidaController>, IPremioPartidaService
    {
        public PremioPartidaServiceAPP()
            : base(new PremioPartidaController())
        {
        }

        public PremioPartida GetByIdView(int id)
        {
            PremioPartidaController controller = new PremioPartidaController();
            return controller.GetByIdView(id);
        }

        public IEnumerable<PremioPartida> GetByDescricao(string descricao)
        {
            PremioPartidaController controller = new PremioPartidaController();
            return controller.GetByDescricao(descricao);
            
        }

        public IEnumerable<PremioPartida> GetByReferencia(string referencia)
        {
            PremioPartidaController controller = new PremioPartidaController();
            return controller.GetByReferencia(referencia);
            
        }
    }
}
