using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models.Views
{
    public class FiltroFatNfeListagem
    {
        public List<int> Colaborador { get; set; }
        public List<int> Vendedor { get; set; }
        public List<int> TipoMovimentacao { get; set; }
        public List<int> UF { get; set; }
        public DateTime DaInclusao { get; set; }
        public DateTime AteInclusao { get; set; }
        public int TipoDocumento { get; set; }
        public int Ordernar { get; set; }
        public bool DescontaDevolvida { get; set; }
        public bool NotaEmitida { get; set; }
        public bool ConsideraUF { get; set; }
    }
}
