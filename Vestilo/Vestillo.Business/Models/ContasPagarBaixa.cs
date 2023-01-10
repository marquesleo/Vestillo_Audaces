using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("contaspagarbaixa", "contaspagarbaixa")]
    public class ContasPagarBaixa
    {
        [Chave]
        public int Id { get; set; }
        public int? ContasPagarId { get; set; }
        public DateTime DataBaixa { get; set; }
        public decimal ValorDinheiro { get; set; }
        public decimal ValorCheque { get; set; }
        public decimal ValorCredito { get; set; }
        public string Obs { get; set; }
        public int? ContasPagarBaixaLoteId { get; set; }
        public int? BancoId { get; set; }

        [NaoMapeado]
        public bool Lote { get; set; }
        [NaoMapeado]
        public List<Cheque> Cheques { get; set; }
        [NaoMapeado]
        public ContasPagarView ContasPagar { get; set; }

        [NaoMapeado]
        public IEnumerable<CreditoFornecedor> Creditos { get; set; }
    }
}
