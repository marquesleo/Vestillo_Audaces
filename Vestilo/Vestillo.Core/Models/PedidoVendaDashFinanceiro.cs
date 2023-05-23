using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Lib;

namespace Vestillo.Core.Models
{
    [Tabela("PedidoVenda", "Pedido de Venda")]
    public class PedidoVendaDashFinanceiro : IModel
    {
        public enum enumStatusPedidoVenda
        {
            Incluido = 1,
            Faturado_Parcial = 2,
            Faturado_Total = 3,
            Finalizado = 4,
            Bloqueado = 5,
            Liberado = 6,
            Conferencia = 7,
            Conferencia_Parcial = 8,
            Aguardando_Liberacao = 9,
            Bloqueado_Financeiro = 10,
            Credito_Liberado = 11
        }

        [Chave]
        public int Id { get; set; }

        [GridColumn("Ref")]
        public string Referencia { get; set; }
        public int EmpresaId { get; set; }
        public int ClienteId { get; set; }
        public int VendedorId { get; set; }
        public int VendedorId2 { get; set; }
        public int TabelaPrecoId { get; set; }
        [GridColumn("Data Emissão")]
        public DateTime DataEmissao { get; set; }
        public DateTime PrevisaoEntrega { get; set; }
        public string Obs { get; set; }
        public int TransportadoraId { get; set; }
        public int RotaId { get; set; }
        public string CodigoPedidoCliente { get; set; }
        public int AlmoxarifadoId { get; set; }
        public int EntregaId { get; set; }
        public enumStatusPedidoVenda Status { get; set; }

        [GridColumn("Ref Cliente")]
        public string RefCliente { get; set; }
        [GridColumn("Nome Cliente")]
        public string NomeCliente { get; set; }
        public string RazaoColaborador { get; set; }
        public string RefVendedor { get; set; }
        [GridColumn("Vendedor")]
        public string NomeVendedor { get; set; }
        public string RefVendedor2 { get; set; }
        public string NomeVendedor2 { get; set; }
        public string RefTabelaPreco { get; set; }
        [GridColumn("Tabela de Preço")]
        public string DescricaoTabelaPreco { get; set; }
        
        public string RefTransportadora { get; set; }
        [GridColumn("Transportadora")]
        public string NomeTransportadora { get; set; }
        public string RefRotaVisita { get; set; }
        [GridColumn("Rota")]
        public string DescricaoRotaVisita { get; set; }
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

        public string DescricaoStatus
        {
            get
            {
                switch (this.Status)
                {
                    case enumStatusPedidoVenda.Finalizado:
                        return "Finalizado";
                    case enumStatusPedidoVenda.Faturado_Parcial:
                        return "Faturado Parcial";
                    case enumStatusPedidoVenda.Faturado_Total:
                        return "Faturado Total";
                    case enumStatusPedidoVenda.Incluido:
                        return "Incluído";
                    case enumStatusPedidoVenda.Bloqueado:
                        return "Bloqueado";
                    case enumStatusPedidoVenda.Liberado:
                        return "Liberado";
                    case enumStatusPedidoVenda.Aguardando_Liberacao:
                        return "Aguardando Liberação";
                    case enumStatusPedidoVenda.Bloqueado_Financeiro:
                        return "Bloqueado Financeiro";
                    default:
                        return "";
                }
            }
        }
    }
}
