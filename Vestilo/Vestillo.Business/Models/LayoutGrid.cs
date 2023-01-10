using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("FormularioLayout", "FormularioLayout")]
    public class LayoutGrid
    {
        [Chave]
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int FormId { get; set; }
        public byte[] ArquivoXML { get; set; }
       
    }
}
