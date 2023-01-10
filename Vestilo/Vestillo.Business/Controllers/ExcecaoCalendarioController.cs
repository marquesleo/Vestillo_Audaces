
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class ExcecaoCalendarioController : GenericController<ExcecaoCalendario, ExcecaoCalendarioRepository>
    {
        public IEnumerable<ExcecaoCalendario> GetPorReferencia(string referencia)
        {
            using (ExcecaoCalendarioRepository repository = new ExcecaoCalendarioRepository())
            {
                return repository.GetPorReferencia(referencia);
            }
        }

        public IEnumerable<ExcecaoCalendario> GetPorDescricao(string desc)
        {
            using (ExcecaoCalendarioRepository repository = new ExcecaoCalendarioRepository())
            {
                return repository.GetPorDescricao(desc);
            }
        }

        public ExcecaoCalendario GetByDataExcecao(int CalendarioId, DateTime data)
        {
            using (ExcecaoCalendarioRepository repository = new ExcecaoCalendarioRepository())
            {
                return repository.GetByDataExcecao(CalendarioId, data);
            }
        }

    }
}
