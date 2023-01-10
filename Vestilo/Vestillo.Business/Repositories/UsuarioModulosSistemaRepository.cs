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
    public class UsuarioModulosSistemaRepository : GenericRepository<UsuarioModulosSistema>
    {
        public UsuarioModulosSistemaRepository()
            : base(new DapperConnection<UsuarioModulosSistema>())
        {
        }

        public void UpdateModuloPadraoUsuario(int usuarioId, int moduloId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("UPDATE UsuariosModulosSistema SET");
            SQL.AppendLine(" Padrao = 0 ");
            SQL.AppendLine("WHERE UsuarioId = " + usuarioId.ToString() + "; ");

            SQL.AppendLine("UPDATE UsuariosModulosSistema SET");
            SQL.AppendLine(" Padrao = 1 ");
            SQL.AppendLine("WHERE UsuarioId = " + usuarioId.ToString() + " ");
            SQL.AppendLine("AND ModuloId = " + moduloId.ToString());

            _cn.ExecuteUpdate(new UsuarioModulosSistema(), SQL.ToString());
        }

        public IEnumerable<UsuarioModulosSistema> GetByUsuario(int usuarioId)
        {
            return _cn.ExecuteToList(new UsuarioModulosSistema(), "UsuarioId = " + usuarioId.ToString());
        }
    }
}
