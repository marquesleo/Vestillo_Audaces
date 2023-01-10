using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class FichaFaccaoView
    {
        public bool ckbFaccao { get; set; }
        public int Id { get; set; }
        public String Referencia { get; set; }
        public String Nome { get; set; }
        public String RazaoSocial { get; set; }
        public decimal ValorPeca { get; set; }
        public decimal PrecoCompra { get; set; }
    }
}
