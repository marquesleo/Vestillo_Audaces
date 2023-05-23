using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Lib;

namespace Vestillo.Core.Models
{  

    public class AcompanhamentoOrdemView
    {
        [GridColumn("Status")]
        public string Status { get; set; }

        [GridColumn("Ref. Ordem")]
        public string RefOP { get; set; }

        [GridColumn("Cor do Talão")]
        public string CorTalao { get; set; }

        [GridColumn("Ref. Produto")]
        public string Produto { get; set; }

        [GridColumn("Cor")]
        public string cor { get; set; }

        [GridColumn("Tamanho")]
        public string Tamanho { get; set; }        
        

        [GridColumn("Data do Corte")]
        public string DataCorte { get; set; }

        [GridColumn("Data da Entrada")]
        public string DataEntrada { get; set; }

        [GridColumn("Data Entrada Interna")]
        public string DataEntradaInterna { get; set; }

        [GridColumn("Qtd da OP")]
        public decimal QtdDaOP { get; set; }

        [GridColumn("Qtd em Producao")]
        public decimal QtdEmProducao { get; set; }

        [GridColumn("Qtd Entregue Facção")]
        public decimal QtdEntregueFaccao { get; set; }

        [GridColumn("Qtd Produzida")]
        public decimal QtdProduzida { get; set; }

        [GridColumn("Qtd sem Pacote")]
        public decimal QtdSemPacote { get; set; }

        public int IdPacote { get; set; }
        public int IdOrdem { get; set; }
        public int ProdutoId { get; set; }
        public int CorId { get; set; }
        public int TamanhoId { get; set; }        
    }
}
