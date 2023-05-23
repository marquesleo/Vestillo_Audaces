

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Lib;

namespace Vestillo.Core.Models
{
    public class UltimoPrecoMaterialView
    {
        [GridColumn("Referência")]
        public string ReferenciaProduto { get; set; }

        [GridColumn("Descrição")]
        public string DescricaoProduto { get; set; }

        [GridColumn("Ultimo Preço")]
        public string preco { get; set; }

        [GridColumn("Número da Nota")]
        public string NumeroNota { get; set; }

        [GridColumn("Data Inclusão")]
        public DateTime DataInclusao { get; set; }
    }
}
