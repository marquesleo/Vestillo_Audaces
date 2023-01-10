using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models.Views
{
    public class FiltroChequeRelatorio
    {
        public List<int> Banco { get; set; }
        public string Conta { get; set; }
        public string Agencia { get; set; }
        public List<int> NumeroCheque { get; set; }
        public List<int> Cliente { get; set; }

        public int Ordenar { get; set; }

        public string DaEmissao { get; set; }
        public string AteEmissao { get; set; }
        public string DoVencimento { get; set; }
        public string AteVencimento { get; set; }

        public bool Compensado { get; set; }
        public string DoCompensado { get; set; }
        public string AteCompensado { get; set; }

        public bool TipoCliente { get; set; }
        public bool TipoEmpresa { get; set; }
        public bool StatusDevolvido { get; set; }
        public bool StatusProrrogado { get; set; }
        public bool StatusResgatado { get; set; }
        public bool StatusIncluido { get; set; }
        public bool StatusCompensado { get; set; }
    }
}
