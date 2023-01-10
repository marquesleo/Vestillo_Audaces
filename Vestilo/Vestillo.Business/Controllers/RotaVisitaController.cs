using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class RotaVisitaController : GenericController<RotaVisita, RotaVisitaRepository>
    {
        public IEnumerable<RotaVisita> GetPorReferencia(string referencia)
        {
            using (RotaVisitaRepository repository = new RotaVisitaRepository())
            {
                return repository.GetPorReferencia(referencia);
            }
        }

        public IEnumerable<RotaVisita> GetPorDescricao(string desc)
        {
            using (RotaVisitaRepository repository = new RotaVisitaRepository())
            {
                return repository.GetPorDescricao(desc);
            }
        }

        public IEnumerable<RotaVisita> GetByIdList(int id)
        {
            using (RotaVisitaRepository repository = new RotaVisitaRepository())
            {
                return repository.GetByIdList(id);
            }
        }

    }
}
