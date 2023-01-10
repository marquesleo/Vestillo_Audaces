using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class EmpresaView : Empresa
    {
        [Vestillo.NaoMapeado]
        public IEnumerable<Produtividade> Produtividades { get; set; }
        [Vestillo.NaoMapeado]
        public IEnumerable<OcorrenciaFuncionarioView> Ocorrencias { get; set; }
        [Vestillo.NaoMapeado]
        public IEnumerable<TempoFuncionario> Tempos { get; set; }
        [Vestillo.NaoMapeado]
        public List<OperacaoOperadoraView> Operacoes { get; set; }
    }
}
