using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models.Views
{
    public class FiltroRelatorioFocoVendas
    {        
        public int[] catalogosIds { get; set; }
        public int[] colecoesIds { get; set; }
        public int[] corIds { get; set; }        
        public int[] tamanhosIds { get; set; }
        public int idAlmoxarifado { get; set; }
        public decimal saldo { get; set; }
    }
}
