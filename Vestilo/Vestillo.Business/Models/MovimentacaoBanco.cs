
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("movimentacaobanco", "Movimentacão no Banco")]
    public class MovimentacaoBanco
    {
        [Chave]
        public int Id { get; set; }
        [FiltroEmpresa]
        public int IdEmpresa { get; set; }
        public int IdBanco { get; set; }
        public int? IdBancoDestino { get; set; }
        public int? IdMovFinanceira { get; set; }
        public int? IdContasReceber { get; set; }
        public int? IdContasPagar { get; set; }
        public int? IdCheque { get; set; }
        public int IdUsuario { get; set; }
        public int? BorderoId { get; set; }
        public DateTime DataMovimento { get; set; }               
        /// <summary>
        /// 1 Credito ; 2 Debito ; 3 Transferência
        /// </summary>
        public int Tipo { get; set; }      
        public decimal Valor { get; set; } 
        public string Observacao { get; set; }                   
       
    }
}
