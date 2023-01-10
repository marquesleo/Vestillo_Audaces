using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models.Views
{
    public class FiltroRelPedidoCliente
    {
        public int[] catalogosIds { get; set; }
        public int[] colecoesIds { get; set; }
        public int[] corIds { get; set; }
        public int[] tamanhosIds { get; set; }                
        public int[] pedidosIds { get; set; }
        public int clientesId { get; set; }
        public int idAlmoxarifado { get; set; }
        public int idTabelaPreco { get; set; }
        public DateTime DaEmissao { get; set; }
        public DateTime AteEmissao { get; set; }
    }
}
