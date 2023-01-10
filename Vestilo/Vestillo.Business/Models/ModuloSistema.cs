using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("ModulosSistema", "ModulosSistema")]
    public class ModuloSistema
    {
        [Chave]
        public int Id { get; set; }
        [RegistroUnico]
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public string XmlMenu { get; set; }
    }
}
