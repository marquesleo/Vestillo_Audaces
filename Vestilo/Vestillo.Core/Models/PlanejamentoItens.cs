
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Core.Models
{
    [Tabela("PlanejamentoItens")]
    public class PlanejamentoItens : IModel
    {
        [Chave]
        public int Id {get; set;}                
        public int PlanejamentoId {get; set;}
        public string OrdensIds {get; set;}
        public string Semana { get; set; }
        public string OrdensRefs {get; set;}                
        public decimal TempoSemana { get; set; }
    }
}
