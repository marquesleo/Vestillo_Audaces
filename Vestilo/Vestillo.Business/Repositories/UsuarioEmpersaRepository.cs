using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class UsuarioEmpresaRepository: GenericRepository<UsuarioEmpresa>
    {
        public UsuarioEmpresaRepository()
            : base(new DapperConnection<UsuarioEmpresa>())
        {
        }

        public IEnumerable<UsuarioEmpresa> GetByUsuarioId(int usuarioId)
        {
            return _cn.ExecuteToList(new UsuarioEmpresa(), "UsuarioId = " + usuarioId.ToString());
        }

        public void DeleteByUsuarioId(int usuarioId)
        {
            _cn.ExecuteNonQuery("DELETE FROM UsuarioEmpresas WHERE UsuarioId = " + usuarioId.ToString());
        }
    }
}
