

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("nfceitens", "nfceitens")]
    public class NfceItensUpdate
    {
        [Chave]
        public int Id { get; set; }             
        public decimal Qtddevolvida { get; set; }
       
    }
}