using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class FluxoCaixa 
    {
        public DateTime Dia { get; set; }
        public decimal EntradaDia { get; set; }
        public decimal SaidaDia { get; set; }
        public decimal DebitoEmpresaDia { get; set; }
        public decimal CreditoEmpresaDia { get; set; }
        public decimal SaldoDia { get; set; }
        public decimal EntradaAcumulada { get; set; }
        public decimal SaidaAcumulada { get; set; }
        public decimal SaldoAcumulado { get; set; }
    }
}
