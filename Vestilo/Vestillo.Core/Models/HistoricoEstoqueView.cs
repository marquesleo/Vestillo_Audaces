
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Lib;

namespace Vestillo.Core.Models
{
    public class HistoricoEstoqueView
    {
        [GridColumn("Data")]
        public DateTime? DataMovimento { get; set; }

        [GridColumn("Referência")]
        public string ProdReferencia { get; set; }

        [GridColumn("Descricao")]
        public string ProdDescricao { get; set; }

        [GridColumn("Cor")]
        public string CorAbreviatura { get; set; }

        [GridColumn("Tamanho")]
        public string TamAbreviatura { get; set; }

        [GridColumn("Unid. Med.")]
        public string UnidMedida { get; set; } 

        [GridColumn("Saida")]
        public decimal Saida { get; set; }

        [GridColumn("Entrada")]
        public decimal Entrada { get; set; }

        //[GridColumn("Quantidade")]
        public decimal Quantidade { get; set; }

        [GridColumn("Segmento")]
        public string Segmento { get; set; }

        [GridColumn("Almoxarifado")]
        public string AlmoxarifadoExibido { get; set; }
        

        [GridColumn("Catálogo")]
        public string Catalogo { get; set; }

        [GridColumn("Saídas Pedido")]
        public decimal SaidasPedido { get; set; }

        [GridColumn("Saídas Faturamento")]
        public decimal SaidasFaturamento { get; set; }

        [GridColumn("Saídas Manual")]
        public decimal SaidasManual { get; set; }

        [GridColumn("Saídas Cupom")]
        public decimal SaidasCupom { get; set; }

        [GridColumn("Entradas Cupom")]
        public decimal EntradasCupom { get; set; }

        [GridColumn("Entradas Devolucao Itens")]
        public decimal EntradasDevolucao { get; set; }

        [GridColumn("Entradas Manual")]
        public decimal EntradasManual { get; set; }

        [GridColumn("Entradas Pedido")]
        public decimal EntradasPedido { get; set; }

        [GridColumn("Entradas Faturamento")]
        public decimal EntradasFaturamnento { get; set; }


        public string Almoxarifado { get; set; }
        public int ProdId { get; set; }
        public int CorId { get; set; }
        public int TamanhoId { get; set; }
        public int IdAlmoxarifado { get; set; }
        

        [GridColumn("Usuário")]
        public string UsuarioNome { get; set; }

        [GridColumn("Observação")]
        public string MovObservacao { get; set; }

        public decimal Valor { get; set; }


               

    }
}
