

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Lib;

namespace Vestillo.Core.Models
{
    [Tabela("MedidasProduto")]
    public class MedidasProdutoView : MedidasProduto
    {       
        [NaoMapeado]
        [GridColumn("Tamanho")]
        public string DescricaoTamanho { get; set; }
        [NaoMapeado]
        [GridColumn("Descrição Medida")]
        public string DescricaoMedida { get; set; }
    }
}

