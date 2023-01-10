using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("PedidoCompra", "Pedido de Compra")]
    public class PedidoCompra
    {
        [Chave]
        public int Id { get; set; }
        [Contador("PedidoCompra")]
        [OrderByColumn]
        [RegistroUnico]
        public string Referencia { get; set; }
        [FiltroEmpresa]
        public int EmpresaId { get; set; }
        public int FornecedorId { get; set; }
        public string Contato { get; set; }
        [DataAtual]
        public DateTime DataEmissao { get; set; }
        public DateTime PrevisaoEntrega { get; set; }
        public DateTime PrevisaoFornecedor { get; set; }
        public string Obs { get; set; }
        public int TransportadoraId { get; set; }
        public string CodigoPedidoFornecedor { get; set; }
        public int AlmoxarifadoId { get; set; }
        public int Status { get; set; }
        public int Semana { get; set; }
        public string OrdensReferencia { get; set; }
        public int TipoFrete { get; set; }

        [NaoMapeado]
        public decimal Total { get; set; }       

        [NaoMapeado]
        public List<ItemPedidoCompraView> Itens { get; set; }

        [NaoMapeado]
        public List<ContasPagarView> Parcelas { get; set; }

        [NaoMapeado]
        public List<PedidoCompraOrdemProducao> PedidosOrdens { get; set; }

    }
}
