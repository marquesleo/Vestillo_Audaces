using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Vestillo.Tabela("ItensOrdemProducao", "Itens da Ordem de Produção")]
    public class ItemOrdemProducao
    {
        [Vestillo.Chave]
        public int Id { get; set; }
        public int OrdemProducaoId { get; set; }
        public int ProdutoId { get; set; }
        public int? CorId { get; set; }
        public int? TamanhoId { get; set; }
        public int? SeqLiberacaoPedido { get; set; }
        public int? PedidoVendaId { get; set; }
        public decimal Quantidade { get; set; }
        public decimal QuantidadeAtendida { get; set; }
        public decimal QuantidadeProduzida { get; set; }
        public string DataQuantidade { get; set; }
        public int Status { get; set; }
        public DateTime? DataPrevisao { get; set; }
        public decimal QuantidadeAvaria { get; set; }
        public decimal QuantidadeDefeito { get; set; }
        public int ItemFinalizado { get; set; }
    }

}
