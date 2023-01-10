using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("UsuarioGrupos", "UsuarioGrupos")]
    public class UsuarioGrupo
    {
        [Chave]
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int GrupoId { get; set; }
    }
}
