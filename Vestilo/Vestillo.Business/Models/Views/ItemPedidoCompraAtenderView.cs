using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class ItemPedidoCompraAtenderView
    {
        public int ItemPedidoId { get; set; }
        public int IdFornecedor { get; set; }
        public DateTime DataEmissao { get; set; }
        public int PedidoCompraId { get; set; }
        public string RefPedido { get; set; }
        public string  DescProduto { get; set; }
        public string DescCor { get; set; }
        public string  DescTamanho { get; set; }
        public int TipoMovimentacaoId { get; set; }
        public int AlmoxarifadoId { get; set; }      
        public string RefProduto { get; set; }        
        public int iditem { get; set; }
        public int? IdTamanho { get; set; }
        public int? IdCor { get; set; }
        public int? UnidadeMedidaId { get; set; }
        public decimal Qtd { get; set; }       
        public decimal Preco { get; set; }
        public decimal? PrecoUnidadeMedida2 { get; set; }
        public decimal QtdAtender { get; set; }
        public decimal PerctIpi { get; set; }
        public int Selecionado { get; set; }
    }
}
