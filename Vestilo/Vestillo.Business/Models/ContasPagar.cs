using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("contaspagar", "Contas a Pagar")]
    public class ContasPagar
    {
        [Chave]
        public int Id { get; set; }
        [FiltroEmpresa]
        public int IdEmpresa { get; set; }
        public int IdPedidoCompra { get; set; }
        public int Status { get; set; }
        [Contador("ContasPagar")]
        public string NumTitulo { get; set; }
        public string Parcela { get; set; }
        public string Prefixo { get; set; }
        [NaoMapeado]
        public string Natureza { get; set; }
        [NaoMapeado]
        public string NaturezaDesc { get; set; }
        public int? IdNotaEntrada { get; set; }
        public int? IdNotaEntradaFaccao { get; set; }
        public string NomeFornecedor { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal ValorParcela { get; set; }
        public DateTime? DataPagamento { get; set; }
        public decimal ValorPago { get; set; }
        public decimal Saldo { get; set; }
        public int IdFornecedor { get; set; }
        public int IdNaturezaFinanceira { get; set; }
        public int IdCheque { get; set; }
        public int? IdBanco { get; set; }
        public int IdTipoDocumento { get; set; }
        public int? IdTipoCobranca { get; set; }
        public int Ativo { get; set; }
        public bool PossuiBoleto { get; set; }   
        public decimal Desconto { get; set; }
        public decimal Juros { get; set; }
        public string Obs { get; set; }
        public int Prazo { get; set; }
        public int Notinha { get; set; }
        public decimal Vale { get; set; }
        public decimal Fator1 { get; set; }
        public decimal Fator2 { get; set; }
        public decimal TotalFator { get; set; }
    }
}
