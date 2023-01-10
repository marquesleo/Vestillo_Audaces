
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("Setores", "Setores")]
    public class Setores
    {
        [Chave]
        public int Id { get; set; }               
        [RegistroUnico]
        public String Abreviatura { get; set; }
        [RegistroUnico]
        public String Descricao { get; set; }
        public int? IdDepartamento { get; set; }  
        public bool Ativo { get; set; }
        public bool Balanceamento { get; set;}
    }
}
