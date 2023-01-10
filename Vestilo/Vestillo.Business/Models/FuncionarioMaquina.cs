using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("FuncionarioMaquina")]
    public class FuncionarioMaquina
    {
        [Chave]
        public int Id { get; set; }
        public int FuncionarioId { get; set; }
        public int TipoMaquinaId { get; set; }
        public int Habilidade { get; set; }
    }
    
    [Tabela("FuncionarioMaquina")]
    public class FuncionarioMaquinaView : FuncionarioMaquina
    {
        [NaoMapeado]
        public string TipoMaquinaDescricao { get; set; }
    }
}
 