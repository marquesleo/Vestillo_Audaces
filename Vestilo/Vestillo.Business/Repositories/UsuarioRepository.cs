using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class UsuarioRepository: GenericRepository<Usuario>
    {
        public UsuarioRepository() : base(new DapperConnection<Usuario>())
        {
        }

        public IEnumerable<Usuario> GetVendedores()
        {
            return _cn.ExecuteStringSqlToList(new Usuario(), "SELECT * FROM Usuarios WHERE Ativo = 1 AND (SELECT COUNT(*) FROM Colaboradores WHERE Colaboradores.UsuarioId = Usuarios.Id) > 0 ORDER BY Nome");
        }

        public Usuario GetByLogin(string login)
        {
            var usu = new Usuario();
            _cn.ExecuteToModel("Login = '" + login + "'", ref usu);
            return usu;
        }
    }
}
