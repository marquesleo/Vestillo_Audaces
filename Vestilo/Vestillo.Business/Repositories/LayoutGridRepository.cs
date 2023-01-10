using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class LayoutGridRepository : GenericRepository<LayoutGrid>
    {
        public LayoutGridRepository()  : base(new DapperConnection<LayoutGrid>())
        {
        }

        public LayoutGrid GetByLayout(int formId, int usuarioId)
        {
            var Layout = new LayoutGrid();
            _cn.ExecuteToModel("FormId = " + formId.ToString()  + " AND usuarioId = " + usuarioId.ToString() , ref Layout );
            return Layout;
        }

        public IEnumerable<LayoutGrid> GetListByUsuarioId(int usuarioId)
        {
            return _cn.ExecuteToList(new LayoutGrid(), "usuarioId = " + usuarioId);
        }
    }
}
