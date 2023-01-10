using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models.Views
{
    public class FiltroRelatorioEtiquetaProduto
    {
        public int[] segmentosIds { get; set; }
        public int[] produtosIds { get; set; }
        public int[] gruposIds { get; set; }
        public int[] corIds { get; set; }
        public int[] colecoesIds { get; set; }
        public int[] tamanhosIds { get; set; }
        public string doAno { get; set; }
        public string ateAno { get; set; }
    }
}
