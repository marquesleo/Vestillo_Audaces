using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("TiposCobranca", "TiposCobranca")]
    public class TipoCobranca
    {
        [Chave]
        public int Id { get; set; }
        [RegistroUnico]
        [OrderByColumn]
        public string Descricao { get; set; }
    }
}
