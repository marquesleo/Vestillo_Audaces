
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class PacoteFaccaoValorizado
    {
        public int FaccaoId { get; set; }
        public int OperacaoId { get; set; }
        public DateTime? DataLancamento { get; set; }       
        public string Faccao { get; set; }
        public string Referencia { get; set; }
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public Decimal ValorOperacao { get; set; }
        public Decimal Total { get; set; }
    }
}
