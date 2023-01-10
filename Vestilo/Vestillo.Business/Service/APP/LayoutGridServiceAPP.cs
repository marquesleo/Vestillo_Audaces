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
    public class LayoutGridServiceAPP : GenericServiceAPP<LayoutGrid , LayoutGridRepository, LayoutGridController>, ILayoutGridService
    {

        public LayoutGridServiceAPP() : base(new LayoutGridController())
        {
        }

        public LayoutGrid GetByLayout(int formId, int usuarioId)
        {
            return controller.GetByLayout(formId, usuarioId);
        }

        public IEnumerable<LayoutGrid> GetListByUsuarioId(int usuarioId)
        {
            return controller.GetListByUsuarioId(usuarioId);
        }
    }
}
