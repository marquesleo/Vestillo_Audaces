using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models.Views;

namespace Vestillo.Business.Models
{
    public class RankingVenda 
    {
        public string Referencia { get; set; }
        public string Descricao { get; set; }
        public string Cor { get; set; }
        public string Tamanho { get; set; }
        public Decimal Quantidade { get; set; }
       
        public Decimal Preco { get; set; }
        public Decimal Total { get; set; }
       
    }
}
