using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models.Views
{
    public class FiltroCustoProdutoAnalitico
    {
        public List<int> Produtos { get; set; }
        public List<int> Colecao { get; set; }
        public List<int> Segmento { get; set; }
        public List<int> Grupo { get; set; }
        public List<decimal> MargemLucro { get; set; }
        public List<decimal> Frete { get; set; }
        public List<decimal> Comissao { get; set; }
        public List<decimal> Encargo { get; set; }
    }
}
