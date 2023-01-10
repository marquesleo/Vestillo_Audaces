
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("EstoqueEmTransito", "EstoqueEmTransito")]
    public class EstoqueEmTransito
    {
        [Chave]
        public int Id { get; set; }
        public int IdItem { get; set; }       
        public int? IdCor { get; set; }
        public int? IdTamanho { get; set; }
        public decimal Quantidade { get; set; }
        public int status { get; set; }
    }
}
