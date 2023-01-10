using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("ItensLiberacaoPedidoVenda", "ItensLiberacaoPedidoVenda")]
    public class ItemLiberacaoPedidoVenda
    {
        [Chave]
        public int Id { get; set; }
        public int LiberacaoId { get; set; }        
        public int ItemPedidoVendaId { get; set; }       
        public int AlmoxarifadoId { get; set; }
        public decimal Qtd { get; set; }
        public decimal QtdFaturada { get; set; }
        public decimal QtdNaoAtendida { get; set; }
        public decimal QtdEmpenhada { get; set; }
        public decimal QtdConferencia { get; set; }
        public decimal QtdConferida { get; set; }
        [DataAtual]
        public DateTime Data { get; set; }
        public int Status { get; set; }
        public int SemEmpenho { get; set; }

        [NaoMapeado]
        public ItemPedidoVendaView Item { get; set; }

       
    }
}
