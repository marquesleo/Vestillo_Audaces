using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Core.Models
{
    public class DadosFaturamentoDashFinanceiro
    {
        public int QtdFaturadoHoje { get; set; }
        public decimal ValorFaturadoHoje { get; set; }
        public int QtdAcumuladoMes { get; set; }
        public decimal ValorAcumuladoMes { get; set; }
    }
}
