using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("cartacorrecao","CCe")]
    public class Cce
    {

        [Chave]
        public int Id { get; set; }
        public int idNotaFiscal { get; set; }
        public DateTime dataEmissao { get; set; }          
        public int seq { get; set; }           
        public string texto { get; set; }         
        public string cartaassinada { get; set; }
        public int status { get; set; }       
    }
}
