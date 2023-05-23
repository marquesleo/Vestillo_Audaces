
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Lib;

namespace Vestillo.Core.Models
{
    [Tabela("EtqComposicao")]
    public class ComposicaoView : Composicao
    {       
        [NaoMapeado]
        public string NomeUsuarioCriacao { get; set; }
        [NaoMapeado]
        [GridColumn("Tipo Artigo")]
        public string DescricaoTipoArtigo { get; set; }       
    }
}
