
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models.Views
{
    public class FiltroRelVendaCerta
    {
        public int[] clientesIds { get; set; }        
        public int catalogosId { get; set; }        
        public DateTime DaEmissao { get; set; }
        public DateTime AteEmissao { get; set; }
    }
}