using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;
using Vestillo.Lib;

namespace Vestillo.Business.Repositories 
{
    public class ModuloSistemaRepository : GenericRepository<ModuloSistema>
    {
        public ModuloSistemaRepository() : base(new DapperConnection<ModuloSistema>())
        {
        }

        public IEnumerable<ModuloSistema> GetByUsuario(int usuarioId)
        {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT M.*");
            SQL.AppendLine("FROM 	ModulosSistema M");
            SQL.AppendLine("	INNER JOIN UsuariosModulosSistema U ON U.ModuloId = M.Id");
            SQL.AppendLine("WHERE U.UsuarioId = ");
            SQL.Append(usuarioId);
            SQL.AppendLine(" ORDER BY U.Padrao DESC, M.Nome ASC");

            return _cn.ExecuteStringSqlToList(new ModuloSistema(), SQL.ToString());
        }
    }
}
