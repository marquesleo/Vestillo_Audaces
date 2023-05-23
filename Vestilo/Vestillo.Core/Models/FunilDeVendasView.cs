using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Core.Models
{
    public class FunilDeVendasView
    {
        public int QtdAtividadeRegistrada { set; get; }
        public int QtdClientesAtivos { get; set; }
        public int QtdAtividadeRegistradaMes { get; set; }
        public int QtdPedidosMes { get; set; }
        public int QtdFaturados { get; set; }
    }
}
