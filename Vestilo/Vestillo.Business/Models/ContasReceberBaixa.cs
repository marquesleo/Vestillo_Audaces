using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("ContasReceberBaixa", "ContasReceberBaixa")]
    public class ContasReceberBaixa
    {
        [Chave]
        public int Id { get; set; }
        public int? ContasReceberId { get; set; }
        public int? BorderoId { get; set; }
        public DateTime DataBaixa { get; set; }
        public decimal ValorDinheiro { get; set; }
        public decimal ValorCheque { get; set; }
        public decimal? ValorCredito { get; set; }
        public string Obs { get; set; }
        public int? ContasReceberBaixaLoteId { get; set; }
        public int? BancoId { get; set; }
        public int RedefiniuBaixa { get; set; }
        

        [NaoMapeado]
        public bool Lote { get; set; }
        [NaoMapeado]
        public List<Cheque> Cheques { get; set; }
        [NaoMapeado]
        public ContasReceberView ContasReceber { get; set; }

        [NaoMapeado]
        public IEnumerable<CreditosClientes> Creditos { get; set; }

        [NaoMapeado]
        public IEnumerable<ContasReceber> ParcelasCtr { get; set; }
    }
}
