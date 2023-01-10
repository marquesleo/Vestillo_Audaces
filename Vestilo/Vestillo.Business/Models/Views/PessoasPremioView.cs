using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Vestillo.Tabela("PessoasPremio", "Pessoas Premio")]
    public class PessoasPremioView : PessoasPremio
    {
        [Vestillo.NaoMapeado]
        public string PessoaNome { get; set; }
        [Vestillo.NaoMapeado]
        public string PessoaCargo { get; set; }
    }
}
