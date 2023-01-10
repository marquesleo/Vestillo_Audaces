using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("ItensPedidoVenda", "ItensPedidoVenda")]
    public class ItemPedidoVenda
    {
        [Chave]
        public int Id { get; set; }
        public int PedidoVendaId { get; set; }
        public int TipoMovimentacaoId { get; set; }
        public int ProdutoId { get; set; }
        public int? TamanhoId { get; set; }
        public int? CorId { get; set; }
        public int? UnidadeMedidaId { get; set; }
        public decimal Qtd { get; set; }
        public int? UnidadeMedida2Id { get; set; }
        public decimal? QtdUnidadeMedida2 { get; set; }
        public decimal Preco { get; set; }
        public decimal? PrecoUnidadeMedida2 { get; set; }
        public string ReferenciaPedidoCliente { get; set; }
        public string SeqPedCliente { get; set; }
    }
}
