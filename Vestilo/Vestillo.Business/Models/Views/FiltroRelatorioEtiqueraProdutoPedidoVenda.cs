using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models.Views
{
    public class FiltroRelatorioEtiquetaProdutoPedidoVenda
    {
        public int[] pedidosIds { get; set; }
        public int[] clientesIds { get; set; }
        public int[] vendedoresIds { get; set; }
        public string daLiberacao { get; set; }
        public string ateLiberacao { get; set; }
    }
}
