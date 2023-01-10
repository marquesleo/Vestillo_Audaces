using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class LiberacaoPedidoVenda
    {
        public int LiberacaoId { get; set; }
        public int PedidoVendaId { get; set; }

        [DataAtual]
        public DateTime DataLiberacao { get; set; }
        public int Status { get; set; }

        [NaoMapeado]
        public List<ItemLiberacaoPedidoVendaView> ItensLiberacao { get; set; }
    }
}
