using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Core.Models
{
    public class BuscaPreco
    {
        public int Id { get; set; }
        public string EAN { get; set; }
        public string Item { get; set; }
        public decimal Preco { get; set; }
    }
}
