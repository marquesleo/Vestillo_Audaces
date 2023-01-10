using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models.Views
{
    public class FiltroPedidoVendaRelatorio
    {
        public List<int> Pedido { get; set; }
        public List<int> Cliente { get; set; }
        public List<int> Vendedor { get; set; }
        public List<int> Produtos { get; set; }
        public List<int> Colecao { get; set; }
        public List<int> Catalogo { get; set; }
        public List<int> Segmento { get; set; }
        public List<int> Cor { get; set; }
        public List<int> Tamanho { get; set; }
        public List<int> Grupo { get; set; }
        public List<int> Rota { get; set; }
        public string DoAno { get; set; }
        public string AteAno { get; set; }
        public string Agrupar { get; set; }
        public string Ordernar { get; set; }
        public int ExibirPedidos { get; set; }
        public DateTime DaEmissao { get; set; }
        public DateTime AteEmissao { get; set; }
        public DateTime DaEntrega { get; set; }
        public DateTime AteEntrega { get; set; }
        public DateTime DaLiberacao { get; set; }
        public DateTime AteLiberacao { get; set; }
        public bool Sintetico { get; set; }

        public bool PrecoVenda { get; set; }
        public bool RefCliente { get; set; }
    }
}
