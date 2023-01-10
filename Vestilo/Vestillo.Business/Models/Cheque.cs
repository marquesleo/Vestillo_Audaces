using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("Cheques","Cheques")]    
    public class Cheque
    {
        public enum enumStatus
        {
            Incluido = 1,
            Compensado = 2,
            Devolvido = 3,
            Prorrogado = 4,
            Resgatado = 5
        }

        [Chave]
        public int Id { get; set; }
        [FiltroEmpresa]
        public int EmpresaId { get; set; }
        [Contador("Cheque")]
        public string Referencia { get; set; }
        public int? ContasReceberBaixaId { get; set; }
        public int? ContasPagarBaixaId { get; set; }
        public string Emitente { get; set; }
        public DateTime DataEmissao { get; set; } 
        public DateTime DataVencimento { get; set; }
        public string Banco { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
        public string NumeroCheque { get; set; }
        public int Status { get; set; }
        public decimal Valor { get; set; }
        public bool DeTerceiro { get; set; }
        public DateTime? ProrrogarPara { get; set; }
        public bool Visado { get; set; }
        public bool Cruzado { get; set; }
        public DateTime? Compensacao { get; set; }
        public DateTime? Resgate { get; set; }
        public int ColaboradorId { get; set; }
        /// <summary>
        /// 1 = Cliente | 2 = Empresa
        /// </summary>
        public int TipoEmitenteCheque { get; set; } // ;1 = Cliente  2 = Empresa
        public int? NaturezaFinanceiraId { get; set; }
        public int? BancoMovimentacaoId { get; set; }
        public decimal? ValorCompensado { get; set; }
        public decimal? ValorJuros { get; set; }
        public int? Alinea1Id { get; set; }
        public int? Alinea2Id { get; set; }
        public DateTime? DataApresentacaoAlinea1 { get; set; }
        public DateTime? DataApresentacaoAlinea2 { get; set; }
        public DateTime? DataAlinea1 { get; set; }
        public DateTime? DataAlinea2 { get; set; }
        public int? ContasPagarGeradoId { get; set; }
        public int? NFeId { get; set; }
        public int? NFCeId { get; set; }
        public string Observacao { get; set; }
    }
}
