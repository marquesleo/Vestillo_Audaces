
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo;

namespace Vestillo.Business.Models
{
    [Vestillo.Tabela("Destinos", "Destinos")]
    public class Destinos
    {
        [Vestillo.Chave]
        public int Id { get; set; }               
        [Vestillo.RegistroUnico]
        public String Abreviatura { get; set; }
        [Vestillo.RegistroUnico]
        public String Descricao { get; set; }       
        public bool Ativo { get; set; }
    }
}
