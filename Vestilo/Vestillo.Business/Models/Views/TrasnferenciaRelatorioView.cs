
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Lib;

namespace Vestillo.Models
{
    
    public class TrasnferenciaRelatorioView
    {
                
        public int id { get; set; }

        [GridColumn("Referência")]
        public string referencia { get; set; }

        [GridColumn("Inclusão")]
        public DateTime DataInclusao { get; set; }

        [GridColumn("Tipo de Transferência")]
        public string DescricaoTipo { get; set; }


        [GridColumn("Almoxarifado Origem")]
        public string AlmoxarifadoOrigem { get; set; }

        [GridColumn("Almoxarifado Destino")]
        public string AlmoxarifadoDestino { get; set; }

        [GridColumn("Usuário")]
        public string Usuario { get; set; }

        [GridColumn("Faturamentos")]
        public string NotasTransferidas { get; set; }

        [GridColumn("Total de Itens")]
        public int totalitens { get; set; }        

        [GridColumn("Produto")]
        public string ReferenciaProduto { get; set; }

        [GridColumn("Descrição")]
        public string DescricaoProduto { get; set; }

        [GridColumn("Cor")]
        public string DescricaoCor { get; set; }

        [GridColumn("Tamanho")]
        public string DescricaoTamanho { get; set; }

        [GridColumn("Quantidade")]
        public decimal quantidade { get; set; }

        [GridColumn("Observação")]
        public string obs { get; set; }

    }
}
