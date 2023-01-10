using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("Cargos")]
    public class Cargo 
    {
        [Chave]
        public int Id { get; set; }
        [RegistroUnico]
        public string Abreviatura { get; set; }
        [OrderByColumn]
        [RegistroUnico]
        public string Descricao { get; set; }
    }
}
