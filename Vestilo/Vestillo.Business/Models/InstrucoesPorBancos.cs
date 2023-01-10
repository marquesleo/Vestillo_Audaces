
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("instrucoesporbancos", "instrucoesporbancos")]
    public class InstrucoesPorBancos
    {

        [Chave]
        public int Id { get; set; }
        public int IdBanco { get; set; }
        public int IdInstrucao { get; set; }
        public string Descricao { get; set; }

    }
}
