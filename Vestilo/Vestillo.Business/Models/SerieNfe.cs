
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("SerieNfe", "SerieNfe")]
    public class SerieNfe
    {
        [Chave]
        public int Id { get; set; }
        [FiltroEmpresa]
        public int IdEmpresa { get; set; }
        [RegistroUnico]
        public int Serie { get; set; }        
        public int NumeracaoAtual { get; set; }
    }
}