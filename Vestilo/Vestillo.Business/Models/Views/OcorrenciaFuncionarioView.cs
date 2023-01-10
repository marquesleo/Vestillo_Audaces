using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Vestillo.Tabela("OcorrenciaFuncionario", "OcorrenciaFuncionario")]
    public class OcorrenciaFuncionarioView: OcorrenciaFuncionario
    {
        [Vestillo.NaoMapeado]
        public string OcorrenciaDescricao { get; set; }
        [Vestillo.NaoMapeado]
        public int OcorrenciaTipo { get; set; }
    }
}
