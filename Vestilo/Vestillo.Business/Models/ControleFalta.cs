using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Vestillo.Tabela("ControleFalta", "Controle de Falta")]
    public class ControleFalta
    {
        [Vestillo.Chave]
        public int Id { get; set; }
        public int Dias { get; set; }
        public decimal PorCento { get; set; }
        public int PremioId { get; set; }
    }
}
