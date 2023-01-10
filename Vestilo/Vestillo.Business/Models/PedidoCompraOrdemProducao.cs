using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("PedidoCompraOrdemProducao", "Pedido de Compra X Ordem Produção")]
    public class PedidoCompraOrdemProducao
    {
        [Chave]
        public int Id { get; set; }
        public int PedidoCompraId { get; set; }
        public int OrdemProducaoId { get; set; }
    }
}
