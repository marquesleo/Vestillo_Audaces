
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Lib;

namespace Vestillo.Core.Models
{
    public class AcaompanhamentoCatalogoClienteView
    {
        public int IdCliente { get; set; }
        public string Catalogo { get; set; }       
        public decimal Quantidade { get; set; }                
        public decimal Descontos { get; set; }
        public decimal Acrescimos { get; set; }
        public decimal TotalItens { get; set; }
        public decimal PedidoLiberado { get; set; }
        public int IdCatalogo { get; set; }
    }
}
