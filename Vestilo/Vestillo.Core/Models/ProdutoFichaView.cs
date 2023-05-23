
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Lib;

namespace Vestillo.Core.Models
{
    [Tabela("ProdutoFicha")]
    public class ProdutoFichaView : ProdutoFicha
    {
        [NaoMapeado]
        public string RefMaterial { get; set; }
        [NaoMapeado]
        public string DescricaoMaterial { get; set; }
        [NaoMapeado]
        public string DescricaoGrupo { get; set; }
        [NaoMapeado]
        public string DescricaoUnidade { get; set; }
        
    }
}
