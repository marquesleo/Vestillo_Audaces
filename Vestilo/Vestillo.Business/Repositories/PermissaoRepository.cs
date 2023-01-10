using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class PermissaoRepository : GenericRepository<Permissao>
    {
        public PermissaoRepository()
            : base(new DapperConnection<Permissao>())
        {
        }

        public IEnumerable<Permissao> GetByGrupos(string grupos)
        {
            var p = new Permissao();
            return _cn.ExecuteStringSqlToList(p, "SELECT Permissoes.* FROM Permissoes INNER JOIN PermissoesGrupo ON PermissoesGrupo.PermissaoId = Permissoes.Id WHERE  GrupoId IN (" + grupos + ")");
        }
    }
}
