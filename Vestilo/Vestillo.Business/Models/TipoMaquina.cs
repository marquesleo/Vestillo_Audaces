using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("TipoMaquinas", "Tipo de Maquinas")]
    public class TipoMaquina
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
