using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
     [Tabela("PlanoAnualDetalhes", "Plano Anual Detalhes")]
    public class PlanoAnualDetalhes
    {
        [Chave]
        public int Id { get; set; }      
        public int PlanoId { get; set; }
        public int GrupoId { get; set; }
        public int Mes { get; set; }
       
        public int Costureira { get; set; }
        public int DiasUteis { get; set; }
        public decimal Jornada { get; set; }
        public decimal Presenca { get; set; }
        public decimal Aproveitamento { get; set; }
        public decimal Eficiencia { get; set; }
        public decimal TempoMedio { get; set; }
    }
}
