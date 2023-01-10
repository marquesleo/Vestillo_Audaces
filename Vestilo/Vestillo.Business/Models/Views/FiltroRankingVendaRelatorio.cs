using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models.Views
{
    public class FiltroRankingVendaRelatorio
    {  
        public List<int> Produtos { get; set; }
        public List<int> Colecao { get; set; }
        public List<int> Segmento { get; set; }
        public List<int> Cor { get; set; }
        public List<int> Tamanho { get; set; }
        public List<int> Grupo { get; set; }

        public string DoAno { get; set; }
        public string AteAno { get; set; }
        public bool Agrupar { get; set; }
        public string Ordernar { get; set; }
        public bool SomenteEmitidos { get; set; }
        public DateTime DaEmissao { get; set; }
        public DateTime AteEmissao { get; set; }
        public bool nfe { get; set; }
        public bool nfce { get; set; }
        public bool precoDiferente { get; set; }
    }
}
