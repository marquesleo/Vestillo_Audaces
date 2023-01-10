using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models.Views
{
    public class CustoConsumo
    {
        public string Referencia { get; set; }
        public string Material { get; set; }
        public string Unidade { get; set; }
        public string Cor { get; set; }
        public string Tamanho { get; set; }
        public decimal Consumo { get; set; }
        public decimal Preco { get; set; }
        public decimal Total { get; set; }
        
    }
}
