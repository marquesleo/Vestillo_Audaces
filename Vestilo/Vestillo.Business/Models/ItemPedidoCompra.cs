using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("ItensPedidoCompra", "ItensPedidoCompra")]
    public class ItemPedidoCompra
    {
        [Chave]
        public int Id { get; set; }
        public int PedidoCompraId { get; set; }
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
        public decimal QtdAtendida { get; set; }
        public decimal? Excedente { get; set; }

        [NaoMapeado]
        public decimal QtdRestante {
            get
            {
                return Qtd - QtdAtendida;
            }
        }
    }
}
