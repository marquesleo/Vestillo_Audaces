using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("PlanoContasNaturezasFinanceiras", "PlanoContasNaturezasFinanceiras")]
    public class PlanoContasNaturezasFinanceiras
    {
        [Chave]
        public int Id { get; set; }
        public int PlanoContasId { get; set; }
        public int NaturezaFinanceiraId { get; set; }
    }
}
