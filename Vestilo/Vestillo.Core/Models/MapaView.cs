
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Lib;

namespace Vestillo.Core.Models
{
    public class MapaView
    {
        
        [GridColumn("Op")]
        public string Op { get; set; }

        [GridColumn("Família")]
        public string Colecao { get; set; }

        [GridColumn("Ref")]
        public string Referencia { get; set; }

        [GridColumn("Cor")]
        public string Cor { get; set; }

        [GridColumn("Tam")]
        public string Tamanho { get; set; }       

        [GridColumn("Cliente")]
        public string Cliente { get; set; }

        [GridColumn("Inspeção")]
        public DateTime? Inspecao { get; set; }

        [GridColumn("Entrega")]
        public DateTime? Entrega { get; set; }

        [GridColumn("Previsao")]
        public DateTime? Previsao { get; set; }

        [GridColumn("Quantidade")]
        public decimal Quantidade { get; set; }

        [GridColumn("Tempo X Quantidade")]
        public decimal TempoQuantidade { get; set; }

        [GridColumn("Pedido")]
        public string Pedido { get; set; }       

        [GridColumn("Valor")]
        public decimal? Valor { get; set; }

        [GridColumn("Obs")]
        public string Obs { get; set; }

               

    }
}
