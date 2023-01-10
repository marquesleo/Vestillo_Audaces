
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class DestinosController : GenericController<Destinos, DestinosRepository>
    {
        public IEnumerable<Destinos> GetByAtivos(int AtivoInativo)
        {
            using (DestinosRepository repository = new DestinosRepository())
            {
                return repository.GetByAtivos(AtivoInativo);
            }
        }

        public IEnumerable<Destinos> GetListPorReferencia(string Abreviatura)
        {
            using (DestinosRepository repository = new DestinosRepository())
            {
                return repository.GetListPorReferencia(Abreviatura);
            }
        }

        public IEnumerable<Destinos> GetListPorDescricao(string desc)
        {
            using (DestinosRepository repository = new DestinosRepository())
            {
                return repository.GetListPorDescricao(desc);
            }
        }

        public IEnumerable<Destinos> GetListById(int id)
        {
            using (DestinosRepository repository = new DestinosRepository())
            {
                return repository.GetListById(id);
            }
        }

    }
}

