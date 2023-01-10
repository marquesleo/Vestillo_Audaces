using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Vestillo.Tabela("observacaoproduto")]
    public class ObservacaoProdutoView : ObservacaoProduto
    {
        [NaoMapeado]
        public string Referencia { get; set; }
    }
}
