
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("pedidovendaconferenciaitensDci", "pedidovendaconferenciaitensDci")]
    public class PedidoVendaConferenciaitensDci
    {
        [Chave]
        public int Id { get; set; }
        public int PedidoVendaConferenciaId { get; set; }
        public int PedidoVendaId { get; set; }
        public int ProdutoId { get; set; }
        public int CorId { get; set; }
        public int TamanhoId { get; set; }        
        public decimal QtdConferida { get; set; }
        public string Observacao { get; set; }
        public int VirouPedidoDci { get; set; }
    }
}