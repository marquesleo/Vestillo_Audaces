
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Lib;

namespace Vestillo.Business.Models
{
    public class PedidoVendaConferenciaitensDciView
    {
        public bool Checked { get; set; }
        public int Id { get; set; }
        public int PedidoVendaConferenciaId { get; set; }
        public int PedidoVendaId { get; set; }
        public int ProdutoId { get; set; }
        public int CorId { get; set; }
        public int TamanhoId { get; set; }
        public string PedidoReferencia { get; set; }
        public string ClienteReferencia { get; set; }
        public string ClienteNome { get; set; }
        public string ObsPedido { get; set; }
        public string ProdutoReferencia { get; set; }
        public string ProdutoDescricao { get; set; }
        public string CorDescricao { get; set; }
        public string TamanhoDescricao { get; set; }
        public string CorReferencia { get; set; }
        public string TamanhoReferencia { get; set; }
        public decimal QtdConferida { get; set; }
    }        
            
}
