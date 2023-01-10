using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class PermissaoGrupoRepository : GenericRepository<PermissaoGrupo>
    {
        public PermissaoGrupoRepository()
            : base(new DapperConnection<PermissaoGrupo>())
        {
        }

        public IEnumerable<PermissaoGrupo> GetByPermissaoId(int permissaoId)
        {
            var pg = new PermissaoGrupo();
            return _cn.ExecuteToList(pg, "PermissaoId = " + permissaoId.ToString() + "");
        }
    }
}
