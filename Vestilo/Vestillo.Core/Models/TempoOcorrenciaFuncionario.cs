using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Core.Models
{
    public class TempoOcorrenciaFuncionario
    {
        public int FuncionarioId { get; set; }
        public string NomeFuncionario { get; set; }
        public string Descricao { get; set; }
        public decimal Tempo { get; set; }
        public int Tipo { get; set; }
    }
}
