using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("UsuarioEmpresas", "UsuarioEmpresas")]
    public class UsuarioEmpresa
    {
        [Chave]
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int EmpresaId { get; set; }
        public bool Principal { get; set; }
    }
}
