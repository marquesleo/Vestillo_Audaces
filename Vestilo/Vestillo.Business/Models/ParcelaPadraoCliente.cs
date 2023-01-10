using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Vestillo.Tabela("ParcelaPadraoCliente", "Parcela Padrao no Cliente")]
    public class ParcelaPadraoCliente
    {
        [Vestillo.Chave]
        public int Id { get; set; }
        public int Quantidade { get; set; }
        public string NumeroParcela { get; set; }
        public int Intervalo { get; set; }
        public int ColaboradorId { get; set; }
        public bool IntervaloDiferenciado { get; set; }
    }
}
