
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Lib;

namespace Vestillo.Core.Models
{
    public class CurvaAbcItensView
    {
        public int IdCliente { set; get; }
        [GridColumn("Ref")]
        public string RefProduto { get; set; }
        [GridColumn("Descrição")]
        public string DescrProduto { get; set; }
        [GridColumn("Qtd")]
        public decimal Qtd { get; set; }
        [GridColumn("Total")]
        public decimal TotalItens { get; set; }        
    }
}
