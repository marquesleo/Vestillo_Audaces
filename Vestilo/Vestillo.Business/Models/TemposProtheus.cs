
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo;

namespace Vestillo.Business.Models
{
    [Tabela("TemposProtheus", "Tempos Protheus")]
    public class TemposProtheus
    {        
        [Chave]
        public int Id { get; set; }
        [FiltroEmpresa]        
        public string Produto { get; set; }        
        public decimal TempoTotal { get; set; }
        public decimal TempoMenosInterno { get; set; }         
        public DateTime DataAlteracao { get; set; }        
        public int Status { get; set; }
    }

}
