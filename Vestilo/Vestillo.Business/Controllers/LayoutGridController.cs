using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class LayoutGridController : GenericController<LayoutGrid , LayoutGridRepository>
    {
        public LayoutGrid GetByLayout(int formId, int usuarioId)
        {
            using (LayoutGridRepository repository = new LayoutGridRepository())
            {
                return repository.GetByLayout(formId, usuarioId);
            }
        }

        public IEnumerable<LayoutGrid> GetListByUsuarioId(int usuarioId)
        {
            using (LayoutGridRepository repository = new LayoutGridRepository())
            {
                return repository.GetListByUsuarioId(usuarioId);
            }
        }
    }
}
