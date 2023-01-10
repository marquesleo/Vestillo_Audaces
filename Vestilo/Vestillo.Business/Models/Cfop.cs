using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("Cfops","Cfops")]
    public class Cfop
    {

        [Chave]
        public int Id { get; set; }
        public string Referencia { get; set; }
        public string Descricao { get; set; }

    }
}
