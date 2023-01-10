using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class ListaFatVendaView
    {
        public int IdVendedor { get; set; }
        public string RefVendedor { get; set; }
        public string NomeVendedor { get; set; }
        public int QtdTotalPecas { get; set; }
        public int QtdTotalFaturamentos { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorNcc { get; set; }
        public int IdNfce { get; set; }
    }
}
