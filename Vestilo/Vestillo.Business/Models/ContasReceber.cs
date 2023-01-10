using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("contasreceber", "Contas a Receber")]
    public class ContasReceber
    {
        [Chave]
        public int Id { get; set; }
        [FiltroEmpresa]
        public int IdEmpresa { get; set; }
        public int Status { get; set; }
        [Contador("ContasReceber")]
        public string NumTitulo { get; set; }
        public string NotaFiscal { get; set; }
        [NaoMapeado]
        public string NFCE { get; set; }         
        public string Parcela { get; set; }
        public string Prefixo { get; set; }
        [NaoMapeado]
        public string Natureza { get; set; }
        [NaoMapeado]
        public string NaturezaDesc { get; set; }
        public string NomeCliente { get; set; }
        [NaoMapeado]
        public string razaosocial { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal ValorParcela { get; set; }
        public DateTime? DataPagamento { get; set; }
        public decimal ValorPago { get; set; }
        public decimal Saldo { get; set; }        
        public int? IdFatNfe { get; set; }
        public int? IdNotaConsumidor { get; set; }
        public int? IdPedidoVenda { get; set; }
        public int? IdCliente { get; set; }
        public int? IdNaturezaFinanceira { get; set; }
        public int? IdBanco { get; set; }
        public int? IdTipoDocumento { get; set; }
        public int? IdVendedor { get; set; }
        public int? IdTipoCobranca { get; set; }
        public int Ativo { get; set; }        
        public decimal Desconto { get; set; }
        public decimal Juros { get; set; }
        public string Obs { get; set; }
        public int? IdTituloPai { get; set; }
        public string SisCob { get; set; }
        public int EnviadoCobranca { get; set; }
        public int Prazo { get; set; }
        

        /// <summary>
        /// Numero do cartão de crédito / débito
        /// </summary>
        public string NumeroCartao { get; set; }
        /// <summary>
        /// Id da administradora de cartão de crédito / débito
        /// </summary>
        public int? IdAdmCartao { get; set; }
        
        [NaoMapeado(true)]
        public int? IdCheque { get; set; }

        [NaoMapeado]
        public LancamentoParcela ParcelaDetalhes { get; set; }

        [NaoMapeado]
        public bool Excluir { get; set; }
    }
}
