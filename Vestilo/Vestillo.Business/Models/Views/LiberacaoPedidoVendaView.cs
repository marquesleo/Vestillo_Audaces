using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class LiberacaoPedidoVendaView : LiberacaoPedidoVenda
    {
        public decimal Qtd { get; set; }
        public decimal QtdFaturada { get; set; }
        public decimal QtdLiberada { get; set; }
        public decimal QtdConferencia { get; set; }
        public decimal QtdConferida { get; set; }
        
        [NaoMapeado]
        public string DescricaoStatus
        {
            get
            {
                switch (this.Status)
                {
                    case (int)enumStatusLiberacaoPedidoVenda.Atendido:
                        return "Atendido";
                    case (int)enumStatusLiberacaoPedidoVenda.Atendido_Parcial:
                        return "Atendido Parcial";
                    case (int)enumStatusLiberacaoPedidoVenda.Sem_Estoque:
                        return "Sem Estoque";
                    case (int)enumStatusLiberacaoPedidoVenda.Atendido_Producao:
                        return "Atendido e Produção";
                    case (int)enumStatusLiberacaoPedidoVenda.Producao:
                        return "Produção";
                    default:
                        return "";
                }
            }
        }
    }
}
