using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
     [Tabela("formularios", "formularios")]
    public class Formularios
    {
        [Chave]
        public int Id {get; set;}
        [OrderByColumn]
        public string NomeForm {get; set;}
        public int TipoSistema { get; set; }
    }
}
