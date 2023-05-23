using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Lib;

namespace Vestillo.Core.Models
{
    public class PedidoXOrdemView
    {
        public int IdPedido { get; set; }

        [GridColumn("Pedido")]
        public string RefPedido { get; set; }

        [GridColumn("Liberação")]
        public DateTime Liberacao { get; set;}

        [GridColumn("Vendedor")]
        public string Vendedor { get; set; }

        [GridColumn("Cliente")]
        public string NomeCliente { get; set; }

        [GridColumn("Catálogo")]
        public string Catalogo { get; set; }

        [GridColumn("Coleção")]
        public string Colecao { get; set; }

        public int ProdutoId { get; set; }

        [GridColumn("Produto")]
        public string RefProduto { get; set; }

        public int CorId { get; set; }

        [GridColumn("Cor")]
        public string RefCor { get; set; }

        public int TamanhoId { get; set; }

        [GridColumn("Tamanho")]
        public string RefTamanho { get; set; }

        [GridColumn("Preço Unitário")]
        public decimal Preco { get; set; }


        [GridColumn("Qtd. Para Atender")]
        public decimal QtdParaAtender { get; set; } // quantidade que falta para atender do pedido

        [GridColumn("Qtd. que será atendida")]
        public decimal QtdQueSeraAtendida { get; set; } // quanto da op vai atender esse pedido

        public int? IdOrdem { get; set; }

        [GridColumn("Ordem")]
        public string RefDaOrdem { get; set; }

        [GridColumn("Data Prevista da Liberação")]
        public DateTime? DataQueIraAtender { get; set; }

        [GridColumn("Previsão de Atendimento")]
        public string Atendido { get; set; }

        [GridColumn("Observação da Ordem")]
        public string ObsOrdem { get; set; }

        [GridColumn("Observação do Pedido")]
        public string ObsPedido { get; set; }

    }
}
