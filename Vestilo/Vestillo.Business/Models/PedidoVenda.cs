using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("PedidoVenda", "Pedido de Venda")]
    public class PedidoVenda
    {
        [Chave]
        public int Id { get; set; }
        [Contador("PedidoVenda")]
        [OrderByColumn]
        [RegistroUnico]
        public string Referencia { get; set; }
        [FiltroEmpresa]
        public int EmpresaId { get; set; }
        public int ClienteId { get; set; }
        public int VendedorId { get; set; }
        public int VendedorId2 { get; set; }
        public int TabelaPrecoId { get; set; }
        [DataAtual]
        public DateTime DataEmissao { get; set; }
        public DateTime PrevisaoEntrega { get; set; }
        public DateTime? DataSaida { get; set; }
        public string Obs { get; set; }
        public int TransportadoraId { get; set; }
        public int RotaId { get; set; }
        public string CodigoPedidoCliente { get; set; }
        public int AlmoxarifadoId { get; set; }
        public int EntregaId { get; set; }
        public int Status { get; set; }
        public bool Conferencia { get; set; }
        public string Impresso { get; set; }

        public decimal ComissaoVendedor { get; set; }
        public decimal ComissaoVendedor2 { get; set; }

        public DateTime? DataInspecao { get; set; }
        public DateTime? DataAgendamento { get; set; }
        public string OrdemProducao { get; set; }
        public string Familia { get; set; }
        public string ObsAux { get; set; }
        public DateTime? DataFaturamento { get; set; }

        public decimal QtdPedida { get; set; }
        public decimal QtdEmpenhada { get; set; }
        public decimal QtdLiberada { get; set; }
        public decimal ValorEmpenhadoTotal { get; set; }
        public decimal ValorLiberadoTotal { get; set; }
        public int TipoFrete { get; set; }
        public int OpcaoTabelaPreco { get; set; }
        public decimal DescPercent { get; set; }
        public decimal DescValor { get; set; }

        [NaoMapeado]
        public decimal AtendidoTotal { get; set; }
        [NaoMapeado]
        public decimal NaoAtendidoTotal { get; set; }

        [NaoMapeado]
        public string Atendido
        {
            get
            {
                return AtendidoTotal.ToString("#0.00");
            }
        }

        [NaoMapeado]
        public string NaoAtendido
        {
            get
            {
                return NaoAtendidoTotal.ToString("#0.00");
            }
        }

        [NaoMapeado]
        public List<ItemPedidoVendaView> Itens { get; set; }

        [NaoMapeado]
        public List<ItemLiberacaoPedidoVenda> ItensLiberacao { get; set; }

        [NaoMapeado]
        public List<ContasReceberView> Parcelas { get; set; }

        [NaoMapeado]
        public bool AtualizaEstoque { get; set; }

        //[NaoMapeado]
        //public int PossuiProducao { get; set; }

    }
}
