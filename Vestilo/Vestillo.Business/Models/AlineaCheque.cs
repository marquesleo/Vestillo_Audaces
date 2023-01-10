using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("AlineaCheque", "AlineaCheque")]
    public class AlineaCheque
    {
        [Chave]
        public int Id { get; set; }
        [RegistroUnico]
        public string Abreviatura { get; set; }
        [RegistroUnico]
        public string Descricao { get; set; }
    }
}