using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models.Views
{
    public class FiltroRelatorioEtiquetaProdutoOrdem
    {
        public int[] ordensIds { get; set; }
        public string daEmissao { get; set; }
        public string ateEmissao { get; set; }
        public int ordemStatus { get; set; }
    }
}
