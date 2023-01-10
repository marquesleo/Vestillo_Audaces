using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models.Views
{
    public class FiltroCustoProdutoSintetico
    {
        public List<int> Produtos { get; set; }
        public List<int> Colecao { get; set; }
        public List<int> Segmento { get; set; }
        public List<int> Grupo { get; set; }
        public string DoAno { get; set; }
        public string AteAno { get; set; }
        public int OrdenarPor { get; set; }
    }
}
