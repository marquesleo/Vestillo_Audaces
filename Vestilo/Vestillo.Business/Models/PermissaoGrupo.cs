using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("PermissoesGrupo", "PermissoesGrupo")]
    public class PermissaoGrupo
    {
        [Chave]
        public int Id { get; set; }
        public int PermissaoId { get; set; }
        public int GrupoId { get; set; }
        [NaoMapeado]
        public Permissao Permissao { get; set; }
        [NaoMapeado]
        public Grupo Grupo { get; set; }
    }
}
