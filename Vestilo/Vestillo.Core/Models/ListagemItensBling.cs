
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Lib;

namespace Vestillo.Core.Models
{

    public class ListagemItensBling
    {

        [GridColumn("Id do Item")]
        public int? Id { get; set; }

        [GridColumn("Ref Produto")]
        public string Referencia { get; set; }

        [GridColumn("Descrição Vestillo")]
        public string DescricaoVestillo { get; set; }


        [GridColumn("Nome Bling")]
        public string NomeBling { get; set; }

        [GridColumn("EAN PAI")]
        public string EANPAI { get; set; }

        [GridColumn("Id Cor")]
        public int? IdCor { get; set; }

        [GridColumn("Nome Cor")]
        public string Cor { get; set; }


        [GridColumn("Id Tamanho")]
        public int? IdTamanho { get; set; }


        [GridColumn("Tamanho")]
        public string Tamanho { get; set; }

        [GridColumn("EAN FILHO")]
        public string EANFILHO { get; set; }
        
    }
}
