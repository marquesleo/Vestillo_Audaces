using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models.Views
{
    public class FiltroCustoConsumo
    {
        public List<int> Produtos { get; set; }
        public List<int> Ordem { get; set; }
        public List<int> Cor { get; set; }
        public List<int> Tamanho { get; set; }

        public string DaEmissao { get; set; }
        public string AteEmissao { get; set; }
        public bool MaiorPreco { get; set; }
        public string Exibir { get; set; }
        public bool Agrupar { get; set; }
    }
}
