using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class TitulosView
    {
        public int Id { get; set; }
        public string NumTitulo { get; set; }
        public string NotaFiscal { get; set; }
        public string NFCE { get; set; }
        public string Prefixo { get; set; }
        public string Parcela { get; set; }
        public string Ativo { get; set; }
        public string TipoDocumentos { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public decimal? ValorPago { get; set; }
        public decimal ValorParcela { get; set; }
        public decimal Saldo { get; set; }
        public string RefColaborador { get; set; }
        public string NomeColaborador { get; set; }
        public string TipoCobrancaDescricao { get; set; }
        public string NaturezaDescricao { get; set; }
        public string NomeBanco { get; set; }
        public int Status { get; set; }
        public decimal? Juros { get; set; }
        public decimal? Desconto { get; set; }
       
        public int BancoMovimentacaoId { get; set; }
        public int? ColaboradorId { get; set; }
        public string RefVendedor { get; set; }
        public string NomeVendedor { get; set; }

        public DateTime? DataBaixa { get; set; }
        public decimal? ValorPagoDinheiro { get; set; }
        public decimal? ValorPagoCheque { get; set; }
        public string ObsBaixa { get; set; }
        public decimal? ValorPagoCredito { get; set; }

        public decimal ValorRestante
        {
            get
            {
                return ((ValorParcela  + Juros.GetValueOrDefault() - Desconto.GetValueOrDefault()) - ValorPago.GetValueOrDefault());
            }
        }

        public string DescStatus
        {
            get
            {
                switch (this.Status)
                {
                    case (int)enumStatusContasPagar.Aberto:
                        return "Aberto";
                    case (int)enumStatusContasPagar.Baixa_Parcial:
                        return "Baixa Parcial";
                    case (int)enumStatusContasPagar.Baixa_Total:
                        return "Baixado";
                    default:
                        if (ValorPago == 0)
                            return "Aberto";
                        
                        if (ValorRestante > 0)
                            return "Baixa Parcial";

                        if (ValorRestante == 0 || (ValorPago > (ValorParcela + Juros - Desconto)))
                            return "Baixado";

                        return "";
                }   
            }
        }
    }
}
