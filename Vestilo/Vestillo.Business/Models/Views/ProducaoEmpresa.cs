using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class ProducaoEmpresa
    {
        public DateTime Data { get; set; }
        public decimal Produtividade { get; set; }
        public decimal Jornada { get; set; }
        public decimal Tempo { get; set; }
        public decimal Operacao { get; set; }
        public decimal Pecas { get; set; }
        public decimal Defeito { get; set; }
        public decimal TempoPacote { get; set; }

        public IEnumerable<OcorrenciaFuncionarioView> Ocorrencias { get; set; }
    }
}
