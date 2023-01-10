
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("Nfe", "Nfe")]    
    public class NfeUpdate
    {
        [Chave]
        public int Id { get; set; }                        
        public int TotalmenteDevolvida { get; set; }
        public int PossuiDevolucao { get; set; }                       
    }
}