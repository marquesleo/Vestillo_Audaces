using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models.Views
{
    public class ValorBaseMesDias
    {
        public int Faixa { get; set; }
        public decimal Minutos { get; set; }
        public decimal Minimo { get; set; }
        public decimal Premio { 
            get {
                return Minutos.ToRound(2) * Minimo.ToRound(5);
            } 
        }
        public decimal Jan { get; set; }
        public decimal Fev { get; set; }
        public decimal Mar { get; set; }
        public decimal Abr { get; set; }
        public decimal Mai { get; set; }
        public decimal Jun { get; set; }
        public decimal Jul { get; set; }
        public decimal Ago { get; set; }
        public decimal Set { get; set; }
        public decimal Out { get; set; }
        public decimal Nov { get; set; }
        public decimal Dez { get; set; }
    }
}
