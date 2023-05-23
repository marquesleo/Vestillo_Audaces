using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Core.Models
{
    public class DashVendedorCarteiraClientes 
    {
        public int TotalCarteiraClientes { get; set; }
        public int FaturamentoUltimos12Meses { get; set; }
        public int TotalFaturadoMes { get; set; }
        public int NovosClientes { get; set; }
    }
}
