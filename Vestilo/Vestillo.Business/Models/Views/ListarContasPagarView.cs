using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class ListarContasPagarView
    {
        public DateTime DataVencimentoInicial { get; set; }
        public DateTime DataVencimentoFinal { get; set; }
        public string NumeroTitulo { get; set; }
    }
}
