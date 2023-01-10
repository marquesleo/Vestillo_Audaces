using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("Ncm", "Ncm")]
    public class Ncm
    {
        [Chave]
        public int Id { get; set; }
        public string Referencia { get; set; }
        public decimal AliqNacional { get; set; }
        public decimal AliqImportado { get; set; }
        public decimal AliqEstadual { get; set; }

    }


}
