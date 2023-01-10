using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class LancamentoParcela
    {
        public LancamentoParcela()
        {
            Obs = string.Empty;
        }

        public class DadosCartao
        {
            public int AdmCartaoId { get; set; }
            public string NumeroCartao { get; set; }
        }

        public DateTime DataVencimento { get; set; }
        public string Parcela { get; set; }
        public decimal Valor { get; set; }
        public int TipoDocumentoId { get; set; }
        public int BancoId { get; set; }
        public string TipoDocumentoDescricao { get; set; }
        public string Obs { get; set; } 
        public string Detalhes  { get; set; }
        public Cheque Cheque { get; set; }
        public DadosCartao Cartao { get; set; }
        public int CreditoClienteId { get; set; }
        public int Prazo { get; set; }
    }
}
