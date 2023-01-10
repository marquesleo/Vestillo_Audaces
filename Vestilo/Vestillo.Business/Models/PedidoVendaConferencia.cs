using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("PedidoVendaConferencia")]
    public class PedidoVendaConferencia
    {
        [Chave]
        public int Id { get; set; }
        public int PedidoVendaId { get; set; }
        public DateTime DataConferencia { get; set; }
        public int UsuarioId { get; set; }

        [NaoMapeado]
        public List<PedidoVendaConferenciaItem> Itens { get; set; }

        [NaoMapeado]
        public List<ItemLiberacaoPedidoVenda> ItensLiberados { get; set; }

        public PedidoVendaConferencia()
        {
            Itens = new List<PedidoVendaConferenciaItem>();
            PedidosIds = new List<int>();
        }
        [NaoMapeado]
        public List<int> PedidosIds { get; set; }

        [NaoMapeado]
        public int VirouPedidoDci { get; set; }

    }

    public class PedidoVendaConferenciaView
    {
        public bool Checked { get; set; }
        public int Id { get; set; }
        public int PedidoVendaId { get; set; }
        public string PedidoReferencia { get; set; }
        public string ClienteReferencia { get; set; }
        public string ClienteNome { get; set; }
        public DateTime? DataConferencia { get; set; }
        public DateTime DataLiberacao { get; set; }
        public DateTime DataEmissao { get; set; }
        public bool Conferir { get; set; }
        public int VirouPedidoDci { get; set; }
        public string ObsPedido { get; set; }
    }
}
