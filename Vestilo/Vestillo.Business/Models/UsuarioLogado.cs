using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("UsuariosLogados", "UsuariosLogados")]
    public class UsuarioLogado
    {
        [Chave]
        public int UsuarioId { get; set; }
        public DateTime DataLogin { get; set; }
        public string Maquina { get; set; }
        public string Ip { get; set; }
    }
}
