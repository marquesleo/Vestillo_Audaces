using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("MaquinaColaboradores", "MaquinaColaboradores")]
    public class MaquinaColaborador
    {
        [Chave]
        public int Id { get; set; }        
        public int IdTipoMaquina { get; set; }
        public int IdColaborador { get; set; }
        public int Quantidade { get; set; }

    }
}
