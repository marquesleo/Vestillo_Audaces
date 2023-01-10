using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
     [Tabela("Municipiosibge", "Municipiosibge")]
    public class MunicipioIbge
    {
        [Chave]
        public int Id { get; set; }
        [OrderByColumn]
        public string Municipio { get; set; }
        public string Uf { get; set; }
    }
}
