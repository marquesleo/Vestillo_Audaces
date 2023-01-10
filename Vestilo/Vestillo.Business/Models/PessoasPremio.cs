using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Vestillo.Tabela("PessoasPremio", "Pessoas Premio")]
    public class PessoasPremio
    {
        [Vestillo.Chave]
        public int Id { get; set; }
        public int PessoaId { get; set; }
        public int PremioId { get; set; }
    }
}
