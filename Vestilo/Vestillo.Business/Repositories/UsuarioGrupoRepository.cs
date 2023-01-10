using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class UsuarioGrupoRepository: GenericRepository<UsuarioGrupo>
    {
        public UsuarioGrupoRepository()
            : base(new DapperConnection<UsuarioGrupo>())
        {
        }

        public  IEnumerable<UsuarioGrupo> GetByUsuarioId(int usuarioId)
        {
            var ug = new UsuarioGrupo();
            return _cn.ExecuteToList(ug, "UsuarioId = " + usuarioId.ToString() + "");
        }

        public bool UsuarioAdministrador(int IdUsuario, int VerificaOutroGrupo = 0)
        {
            bool TemPermissao = false;
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT * from usuariogrupos where UsuarioId = " + IdUsuario + " AND (GrupoId = 1 OR GrupoId = " + VerificaOutroGrupo + ")");

            var c = new UsuarioGrupo();
            var dados = _cn.ExecuteStringSqlToList(c, sql.ToString());

            if (dados != null && dados.Count() > 0)
            {
                TemPermissao = true;
            }
            return TemPermissao;

        }
    }
}
