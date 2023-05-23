using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Core.Models
{
    public class DespesaXReceitaDashFinanceiro
    {
        public decimal ValorReceita { get; set; }
        public decimal ValorDespesa { get; set; }
        public decimal Resultado
        {
            get
            {
                return ValorReceita - ValorDespesa;
            }
        }
    }
}
