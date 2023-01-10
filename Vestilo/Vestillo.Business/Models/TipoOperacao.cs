using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
     [Tabela("TipoOperacao", "Tipo de Operação")]
    public class TipoOperacao
    {
        [Chave]
        public int Id { get; set; }
        [RegistroUnico]
        public String Abreviatura { get; set; }
        [RegistroUnico]
        public String Descricao { get; set; }
        public bool Ativo { get; set; }
        
    }
}
