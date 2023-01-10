
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("SerieNfce", "SerieNfce")]
    public class SerieNfce
    {
        [Chave]
        public int Id { get; set; }
        [FiltroEmpresa]
        public int IdEmpresa { get; set; }        
        public int Serie { get; set; }
        [RegistroUnico]
        public int NumeracaoAtual { get; set; }
    }
}