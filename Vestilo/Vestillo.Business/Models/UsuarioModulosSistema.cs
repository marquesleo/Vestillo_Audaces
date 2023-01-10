using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("UsuariosModulosSistema", "UsuariosModulosSistema")]
    public class UsuarioModulosSistema
    {
        [Chave]
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int ModuloId { get; set; }
        public bool Padrao { get; set; }
    }
}
