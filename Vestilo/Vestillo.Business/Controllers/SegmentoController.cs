using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class SegmentoController : GenericController<Segmento, SegmentoRepository>
    {
        public IEnumerable<Segmento> GetListPorDescricao(string Descricao)
        {
            using (SegmentoRepository repository = new SegmentoRepository())
            {
                return repository.GetListPorDescricao(Descricao);
            }
        }
    }
}
