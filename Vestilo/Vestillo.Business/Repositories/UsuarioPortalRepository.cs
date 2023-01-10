
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class UsuarioPortalRepository : GenericRepository<UsuarioPortal>
    {
        public UsuarioPortalRepository() : base(new DapperConnection<UsuarioPortal>())
        {
        }

        public IEnumerable<UsuarioPortal> GetVendedores()
        {
            return _cn.ExecuteStringSqlToList(new UsuarioPortal(), "SELECT * FROM usuariosportal WHERE Ativo = 1 AND (SELECT COUNT(*) FROM Colaboradores WHERE Colaboradores.Id = Usuariosportal.IdVendedor) > 0 ORDER BY Nome");
        }

        public UsuarioPortal GetByLogin(string login)
        {
            var usu = new UsuarioPortal();
            _cn.ExecuteToModel("Login = '" + login + "'", ref usu);
            return usu;
        }
    }
}
