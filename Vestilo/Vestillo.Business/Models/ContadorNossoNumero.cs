
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("ContadorNossoNumero", "ContadorNossoNumero")]
    public class ContadorNossoNumero
    {
        [Chave]
        public int Id { get; set; }
        [RegistroUnico]
        public int IdBanco { get; set; }
        [RegistroUnico]
        public int NumeracaoAtual { get; set; }
    }
}