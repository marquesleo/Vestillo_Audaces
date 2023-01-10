using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class ComissoesVendedorLiberada
    {
        public int IdComissao { get; set; }
        public int IdVendedor { get; set; }
        public int IdGuia { get; set; }
        public decimal Valor { get; set; }
        public int IdBanco { get; set; }
        public int IdNatureza { get; set; }

    }
}
