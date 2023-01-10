using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models.Views
{
    public class PedidoVendaLiberacaoView
    {
        public int Id { get; set; }
        public string Referencia { get; set; }
        public int EmpresaId { get; set; }
        public int ClienteId { get; set; }
        public int VendedorId { get; set; }
        public int TabelaPrecoId { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime PrevisaoEntrega { get; set; }
        public string Obs { get; set; }
        public int TransportadoraId { get; set; }
        public int RotaId { get; set; }
        public string CodigoPedidoCliente { get; set; }
        public int AlmoxarifadoId { get; set; }
        public int EntregaId { get; set; }
        public string DescEntrega { get; set; }
        public int Status { get; set; }
        public string RefCliente { get; set; }
        public string NomeCliente { get; set; }
        public string RazaoSocialCliente { get; set; }
        public string RefVendedor { get; set; }
        public string NomeVendedor { get; set; }
        public string RefVendedor2 { get; set; }
        public string NomeVendedor2 { get; set; }
        public decimal QtdPedida { get; set; }
        public decimal QtdEmpenhada { get; set; }
        public decimal QtdLiberada { get; set; }
        public decimal ValorEmpenhadoTotal { get; set; }
        public decimal ValorLiberadoTotal { get; set; }
        public decimal AtendidoTotal { get; set; }
        public decimal NaoAtendidoTotal { get; set; }

        public decimal Atendido
        {
            get
            {
                return AtendidoTotal;
            }
        }

        public decimal NaoAtendido
        {
            get
            {
                return NaoAtendidoTotal;
            }
        }

        public bool Conferencia { get; set; }
        public string Impresso { get; set; }

        public DateTime? DataInspecao { get; set; }
        public DateTime? DataAgendamento { get; set; }
        public DateTime? DataFaturamento { get; set; }
        public string OrdemProducao { get; set; }
        public string Familia { get; set; }

        public string DescricaoStatus
        {
            get
            {
                switch (this.Status)
                {
                    case (int)enumStatusPedidoVenda.Finalizado:
                        return "Finalizado";
                    case (int)enumStatusPedidoVenda.Faturado_Parcial:
                        return "Faturado Parcial";
                    case (int)enumStatusPedidoVenda.Faturado_Total:
                        return "Faturado Total";
                    case (int)enumStatusPedidoVenda.Incluido:
                        return "Incluído";
                    case (int)enumStatusPedidoVenda.Bloqueado:
                        return "Bloqueado";
                    case (int)enumStatusPedidoVenda.Liberado:
                        return "Liberado";
                    case (int)enumStatusPedidoVenda.Conferencia:
                        return "Conferência";
                    case (int)enumStatusPedidoVenda.Conferencia_Parcial:
                        return "Conferência Parcial";
                    case (int)enumStatusPedidoVenda.Aguardando_Liberacao:
                        return "Aguardando Liberação";
                    case (int)enumStatusPedidoVenda.Bloqueado_Financeiro:
                        return "Bloqueado Financeiro";
                    case (int)enumStatusPedidoVenda.Credito_Liberado:
                        return "Crédito Liberado";
                    default:
                        return "";
                }
            }
        }
        public string RefTabelaPreco { get; set; }
        public string DescricaoTabelaPreco { get; set; }
        public string RefTransportadora { get; set; }
        public string NomeTransportadora { get; set; }
        public string RefRotaVisita { get; set; }
        public string DescricaoRotaVisita { get; set; }

        public List<LiberacaoPedidoVendaView> LiberacaoPedidoVenda { get; set; }
    }
}
