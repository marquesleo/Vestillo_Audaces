using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class FluxoCaixaConsulta
    {
        public List<ContasReceberView> Entradas { get; set; }
        public List<ContasPagarView> Saidas { get; set; }
        public List<CreditosClientesView> DebitosEmpresa { get; set; }
        public List<CreditoFornecedorView> CreditoEmpresa { get; set; }
        public List<ChequeView> Cheques { get; set; }
    }
}
