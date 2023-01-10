using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Vestillo.Tabela("OcorrenciaFuncionario", "OcorrenciaFuncionario")]
    public class OcorrenciaFuncionario
    {
        [Vestillo.Chave]
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public int FuncionarioId { get; set; }
        public int OcorrenciaId { get; set; }
        public decimal Tempo { get; set; }
        public string Observacao { get; set; }
    }
}
