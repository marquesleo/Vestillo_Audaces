using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("PedidoVendaConferenciaItens")]
    public class PedidoVendaConferenciaItem
    {
        [Chave]
        public int Id { get; set; }
        public int Sequencia { get; set; }
        public int PedidoVendaConferenciaId { get; set; }
        public int ProdutoId { get; set; }
        public int? CorId { get; set; }
        public int? TamanhoId { get; set; }
        public decimal QtdConferida { get; set; }
        public string Observacao { get; set; }

        [NaoMapeado]
        public decimal QtdAtualizarConferencia { get; set; }
    }

    public class PedidoVendaConferenciaItemView : PedidoVendaConferenciaItem
    {

        [NaoMapeado]
        public string ProdutoReferencia { get; set; }        
        [NaoMapeado]
        public string ProdutoDescricao { get; set; } 
        [NaoMapeado]
        public string TamanhoDescricao { get; set; }
        [NaoMapeado]
        public string CorDescricao { get; set; }
        [NaoMapeado]
        public string TamanhoReferencia { get; set; }
        [NaoMapeado]
        public string CorReferencia { get; set; }
        [NaoMapeado]
        public int? UnidadeMedidaId { get; set; }
        

    }
}
