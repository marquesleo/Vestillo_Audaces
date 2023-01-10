using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class MovimentoFluxoCaixa
    {
        public DateTime Dia { get; set; }
        public string Movimento { get; set; }
        public int TipoMovimento { get; set; }
        public decimal Valor { get; set; }
    }
}
