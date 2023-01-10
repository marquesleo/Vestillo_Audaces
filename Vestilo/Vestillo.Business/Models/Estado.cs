using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("Estados", "Estados")]
    public class Estado
    {
        [Chave]
        public int Id { get; set; }
        public string Abreviatura { get; set; }
        public string Nome { get; set; }       
        public double Icms { get; set; }
        public double icmsinterno { get; set; } 
        public int CodigoIbge { get; set; }
        public double Pfcp { get; set; }

    }
}
