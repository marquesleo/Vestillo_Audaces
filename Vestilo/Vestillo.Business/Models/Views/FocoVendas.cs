using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models.Views
{
    public class FocoVendas
    {
        public int ProdutoId { get; set; }
        public string segmento { get; set; }
        public byte[] imagem { get; set; }
        public string referencia { get; set; }
        public string cor { get; set; }
        public string tamanho { get; set; }
        public string catalogo { get; set; }
        public string colecao { get; set; }
        public decimal saldo { get; set; }
        public decimal venda { get; set; }
        public decimal giro { get; set; }
    }
}
