using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class PlanoAnualDetalheTotais
    {
        public string Header { get; set; }
        public double Costureira { get; set; }
        public double DiasUteis { get; set; }
        public decimal Jornada { get; set; }
        public decimal Presenca { get; set; }
        public decimal Aproveitamento { get; set; }
        public decimal Eficiencia { get; set; }
        public decimal TempoMedio { get; set; }
        public double MinTotais { get; set; }


        public double MinPresenca { get; set; }
        public double MinUteis { get; set; }
        public double MinProducao { get; set; }
        public double Pecas { get; set; }
    }
}
