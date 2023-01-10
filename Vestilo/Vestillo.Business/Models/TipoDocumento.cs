using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("TipoDocumentos", "TipoDocumentos")]
    public class TipoDocumento
    {
        [Chave]
        public int Id { get; set; }
        [FiltroEmpresa]
        public int IdEmpresa { get; set; }
        [RegistroUnico]
        public String Referencia { get; set; }
        [RegistroUnico]
        public String Descricao { get; set; }

    }
}
