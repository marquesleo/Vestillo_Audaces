using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models.Views
{
    public class PedidoVendaView : PedidoVenda
    {
        public string RefCliente { get; set; }
        public string NomeCliente { get; set; }
        public string RazaoColaborador { get; set; }        
        public string RefVendedor { get; set; }
        public string NomeVendedor { get; set; }
        public string RefVendedor2 { get; set; }
        public string NomeVendedor2 { get; set; }
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
        public DateTime? DataInspecao { get; set; }
        public DateTime? DataAgendamento { get; set; }
        public string OrdemProducao { get; set; }
        public string Familia { get; set; }


        [NaoMapeado]
        public List<LiberacaoPedidoVenda> LiberacaoPedidoVenda { get; set; }

        //[NaoMapeado]
        //public int PossuiProducao { get; set; }
    }
}
