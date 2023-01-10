using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models.Views
{
    public class PedidoMatriz
    {
        public int IdPedido { get; set; }
        public int IdCliente { get; set; }
        public int IdItem { get; set; }
        public string RefPedido { get; set; }
        public string Cliente { get; set; }
        public string RefItem { get; set; }
        public string DescricaoItem { get; set; }
        public int IdCor { get; set; }
        public string Cor { get; set; }
        public int IdTamanho { get; set; }
        public string Tamanho { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Valor { get; set; }
        public decimal Total { get; set; }
    }
}
