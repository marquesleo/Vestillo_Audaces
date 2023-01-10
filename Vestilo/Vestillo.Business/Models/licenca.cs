
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("licenca", "licenca")]
    public class licenca
    {
        [Chave]
        public int Id { get; set; }
        public string Cnpj { get; set; }
        public string Dia { get; set; }
        public string Bloqueado { get; set; }
        public int IdNaWeb { get; set; }
        public string BaseWeb { get; set; }
    }
}
