
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models.Views
{
    public class FiltroExtratoBancarioRel
    {
        public int IdBanco { get; set; }
        public List<int> IdsNatureza { get; set; }       
        public string Ordernar { get; set; }
        public string Lancamentos { get; set; }        
        public DateTime DaEmissao { get; set; }
        public DateTime AteEmissao { get; set; }      
    }
}

