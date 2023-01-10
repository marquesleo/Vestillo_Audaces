using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("Permissoes", "Permissões de Usuário")]
    public class Permissao
    {
        [Chave]
        public int Id { get; set; }
        [OrderByColumn]
        public string Chave { get; set; }
        public string Descricao { get; set; }
        [NaoMapeado]
        public IEnumerable<PermissaoGrupo> Grupos { get; set; }
    }
}
