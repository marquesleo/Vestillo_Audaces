using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models.Views
{
    public class FiltroRelatorioEtiquetaComposicao
    {
        public int[] produtosIds { get; set; }
        public int[] corIds { get; set; }
        public int[] tamanhosIds { get; set; }
        public int[] segmentosIds { get; set; }
        public int[] colecoesIds { get; set; }
    }
}
